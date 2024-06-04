using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using iTextSharp.text.pdf;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.Utils
{
    public class FileUtil
    {
        public static bool IsExcelFile(string fileName, string contentType)
        {
            string ext = "*" + Path.GetExtension(System.Web.HttpUtility.HtmlEncode(fileName)).ToLower();
            if (!SMX.BatchProcessingService.ListImportExcelFile.Contains(ext))
                return false;

            // kiem tra content type (truong hop rename extension)
            if (!"text/plain".Equals(contentType, StringComparison.OrdinalIgnoreCase) &&
                !"application/vnd.ms-excel".Equals(contentType, StringComparison.OrdinalIgnoreCase) &&
                !"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet".Equals(contentType, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static void TryDeleteDirectory(string fullPath)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(fullPath))
                {
                    Directory.Delete(fullPath, true);
                }
            }
            catch (Exception ex)
            {
                LogManager.ServiceLogger.LogError(ex.ToString());
            }
        }

        public static void TryDelete(string fullFilePath)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(fullFilePath))
                {
                    WaitingReadyFile(fullFilePath);
                    File.Delete(fullFilePath);
                }
            }
            catch (Exception ex)
            {
                LogManager.ServiceLogger.LogError(ex.ToString());
            }
        }

        public static byte[] GetBytes(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Waiting until can read the file
        /// LEON's solution
        /// </summary>
        /// <param name="filePath"></param>
        public static void WaitingReadyFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            int tryCount = 0;
            while (true)
            {
                try
                {
                    StreamReader sr = new StreamReader(filePath);
                    sr.Close();
                    break;
                }
                catch { }

                tryCount++;
                if (tryCount > 100)
                    throw new Exception("Access file failed: " + filePath);

                Thread.Sleep(1000);
            }
        }



        /// <summary>
        /// merge more pdf file into one
        /// </summary>
        /// <param name="lstInputFile"></param>
        /// <param name="outputFile"></param>
        public static void MergePDF(List<string> lstInputFile, string outputFile)
        {
            iTextSharp.text.Document document = null;
            iTextSharp.text.pdf.PdfCopy copier = null;
            iTextSharp.text.pdf.PdfImportedPage readerPage = null;

            // initialize output document
            document = new iTextSharp.text.Document();
            copier = new iTextSharp.text.pdf.PdfCopy(document, new FileStream(outputFile, FileMode.Create));
            document.Open();

            // copy data from input to output
            foreach (string inputFile in lstInputFile)
            {
                // get number of pages in input file
                using (iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(inputFile))
                {
                    int pageNumber = reader.NumberOfPages;

                    // copy all pages of input and add into output
                    for (int pageIndex = 1; pageIndex <= pageNumber; pageIndex++)
                    {
                        readerPage = copier.GetImportedPage(reader, pageIndex);
                        copier.AddPage(readerPage);
                    }
                }
            }

            copier.Close();
            document.Close();
        }

        /// <summary>
        /// add watermark into pdf file
        /// </summary>
        /// <param name="pdfFile"></param>
        /// <param name="watermark"></param>
        /// <returns></returns>
        public static byte[] AddPdfWatermark(byte[] fileContent, string watermark)
        {
            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(fileContent);
            iTextSharp.text.pdf.BaseFont baseFont = null;
            try
            {
                string FONT = "c:/windows/fonts/arial.ttf";
                baseFont = iTextSharp.text.pdf.BaseFont.CreateFont(FONT,
                    iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);
            }
            catch
            {
                baseFont = iTextSharp.text.pdf.BaseFont.CreateFont(
                    iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED);
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                iTextSharp.text.pdf.PdfStamper pdfStamper = new iTextSharp.text.pdf.PdfStamper(reader, memoryStream);

                for (int i = 1; i <= reader.NumberOfPages; i++) // Must start at 1 because 0 is not an actual page.
                {
                    iTextSharp.text.Rectangle pageSize = reader.GetPageSizeWithRotation(i);
                    iTextSharp.text.pdf.PdfContentByte pdfPageContents = pdfStamper.GetUnderContent(i);

                    // opacity
                    iTextSharp.text.pdf.PdfGState gstate = new iTextSharp.text.pdf.PdfGState { FillOpacity = 0.1f, StrokeOpacity = 0.3f };
                    pdfPageContents.SaveState();
                    pdfPageContents.SetGState(gstate);

                    pdfPageContents.BeginText(); // Start working with text.

                    pdfPageContents.SetFontAndSize(baseFont, 60); // 40 point font
                    pdfPageContents.SetColorFill(iTextSharp.text.BaseColor.GRAY); // Sets the color of the font, RED in this instance
                    pdfPageContents.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_CENTER, watermark, 300, 400, 30);

                    pdfPageContents.EndText(); // Done working with text

                    pdfPageContents.RestoreState();
                }
                pdfStamper.FormFlattening = true; // enable this if you want the PDF flattened. 
                pdfStamper.Close(); // Always close the stamper or you'll have a 0 byte stream. 

                reader.Close();
                reader.Dispose();

                return memoryStream.ToArray();
            }
        }

        public static byte[] RemovePdfWatermark(byte[] fileContent)
        {
            PdfReader.unethicalreading = true;
            PdfReader reader = new PdfReader(fileContent);
            reader.RemoveUnusedObjects();
            int pageCount = reader.NumberOfPages;
            for (int i = 1; i <= pageCount; i++)
            {
                var page = reader.GetPageN(i);
                PdfDictionary resources = page.GetAsDict(PdfName.RESOURCES);
                PdfDictionary extGStates = resources.GetAsDict(PdfName.EXTGSTATE);
                if (extGStates == null)
                    continue;

                foreach (PdfName name in extGStates.Keys)
                {
                    var obj = extGStates.Get(name);
                    PdfDictionary extGStateObject = (PdfDictionary)PdfReader.GetPdfObject(obj);
                    var stateNumber = extGStateObject.Get(PdfName.ca);
                    if (stateNumber == null)
                        continue;

                    var caNumber = (PdfNumber)PdfReader.GetPdfObject(stateNumber);
                    if (caNumber.FloatValue != 1f)
                    {
                        extGStateObject.Remove(PdfName.ca);

                        extGStateObject.Put(PdfName.ca, new PdfNumber(0f));
                    }
                }
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (PdfStamper pdfStamper = new PdfStamper(reader, memoryStream))
                {
                    pdfStamper.SetFullCompression();
                    pdfStamper.Close();
                }
                return memoryStream.ToArray();
            }

        }
    }
}