using System.Data;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SM.SmartInfo.SharedComponent.Entities
{
    #region WordTransform
    [XmlRoot("WordTransform")]
    public class WordTransform
    {
        public const string Output_SPCode = "fkcode";
        public const string Output_SPName = "fkname";
        public const string Output_Dic = "fkdic";
        public const string Output_Remove = "remove";
        public const string Output_RemoveBlank = "removeblank";

        /// <summary>
        /// dung cho truong hop muon lap lai mot tap cac row (repeat)
        /// </summary>
        public const string Output_RowRepeater = "rowrepeater";
        /// <summary>
        /// Sinh cột động theo cấu trúc table. Lấy tên cột làm tiêu đề
        /// </summary>
        public const string Table_Struct = "table_struct";

        public string TemplateName { get; set; }
        public WordTransformExportType ExportType { get; set; }

        [XmlArray("Params")]
        [XmlArrayItem("Param")]
        public List<TransformParam> ListQueryParam { get; set; }

        [XmlArray("Ranges")]
        [XmlArrayItem("Range")]
        public List<WordTransformRange> ListRange { get; set; }

        [XmlArray("Tables")]
        [XmlArrayItem("Table")]
        public List<WordTransformTable> ListTable { get; set; }
    }
    #endregion

    #region WordTransformRange
    public class WordTransformRange
    {
        [XmlArray("Outputs")]
        [XmlArrayItem("Output")]
        public List<WordTransformOutput> ListOutput { get; set; }
        public string QueryData { get; set; }
        public string Name { get; set; }
        public bool AutoFill { get; set; }

        [XmlIgnore]
        public DataTable DataSourceTable { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<WordTransformItem> ListItem { get; set; }

        public object GetCellValue(string propertyName, out bool isExist)
        {
            isExist = false;
            if (ListItem == null)
                return null;

            WordTransformItem item = ListItem.FirstOrDefault(c => string.Equals(c.PropertyName, propertyName));
            if (item != null)
            {
                isExist = true;
                return item.PropertyValue;
            }

            return null;
        }
    }
    #endregion

    #region WordTransformCell
    public class WordTransformItem : TransformItemProperty
    {
        public string ItemName { get; set; }
        public int ImageWidth { get; set; }

        [XmlIgnore]
        public string ItemAddress
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ItemName))
                    return PropertyName;

                return ItemName;
            }
        }
    }
    #endregion

    #region WordTransformTable
    public class WordTransformTable
    {
        public string Name { get; set; } // table Title/Caption in docx file
        public string Type { get; set; } // rowrepeater hoac blank
        /// <summary>
        /// Set độ rộng của các cột bất kỳ theo thứ tự.
        /// Ex: "10,,,8" => set độ rộng của cột 1 và 4
        /// </summary>
        public string ColumnsWidth { get; set; }
        /// <summary>
        /// Tên bookmark sẽ xóa sau khi đẩy dữ liệu vào bảng.
        /// Mục đích:
        /// Trường hợp bảng phức tạp (Có merge header, Header nhiều dòng...) sẽ phải tách thành 2.
        /// Khi đẩy dữ liệu thì chỉ đẩy vào bảng 2.
        /// Sau đó, để 2 bảng sẽ nhập lại thành 1
        /// </summary>
        public string DeleteBookmark { get; set; }
        public string ShowCondition { get; set; } // removeblank
        public int RepeatColumnNumber { get; set; } // truong hop muon chia table theo cot (phu luc hinh anh). So lan lap cot

        [XmlArray("Outputs")]
        [XmlArrayItem("Output")]
        public List<WordTransformOutput> ListOutput { get; set; }
        public string RangeName { get; set; }
        public int? TableIndex { get; set; } // start from 1
        public string QueryData { get; set; }
        public bool RemoveIfNoData { get; set; }
        public TableTransformType TransformType { get; set; }

        [XmlIgnore]
        public DataTable DataSourceTable { get; set; }
    }

    public class WordTransformOutput
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("ColumnName")]
        public string ColumnName { get; set; }

        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("Param")]
        public string Param { get; set; }
    }
    #endregion

    #region WordTransformExportType
    public enum WordTransformExportType
    {
        Word,
        Pdf,
    }

    public enum TableTransformType
    {
        Data,
        DataAndStruct
    }
    #endregion
}