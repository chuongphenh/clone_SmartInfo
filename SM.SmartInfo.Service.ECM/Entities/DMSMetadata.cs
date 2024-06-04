using System.Xml.Serialization;

namespace SM.SmartInfo.Service.ECM.Entities
{
    [XmlRoot("Metadata")]
    public class DMSMetadata
    {
        /// <summary>
        /// Tên của thuộc tính khi tải lên
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Giá trị của thuộc tính khi tải lên
        /// </summary>
        public string Value { get; set; }

        public DMSMetadata()
        {
        }

        public DMSMetadata(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
