using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SoftMart.Wrapper.Office.OpenOffice;
using SM.SmartInfo.SharedComponent.Entities;
using System.Data;
using DocumentFormat.OpenXml.Packaging;

namespace SM.SmartInfo.Utils
{
    public class ExcelTransformHelper
    {
        private WorksheetPart _activeWorksheetPart;
        #region Read data
        public void ReadExcel(string filePath, ExcelTransform transform)
        {
            using (ExcelWrapper excel = new ExcelWrapper(filePath, true))
            {
                if (transform.ListSheet != null)
                {
                    foreach (ExcelTransformSheet sheet in transform.ListSheet)
                    {
                        ReadSheet(excel, sheet);
                    }
                }

                excel.Dispose();
            }
        }

        private void ReadSheet(ExcelWrapper excel, ExcelTransformSheet sheet)
        {
            excel.ActiveSheet(sheet.Name);

            if (sheet.ListRange != null)
            {
                foreach (ExcelTransformRange range in sheet.ListRange)
                {
                    ReadRange(excel, range);
                }
            }

            if (sheet.ListTable != null)
            {
                foreach (ExcelTransformTable table in sheet.ListTable)
                {
                    ReadTable(excel, table);
                }
            }
        }

        private void ReadRange(ExcelWrapper excel, ExcelTransformRange range)
        {
            if (range.ListCell == null)
                return;

            foreach (ExcelTransformCell cellItem in range.ListCell)
            {
                object cellObj = excel.GetCellValue(cellItem.CellAddress);
                cellItem.PropertyValue = GetRealObjectValue(cellObj, cellItem.PropertyType, cellItem.PropertyFormat);
            }
        }

        private object GetRealObjectValue(object objOriginal, TransformItemType valType, string valFormat)
        {
            if (objOriginal == null)
                return null;

            switch (valType)
            {
                case TransformItemType.DateTime:
                    if (objOriginal is DateTime)
                        return objOriginal;

                    DateTime dt;
                    if (DateTime.TryParseExact(objOriginal.ToString(), valFormat, null,
                        System.Globalization.DateTimeStyles.None, out dt))
                        return dt;

                    return null;
                case TransformItemType.Decimal:
                    if (objOriginal is decimal)
                        return objOriginal;

                    decimal d;
                    if (decimal.TryParse(objOriginal.ToString(), System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.GetCultureInfo("en-US"), out d))
                        return d;

                    return null;
                case TransformItemType.Int:
                    if (objOriginal is int)
                        return objOriginal;

                    int i;
                    if (int.TryParse(objOriginal.ToString(), out i))
                        return i;

                    return null;
                default:
                    return objOriginal.ToString();
            }
        }

        private void ReadTable(ExcelWrapper excel, ExcelTransformTable table)
        {
            object[,] lstValue = excel.GetRangeValue(table.RangeName);

            if (lstValue != null && lstValue.Length > 0)
            {
                int rowCount = lstValue.GetLength(0);
                int colCount = lstValue.GetLength(1);
                table.DataReader = new object[rowCount, colCount];

                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < colCount; colIndex++)
                    {
                        // excel array base on 1 index, C# array base on 0 index
                        object objVal = lstValue[rowIndex + 1, colIndex + 1];

                        // cast to real type
                        if (table.ListColumns != null && table.ListColumns.Exists(c => c.ColumnIndex == colIndex))
                        {
                            TransformTableColumn column = table.ListColumns.Find(c => c.ColumnIndex == colIndex);
                            objVal = GetRealObjectValue(objVal, column.PropertyType, column.PropertyFormat);
                        }

                        table.DataReader[rowIndex, colIndex] = objVal;
                    }
                }
            }
            else
            {
                table.DataReader = null;
            }
        }
        #endregion

        #region Transform data
        public void Transform(ExcelTransform transform, string filePath)
        {
            // validate data
            if (transform == null || transform.ListSheet == null || transform.ListSheet.Count == 0)
                return;

            // transform
            using (ExcelWrapper excel = new ExcelWrapper(filePath, false))
            {
                foreach (ExcelTransformSheet sheet in transform.ListSheet)
                {
                    TransformSheet(excel, sheet);
                }

                if (transform.IsProtected)
                {
                    excel.ProtectSheet("SM.FlexValuation.01");
                }

                excel.Save();

                excel.Dispose();
            }
        }

        private void TransformSheet(ExcelWrapper excel, ExcelTransformSheet sheet)
        {
            excel.ActiveSheet(sheet.Name);

            if (sheet.ListRange != null)
            {
                foreach (ExcelTransformRange range in sheet.ListRange)
                {
                    TransformRange(excel, range);
                }
            }

            if (sheet.ListTable != null)
            {
                foreach (ExcelTransformTable table in sheet.ListTable)
                {
                    TransformTable(excel, table);
                }
            }
        }

        private void TransformRange(ExcelWrapper excel, ExcelTransformRange range)
        {
            System.Data.DataTable sourceData = range.DataSourceTable;

            // validate
            if (range.ListCell == null || sourceData == null || sourceData.Rows.Count == 0)
                return;

            // set value
            System.Data.DataRow firstRow = sourceData.Rows[0];
            foreach (ExcelTransformCell cell in range.ListCell)
            {
                string colName = cell.PropertyName;
                if (!sourceData.Columns.Contains(colName))
                    continue;

                object objVal = firstRow[colName];
                excel.SetCellValue(cell.CellAddress, objVal);
            }
        }

        private void TransformTable(ExcelWrapper excel, ExcelTransformTable table)
        {
            System.Data.DataTable sourceData = table.DataSourceTable;

            // validate
            if (sourceData == null || sourceData.Rows.Count == 0)
                return;

            // convert datatable to two dimension array
            int rowCount = sourceData.Rows.Count;
            int colCount = table.ColumnCount;
            object[,] arrData = new object[rowCount, colCount];
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < colCount; colIndex++)
                {
                    TransformTableColumn defineCol = table.ListColumns.Find(c => c.ColumnIndex == colIndex);
                    if (defineCol == null)
                    {
                        arrData[rowIndex, colIndex] = string.Empty;
                        continue;
                    }

                    if (sourceData.Columns.Contains(defineCol.PropertyName))
                        arrData[rowIndex, colIndex] = sourceData.Rows[rowIndex][defineCol.PropertyName];
                    else
                        arrData[rowIndex, colIndex] = string.Empty;
                }
            }

            // set data
            excel.SetTableValue(table.RangeName, arrData);
        }
        #endregion

        #region Transform data (table for report)
        public void Transform(string excelFile, string tableName, System.Data.DataTable tableData,
            Dictionary<string, object> dicFixCell = null)
        {
            // validate
            if (tableData == null || tableData.Rows.Count == 0)
                return;

            // convert datatable to two dimension array
            int rowCount = tableData.Rows.Count;
            int colCount = tableData.Columns.Count;
            object[,] arrData = new object[rowCount, colCount];

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < colCount; colIndex++)
                {
                    arrData[rowIndex, colIndex] = tableData.Rows[rowIndex][colIndex];
                }
            }

            // set data
            using (ExcelWrapper excel = new ExcelWrapper(excelFile, false))
            {
                // transform
                //excel.SetTableValue(tableName, arrData);

                TableDefinitionPart tableDefinitionPart = _activeWorksheetPart.TableDefinitionParts.FirstOrDefault((TableDefinitionPart c) => (string)c.Table.Name == tableName);
                excel.SetTableValue(tableName, tableData);

                // fix cell
                if (dicFixCell != null)
                {
                    foreach (KeyValuePair<string, object> cell in dicFixCell)
                    {
                        excel.SetCellValue(cell.Key, cell.Value);
                    }
                }

                // save
                excel.Save();

                // dispose
                excel.Dispose();
            }
        }

        public void Transform(string excelFile,
            string sheetName1, string tableName1, System.Data.DataTable tableData1,
            string sheetName2, string tableName2, System.Data.DataTable tableData2,
            Dictionary<string, object> dicFixCell1 = null, Dictionary<string, object> dicFixCell2 = null)
        {
            object[,] arrData1 = null;
            object[,] arrData2 = null;

            // validate
            if (tableData1 != null && tableData1.Rows.Count > 0)
            {
                // convert datatable to two dimension array
                int rowCount = tableData1.Rows.Count;
                int colCount = tableData1.Columns.Count;
                arrData1 = new object[rowCount, colCount];
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < colCount; colIndex++)
                    {
                        arrData1[rowIndex, colIndex] = tableData1.Rows[rowIndex][colIndex];
                    }
                }
            }
            if (tableData2 != null && tableData2.Rows.Count > 0)
            {
                // convert datatable to two dimension array
                int rowCount = tableData2.Rows.Count;
                int colCount = tableData2.Columns.Count;
                arrData2 = new object[rowCount, colCount];
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < colCount; colIndex++)
                    {
                        arrData2[rowIndex, colIndex] = tableData2.Rows[rowIndex][colIndex];
                    }
                }
            }

            if (arrData1 == null && arrData2 == null)
                return;

            // set data
            using (ExcelWrapper excel = new ExcelWrapper(excelFile, false))
            {
                // transform
                if (arrData1 != null)
                {
                    excel.ActiveSheet(sheetName1);
                    excel.SetTableValue(tableName1, arrData1);

                    // fix cell
                    if (dicFixCell1 != null)
                    {
                        foreach (KeyValuePair<string, object> cell in dicFixCell1)
                        {
                            excel.SetCellValue(cell.Key, cell.Value);
                        }
                    }
                }
                if (arrData2 != null)
                {
                    excel.ActiveSheet(sheetName2);
                    excel.SetTableValue(tableName2, arrData2);

                    // fix cell
                    if (dicFixCell2 != null)
                    {
                        foreach (KeyValuePair<string, object> cell in dicFixCell2)
                        {
                            excel.SetCellValue(cell.Key, cell.Value);
                        }
                    }
                }

                // save
                excel.Save();

                // dispose
                excel.Dispose();
            }
        }

        public System.Data.DataTable ConvertList2DataTable<T>(IEnumerable<T> datas, List<string> lstProperty, bool addOrderColumn)
        {
            string orderColumnName = "OrderNo";
            // prepare list column
            List<string> lstColName = new List<string>();
            if (lstProperty == null)
                lstColName = SoftMart.Core.Utilities.ReflectionHelper.GetPropertyNames(typeof(T));
            else
                lstColName = lstProperty.Where(c => !string.IsNullOrWhiteSpace(c)).ToList();

            // create table structure
            System.Data.DataTable table = new System.Data.DataTable();
            if (addOrderColumn && !lstColName.Exists(c => String.Equals(orderColumnName, c, StringComparison.OrdinalIgnoreCase)))
                table.Columns.Add(orderColumnName, typeof(object));
            foreach (string colName in lstColName)
                table.Columns.Add(colName, typeof(object));

            // add data into table
            if (datas != null)
            {
                int index = 1;
                foreach (var data in datas)
                {
                    System.Data.DataRow row = table.NewRow();
                    if (addOrderColumn)
                        row[orderColumnName] = index;

                    foreach (string colName in lstColName)
                    {
                        object val = SoftMart.Core.Utilities.ReflectionHelper.GetPropertyValue(data, colName);
                        if (val is string)
                        {
                            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
                            val = Regex.Replace(val.ToString(), r, "", RegexOptions.Compiled);
                        }
                        row[colName] = val;
                    }

                    index++;
                    table.Rows.Add(row);
                }
            }

            return table;
        }
        #endregion
    }
}