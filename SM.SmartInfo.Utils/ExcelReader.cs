using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using SoftMart.Core.OfficeUtilities.Excel;
using SoftMart.Core.OfficeUtilities.Excel.Entity;
using OfficeOpenXml;
using System.Linq;

namespace SM.SmartInfo.Utils
{
    public class ExcelReader
    {
        public static List<T> Read<T>(string excelFileName, byte[] excelFileContent, string xmlMappingFileName) where T : class
        {
            List<T> lstResult = new List<T>();

            // load file xml mapping
            string xmlFilePath = System.IO.Path.Combine(ConfigUtils.DynamicReportFolder, xmlMappingFileName);
            var mappingInfo = SoftMart.Core.OfficeUtilities.Excel.ExcelMappingHelper.LoadExcelMappingInfo(xmlFilePath);

            // luu file excel vao thu muc temporary
            string excelFilePath = ConfigUtils.GenTemporaryFilePath(string.Format("{0}{1}", Guid.NewGuid().ToString(), excelFileName));
            System.IO.File.WriteAllBytes(excelFilePath, excelFileContent);

            try
            {
                // doc file excel -> data table
                ExcelDataReader reader = new ExcelDataReader();
                DataTable tableData = reader.Read(excelFilePath, mappingInfo, SoftMart.Wrapper.Office.OpenOffice.ExcelWrapper.Activator);

                // convert data table -> list object
                if (mappingInfo.ListExcelMappingTableInfo == null || mappingInfo.ListExcelMappingTableInfo.Count == 0)
                    return lstResult;

                var mappingInfoTable = mappingInfo.ListExcelMappingTableInfo[0];
                foreach (DataRow row in tableData.Rows)
                {
                    T item = Activator.CreateInstance<T>();
                    SetPropertyValue<T>(ref item, row, mappingInfoTable);
                    lstResult.Add(item);
                }
            }
            finally
            {
                FileUtil.TryDelete(excelFilePath);
            }

            return lstResult;
        }

        public static DataTable ConvertExcelFileBytesToDataTable(byte[] fileBytes)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            DataTable dataTable = new DataTable();

            using (var memoryStream = new System.IO.MemoryStream(fileBytes))
            {
                using (var package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                    if (worksheet != null)
                    {
                        int totalRows = worksheet.Dimension.End.Row;
                        int totalCols = worksheet.Dimension.End.Column;

                        for (int i = 1; i <= totalCols; i++)
                        {
                            var cellValue = worksheet.Cells[1, i].Value?.ToString();
                            dataTable.Columns.Add(string.IsNullOrEmpty(cellValue) ? "Column" + i : cellValue);
                        }

                        for (int row = 2; row <= totalRows; row++)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            for (int col = 1; col <= totalCols; col++)
                            {
                                dataRow[col - 1] = worksheet.Cells[row, col].Value?.ToString();
                            }
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                }
            }

            return dataTable;
        }

        private static void SetPropertyValue<T>(ref T obj, System.Data.DataRow row, ExcelMappingTableInfo tableInfo)
        {
            string errMsg = string.Empty;
            foreach (ExcelCellInfo enCellInfo in tableInfo.ListExcelCellInfo)
            {
                if (!enCellInfo.IsAutoSetProperty)
                {
                    continue;
                }

                try
                {
                    PropertyInfo enProperty = obj.GetType().GetProperty(enCellInfo.PropertyName);
                    if (enProperty != null)
                    {
                        object rowValue = row[enCellInfo.ColHeader];
                        if (rowValue is DBNull)
                        {
                            rowValue = null;
                        }
                        if (rowValue != null)
                        {
                            if (enCellInfo.SqlDbType == SqlDbType.Decimal && !(rowValue is decimal))
                            {
                                enProperty.SetValue(obj, Utility.GetNullableDecimal(rowValue == null ? "" : rowValue.ToString()), null);
                            }
                            else if (enCellInfo.SqlDbType == SqlDbType.DateTime && !(rowValue is DateTime))
                            {
                                enProperty.SetValue(obj, Utility.GetNullableDate(rowValue == null ? "" : rowValue.ToString(), enCellInfo.DateTimeFormat), null);
                            }
                            else if (enCellInfo.SqlDbType == SqlDbType.Int && !(rowValue is decimal))
                            {
                                enProperty.SetValue(obj, Utility.GetNullableInt(rowValue == null ? "" : rowValue.ToString()), null);
                            }
                            else if (enCellInfo.SqlDbType == SqlDbType.NVarChar)
                            {
                                enProperty.SetValue(obj, HtmlUtils.EncodeHtml(rowValue.ToString()), null);
                            }
                            else
                            {
                                enProperty.SetValue(obj, rowValue, null);
                            }
                        }
                    }
                    else
                    {
                        errMsg += string.Format("Không tìm thấy thuộc tính '{0}' trong kiểu '{1}'. ", enCellInfo.PropertyName, obj.GetType());
                    }
                }
                catch (Exception ex)
                {
                    errMsg += string.Format("Quá trình set giá trị cho dữ liệu lỗi. Tên trường : '{0}': '{1}'. ", enCellInfo.PropertyName, ex.ToString());
                }
            }

            if (!string.IsNullOrWhiteSpace(errMsg))
                throw new SMXException(errMsg);
        }
    }
}