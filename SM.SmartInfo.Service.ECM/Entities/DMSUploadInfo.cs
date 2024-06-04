using System.Collections.Generic;
using System.Xml.Serialization;

namespace SM.SmartInfo.Service.ECM.Entities
{
    [XmlRoot("Document")]
    public class DMSUploadInfo
    {
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
        /// Chuỗi base64 của nội dung file
        /// </summary>
        public string FileContent { get; set; }
        /// <summary>
        /// Mô tả sơ bộ về tài liệu
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Người tải lên
        /// </summary>
        public string UploadBy { get; set; }

        [XmlArray("ListMetadata")]
        [XmlArrayItem("Metadata")]
        public List<DMSMetadata> ListMetadata { get; set; }

        public void AddMetadata(string name, string value)
        {
            if (ListMetadata == null)
                ListMetadata = new List<DMSMetadata>();

            ListMetadata.Add(new DMSMetadata() { Name = name, Value = value });
        }
    }
}
