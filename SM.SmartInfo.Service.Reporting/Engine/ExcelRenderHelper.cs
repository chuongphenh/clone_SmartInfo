using System;
using System.IO;
using System.Data;
using System.Linq;
using SpreadsheetGear;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.EntityInfos;

namespace SM.SmartInfo.Service.Reporting.Engine
{
    public class ExcelRenderHelper
    {
        #region Constructors
        /// <summary>
        /// Workbook render cuối cùng
        /// </summary>
        private IWorkbook _LastRenderWorkbook;
        protected IHelper _helper;
        protected ExcelRenderInfo _renderInfo;

        public ExcelRenderHelper(IHelper helper, ExcelRenderInfo renderInfo)
        {
            _renderInfo = renderInfo;
            _helper = helper;
        }

        #endregion

        #region SaveAs
        /// <summary>
        /// Render và lưu vào memory
        /// </summary>
        public virtual byte[] RenderToMemory()
        {
            //Đọc template
            var wb = Render();

            var content = wb.SaveToMemory(FileFormat.OpenXMLWorkbook);
            return content;
        }
        #endregion

        #region Render
        /// <summary>
        /// Lấy dữ liệu và merge vào template
        /// </summary>
        /// <param name="templateFilePath"></param>
        /// <returns></returns>
        public virtual IWorkbook Render()
        {
            string templateFilePath = _helper.TemplateFilePath;
            if (!File.Exists(templateFilePath))
            {
                throw new FileNotFoundException("File template không tồn tại: " + templateFilePath);
            }

            IWorkbook wb = Factory.GetWorkbook(templateFilePath);

            Process(wb);

            _LastRenderWorkbook = wb;
            return wb;
        }

        /// <summary>
        /// Set picture thay thế vào vị trí 1 shape trên Workbook vừa render
        /// </summary>
        /// <param name="pictureData">Dữ liệu ảnh. Nếu null: Xóa shape nếu có</param>
        /// <param name="shapeName">Tên của Shape</param>
        public void SetPicture(byte[] pictureData, string shapeName)
        {
            if (_LastRenderWorkbook == null)
                return;

            var worksheet = _LastRenderWorkbook.ActiveWorksheet;
            var shape = worksheet.GetShape(shapeName);
            if (shape == null)
                return;

            if (pictureData == null)
            {
                worksheet.DeleteShape(shape);
            }
            else
            {
                worksheet.SetPicture(pictureData, shape);

                //Check xem ảnh có trong vùng in không, nếu ko mở rộng vùng Print Area cho vừa
                var printAreaRange = worksheet.Range[worksheet.PageSetup.PrintArea];
                //Nếu ảnh bị tràn ra ngoài thì lấy row cuối chứa ảnh, + 1 row để đảm bảo ảnh hiển thị đủ
                var rowOffset = Math.Max(shape.BottomRightCell.Row, printAreaRange.Row + printAreaRange.RowCount);
                //Giữ nguyên vị trí cột
                var columnOffset = printAreaRange.Column + printAreaRange.ColumnCount - 1;
                //Lấy range mới và set lại PrintArea
                var newRange = worksheet.Range[printAreaRange.Row, printAreaRange.Column, rowOffset, columnOffset];
                var newPrintArea = newRange.GetAddress(true, true, ReferenceStyle.A1, false, null);
                worksheet.PageSetup.PrintArea = newPrintArea;
            }
        }
        #endregion

        #region Render Excel
        protected virtual void Process(IWorkbook workbook)
        {
            //Set dữ liệu cho từng vùng
            foreach (var render in _renderInfo.Renders)
            {
                SetDynamicData(workbook, render);
            }
        }

        protected virtual void SetDynamicData(IWorkbook workbook, ExcelRenderInfo.RenderInfo renderInfo)
        {
            //Active worksheet
            if (!string.IsNullOrWhiteSpace(renderInfo.ActiveSheet))
            {
                workbook.ActivateWorksheet(renderInfo.ActiveSheet);
            }
            IWorksheet worksheet = workbook.ActiveWorksheet;

            //Get data
            DataTable dataTable = GetData(worksheet, renderInfo);

            //0. Set dữ liệu vào các vùng tĩnh (DefinedName)
            if (renderInfo.SetDefinedName && dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                worksheet.SetDataForDefinedNames(row);
            }

            //Gen và set Barcode
            if (renderInfo.Barcodes.Count > 0 && dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                SetBarcode(worksheet, renderInfo.Barcodes, row);
            }

            //Gen và set QRcode
            if (renderInfo.QRcodes.Count > 0 && dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                SetQRcode(worksheet, renderInfo.QRcodes, row);
            }

            //1. Tự tạo cột động
            AutoCreateColumn(worksheet, dataTable, renderInfo.AutoCreateColumns);

            //2. Set dữ liệu vào excel dạng Table
            var virtualTable = renderInfo.VirtualTable;
            if (virtualTable != null)
            {
                worksheet.SetDataForVirtualTable(dataTable, virtualTable.TemplateRowName, virtualTable.HideWhenEmpty);
            }

            //3. Set dữ liệu vào Excel dạng Table có nhóm
            var virtualTableGroup = renderInfo.VirtualTableGroup;
            if (virtualTableGroup != null)
            {
                worksheet.SetDataForVirtualTableGroup(dataTable, virtualTableGroup.GroupColumns, virtualTableGroup.GroupDefinedName, virtualTableGroup.HideWhenEmpty);
            }

            //4. Fill dữ liệu theo vùng, vùng có thể có nhiều row
            var fillTemplate = renderInfo.FillTemplate;
            if (fillTemplate != null)
            {
                worksheet.FillTemplateAndCopyDown(dataTable, fillTemplate.RegionName);
            }

            //5. Fill trực tiếp dữ liệu từ 1 DataTable
            var fillFromDataTable = renderInfo.FillDataTable;
            if (fillFromDataTable != null)
            {
                worksheet.FillFromDataTable(dataTable, fillFromDataTable.FromRange);
            }

            //6. Fill từ 2 cột Name và Value của DataTable
            var fillNameValue = renderInfo.FillNameValue;
            if (fillNameValue != null)
            {
                worksheet.FillNameValueDataTable(dataTable, fillNameValue.NameColumn, fillNameValue.ValueColumn);
            }

            //7. Tạo cột động và fill dữ liệu theo cột vừa tạo
            var fillDynamicColumn = renderInfo.FillDynamicColumn;
            if (fillDynamicColumn != null)
            {
                worksheet.FillDynamicColumn(dataTable, fillDynamicColumn.TemplateColumn);
            }

            //10. Convert số => chữ
            ConvertNumberToText(worksheet, renderInfo.NumberToTexts);

            //11. Merge (Do việc thêm động cột - Dòng không hỗ trợ vùng đã merge => Tiến hanh merge sau)
            MergeRanges(worksheet, renderInfo.MergeRanges);

            //12. Merge cột theo nhóm
            MergeColumnGroups(worksheet, renderInfo.MergeColumnGroups);

            //13. Merge các row liền nhau với nhau nếu chúng có cùng giá trị trên 1 cột nào đó.
            MergeRowByColumnValue(worksheet, renderInfo.MergeRowByColumnValues);

            //14. Auto fit
            AutoFit(worksheet, renderInfo.AutoFitRows);

            //15. Delete Row
            DeleteRow(worksheet, renderInfo.DeleteRows);

            //todo: GiangNT: Dựa vào kiểu render để xử lý file excel
        }

        protected virtual DataTable GetData(IWorksheet worksheet, ExcelRenderInfo.RenderInfo renderInfo)
        {
            string cmdText;
            switch (renderInfo.SourceStyle)
            {
                case ExcelRenderInfo.DeclarationStyle.CSharp:
                    string methodName = renderInfo.SourceDynamic.Trim(); //Trim vì đôi khi viết thừa dấu space
                    cmdText = (string)_helper.InvokeCSharpMethod(methodName, worksheet);
                    break;
                case ExcelRenderInfo.DeclarationStyle.Direct:
                default:
                    cmdText = renderInfo.SourceDynamic;
                    break;
            }

            DataTable dataTable = _helper.GetData(cmdText);
            return dataTable;
        }

        protected virtual void AutoCreateColumn(IWorksheet worksheet, DataTable dtData, List<ExcelRenderInfo.AutoCreateColumn> lstColumn)
        {
            foreach (var colDefinedName in lstColumn)
            {
                var lstCol = dtData.Rows.OfType<DataRow>().Select(c => (string)c[0]).ToList();
                worksheet.AutoCreateColumn(lstCol, colDefinedName.PrefixName, colDefinedName.HeaderName);
            }
        }

        protected virtual void ConvertNumberToText(IWorksheet worksheet, List<string> lstNumberToText)
        {
            foreach (var item in lstNumberToText)
            {
                var range = worksheet.GetRange(item);
                var rawValue = range.Value;

                //Thử với int
                int? intValue = rawValue as int?;

                //Thử với kiểu double
                if (intValue == null && rawValue is double)
                {
                    var doubleValue = (double)rawValue;
                    intValue = (int)doubleValue;
                }

                //Thử với string
                if (intValue == null && rawValue is string)
                {
                    int temp;
                    if (int.TryParse((string)rawValue, out temp))
                        intValue = temp;
                }

                if (intValue != null)
                {
                    var strValue = _helper.NumberToText(intValue.Value);
                    range.SetValue(strValue);
                }
            }
        }

        protected virtual void MergeRanges(IWorksheet worksheet, List<string> lstRangeName)
        {
            foreach (var item in lstRangeName)
            {
                var range = worksheet.GetRange(item);
                range.Merge();
            }
        }

        protected virtual void MergeColumnGroups(IWorksheet worksheet, List<ExcelRenderInfo.MergeColumnGroup> lstGroup)
        {
            foreach (var item in lstGroup)
            {
                worksheet.MergeMultiColumns(item.StartRange, item.ColumnCount, item.TotalColumnCountRange);
            }
        }

        protected virtual void MergeRowByColumnValue(IWorksheet worksheet, List<ExcelRenderInfo.MergeRowByColumnValue> lstMerge)
        {
            foreach (var item in lstMerge)
            {
                worksheet.MergeRowByColumnValue(item.MergeRange, item.ValueColumn);
            }
        }

        protected virtual void AutoFit(IWorksheet worksheet, List<string> lstRow)
        {
            foreach (var item in lstRow)
            {
                var row = worksheet.GetRange(item).EntireRow;
                row.AutoRowHeight();
            }
        }

        protected virtual void DeleteRow(IWorksheet worksheet, List<ExcelRenderInfo.DeleteRow> lstIInfo)
        {
            foreach (var item in lstIInfo)
            {
                if (item.ColumnRangName != null && item.CellValue != null)
                {
                    worksheet.DeleteRow(item.ColumnRangName, item.CellValue);
                }
            }
        }

        protected virtual void SetBarcode(IWorksheet worksheet, List<ExcelRenderInfo.Barcode> lstBarcodeInfo, DataRow row)
        {
            const int SCALE = 5; //Do ảnh render ra nhỏ quá thì sẽ mờ nên cần cho to ra

            foreach (var item in lstBarcodeInfo)
            {
                string value = row[item.ColumnName].ToString();
                var template = worksheet.GetShape(item.ShapeName);
                if (template != null)
                {
                    //Width, Height lấy từ Excel thì là Point (double)
                    //Barcode gen thì lại là pixel (int)
                    //=> sẽ bị lệch kích thước giữa ảnh và template
                    //=> ảnh đưa vào excel vẫn bị resize

                    var pictureData = _helper.GenBarcode(value, item.BarcodeType, (int)(template.Width * SCALE), (int)(template.Height * SCALE));
                    worksheet.SetPicture(pictureData, template);
                }
            }
        }

        protected virtual void SetQRcode(IWorksheet worksheet, List<ExcelRenderInfo.QRcode> lstBarcodeInfo, DataRow row)
        {
            const int SCALE = 5; //Do ảnh render ra nhỏ quá thì sẽ mờ nên cần cho to ra

            foreach (var item in lstBarcodeInfo)
            {
                string value = row[item.ColumnName].ToString();
                var template = worksheet.GetShape(item.ShapeName);
                if (template != null)
                {
                    //Width, Height lấy từ Excel thì là Point (double)
                    //Barcode gen thì lại là pixel (int)
                    //=> sẽ bị lệch kích thước giữa ảnh và template
                    //=> ảnh đưa vào excel vẫn bị resize

                    var pictureData = _helper.GenQRcode(value, (int)(template.Height * SCALE));
                    worksheet.SetPicture(pictureData, template);
                }
            }
        }

        #endregion

        public interface IHelper
        {
            /// <summary>
            /// Đường dẫn đến file template dùng để render
            /// </summary>
            string TemplateFilePath { get; set; }
            /// <summary>
            /// Thực thi lệnh sql lấy dữ liệu
            /// </summary>
            /// <param name="cmdText">Sql query</param>
            /// <returns></returns>
            DataTable GetData(string cmdText);
            /// <summary>
            /// Invoke csharp method để lấy dữ liệu
            /// </summary>
            /// <param name="methodName">Tên file method để execute</param>
            /// <param name="workSheet">WorkSheet</param>
            /// <returns></returns>
            object InvokeCSharpMethod(string methodName, IWorksheet workSheet);
            /// <summary>
            /// Đổi chữ thành số
            /// </summary>
            /// <param name="number"></param>
            /// <returns></returns>
            string NumberToText(int number);
            /// <summary>
            /// Tạo ảnh barcode
            /// </summary>
            /// <param name="value">Giá trị để tạo barcode, cần tương ứng với loại Barcode</param>
            /// <param name="barcodeType">Mã loại Barcode</param>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <returns></returns>
            byte[] GenBarcode(string value, string barcodeType, int width, int height);
            /// <summary>
            /// Tạo ảnh QRcode
            /// </summary>
            /// <param name="value">Giá trị để tạo QRcode</param>
            /// <param name="height"></param>
            /// <returns></returns>
            byte[] GenQRcode(string value, int height);
        }
    }
}