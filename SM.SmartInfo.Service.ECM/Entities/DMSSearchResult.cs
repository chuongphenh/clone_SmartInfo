using System.Collections.Generic;
using System.Xml.Serialization;

namespace SM.SmartInfo.Service.ECM.Entities
{
    [XmlRoot("ECM")]
    public class DMSSearchResult
    {
        public string ErrorMessage { get; set; }

        [XmlArray("ListDocument")]
        [XmlArrayItem("DocumentInfo")]
        public List<DMSDocumentInfo> ListDocument { get; set; }
    }
}
