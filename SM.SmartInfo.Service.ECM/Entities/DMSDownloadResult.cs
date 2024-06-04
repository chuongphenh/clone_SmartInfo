using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SM.SmartInfo.Service.ECM.Entities
{
    [XmlRoot("ECM")]
    public class DMSDownloadResult
    {
        public string ErrorMessage { get; set; }

        [XmlElement("Document")]
        public DMSDocumentInfo Document { get; set; }
    }
}
