using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.CSharp;

namespace SM.SmartInfo.Service.ECM.Service
{
    static class SOAPHelper
    {
        /// <summary>
        /// execute with Generated proxy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <param name="protocolName"></param>
        /// <returns></returns>
        public static T Execute<T>(string service, string methodName, object[] args, string protocolName = "SOAP12") where T : class
        {
            if (!service.ToLower().EndsWith("?wsdl"))
                service = service + "?wsdl";

            // log ben ngoai
            //Utils.LogManager.ServiceECM.LogDebug("ServiceLink: " + service);
            //Utils.LogManager.ServiceECM.LogDebug("Method: " + methodName);

            // get wsdl content
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(service);

            ServiceDescription description = ServiceDescription.Read(stream);
            ServiceDescriptionImporter descriptionImporter = new ServiceDescriptionImporter();
            descriptionImporter.ProtocolName = protocolName;
            descriptionImporter.AddServiceDescription(description, null, null);
            descriptionImporter.Style = ServiceDescriptionImportStyle.Client;
            descriptionImporter.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;
            CodeNamespace codeNamespace = new CodeNamespace();
            CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
            codeCompileUnit.Namespaces.Add(codeNamespace);
            ServiceDescriptionImportWarnings serviceDescriptionImportWarnings =
            descriptionImporter.Import(codeNamespace, codeCompileUnit);

            if (serviceDescriptionImportWarnings == ServiceDescriptionImportWarnings.NoCodeGenerated)
                throw new Exception("Generated proxy failed.");

            CodeDomProvider compiler = new CSharpCodeProvider();
            string[] references = new[] { "System.Web.Services.dll", "System.Xml.dll" };
            CompilerParameters parameters = new CompilerParameters(references);
            CompilerResults results = compiler.CompileAssemblyFromDom(parameters, codeCompileUnit);

            Type[] assemblyTypes = results.CompiledAssembly.GetExportedTypes();
            object webServiceProxy = Activator.CreateInstance(assemblyTypes[0]);

            MethodInfo methodInfo = webServiceProxy.GetType().GetMethod(methodName);
            var result = methodInfo.Invoke(webServiceProxy, args);

            return result as T;
        }

        public static string Download(string url, string fileName)
        {
            Utils.LogManager.ServiceECM.LogDebug(string.Format("Download with URL = [{0}], FileName = [{1}]", url, fileName));

            // generate file path
            string normalizeFileName = System.Text.RegularExpressions.Regex.Replace(fileName, "[^a-zA-Z0-9_.]+", "_", 
                System.Text.RegularExpressions.RegexOptions.Compiled);
            string filePath = string.Format("{0}_{1}", Guid.NewGuid().ToString(), normalizeFileName);
            filePath = Path.Combine(Utils.ConfigUtils.TemporaryFolder, filePath);

            // download file
            WebClient client = new WebClient();
            client.DownloadFile(url, filePath);

            // get content of file and delete
            byte[] fileContent = File.ReadAllBytes(filePath);
            string fileContent64 = Convert.ToBase64String(fileContent);
            Utils.LogManager.ServiceECM.LogDebug(string.Format("Length of [{0}] = [{1}]", fileName, fileContent.Length));
            Utils.FileUtil.TryDelete(filePath);

            return fileContent64;
        }

        #region call direct
        /// <summary>
        /// execute with HttpWebRequest
        /// </summary>
        /// <param name="serviceLink"></param>
        /// <param name="templateFile"></param>
        /// <param name="methodName"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static string Execute(string serviceLink, string methodName, string arg)
        {
            Utils.LogManager.ServiceECM.LogDebug("Service link: " + serviceLink);
            Utils.LogManager.ServiceECM.LogDebug("Method name: " + methodName);
            //Utils.LogManager.ServiceECM.LogDebug("Arg: " + arg);

            string soapXml11 = arg;
            string originalSoapResult = CallWebService(serviceLink, methodName, soapXml11);
            originalSoapResult = originalSoapResult.Replace(" xsi:nil=\"true\"", string.Empty);
            Utils.LogManager.ServiceECM.LogDebug("Result: " + originalSoapResult);

            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            xDoc.LoadXml(originalSoapResult);
            System.Xml.XmlNodeList xNode = xDoc.GetElementsByTagName("soapenv:Body");
            string xmlDataResult = xDoc.InnerXml;
            if (xNode != null && xNode.Count > 0)
                xmlDataResult = xNode[0].InnerXml;

            xmlDataResult = System.Web.HttpUtility.HtmlDecode(xmlDataResult);
            return xmlDataResult;
        }

        private static string CallWebService(string url, string action, string soapxml)
        {
            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(soapxml);
            HttpWebRequest webRequest = CreateWebRequest(url, action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // get the response from the completed web request.
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    return soapResult;
                }
            }
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            if (!string.IsNullOrWhiteSpace(action))
                webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string soapxml)
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(soapxml);
            return soapEnvelop;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
        #endregion
    }
}
