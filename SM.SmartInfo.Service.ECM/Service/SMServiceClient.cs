//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Xml.Linq;

//namespace SM.SmartInfo.Service.ECM.Service
//{
//    class SMServiceClient
//    {
//        private string _serviceUrl { get; set; }
//        public SMServiceClient(string serviceUrl)
//        {
//            _serviceUrl = serviceUrl;
//        }

//        public string Execute(string systemName, string methodName, string requestData)
//        {
//            //Prepare data
//            string soapAction = "softmart.net.vn/Execute";
//            string soapXml11 = string.Format(@"
//<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
//  <soap:Body>
//    <Execute xmlns=""softmart.net.vn"">
//      <systemName>{0}</systemName>
//      <methodName>{1}</methodName>
//      <requestData>{2}</requestData>
//    </Execute>
//  </soap:Body>
//</soap:Envelope>
//", HttpUtility.HtmlEncode(systemName), HttpUtility.HtmlEncode(methodName), HttpUtility.HtmlEncode(requestData));

//            //Call service
//            string originalSoapResult = SOAPHelper.CallWebService(_serviceUrl, soapAction, soapXml11);

//            //Get data from SOAP result
//            XDocument xDoc = XDocument.Load(new StringReader(originalSoapResult));
//            var nodeBody = xDoc.Descendants((XNamespace)"http://schemas.xmlsoap.org/soap/envelope/" + "Body").First().FirstNode;
//            var nodeReult = nodeBody.Document.Descendants((XNamespace)"softmart.net.vn" + "ExecuteResult").First().FirstNode;

//            string xmlDataResult = HttpUtility.HtmlDecode(nodeReult.ToString());
//            return xmlDataResult;
//        }
//    }
//}
