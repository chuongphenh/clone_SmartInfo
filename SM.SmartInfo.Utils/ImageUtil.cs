using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace SM.SmartInfo.Utils
{
    public class ImageUtil
    {
        public static bool ResizeImage(byte[] inputByte, string outputFile, int width, int height)
        {
            if (inputByte == null || inputByte.Length == 0)
            {
                return false;
            }

            string dir = outputFile.Substring(0, outputFile.LastIndexOf('\\'));

            if (!Directory.Exists(dir))
            {
                try
                {
                    Directory.CreateDirectory(dir);
                }
                catch (Exception ex)
                {
                    LogManager.WebLogger.LogError("Create folder fail: " + dir, ex);
                }
            }

            Image inputImg;
            using (var ms = new MemoryStream(inputByte))
            {
                inputImg = Image.FromStream(ms);
            }

            //Image inputImg = Image.FromFile(inputFile);

            int actualWidth;
            int actualHeight;

            if (width / inputImg.Width <= height / inputImg.Height)
            {
                actualWidth = width;
                actualHeight = (int)(((float)width / inputImg.Width) * inputImg.Height);
            }
            else
            {
                actualWidth = (int)(((float)height / inputImg.Height) * inputImg.Width);
                actualHeight = height;
            }

            string extension = outputFile.Substring(outputFile.LastIndexOf('.'));

            Image outputImg = new Bitmap(actualWidth, actualHeight);
            try
            {
                using (Graphics g = Graphics.FromImage(outputImg))
                {
                    // Draw image
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.DrawImage(inputImg, 0, 0, actualWidth, actualHeight);

                    // Save image
                    ImageCodecInfo jpegCodec;
                    switch (extension.ToLower())
                    {
                        case ".png":
                            jpegCodec = ImageCodecInfo.GetImageEncoders().First(item => item.MimeType == "image/png");
                            break;

                        default:
                            jpegCodec = ImageCodecInfo.GetImageEncoders().First(item => item.MimeType == "image/jpeg");
                            break;
                    }

                    EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                    EncoderParameters encoderParams = new EncoderParameters();
                    encoderParams.Param[0] = qualityParam;
                    outputImg.Save(outputFile, jpegCodec, encoderParams);

                    g.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {
                LogManager.WebLogger.LogError("Resize image fail: " + outputFile, ex);
            }
            finally
            {
                outputImg.Dispose();
                inputImg.Dispose();
            }

            return false;
        }
    }
}
