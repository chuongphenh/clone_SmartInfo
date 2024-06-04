
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SM.SmartInfo.Service.ECM.Entities
{
    [XmlRoot("Document")]
    public class DMSSearchInfo
    {
        /// <summary>
        /// ID của tài liệu trên ECM
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Tên của tài liệu khi upload
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Loại tài liệu khi upload
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Tên file của tài liệu
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Loại file
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// Mô tả sơ bộ về tài liệu
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Hệ thống tải lên
        /// </summary>
        public string UploadFrom { get; set; }
        /// <summary>
        /// Người tải lên
        /// </summary>
        public string UploadBy { get; set; }
        /// <summary>
        /// Thời gian tải lên
        /// </summary>
        public string UploadDTG { get; set; }

        [XmlArray("ListMetadata")]
        [XmlArrayItem("Metadata")]
        public List<DMSMetadata> ListMetadata { get; set; }

        public void AddMetadata(string name, string value)
        {
            if (ListMetadata == null)
                ListMetadata = new List<DMSMetadata>();

            ListMetadata.Add(new DMSMetadata(name, value));
        }

        public string GetMetadata(string name)
        {
            if (ListMetadata == null)
                return null;

            DMSMetadata metadata = ListMetadata.FirstOrDefault(c => string.Equals(c.Name, name, System.StringComparison.OrdinalIgnoreCase));
            return metadata == null ? null : metadata.Value;
        }
    }
}
