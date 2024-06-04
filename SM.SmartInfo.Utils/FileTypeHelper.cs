
namespace SM.SmartInfo.Utils
{
    public static class FileTypeHelper
    {
        public static bool IsImage(byte[] fileContent)
        {
            try
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream(fileContent))
                {
                    System.Drawing.Image.FromStream(stream);
                }
                return true;
            }
            catch { }

            return false;

            //List<string> lstContentType = new List<string>()
            //{
            //    "image/jpeg",
            //    "image/jpg",
            //    "image/png",
            //    "image/bmp",
            //    "image/gif",
            //};

            //return lstContentType.Exists(c => c == contentType);
        }

        public static bool IsPdf(byte[] fileContent)
        {
            try
            {
                iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(fileContent);

                return true;
            }
            catch { }

            return false;

            //List<string> lstContentType = new List<string>()
            //{
            //    "application/pdf",
            //};

            //return lstContentType.Exists(c => c == contentType);
        }

        public static bool IsDocx(byte[] fileContent)
        {
            try
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream(fileContent))
                {
                    var package = System.IO.Packaging.Package.Open(stream);
                    package.Close();
                }

                return true;
            }
            catch { }

            return false;
        }

        public static bool IsXlsx(byte[] fileContent)
        {
            try
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream(fileContent))
                {
                    var doc = DocumentFormat.OpenXml.Packaging.SpreadsheetDocument.Open(stream, false);
                    doc.Close();
                }

                return true;
            }
            catch { }

            return false;
        }
    }
}
