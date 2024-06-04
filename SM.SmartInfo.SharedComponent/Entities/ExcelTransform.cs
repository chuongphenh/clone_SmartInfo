using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using System.Linq;

namespace SM.SmartInfo.SharedComponent.Entities
{
    #region ExcelTransform
    [XmlRoot("ExcelTransform")]
    public class ExcelTransform
    {
        public string TemplateName { get; set; }
        public bool IsProtected { get; set; }

        [XmlArray("Sheets")]
        [XmlArrayItem("Sheet")]
        public List<ExcelTransformSheet> ListSheet { get; set; }

        public void MapRange2Oject<T>(string sheetName, string rangeName, ref T obj)
        {
            if (ListSheet == null)
                return;

            ExcelTransformSheet sheet = ListSheet.FirstOrDefault(c => string.Equals(c.Name, sheetName));
            if (sheet != null)
                sheet.MapRange2Object<T>(rangeName, ref obj);
        }

        public void MapTable2Oject<T>(string sheetName, string rangeName, ref List<T> obj)
        {
            if (ListSheet == null)
                return;

            ExcelTransformSheet sheet = ListSheet.FirstOrDefault(c => string.Equals(c.Name, sheetName));
            if (sheet != null)
                sheet.MapTable2Object<T>(rangeName, ref obj);
        }
    }
    #endregion

    #region ExcelTransformSheet
    public class ExcelTransformSheet
    {
        public string Name { get; set; }

        [XmlArray("Params")]
        [XmlArrayItem("Param")]
        public List<TransformParam> ListQueryParam { get; set; }

        [XmlArray("Ranges")]
        [XmlArrayItem("Range")]
        public List<ExcelTransformRange> ListRange { get; set; }

        [XmlArray("Tables")]
        [XmlArrayItem("Table")]
        public List<ExcelTransformTable> ListTable { get; set; }

        public void MapRange2Object<T>(string rangeName, ref T obj)
        {
            if (ListRange == null)
                return;

            ExcelTransformRange range = ListRange.FirstOrDefault(c => string.Equals(c.Name, rangeName));
            if (range != null)
                range.Map2Object<T>(ref obj);
        }

        public void MapTable2Object<T>(string rangeName, ref List<T> obj)
        {
            if (ListTable == null)
                return;

            ExcelTransformTable table = ListTable.FirstOrDefault(c => string.Equals(c.RangeName, rangeName));
            if (table != null)
                table.Map2Object<T>(ref obj);
        }
    }
    #endregion

    #region ExcelTransformRange
    public class ExcelTransformRange
    {
        public string QueryData { get; set; }
        public string Name { get; set; }

        [XmlIgnore]
        public DataTable DataSourceTable { get; set; }

        [XmlArray("Cells")]
        [XmlArrayItem("Cell")]
        public List<ExcelTransformCell> ListCell { get; set; }

        public object GetCellValue(string propertyName, out bool isExist)
        {
            isExist = false;
            if (ListCell == null)
                return null;

            ExcelTransformCell cell = ListCell.FirstOrDefault(c => string.Equals(c.PropertyName, propertyName));
            if (cell != null)
            {
                isExist = true;
                return cell.PropertyValue;
            }

            return null;
        }

        public void Map2Object<T>(ref T obj)
        {
            if (ListCell == null || ListCell.Count == 0)
                return;

            System.Reflection.PropertyInfo[] arrPro = obj.GetType().GetProperties();
            foreach (ExcelTransformCell cell in ListCell)
            {
                try
                {
                    System.Reflection.PropertyInfo pro = arrPro.FirstOrDefault(c => string.Equals(c.Name, cell.PropertyName));
                    if (pro != null)
                        pro.SetValue(obj, cell.PropertyValue, null);
                }
                catch (System.Exception ex)
                {
                    throw new System.Exception(string.Format("Map data for [{0}].[{1}] failed.", Name, cell.PropertyName), ex);
                }
            }
        }
    }
    #endregion

    #region ExcelTransformCell
    public class ExcelTransformCell : TransformItemProperty
    {
        public string CellColumn { get; set; }
        public int CellRow { get; set; }
        public string CellName { get; set; }

        [XmlIgnore]
        public string CellAddress
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(CellName))
                    return CellName;

                if (!string.IsNullOrWhiteSpace(CellColumn))
                    return string.Format("{0}{1}", CellColumn, CellRow);

                return PropertyName;
            }
        }
    }
    #endregion

    #region ExcelTransformTable
    public class ExcelTransformTable
    {
        public string RangeName { get; set; }
        public string QueryData { get; set; }
        public int TotalColumn { get; set; } // so cot duoc dinh nghia trong Table o file excel

        [XmlArray("Columns")]
        [XmlArrayItem("Column")]
        public List<TransformTableColumn> ListColumns { get; set; }

        [XmlIgnore]
        public int ColumnCount
        {
            get
            {
                if (TotalColumn == 0 && ListColumns != null) // truong hop khong dinh nghia trong xml se lay trong ket qua truy van
                    return ListColumns.Count;

                return TotalColumn;
            }
        }

        [XmlIgnore]
        public DataTable DataSourceTable { get; set; }

        [XmlIgnore]
        public object[,] DataReader { get; set; }

        [XmlIgnore]
        public int DataReaderRowCount
        {
            get
            {
                if (DataReader == null)
                    return -1;

                return DataReader.GetLength(0);
            }
        }

        [XmlIgnore]
        public int DataReaderColumnCount
        {
            get
            {
                if (DataReader == null)
                    return -1;

                return DataReader.GetLength(1);
            }
        }

        public object GetDataReaderItem(int rowIndex, int columnIndex)
        {
            return DataReader[rowIndex, columnIndex];
        }

        public void Map2Object<T>(ref List<T> lstObj)
        {
            if (lstObj == null)
                lstObj = new List<T>();

            if (ListColumns == null || ListColumns.Count == 0)
                return;

            int rowCount = DataReaderRowCount;
            int colCount = DataReaderColumnCount;
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                T obj = System.Activator.CreateInstance<T>();
                for (int colIndex = 0; colIndex < colCount; colIndex++)
                {
                    TransformTableColumn column = ListColumns.FirstOrDefault(c => c.ColumnIndex == colIndex);
                    if (column == null || string.IsNullOrWhiteSpace(column.PropertyName))
                        continue;
                    try
                    {
                        System.Reflection.PropertyInfo pro = obj.GetType().GetProperty(column.PropertyName);
                        if (pro != null)
                        {
                            object proVal = GetDataReaderItem(rowIndex, colIndex);
                            pro.SetValue(obj, proVal, null);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        throw new System.Exception(string.Format("Map data for [{0}].[{1}].[{2}] failed.", rowIndex, colIndex, column.PropertyName), ex);
                    }
                }
                lstObj.Add(obj);
            }
        }
    }

    public class TransformTableColumn : TransformItemProperty
    {
        public int ColumnIndex { get; set; }
    }
    #endregion

    #region TransformItemProperty
    public class TransformItemProperty
    {
        public TransformItemType PropertyType { get; set; }
        public string PropertyFormat { get; set; }
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
    }
    #endregion

    #region TransformItemType
    public enum TransformItemType
    {
        DateTime,
        Int,
        Decimal,
        String,
        Image
    }
    #endregion

    #region TransformParam
    public class TransformParam
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
    #endregion TransformParam
}
