using System;
using SpreadsheetGear;

namespace SM.SmartInfo.Service.Reporting.Engine
{
    public static class IWorkbookExt
    {
        ///// <summary>
        ///// Lưu file.
        ///// Hiển thị Dialog cho chọn đường dẫn
        ///// </summary>
        ///// <param name="wb"></param>
        ///// <param name="suggestedFileName">Tên file hiển thị khi show Dialog</param>
        //public static void SaveAsFile(this IWorkbook wb, string suggestedFileName)
        //{
        //    var dialog = new SaveFileDialog();
        //    dialog.FileName = suggestedFileName;
        //    dialog.Filter = "Excel file (*.xlsx)|*.xlsx";
        //    var result = dialog.ShowDialog();
        //    if (result != System.Windows.Forms.DialogResult.OK)
        //        return;

        //    if (!string.IsNullOrWhiteSpace(dialog.FileName))
        //        wb.SaveAs(dialog.FileName, FileFormat.OpenXMLWorkbook);
        //}

        /// <summary>
        /// Tạo DefinedName cho 1 range.
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="range"></param>
        /// <param name="name">Nếu tên đã có: Đổi vùng range</param>
        public static void CreateDefinedName(this IWorkbook wb, IRange range, string name)
        {
            wb.Names.Add(name, "=" + range.Address);
        }

        public static IWorkbook OpenFromMemory(byte[] fileContent)
        {
            // Create a new empty workbook set.
            IWorkbookSet workbookSet = SpreadsheetGear.Factory.GetWorkbookSet();
            // Create a new empty workbook in the workbook set.
            IWorkbook workbook = workbookSet.Workbooks.Add();
            // Open the saved workbook from memory.
            var wb = workbookSet.Workbooks.OpenFromMemory(fileContent);
            return wb;
        }

        public static void ActivateWorksheet(this IWorkbook wb, string name)
        {
            var ws = wb.Worksheets[name];
            if (ws == null)
                throw new Exception("Worksheet không tồn tại: " + name);

            ws.Select();
        }
    }
}