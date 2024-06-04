using System;
using System.Data;
using System.Linq;
using SpreadsheetGear;
using System.Collections.Generic;

namespace SM.SmartInfo.Service.Reporting.Engine
{
    public static class IWorksheetExt
    {
        #region SetDataForDefinedNames
        /// <summary>
        /// Set giá trị cho các DefinedName từ DataRow.
        /// Từ 1 DefinedName, tìm trong DataTable nếu có cột đó thì set
        /// </summary>
        /// <param name="workbook">Template</param>
        /// <param name="row">Dữ liệu để fill vào Template, Mapping giữa ColumName với DefinedName</param>
        /// <param name="kytuthaythe"></param>
        /// <returns>Vị trí row vừa được set dữ liệu. Bắt đầu từ 0. Null nếu không có bookmark nào được thay</returns>
        public static int? SetDataForDefinedNames(this IWorksheet worksheet, DataRow row)
        {
            var definedNames = worksheet.Workbook.Names;

            int? rowIndex = null;
            foreach (IName range in definedNames)
            {
                var rangeName = range.Name;
                if (row.Table.Columns.Contains(rangeName))
                {
                    var iRange = worksheet.GetRange(rangeName);
                    var value = row[rangeName];
                    iRange.SetValue(value);
                    rowIndex = iRange.Row;
                    continue;
                }
            }
            return rowIndex;
        }
        #endregion

        #region SetDataForVirtualTable
        /// <summary>
        /// Set dữ liệu từ 1 DataTable thành nhiều Rows dựa vào 1 row template.
        /// Từ 1 DefinedName, tìm trong DataTable nếu có cột đó thì set vào row.
        /// VD:
        /// |III	|Khám bệnh                  |SUM()  |SUM()  |               = Summary: Dòng tổng hợp thông tin (SUM,...) dựa vào các dòng được fill vào phía dưới
        /// |1      |Mất ngủ    |Lần    |1.0    |12     |13     |               = Template: Dòng  để fill data vào, chứa các cell được định nghĩa DefinedName
        /// |   -   |     -     |   -   |   -   |   -   |   -   |               = Dòng trống Hide đi, phục vụ cho SUM
        /// </summary>
        /// <param name="workbook">Template</param>
        /// <param name="dtData">Dữ liệu để fill vào dòng Template, Mapping giữa ColumName với DefinedName</param>
        /// <param name="templateRowName">
        /// Name xác định row chứa Template (Chỉ cần 1 cell trên row đó cũng được).
        /// Nếu dùng DefinedName thì thứ tự gọi hàm ko quan trọng, nhưng nếu dùng vị trí (A11,...) thì phải gọi hàm ngược từ dưới lên (A15 => A13 => A11...)
        /// VD: A11, DoiTuong...</param>
        /// <param name="hideWhenEmpty">True: Nếu dtData rỗng thì ẩn 2 dòng Tổng hợp và Template đi</param>
        public static void SetDataForVirtualTable(this IWorksheet worksheet, DataTable dtData, string templateRowName, bool hideWhenEmpty)
        {
            var lstDataRow = dtData.Rows.Cast<DataRow>().ToList();
            var excelTemplateRow = worksheet.GetRange(templateRowName).EntireRow;

            //Nếu không có dữ liệu thì hide 2 dòng trên đi
            if (hideWhenEmpty && lstDataRow.Count == 0)
            {
                //Ẩn dòng Template
                excelTemplateRow.Hidden = true;
                //Ẩn dòng Summary
                int rowOffset = -1; //-1: Dòng ngay trước row Template
                var summaryRow = excelTemplateRow.Rows[rowOffset, 0].EntireRow;
                summaryRow.Hidden = true;
            }
            else
            {
                SetDataForRows(worksheet, lstDataRow, templateRowName);
            }
        }

        /// <summary>
        /// Set dữ liệu từ 1 DataTable thành nhiều Rows dựa vào 1 row template
        /// Từ 1 DefinedName, tìm trong DataTable nếu có cột đó thì set vào row.
        /// Có thể set toàn bộ hoặc 1 phần DataTable.
        /// </summary>
        /// <param name="workbook">Template</param>
        /// <param name="dtData">Dữ liệu để fill vào dòng Template, Mapping giữa ColumName với DefinedName</param>
        /// <param name="templateRowName">DefinedName xác định row chứa Template (Chỉ cần 1 cell trên row đó cũng được). VD: A11, DoiTuong...</param>
        private static void SetDataForRows(this IWorksheet worksheet, List<DataRow> lstDataRow, string templateRowName)
        {
            if (lstDataRow.Count == 0)
                return;

            var excelTemplateRow = worksheet.GetRange(templateRowName).EntireRow;

            //Insert rows.
            int templateRowIndex = excelTemplateRow.Row;
            int dataRowIndex = templateRowIndex + 1;
            int? rowOffset; //Vị trí row được thay thế
            for (int i = 0; i < lstDataRow.Count; i++)
            {
                //Fill vào row template
                DataRow dtRow = lstDataRow[i];
                rowOffset = SetDataForDefinedNames(worksheet, dtRow);
                if (rowOffset != null)
                    worksheet.AutoRowHeight(rowOffset.Value);

                InsertAndCopyRow(worksheet, templateRowIndex, dataRowIndex);

                //Đánh dấu dòng tiếp để đổ dữ liệu
                dataRowIndex++;
            }

            //Xóa row template
            excelTemplateRow.Delete();
        }

        private static void InsertRows(this IWorksheet worksheet, IRange position, int count)
        {
            for (int i = 0; i < count; i++)
            {
                position.Insert();
            }
        }
        #endregion

        #region FillTemplateAndCopyDown
        /// <summary>
        /// Fill dữ liệu cho toàn bộ row bằng cách:
        /// Fill dữ liệu vào vùng Template rồi copy cả vùng đó xuống (Có thể nhiều Row)
        /// </summary>
        /// <param name="workbook">Template</param>
        /// <param name="dtData">Dữ liệu để fill vào dòng Template, Mapping giữa ColumName với DefinedName</param>
        /// <param name="regionName">
        /// DefinedName: Tên vùng teamplate
        /// Vùng template có thể chứa nhiều row (Khác với SetDataForVirtualTable chỉ set cho từng row)
        /// </param>
        public static void FillTemplateAndCopyDown(this IWorksheet worksheet, DataTable dtData, string regionName)
        {
            //todo: Kiểm tra nếu dùng merge area thì sẽ die
            //var templateRegion = worksheet.Range[regionName].MergeArea.EntireRow;

            //Worksheet.UsedRange.Rows.AutoFit();
            var templateRegion = worksheet.GetRange(regionName).EntireRow;
            var lstDataRow = dtData.Rows;

            int dataRowIndex = templateRegion.Row + templateRegion.RowCount;
            for (int i = 0; i < lstDataRow.Count; i++)
            {
                //Dữ liệu để Fill vào row template
                DataRow dtRow = lstDataRow[i];

                //Set giá trị vào các row template
                worksheet.SetDataForDefinedNames(dtRow);

                //Thêm row mới, số row = số row từ template
                var nextRow = worksheet.Range[dataRowIndex, 0].EntireRow;
                InsertRows(worksheet, nextRow, templateRegion.RowCount);

                //Copy xuống dưới chuẩn bị cho lần tiếp
                templateRegion.Copy(nextRow);

                nextRow.AutoRowHeight();

                //Đánh dấu dòng tiếp để đổ dữ liệu
                dataRowIndex += templateRegion.RowCount;
            }

            //Fill xong dữ liệu thì xóa row template
            templateRegion.Delete();
        }
        #endregion

        #region SetDataForVirtualTableGroup
        /// <summary>
        /// Set dữ liệu theo nhóm
        /// Tham khảo file SampleDataGroup.xlsx
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="dtData">Dữ liệu cần order trước theo danh sách cột nhóm</param>
        /// <param name="lstGroupColumn">Danh sách cột trên DataTable để nhóm dữ liệu</param>
        /// <param name="groupPrefixDefinedName">Prefix các DefinedName trên Excel để đổ tên nhóm.
        /// Mỗi nhóm trên lstGroupColumn được map với 1 vùng DefinedName có prefix = groupPrefixDefinedName.
        /// </param>
        /// <param name="hideWhenEmpty">True: Nếu dtData rỗng thì ẩn 2 dòng Tổng hợp và Template đi</param>
        public static void SetDataForVirtualTableGroup(this IWorksheet worksheet, DataTable dtData,
                                                       List<string> lstGroupColumn, string groupPrefixDefinedName,
                                                       bool hideWhenEmpty)
        {
            //Tìm các cột chứa Group và cột chứa template cho dữ liệu
            string GROUP_DEFINED_NAME = string.IsNullOrWhiteSpace(groupPrefixDefinedName) ? "AUTO_GROUP_" : groupPrefixDefinedName;
            int firstGroupRowIndex = worksheet.GetRange(GROUP_DEFINED_NAME + "0").Row;
            int templateRowIndex = firstGroupRowIndex + lstGroupColumn.Count;

            //Dữ liệu phải được sắp xếp theo các cột
            //Mỗi cột nhóm phải tương ứng với 1 HEADER ROW TEMPLATE
            //Tên DefinedName của nhóm: 
            var lstDataRow = dtData.Rows.Cast<DataRow>().ToList();
            if (lstDataRow.Count == 0)
            {
                //Nếu không có dữ liệu thì hide các dòng group và template đi
                if (hideWhenEmpty)
                {
                    for (int i = firstGroupRowIndex; i <= templateRowIndex; i++)
                    {
                        var excelTemplateRow = worksheet.Range[i, 0].EntireRow;
                        excelTemplateRow.Hidden = true;
                    }
                }
                return;
            }

            //Nơi sẽ được fill dữ liệu tiếp theo
            int dataRowIndex = templateRowIndex + 1;

            DataRow previousRow = null;
            foreach (var dtRow in lstDataRow)
            {
                //1. Tính toán xem có copy những group nào không
                //  Tiến hành copy các nhóm bị lệch
                int index = FindChangedGroupIndex(previousRow, dtRow, lstGroupColumn);
                for (int i = index; i < lstGroupColumn.Count; i++)
                {
                    string groupName = lstGroupColumn[i];
                    string groupDefinedName = GROUP_DEFINED_NAME + i;
                    object groupValue = dtRow[groupName];
                    var iRange = worksheet.GetRange(groupDefinedName);
                    iRange.SetValue(groupValue);

                    //Set value cho nhóm, phục vụ cho việc tính toán
                    SetGroupValues(worksheet, iRange.EntireRow, dtRow, i, lstGroupColumn);

                    //Insert 1 row phía dưới row template
                    //Copy format từ row template xuống row mới vừa insert
                    InsertAndCopyRow(worksheet, firstGroupRowIndex + i, dataRowIndex);
                    //Đánh dấu dòng tiếp để đổ dữ liệu
                    dataRowIndex++;
                }

                //2. Set value cho {$ColumnTemplate}, copy xuống dưới chuẩn bị cho row tiếp
                var rowOffset = worksheet.SetDataForDefinedNames(dtRow);
                if (rowOffset != null)
                    worksheet.AutoRowHeight(rowOffset.Value);

                //Insert 1 row phía dưới row template
                //Copy format từ row template xuống row mới ngay sau
                InsertAndCopyRow(worksheet, templateRowIndex, dataRowIndex);
                //Đánh dấu dòng tiếp để đổ dữ liệu
                dataRowIndex++;

                //Lưu lại row để so sánh nhóm với row sau
                previousRow = dtRow;
            }

            //Xóa các row template: = số nhóm + dòng data template
            for (int i = 0; i < lstGroupColumn.Count + 1; i++)
            {
                worksheet.Range[firstGroupRowIndex, 0].EntireRow.Delete();
            }
        }
        /// <summary>
        /// Tìm xem dữ liệu 2 row bị lệch nhóm so với nhau ở đâu
        /// </summary>
        /// <param name="previousRow"></param>
        /// <param name="currentRow"></param>
        /// <param name="lstGroupColumn"></param>
        /// <returns></returns>
        private static int FindChangedGroupIndex(DataRow previousRow, DataRow currentRow, List<string> lstGroupColumn)
        {
            if (previousRow == null)
                return 0;

            for (int i = 0; i < lstGroupColumn.Count; i++)
            {
                var colName = lstGroupColumn[i];
                //Do dữ liệu được đóng gói vào object nên so sánh trực tiếp không đúng
                var previousValue = previousRow[colName] as string;
                var currentValue = currentRow[colName] as string;
                if (previousValue != currentValue)
                    return i;
            }

            return lstGroupColumn.Count;
        }

        /// <summary>
        /// Insert 1 row vào nơi cần để dữ liệu
        /// Copy format từ row template (Đã được set dữ liệu) xuống row mới vừa insert
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="rowSourceIndex">Vị trí row nguồn, base 0</param>
        /// <param name="rowDestinationIndex">Vị trí row đích, base 0</param>
        private static void InsertAndCopyRow(IWorksheet ws, int rowSourceIndex, int rowDestinationIndex)
        {
            var desRow = ws.Range[rowDestinationIndex, 0].EntireRow;
            desRow.Insert(InsertShiftDirection.Down);

            var sourceRow = ws.Range[rowSourceIndex, 0].EntireRow;
            sourceRow.Copy(desRow, PasteType.All, PasteOperation.None, false, false);

            desRow.Hidden = false;
        }

        /// <summary>
        /// Set value cho nhóm, phục vụ cho việc tính toán.
        /// Muốn xem thực tế giá trị thì rem lại, so sánh với khi chưa rem là rõ
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="entireRow"></param>
        /// <param name="dtRow"></param>
        /// <param name="index"></param>
        /// <param name="lstGroupColumn"></param>
        private static void SetGroupValues(IWorksheet ws, IRange entireRow, DataRow dtRow, int index, List<string> lstGroupColumn)
        {
            for (int i = 0; i <= index; i++)
            {
                var colName = lstGroupColumn[i];
                //Lấy DefinedName hiển thị giá trị nhóm (VD: "Tinh"). Dùng cột đó để fill giá trị nhóm.
                var definedRange = ws.GetRange(colName);

                entireRow[0, definedRange.Column].Value = dtRow[colName];
            }
        }
        #endregion

        #region FillFromDataTable
        /// <summary>
        /// Set trực tiếp DataTable vào excel, lần lượt theo từng cột
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="dtData"></param>
        /// <param name="fromRange">Vị trí bắt đầu</param>
        public static void FillFromDataTable(this IWorksheet ws, DataTable dtData, string fromRange)
        {
            var startRange = ws.GetRange(fromRange);
            var range = ws.Range[startRange.Row, startRange.Column, startRange.Row + dtData.Rows.Count, startRange.Column + dtData.Columns.Count];

            try
            {
                range.CopyFromDataTable(dtData, SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);
            }
            catch (Exception ex)
            {
                throw new Exception("CopyFromDataTable failed. Range: " + range.Address, ex);
            }
        }
        #endregion

        #region FillNameValueDataTable
        /// <summary>
        /// Fill từ 2 cột Name và Value của DataTable
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="dtData"></param>
        /// <param name="nameColumn">Tên cột chứa Name, dùng để map với tên Bookmark trên Excel</param>
        /// <param name="valueColumn">Tên cột chứa Value</param>
        public static void FillNameValueDataTable(this IWorksheet ws, DataTable dtData, string nameColumn, string valueColumn)
        {
            foreach (DataRow row in dtData.Rows)
            {
                var rangeName = (string)row[nameColumn];
                var rangeValue = row[valueColumn];
                var range = ws.GetRange(rangeName);
                range.SetValue(rangeValue);
            }
        }
        #endregion

        #region FillDynamicColumn
        /// <summary>
        /// Tạo cột động và fill dữ liệu theo cột vừa tạo.
        /// Để giữ được format thì các cell trên cột template sẽ đặt Bookmark.
        /// Dòng đầu tiên của DataTable sẽ chứa các bookmark tương ứng
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="dtData"></param>
        /// <param name="templateColumn">Cột template</param>
        public static void FillDynamicColumn(this IWorksheet worksheet, DataTable dtData, string templateColumn)
        {
            DataTable dtRotated = dtData;

            var lstDataRow = dtRotated.Rows.Cast<DataRow>().ToList();
            SetDataForColumns(worksheet, lstDataRow, templateColumn);
        }

        /// <summary>
        /// Set dữ liệu từ 1 DataTable thành nhiều Columns dựa vào 1 Column template
        /// Từ 1 DefinedName, tìm trong DataTable nếu có cột đó thì set vào Column.
        /// Có thể set toàn bộ hoặc 1 phần DataTable.
        /// </summary>
        /// <param name="workbook">Template</param>
        /// <param name="dtData">Dữ liệu để fill vào dòng Template, Mapping giữa ColumName với DefinedName</param>
        /// <param name="templateColumnName">DefinedName xác định row chứa Template (Chỉ cần 1 cell trên row đó cũng được). VD: A11, DoiTuong...</param>
        private static void SetDataForColumns(this IWorksheet worksheet, List<DataRow> lstDataRow, string templateColumnName)
        {
            if (lstDataRow.Count == 0)
                return;

            var excelTemplateColumn = worksheet.GetRange(templateColumnName).EntireColumn;

            //Insert rows.
            int templateColumnIndex = excelTemplateColumn.Column;
            int dataColumnIndex = templateColumnIndex + 1;
            for (int i = 0; i < lstDataRow.Count; i++)
            {
                //Fill vào column template
                DataRow dtRow = lstDataRow[i];
                SetDataForDefinedNames(worksheet, dtRow);
                InsertAndCopyColumn(worksheet, templateColumnIndex, dataColumnIndex);

                //Đánh dấu cột tiếp để đổ dữ liệu
                dataColumnIndex++;
            }

            //Xóa row template
            excelTemplateColumn.Delete();
        }

        /// <summary>
        /// Insert 1 column vào nơi cần để dữ liệu
        /// Copy format từ column template (Đã được set dữ liệu) sang column mới vừa insert
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="columnSourceIndex">Vị trí column nguồn, base 0</param>
        /// <param name="columnDestinationIndex">Vị trí column đích, base 0</param>
        private static void InsertAndCopyColumn(IWorksheet ws, int columnSourceIndex, int columnDestinationIndex)
        {
            var desColumn = ws.Range[0, columnDestinationIndex].EntireColumn;
            desColumn.Insert(InsertShiftDirection.Right);

            var sourceColumn = ws.Range[0, columnSourceIndex].EntireColumn;
            sourceColumn.Copy(desColumn, PasteType.All, PasteOperation.None, false, false);

            desColumn.Hidden = false;
        }

        //Chạy rồi nhưng rem lại vì chưa dùng
        ///// <summary>
        ///// Xoay giá trị dòng/cột của DataTable.
        ///// Dòng đầu tiên lấy làm tiêu đề cột
        ///// Số cột là tĩnh vì biết trước trên lệnh SQL.
        ///// 
        ///// Input:
        ///// BookmarkName| Dư kho tổng   | Phôi lỗi  | Phôi Test | Phôi in hỏng  |
        ///// ---------------------------------------------------------------------
        ///// Thẻ Gold    | Gold_Tong     | Gold_Loi  | Gold_Test |  Gold_Hong    |
        ///// Thẻ Classic | Clas_Tong     | Clas_Loi  | Clas_Test |  Clas_Hong    |
        ///// Thẻ JCB     | JCB_Tong      | JCB_Loi   | JCB_Test  |  JCB_Hong     |
        ///// 
        ///// Output:
        ///// BookmarkName| Thẻ Gold      |Thẻ Classic| Thẻ JCB   |
        ///// -----------------------------------------------------
        ///// Dư kho tổng | Gold_Tong     | Clas_Tong | JCB_Tong  |
        ///// Phôi lỗi    | Gold_Loi      | Clas_Loi  | JCB_Loi   |
        ///// Phôi Test   | Gold_Test     | Clas_Test | JCB_Test  |
        ///// Phôi in hỏng| Gold_Hong     | Clas_Hong | JCB_Hong  |
        ///// 
        ///// BookmarkName: Thường là tên bookmark trên Header của cột để fill tên có nghĩa vào
        ///// </summary>
        ///// <param name="dtSource"></param>
        ///// <returns></returns>
        //public static DataTable RotateDataTable(DataTable dtSource)
        //{
        //    //01. Tạo structure
        //    DataTable dtDest = new DataTable();

        //    //Tên cột của Dest là giá trị các row trên cột đầu tiên của Source
        //    //Giữ nguyên tên cột đầu làm bookmark
        //    dtDest.Columns.Add(dtSource.Columns[0].ColumnName, typeof(object));
        //    foreach (DataRow row in dtSource.Rows)
        //    {
        //        var value = row[0] as string;
        //        dtDest.Columns.Add(value, typeof(object));
        //    }

        //    //Giá trị các row trên cột đầu tiên của Dest là tên các cột của Source
        //    for (int i = 1; i < dtSource.Columns.Count; i++)
        //    {
        //        var newRow = dtDest.NewRow();
        //        newRow[0] = dtSource.Columns[i].ColumnName;
        //        dtDest.Rows.Add(newRow);
        //    }

        //    //02. Chuyển giá trị
        //    for (int rowIndex = 0; rowIndex < dtSource.Rows.Count; rowIndex++)
        //    {
        //        //Bắt đầu từ 1 vì cột 0 chứa tên thẻ
        //        for (int colIndex = 1; colIndex < dtSource.Columns.Count; colIndex++)
        //        {
        //            //Bắt đầu set từ cột 1 vì cột 0 chứa tên cột của table source
        //            dtDest.Rows[colIndex - 1][rowIndex + 1] = dtSource.Rows[rowIndex][colIndex];
        //        }
        //    }

        //    return dtDest;
        //}

        #endregion

        #region AutoCreateColumn
        /// <summary>
        /// Tự động tạo cột và set giá trị cho header
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="lstCol"></param>
        /// <param name="columnDefinedName">Cell để tạo defined name</param>
        /// <param name="headerValueRange">Cell để fill tên cột của DataTable (Select từ sql)</param>
        public static void AutoCreateColumn(this IWorksheet worksheet, List<string> lstCol, string columnDefinedName, string headerValueRange)
        {
            //1. Tạo các cột mới nếu chưa đủ
            var range = worksheet.GetRange(columnDefinedName);
            int newColIndex = range.Column; //Tạo về phía bên trái của cột template. Vì 10 cột tĩnh ở phía trước

            for (int i = 0; i < lstCol.Count; i++)
            {
                //Tìm bookmark
                var definedName = columnDefinedName + i;
                var colRange = worksheet.TryGetRange(definedName);

                //Nếu chưa có thì tạo mới cột và bookmark
                if (colRange == null)
                {
                    InsertAndCopyColumn(worksheet, newColIndex);

                    var newRange = worksheet.Range[range.Row, newColIndex];
                    worksheet.Workbook.CreateDefinedName(newRange, definedName);
                    newColIndex++;
                }
            }

            //2. Kiểm tra danh sách các cột đã được tạo để set value cho header
            if (!string.IsNullOrWhiteSpace(headerValueRange))
            {
                var headerRange = worksheet.GetRange(headerValueRange);
                for (int i = 0; i < lstCol.Count; i++)
                {
                    //Tìm bookmark
                    var definedName = columnDefinedName + i;
                    var colRange = worksheet.GetRange(definedName);

                    //Set giá trị cho cell header
                    var newHeaderRange = worksheet.Range[headerRange.Row, colRange.Column];
                    var headerValue = lstCol[i];
                    newHeaderRange.SetValue(headerValue);
                }
            }
        }

        /// <summary>
        /// Insert 1 col vào ngay vị trí template và đẩy cột template sang phải.
        /// Copy format từ col template (Đã được set dữ liệu) sang col mới vừa insert
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="colSourceIndex">Vị trí col nguồn, base 0</param>
        private static void InsertAndCopyColumn(IWorksheet ws, int colSourceIndex)
        {
            var desCol = ws.Range[0, colSourceIndex].EntireColumn;
            desCol.Insert();

            //Khi insert cột mới vào thì cột template bị đẩy sang phải 1 vị trí
            var sourceCol = ws.Range[0, colSourceIndex + 1].EntireColumn;

            sourceCol.Copy(desCol, PasteType.All, PasteOperation.None, false, false);

            desCol.Hidden = false;
        }
        #endregion

        #region DeleteRow
        /// <summary>
        /// Cấu hình các row bị xóa nếu có dấu hiệu đặc biệt
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="rangeName">Vùng kiểm tra, thường là 1 cột hoặc 1 phần của cột</param>
        /// <param name="value">Trên cột đó, nếu cell có giá trị đặc biệt thì xóa row</param>
        public static void DeleteRow(this IWorksheet worksheet, string rangeName, string value)
        {
            if (rangeName == null || value == null)
                throw new ArgumentNullException();

            var colRange = worksheet.GetRange(rangeName);

            for (int i = colRange.RowCount - 1; i >= 0; i--)
            {
                var rowIndex = colRange.Row + i;
                var col = colRange.Column;

                var cell = worksheet.Range[rowIndex, col];
                if (cell.Value == null)
                    continue;

                if (value.Equals(cell.Value.ToString(), StringComparison.OrdinalIgnoreCase))
                    cell.EntireRow.Delete();
            }
        }
        #endregion

        #region MergeMultiColumns
        /// <summary>
        /// Merge cột theo nhóm
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="startRange">Vị trí bắt đầu, chứa 1 cột nhưng có thể chứa nhiều row</param>
        /// <param name="columnCount">Số cột được merge thành 1 nhóm</param>
        /// <param name="totalColumnCountRange">Vị trí range chứa Tổng số cột cần xử lý</param>
        public static void MergeMultiColumns(this IWorksheet ws, string startRange, int columnCount, string totalColumnCountRange)
        {
            var stRange = ws.GetRange(startRange);
            var rangeValue = ws.GetRange(totalColumnCountRange).Value;
            var totalColumnCount = (int)(double)rangeValue;

            int startColumn = stRange.Column;
            int endColumn = startColumn + totalColumnCount - 1; //Trừ 1 vì merge từ ô Start
            while (startColumn <= endColumn)
            {
                //Nếu hết thì lấy phần còn lại nhưng luôn <= columnCount
                int endColumnOffset = Math.Min(startColumn + columnCount - 1, stRange.Column + totalColumnCount - 1);
                var range = ws.Range[stRange.Row, startColumn, stRange.Row, endColumnOffset];
                range.Merge();
                startColumn = endColumnOffset + 1;
            }
        }
        #endregion

        #region MergeRowByColumnValue
        /// <summary>
        /// Merge các row liền nhau với nhau nếu chúng có cùng giá trị trên 1 cột nào đó.
        /// Thường thì row đầu (Header) và row cuối (ẩn) là 2 row cố định  để xác định range nên 2 row này sẽ bỏ qua khi tính toán
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="mergeRangeName">Vùng tính toán để merge. Ex: "I3:J300"</param>
        /// <param name="valueColumn">Tên cột dữ liệu để so sánh. Ex: B</param>
        public static void MergeRowByColumnValue(this IWorksheet ws, string mergeRangeName, string valueColumn)
        {
            var rangeMerge = ws.GetRange(mergeRangeName);
            var valueColumnOffset = ws.Range[valueColumn + "1"].Column;

            //Ko có row hoặc 1 thì bỏ qua luôn
            if (rangeMerge.RowCount <= 1)
                return;

            //Bỏ qua Row đầu (Header) và row cuối (row ẩn để lấy range)
            var rowStart = rangeMerge.Row + 1;
            var rowEnd = rangeMerge.Row + rangeMerge.Rows.RowCount - 2;

            //Vị trí block đầu và Giá trị so sánh: lấy luôn của row đầu
            //Vị trí block đầu
            int rowNewBlock = rowStart;
            //Giá trị ref dùng so sánh giữa các row
            string rowNewBlockValueRef = ws.Range[rowStart, valueColumnOffset].Value as string;

            //Cứ lần lượt duyệt từng row, nếu row có value ref khác với của row trước thì merge các row phía trước lại
            for (int i = rowStart + 1; i <= rowEnd; i++)
            {
                var value = ws.Range[i, valueColumnOffset].Value as string;

                //Nếu là row cuối có 2 khả năng xảy ra
                if (i == rowEnd)
                {
                    if (rowNewBlockValueRef != value)
                    {
                        //1: Row cuối ko merge được với block trước
                        //Merge các row trước lại 
                        var rangeBlock = ws.Range[rowNewBlock, rangeMerge.Column, i - 1, rangeMerge.Column + rangeMerge.ColumnCount - 1];
                        rangeBlock.Merge();

                        //Merge row cuối riêng lẻ (Trường hợp vùng merge có nhiều cột, còn có 1 cột thì khỏi cần)
                        var lastBlock = ws.Range[rowEnd, rangeMerge.Column, rowEnd, rangeMerge.Column + rangeMerge.ColumnCount - 1];
                        lastBlock.Merge();
                    }
                    else
                    {
                        //2: Row cuối có thể merge với block trước
                        //Merge row cuối với các row trước
                        var rangeBlock = ws.Range[rowNewBlock, rangeMerge.Column, rowEnd, rangeMerge.Column + rangeMerge.ColumnCount - 1];
                        rangeBlock.Merge();
                    }
                }
                //Nếu giá trị tham khảo không còn giống row trước thì merge các row trước lại với nhau
                else if (rowNewBlockValueRef != value)
                {
                    var rangeBlock = ws.Range[rowNewBlock, rangeMerge.Column, i - 1, rangeMerge.Column + rangeMerge.ColumnCount - 1];
                    rangeBlock.Merge();

                    //Lưu giá trị bắt đầu để tính block tiếp theo
                    rowNewBlock = i;
                    rowNewBlockValueRef = value;
                }
            }

        }
        #endregion

        #region AutoRowHeight
        /// <summary>
        /// Độ cao lớn nhất của 1 row. Nếu quá giá trị này sẽ báo lỗi
        /// </summary>
        const int MAX_ROW_HEIGHT = 408;
        /// <summary>
        /// Tự động set chiều cao cho 1 row
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="rowOffset">Số thứ tự dòng cần tính, bắt đầu từ 0</param>
        public static void AutoRowHeight(this IWorksheet worksheet, int rowOffset)
        {
            IRange entireRow = worksheet.Range[rowOffset, 0].EntireRow;

            entireRow.AutoRowHeight();
        }

        public static void AutoRowHeight(this IRange row)
        {
            var entireRow = row.IsEntireRows ? row : row.EntireRow;

            //Gọi AutoFit để excel tự tính toán lại chiều cao. Từ đó mới có chiều cao mới
            entireRow.AutoFit();
            //Component đã tính được độ cao gần đúng theo giá trị có trong row
            //Nhưng nếu không set lại thì độ cao vẫn ko đổi (Chắc là default - auto)
            var height = entireRow.RowHeight;
            //Độ cao row bị thiếu so với yêu cầu 1 chút, càng nhiều row thì càng thiếu => Tính tỉ lệ để bù thêm
            //100: Bù thêm cho các row độ cao thấp
            var ratio = (100 + height) / (double)2200;
            height = height * (1 + ratio);

            //Kiểm tra vượt ngưỡng không
            height = Math.Min(height, MAX_ROW_HEIGHT);

            //Set lại độ cao (Chắc để thành Manual) để giữ độ cao của row
            entireRow.RowHeight = height;
        }
        #endregion

        #region Shape - SetPicture

        public static SpreadsheetGear.Shapes.IShape GetShape(this IWorksheet worksheet, string shapeName)
        {
            //Tìm ảnh template theo tên
            var shape = worksheet.Shapes.OfType<SpreadsheetGear.Shapes.IShape>()
                                        .FirstOrDefault(c => c.Name == shapeName);
            return shape;
        }

        public static void DeleteShape(this IWorksheet worksheet, SpreadsheetGear.Shapes.IShape shape)
        {
            //Xóa ảnh template
            shape.Delete();
        }

        /// <summary>
        /// Set picture thay thế vào vị trí 1 shape trên template
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="pictureData"></param>
        /// <param name="shape"></param>
        public static void SetPicture(this IWorksheet worksheet, byte[] pictureData, SpreadsheetGear.Shapes.IShape shape)
        {
            //Lưu Name trước nếu không sau khi xóa sẽ ko đọc dc
            string name = shape.Name;

            //Xóa ảnh template
            shape.Delete();

            // Insert image mới có vị trí và kích cỡ của ảnh template
            var newPicture = worksheet.Shapes.AddPicture(pictureData, shape.Left, shape.Top, shape.Width, shape.Height);
            newPicture.Name = name;
        }

        #endregion

        #region Utils
        public static IRange GetRange(this IWorksheet worksheet, string rangeName)
        {
            IRange iRange = null;
            try
            {
                iRange = worksheet.Range[rangeName];
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Get range failed. RangeName: [{0}]", rangeName), ex);
            }

            if (iRange == null)
                throw new Exception(string.Format("Range not exist: [{0}]", rangeName));

            return iRange;
        }

        public static IRange TryGetRange(this IWorksheet worksheet, string rangeName)
        {
            try
            {
                IRange iRange = worksheet.Range[rangeName];
                return iRange;
            }
            catch
            {
                return null;
            }
        }

        public static void SetValue(this IRange range, object value)
        {
            try
            {
                range.Value = value;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Set range value failed: [{0}] = [{1}]", range.Name, value), ex);
            }
        }
        #endregion

        #region Các method sẽ revamp lại

        /// <summary>
        /// add range
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="dtData"></param>
        /// <param name="regionName"></param>
        /// <param name="sheetName"> tên sheet fill data</param>
        public static void FillTemplateAndCopyRange(this IWorksheet worksheet, DataTable dtData, string regionName)
        {
            var cells = worksheet.Cells;
            var templateRegion = cells[regionName].EntireRow;
            // Create a new workbook and worksheet.

            //SpreadsheetGear.IWorksheet worksheet = workbook.Worksheets[sheetName];
            //worksheet.Name = "BA02";

            // Get the top left cell for the DataTable.
            SpreadsheetGear.IRange range = worksheet.Cells[regionName];

            // Copy the DataTable to the worksheet range.
            range.CopyFromDataTable(dtData, SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);

            // Auto size all worksheet columns which contain data
            worksheet.UsedRange.Rows.AutoFit();
        }

        #endregion
    }
}