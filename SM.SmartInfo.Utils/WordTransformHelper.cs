using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using SoftMart.Wrapper.Office.OpenOffice;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.Utils
{
    public class WordTransformHelper : IDisposable
    {
        private List<string> _lstTempFile = new List<string>();

        #region Transform data
        public void Transform(WordTransform transform, string templateFilePath, string destFilePath)
        {
            // validate data
            if (transform == null)
                return;

            // copy template ra file dich
            File.Copy(templateFilePath, destFilePath);
            using (var word = new WordWrapper(destFilePath))
            {
                if (transform.ListRange != null)
                {
                    foreach (WordTransformRange range in transform.ListRange)
                    {
                        TransformRange(word, range);
                    }
                }

                if (transform.ListTable != null)
                {
                    foreach (WordTransformTable table in transform.ListTable)
                    {
                        TransformTable(word, table);
                    }
                }
            }
        }

        private void TransformRange(WordWrapper word, WordTransformRange range)
        {
            System.Data.DataTable sourceData = range.DataSourceTable;

            // validate
            if (range.ListItem == null || sourceData == null || sourceData.Rows.Count == 0)
                return;

            // set value
            System.Data.DataRow firstRow = sourceData.Rows[0];
            if (range.AutoFill) // fill toan bo tu source
            {
                foreach (System.Data.DataColumn col in sourceData.Columns)
                {
                    string colName = col.ColumnName;
                    object objVal = firstRow[colName];
                    TransformItem(word, colName, objVal);
                }
            }
            else // fill theo cau hinh
            {
                foreach (WordTransformItem item in range.ListItem)
                {
                    string colName = item.PropertyName;
                    if (!sourceData.Columns.Contains(colName))
                        continue;

                    object objVal = firstRow[colName];
                    TransformItem(word, item.ItemAddress, objVal, item.ImageWidth);
                }
            }
        }

        private void TransformTable(WordWrapper word, WordTransformTable table)
        {
            DataTable srcTable = table.DataSourceTable;
            string tableTitle = table.Name;

            if (srcTable == null || string.IsNullOrWhiteSpace(tableTitle))
                return;

            if (WordTransform.Output_RemoveBlank.Equals(table.ShowCondition, StringComparison.OrdinalIgnoreCase) &&
                srcTable.Rows.Count == 0)
            {
                word.DeleteTable(tableTitle);
                return;
            }

            if (WordTransform.Output_RowRepeater.Equals(table.Type, StringComparison.OrdinalIgnoreCase))
            {
                if (table.RepeatColumnNumber > 1)
                    srcTable = RepeatColumn(srcTable, table.RepeatColumnNumber);
                Dictionary<string, long> dicImageWidth = new Dictionary<string, long>();
                Dictionary<string, long> dicImageHeight = new Dictionary<string, long>();

                word.SetTableValueByTag(tableTitle, srcTable, dicImageWidth, dicImageHeight);
            }
            else
                word.SetTableValue(tableTitle, srcTable, "");

            if (!string.IsNullOrWhiteSpace(table.DeleteBookmark))
            {
                word.DeleteBookmark(table.DeleteBookmark);
            }
        }

        private DataTable RepeatColumn(DataTable srcTable, int repeatColNumber)
        {
            if (repeatColNumber <= 1)
                return srcTable;

            // nhan so cot
            DataTable desTable = new DataTable();
            for (int i = 1; i <= repeatColNumber; i++)
            {
                foreach (DataColumn column in srcTable.Columns)
                {
                    string newColName = string.Format("{0}{1}", column.ColumnName, i);
                    desTable.Columns.Add(newColName, column.DataType);
                }
            }

            // cat row
            int srcTotalRow = srcTable.Rows.Count;
            int desTotalRow = srcTotalRow / repeatColNumber;
            if ((srcTotalRow % repeatColNumber) > 0)
                desTotalRow = desTotalRow + 1;

            for (int desRowIndex = 0; desRowIndex < desTotalRow; desRowIndex++)
            {
                DataRow desRow = desTable.NewRow();
                for (int i = 1; i <= repeatColNumber; i++)
                {
                    int srcRowIndex = desRowIndex * repeatColNumber + (i - 1);
                    if (srcRowIndex >= srcTotalRow)
                        continue;

                    DataRow srcRow = srcTable.Rows[srcRowIndex];
                    foreach (DataColumn column in srcTable.Columns)
                    {
                        string newColName = string.Format("{0}{1}", column.ColumnName, i);
                        desRow[newColName] = srcRow[column];
                    }
                }
                desTable.Rows.Add(desRow);
            }

            return desTable;
        }

        private void TransformItem(WordWrapper word, string bmName, object objVal, int? imageWidth = null)
        {
            if (objVal != null && objVal is byte[])
            {
                string fileName = Guid.NewGuid() + ".png";
                string filePath = ConfigUtils.GenTemporaryFilePath(fileName);

                byte[] result;
                if (imageWidth != null)
                    result = ResizeWidthImage((byte[])objVal, imageWidth.Value);
                else
                    result = (byte[])objVal;

                File.WriteAllBytes(filePath, result);
                word.AddPicture(bmName, filePath);
                _lstTempFile.Add(filePath);
            }
            else
            {
                string strVal = GetObjectStringValue(objVal, TransformItemType.String, string.Empty);
                word.SetBookmarkValue(bmName, strVal);
            }
        }

        private string GetObjectStringValue(object objOriginal, TransformItemType valType, string valFormat)
        {
            if (objOriginal == null)
                return string.Empty;

            switch (valType)
            {
                case TransformItemType.DateTime:
                    DateTime dt;
                    if (objOriginal is DateTime)
                    {
                        dt = (DateTime)objOriginal;
                        return Utility.GetDateString(dt);
                    }
                    else
                    {
                        if (DateTime.TryParseExact(objOriginal.ToString(), valFormat, null,
                            System.Globalization.DateTimeStyles.None, out dt))
                            return Utility.GetDateString(dt);
                        else
                            return string.Empty;
                    }
                case TransformItemType.Decimal:
                    decimal d;
                    if (objOriginal is decimal)
                    {
                        d = (decimal)objOriginal;
                        return Utility.GetString(d);
                    }
                    else
                    {
                        if (decimal.TryParse(objOriginal.ToString(), System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.GetCultureInfo("en-US"), out d))
                            return Utility.GetString(d);
                        else
                            return string.Empty;
                    }
                case TransformItemType.Int:
                    int i;
                    if (objOriginal is int)
                    {
                        i = (int)objOriginal;
                        return Utility.GetString(i);
                    }
                    else
                    {
                        if (int.TryParse(objOriginal.ToString(), out i))
                            return Utility.GetString(i);

                        return string.Empty;
                    }
                default:
                    return objOriginal.ToString();
            }
        }
        #endregion

        #region Image Helper
        private byte[] ResizeWidthImage(byte[] source, int newWidth)
        {
            byte[] result;
            using (var ms = new MemoryStream(source))
            {
                using (var image = Image.FromStream(ms))
                {
                    double ratio = (double)newWidth / (double)image.Width;
                    int newHeight = (int)(image.Height * ratio);

                    var copy = new Bitmap(newWidth, newHeight);
                    copy.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                    using (var grap = Graphics.FromImage(copy))
                    {
                        grap.CompositingMode = CompositingMode.SourceCopy;
                        grap.CompositingQuality = CompositingQuality.HighQuality;
                        grap.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        grap.SmoothingMode = SmoothingMode.HighQuality;
                        grap.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        using (var wrapMode = new ImageAttributes())
                        {
                            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                            grap.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                        }
                    }

                    using (var outStream = new MemoryStream())
                    {
                        copy.Save(outStream, image.RawFormat);
                        result = outStream.ToArray();
                    }
                }
            }
            return result;
        }

        #endregion

        public void Dispose()
        {
            if (_lstTempFile != null)
            {
                foreach (string filePath in _lstTempFile)
                    FileUtil.TryDelete(filePath);
            }
        }
    }
}