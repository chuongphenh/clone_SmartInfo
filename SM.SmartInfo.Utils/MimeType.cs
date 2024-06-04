using System.IO;
using System.Linq;

namespace SM.SmartInfo.Utils
{
    public class MimeType
    {
        //Document
        private static readonly byte[] OFFICE_FILES = { 208, 207, 17, 224, 161, 177, 26, 225 };
        private static readonly byte[] OFFICEX_FILES = { 80, 75, 3, 4 };
        private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
        //Image
        private static readonly byte[] GIF = { 71, 73, 70, 56 };
        private static readonly byte[] ICO = { 0, 0, 1, 0 };
        private static readonly byte[] JPG = { 255, 216, 255 };
        private static readonly byte[] BMP = { 66, 77 };
        private static readonly byte[] PNG = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
        public static string GetMimeType(byte[] file, string fileName)
        {
            string mime = string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
                return mime;
            //Get the file extension
            string extension = Path.GetExtension(fileName) == null ? string.Empty : Path.GetExtension(fileName).ToUpper();
            if (file.Take(4).SequenceEqual(OFFICEX_FILES))
            {
                switch (extension)
                {
                    case ".DOCX":
                        return mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    case ".XLSX":
                        return mime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    case ".PPTX":
                        return mime = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                }
            }
            if (file.Take(8).SequenceEqual(OFFICE_FILES))
            {
                switch (extension)
                {
                    case ".DOC":
                        return mime = "application/msword";
                    case ".XLS":
                        return mime = "application/excel";
                    case ".PPT":
                        return mime = "application/mspowerpoint";
                }

            }
            if (file.Take(2).SequenceEqual(BMP))
                return mime = "image/bmp";
            if (file.Take(4).SequenceEqual(GIF))
                return mime = "image/gif";
            if (file.Take(4).SequenceEqual(ICO))
                return mime = "image/x-icon";
            if (file.Take(3).SequenceEqual(JPG))
                return mime = "image/jpeg";
            if (file.Take(7).SequenceEqual(PDF))
                return mime = "application/pdf";
            if (file.Take(16).SequenceEqual(PNG))
                return mime = "image/png";
            return mime;
        }
    }
}
