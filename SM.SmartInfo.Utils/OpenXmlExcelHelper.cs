using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;


namespace SM.SmartInfo.Utils
{
    public class OpenXmlExcelHelper : IDisposable
    {
        private SpreadsheetDocument _excel = null;
        private WorkbookPart _book = null;
        private IEnumerable<Sheet> _lstSheet = null;

        #region open/close file
        private void OpenFile(string filePath)
        {
            _excel = SpreadsheetDocument.Open(filePath, true);
            _book = _excel.WorkbookPart;
            _lstSheet = _book.Workbook.Descendants<Sheet>();
        }

        private void CloseFile()
        {
            if (_excel == null)
                return;

            _excel.Close();
            _lstSheet = null;
            _book = null;
            _excel = null;
        }
        #endregion

        #region transform
        public void Transform(string templateFile, params ExcelSheetData[] arrSheetData)
        {
            // open file
            OpenFile(templateFile);
            try
            {
                // transform
                if (arrSheetData != null)
                {
                    foreach (ExcelSheetData sheetData in arrSheetData)
                        TransformSheet(sheetData);
                }

                // save
                _book.Workbook.Save();
                _book.DeletePart(_book.CalculationChainPart);
            }
            finally
            {
                // close
                CloseFile();
            }
        }

        private void TransformSheet(ExcelSheetData data)
        {
            Sheet sheet = null;
            if (string.IsNullOrWhiteSpace(data.SheetName))
                sheet = _lstSheet.FirstOrDefault();
            else
                sheet = _lstSheet.FirstOrDefault(c => string.Equals(c.Name.Value, data.SheetName, StringComparison.OrdinalIgnoreCase));
            if (sheet == null)
                throw new Exception(string.Format("Không tìm thấy sheet [{0}]", data.SheetName));

            WorksheetPart workSheetPart = (WorksheetPart)_book.GetPartById(sheet.Id);
            SheetData sheetData = workSheetPart.Worksheet.GetFirstChild<SheetData>();

            // fixed cell
            TransformFixedCell(workSheetPart, sheetData, data.FixedCells);

            // table
            if (data.Table != null)
            {
                TransformTable(workSheetPart, sheetData, data.Table);
            }
        }

        private void TransformFixedCell(WorksheetPart workSheetPart, SheetData sheetData, Dictionary<string, object> dicFixCell)
        {
            if (dicFixCell == null || dicFixCell.Count == 0)
                return;

            IEnumerable<Row> lstRow = sheetData.Elements<Row>();
            foreach (KeyValuePair<string, object> item in dicFixCell)
            {
                int rowIndex = 0;
                string colName = string.Empty;
                SplitPart(item.Key, out colName, out rowIndex);

                Row row = lstRow.FirstOrDefault(c => c.RowIndex == rowIndex);
                if (row == null)
                    continue;

                Cell cell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference == item.Key);
                if (cell == null)
                {
                    cell = new Cell();
                    cell.CellReference = item.Key;
                    SetCellValue(cell, item.Value);
                    row.AppendChild(cell);
                }
                else
                {
                    SetCellValue(cell, item.Value);
                }
            }
        }

        private void TransformTable(WorksheetPart workSheetPart, SheetData sheetData, ExcelTableData data)
        {
            if (data == null || data.DataSource == null || data.DataSource.Rows.Count == 0 || workSheetPart.TableDefinitionParts.Count() == 0)
                return;

            // find table
            TableDefinitionPart tablePart = workSheetPart.TableDefinitionParts.FirstOrDefault(c => c.Table.Name == data.TableName);
            if (tablePart == null)
                throw new Exception(string.Format("Không tìm thấy table [{0}]", data.TableName));

            // get reference of table (ex A1:D2 => A, 1, D, 2)
            string startColName = string.Empty, endColName = string.Empty;
            int startRowIndex = 0, endRowIndex = 0;
            SplitPart(tablePart.Table.Reference, out startColName, out startRowIndex, out endColName, out endRowIndex);
            startRowIndex = startRowIndex + 1; // first row is header => ignore header

            // prepare list column
            List<string> lstColName = GetListColumnName(startColName, data.DataSource.Columns.Count);

            // get the template row (the second row)
            List<Row> lstRow = sheetData.Elements<Row>().ToList();
            Row firstRow = lstRow.FirstOrDefault(c => c.RowIndex == startRowIndex);
            Row templateRow = CloneRow(firstRow, startRowIndex);

            // set the first row
            SetRowValue(firstRow, lstColName, data.DataSource.Rows[0]);

            // bind data from second row            
            for (int rowIndex = 1; rowIndex < data.DataSource.Rows.Count; rowIndex++)
            {
                Row row = CloneRow(templateRow, startRowIndex + rowIndex);
                SetRowValue(row, lstColName, data.DataSource.Rows[rowIndex]);
                sheetData.AppendChild(row);
            }

            // map data with table (set reference)
            startRowIndex = startRowIndex - 1; // first row is header => include header
            if (data.DataSource.Rows.Count > 0) // extend row = so dong du lieu (-1 do trong template da co san dong dau tien)
                endRowIndex = endRowIndex + data.DataSource.Rows.Count - 1;
            string newRef = GetRangeAddress(startColName, startRowIndex, endColName, endRowIndex);
            tablePart.Table.Reference = newRef;
            if (tablePart.Table.AutoFilter != null)
                tablePart.Table.AutoFilter.Reference = newRef;
        }
        #endregion

        #region helper
        private int GetColumnIndex(string columnName)
        {
            var alpha = new System.Text.RegularExpressions.Regex("^[A-Z]+$");
            if (!alpha.IsMatch(columnName))
                throw new Exception("Invalid column name");

            char[] colLetters = columnName.ToCharArray();
            Array.Reverse(colLetters);

            var convertedValue = 0;
            for (int i = 0; i < colLetters.Length; i++)
            {
                char letter = colLetters[i];
                int current = i == 0 ? letter - 65 : letter - 64; // ASCII 'A' = 65
                convertedValue += current * (int)Math.Pow(26, i);
            }

            return convertedValue;
        }

        private string GetColumnName(int columnIndex)
        {
            var intFirstLetter = ((columnIndex) / 676) + 64;
            var intSecondLetter = ((columnIndex % 676) / 26) + 64;
            var intThirdLetter = (columnIndex % 26) + 65;

            var firstLetter = (intFirstLetter > 64) ? (char)intFirstLetter : ' ';
            var secondLetter = (intSecondLetter > 64) ? (char)intSecondLetter : ' ';
            var thirdLetter = (char)intThirdLetter;

            return string.Concat(firstLetter, secondLetter, thirdLetter).Trim();
        }

        private List<string> GetListColumnName(string startColName, int totalColumn)
        {
            List<string> lstColName = new List<string>();

            int startColIndex = GetColumnIndex(startColName);

            for (int index = 0; index < totalColumn; index++)
            {
                int colIndex = startColIndex + index;
                string colName = GetColumnName(colIndex);
                lstColName.Add(colName);
            }

            return lstColName;
        }

        private string GetCellAddress(string colName, int rowIndex)
        {
            return string.Format("{0}{1}", colName, rowIndex);
        }

        private string GetRangeAddress(string startColName, int startRowIndex, string endColName, int endRowIndex)
        {
            string startCell = GetCellAddress(startColName, startRowIndex);
            string endCell = GetCellAddress(endColName, endRowIndex);

            return GetRangeAddress(startCell, endCell);
        }

        private string GetRangeAddress(string startCell, string endCell)
        {
            return string.Format("{0}:{1}", startCell, endCell);
        }

        private void SplitPart(string cellAddress, out string colName, out int rowIndex)
        {
            colName = string.Empty;
            rowIndex = 0;

            string strRow = string.Empty;
            foreach (char ch in cellAddress)
            {
                if (ch >= '0' && ch <= '9')
                    strRow = strRow + ch.ToString();
                else
                    colName = colName + ch.ToString();
            }
            rowIndex = int.Parse(strRow);
        }

        private void SplitPart(string rangeAddress, out string startColName, out int startRowIndex, out string endColName, out int endRowIndex)
        {
            string[] arrAddress = rangeAddress.Split(':');
            SplitPart(arrAddress[0], out startColName, out startRowIndex);
            SplitPart(arrAddress[1], out endColName, out endRowIndex);
        }

        private Row CloneRow(Row sourceRow, int newRowIndex)
        {
            Row clone = (Row)sourceRow.CloneNode(true);
            clone.RowIndex = (UInt32)newRowIndex;

            string strOldRowIndex = sourceRow.RowIndex.Value.ToString();
            string strNewRowIndex = newRowIndex.ToString();

            // change cell ref
            foreach (var cell in clone.Elements<Cell>())
            {
                string oldCellAddress = cell.CellReference.Value;
                cell.CellReference = new StringValue(oldCellAddress.Replace(strOldRowIndex, strNewRowIndex));
            }

            return clone;
        }

        private void SetRowValue(Row row, List<string> lstColName, DataRow data)
        {
            IEnumerable<Cell> lstCell = row.Elements<Cell>();
            for (int colIndex = 0; colIndex < lstColName.Count; colIndex++)
            {
                string cellAddress = GetCellAddress(lstColName[colIndex], (int)row.RowIndex.Value);
                Cell cell = lstCell.FirstOrDefault(c => c.CellReference == cellAddress);
                SetCellValue(cell, data[colIndex]);
            }
        }

        private void SetCellValue(Cell cell, object value)
        {
            if (cell == null || value == null)
                return;

            string valFormat = value.ToString();
            if (value is bool)
            {
                cell.DataType = CellValues.Boolean;
            }
            else if (value is int ||
                value is decimal ||
                value is double)
            {
                cell.DataType = CellValues.Number;
            }
            else if (value is DateTime)
            {
                cell.DataType = CellValues.Number; // set as Number, do not set as Date
                valFormat = ((DateTime)value).ToOADate().ToString();
            }
            else if (value is string)
            {
                cell.DataType = CellValues.String;
            }
            else
            {
                cell.DataType = CellValues.SharedString;
            }

            cell.CellValue = new CellValue(valFormat);
        }
        #endregion

        public void Dispose()
        {
            CloseFile();
        }

        #region helper class
        public class ExcelSheetData
        {
            public string SheetName { get; set; }

            public ExcelTableData Table { get; set; }

            public Dictionary<string, object> FixedCells { get; set; }
        }

        public class ExcelTableData
        {
            public string TableName { get; set; }

            public DataTable DataSource { get; set; }
        }
        #endregion
    }
}
