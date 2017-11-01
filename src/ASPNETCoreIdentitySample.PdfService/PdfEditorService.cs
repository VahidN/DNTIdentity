using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using DNTPersianUtils.Core;
using ASPNETCoreIdentitySample.PdfService.Helpers;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using ASPNETCoreIdentitySample.Common.StringToolKit;
using ASPNETCoreIdentitySample.BaseService.Contracts;

namespace ASPNETCoreIdentitySample.PdfService
{
    public class PdfEditorService : IPdfEditorService
    {
        #region #Setting
        private static readonly object Lock = new object();
        private readonly IOptions<SiteSettings> _settings;
        private readonly IBaseFileService _baseFileService;
        private readonly IFileInsertService _fileInsertService;
        private readonly IHostingEnvironment _hostingEnvironment;


        public PdfEditorService(IOptions<SiteSettings> settings,
            IBaseFileService baseFileService,
            IFileInsertService fileInsertService,
            IHostingEnvironment hostingEnvironment)
        {
            _settings = settings;
            _baseFileService = baseFileService;
            _fileInsertService = fileInsertService;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region #Helpers
        private void AddSpecialtextCell(PdfPTable table, PdfPCell cell)
        {
            cell.Colspan = 3;
            cell.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
            cell.PaddingLeft = 530f;
            cell.PaddingTop = 6f;
            cell.Border = 0;
            cell.Rotation = 90;
            table.WidthPercentage = 100f;
            table.AddCell(cell);
        }
        private static void AddImageInCell(PdfPCell cell, iTextSharp.text.Image image, float fitWidth, float fitHight, int Alignment)
        {
            image.ScaleToFit(fitWidth, fitHight);
            image.Alignment = Alignment;
            cell.AddElement(image);
        }
        private void AddtextCell(PdfPTable table, PdfPCell cell)
        {
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.Border = 0;
            table.AddCell(cell);
        }
        private void AddtextCell(PdfPTable table, PdfPCell cell, float paddingLeft, float paddingRight)
        {
            cell.Colspan = 3;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            cell.PaddingLeft = paddingLeft;
            cell.PaddingRight = paddingRight;
            cell.Border = 0;
            table.AddCell(cell);
        }
        private void AddtextCell(PdfPTable table, PdfPCell cell, float paddingLeft, float paddingRight, int hAlign)
        {
            cell.Colspan = 3;
            cell.HorizontalAlignment = hAlign; //0=Left, 1=Centre, 2=Right
            cell.PaddingLeft = paddingLeft;
            cell.PaddingRight = paddingRight;
            cell.Border = 0;
            table.AddCell(cell);
        }
        private static void AddtextCell(PdfPTable table, PdfPCell cell, int Colspan, int HorizontalAlignment, int Border)
        {
            cell.Colspan = Colspan;
            cell.HorizontalAlignment = HorizontalAlignment; //0=Left, 1=Centre, 2=Right
            cell.Border = Border;
            table.AddCell(cell);
        }

        private System.Drawing.Image DrawTextImage(String currencyCode, System.Drawing.Font font, Color textColor, Color backColor)
        {
            return DrawTextImage(currencyCode, font, textColor, backColor, Size.Empty);
        }
        private System.Drawing.Image DrawTextImage(String currencyCode, System.Drawing.Font font, Color textColor, Color backColor, Size minSize)
        {
            //first, create a dummy bitmap just to get a graphics object
            SizeF textSize;
            using (System.Drawing.Image img = new Bitmap(1, 1))
            {
                using (Graphics drawing = Graphics.FromImage(img))
                {
                    //measure the string to see how big the image needs to be
                    textSize = drawing.MeasureString(currencyCode, font);
                    if (!minSize.IsEmpty)
                    {
                        textSize.Width = textSize.Width > minSize.Width ? textSize.Width : minSize.Width;
                        textSize.Height = textSize.Height > minSize.Height ? textSize.Height : minSize.Height;
                    }
                }
            }

            //create a new image of the right size
            System.Drawing.Image retImg = new Bitmap((int)textSize.Width, (int)textSize.Height);
            using (var drawing = Graphics.FromImage(retImg))
            {
                //paint the background
                drawing.Clear(backColor);
                drawing.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                drawing.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //create a brush for the text
                using (Brush textBrush = new SolidBrush(textColor))
                {
                    drawing.DrawString(currencyCode, font, textBrush, 0, 0);
                    drawing.Save();
                }
            }
            return retImg;
        }
        private Tuple<bool, string> AddImagesToExistPdfAndReplaceItNewPdfOnOldPdf(string oldPdfFileAddressAndName, List<string> imageList, float xCoordinates, float yCoordinates)
        {
            try
            {
                var fileLocation = oldPdfFileAddressAndName;
                RemoveBlankPdfPages(oldPdfFileAddressAndName);
                var pdfReader = new PdfReader(fileLocation);
                using (var tempFile = new FileStream(fileLocation.Replace(".pdf", "[temp][file].pdf"), FileMode.Create))
                {
                    var stamper = new PdfStamper(pdfReader, tempFile);

                    //img.SetAbsolutePosition(250, 300); // set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                    //ImageFormat format = image.PixelFormat == PixelFormat.Format1bppIndexed
                    //                     || image.PixelFormat == PixelFormat.Format4bppIndexed
                    //                     || image.PixelFormat == PixelFormat.Format8bppIndexed
                    //                         ? ImageFormat.Tiff
                    //                         : ImageFormat.Jpeg;
                    Single oldcurrentxCoordinates = 0f;
                    foreach (var img in imageList)
                    {
                            var image = iTextSharp.text.Image.GetInstance(img);
                            var currentxCoordinates = xCoordinates + (Math.Abs(oldcurrentxCoordinates) <= 0f ? 0f : 103f);
                            image.ScaleToFit(100f, 100f);
                            image.SetAbsolutePosition(currentxCoordinates, yCoordinates);
                            for (var page = 1; page <= pdfReader.NumberOfPages; page++)
                            {
                                stamper.GetOverContent(page).AddImage(image);
                            }
                            oldcurrentxCoordinates = currentxCoordinates;
                    }

                    stamper.Close();
                }



                pdfReader.Close();

                //// now delete the original file and rename the temp file to the original file
                lock (Lock)
                {
                    File.Delete(fileLocation);
                    File.Move(fileLocation.Replace(".pdf", "[temp][file].pdf"), fileLocation);
                }
                return new Tuple<bool, string>(true, "با موفقیت انجام شد");

            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, "خطای سیستمی عمق دو: در ثبت تصویر در فایل پی دی اف بوجود آمده است");
                throw ex;
            }

        }

        private Tuple<string, Guid, string> AddImagesToExistPdf(List<string> imageList, float xCoordinates, float yCoordinates, string originalFileAddressAndName)
        {
            try
            {
                var baseFilePath = _settings.Value.ServerFilesRootPath;
                RemoveBlankPdfPages(originalFileAddressAndName);
                
                using (Stream inputPdfStream = new FileStream(originalFileAddressAndName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var outputPdfTuple = _fileInsertService.InsertStream(inputPdfStream, ".pdf");
                    var outputPdfTuplepath = Path.Combine(_hostingEnvironment.WebRootPath, baseFilePath, outputPdfTuple.Item3);
                    using (Stream outputPdfStream = new FileStream(outputPdfTuplepath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        var pdfReader = new PdfReader(originalFileAddressAndName);
                        var stamper = new PdfStamper(pdfReader, outputPdfStream);
                        
                        var i = 1;
                        foreach (var img in imageList)
                        {
                            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(img);
                            var currentxCoordinates = xCoordinates * i;
                            image.ScaleToFit(100f, 100f);
                            image.SetAbsolutePosition(currentxCoordinates, yCoordinates);
                            for (var page = 1; page <= pdfReader.NumberOfPages; page++)
                            {
                                var pdfContentByte = stamper.GetOverContent(page);
                                pdfContentByte.AddImage(image);
                            }
                            i++;
                        }

                        stamper.Close();
                        pdfReader.Close();
                    }
                    return outputPdfTuple;
                }
            }
            catch (Exception ex)
            {
                return new Tuple<string, Guid, string>(string.Empty, Guid.Empty, string.Empty);
                throw;
            }

        }


        private static void RemoveBlankPdfPages(string pdfSourceFile)
        {
            
                var pdfReader = new PdfReader(pdfSourceFile);
                var raf = new RandomAccessFileOrArray(pdfSourceFile);

                var document = new Document(pdfReader.GetPageSizeWithRotation(1));
                var ss = new FileStream(pdfSourceFile.Replace(".pdf", "[temp][file].pdf"), FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                var writer = new PdfCopy(document, ss);
                document.Open();
                const int blankThreshold = 1000;
                for (var i = 1; i <= pdfReader.NumberOfPages; i++)
                {
                    using (var bs = new MemoryStream())
                    {

                        //var extractedText = PdfTextExtractor.GetTextFromPage(pdfReader, i, new LocationTextExtractionStrategy());
                        //var extractedTextClean = CleanInput(extractedText);

                        //var pg = pdfReader.GetPageN(i);

                        //// recursively search pages, forms and groups for images.
                        //var obj = FindImageInPdfDictionary(pg);

                        //bs.Write(bContent, 0, bContent.Length);

                        //if ((!string.IsNullOrEmpty(extractedTextClean) && !string.IsNullOrWhiteSpace(extractedTextClean)) || (obj != null))
                        //{
                        //    var page = writer.GetImportedPage(pdfReader, i);
                        //    writer.AddPage(page);
                        //}
                        //bs.Dispose();


                        var pageDict = pdfReader.GetPageN(i);
                        var resDict = (PdfDictionary)pageDict.Get(PdfName.Resources);
                        var detectBlank = false;

                        var hasFont = resDict.Get(PdfName.Font) != null;
                        if (hasFont)
                        {
                            Console.WriteLine($"Page {i} has font(s).");
                            detectBlank = false;
                        }
                        else
                        {
                            var hasImage = resDict.Get(PdfName.Xobject) != null;
                            if (hasImage)
                            {
                                Console.WriteLine($"Page {i} has image(s).");
                                detectBlank = false;
                            }
                            else
                            {
                                detectBlank = true;
                            }
                        }



                        var bContent = pdfReader.GetPageContent(i, raf);
                        if (bContent.Length <= blankThreshold)
                        {
                            Console.WriteLine($"Page {i} is blank");
                            detectBlank = true;
                        }

                        if (detectBlank) continue;

                        bs.Write(bContent, 0, bContent.Length);
                        var page = writer.GetImportedPage(pdfReader, i);
                        writer.AddPage(page);

                    }

                }
                document.Close();
                ss.Dispose();
                writer.Close();

                raf.Close();
                pdfReader.Close();


            lock (Lock)
            {
                File.Delete(pdfSourceFile);
                File.Move(pdfSourceFile.Replace(".pdf", "[temp][file].pdf"), pdfSourceFile);
            }
        }
        /// <summary>
        /// recursively search pages, forms and groups for images.
        /// </summary>
        /// <param name="pg">PdfDictionary</param>
        /// <returns>object</returns>
        private static PdfObject FindImageInPdfDictionary(PdfDictionary pg)
        {
            PdfDictionary res = (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.Resources));


            PdfDictionary xobj =
              (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.Xobject));
            if (xobj != null)
            {
                foreach (PdfName name in xobj.Keys)
                {

                    PdfObject obj = xobj.Get(name);
                    if (obj.IsIndirect())
                    {
                        PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);

                        PdfName type =
                          (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.Subtype));

                        //image at the root of the pdf
                        if (PdfName.Image.Equals(type))
                        {
                            return obj;
                        }// image inside a form
                        else if (PdfName.Form.Equals(type))
                        {
                            return FindImageInPdfDictionary(tg);
                        } //image inside a group
                        else if (PdfName.Group.Equals(type))
                        {
                            return FindImageInPdfDictionary(tg);
                        }

                    }
                }
            }

            return null;

        }

        private static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            return Regex.Replace(strIn, @"[^\w\.@-]", "");
        }
        #endregion

        public string GenerateReport()
        {

            var baseFilePath = _settings.Value.ServerFilesRootPath;
            var filePdf = $"Report{StringUtil.GeneratetrackId(10)}[Temp].pdf";

            var outputPdfTuplepath = Path.Combine(_hostingEnvironment.WebRootPath, baseFilePath, filePdf);
            using (Stream outputPdfStream = new FileStream(outputPdfTuplepath, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 55f, 55f);
                var writer = PdfWriter.GetInstance(pdfDoc, outputPdfStream);
                writer.PageEvent = new PageEvents(_settings, _hostingEnvironment);

                pdfDoc.Open();

                var fontPath = Path.Combine(_hostingEnvironment.WebRootPath, "fonts", _settings.Value.PdfServiceFontName);
                if (!File.Exists(fontPath))
                    fontPath = Environment.GetEnvironmentVariable("SystemRoot") + $"\\fonts\\tahoma.ttf";

                var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                iTextSharp.text.Rectangle page = pdfDoc.PageSize;
                iTextSharp.text.Font VerdanaBoldRed = FontFactory.GetFont("Arial", 17F, iTextSharp.text.Font.BOLD, new BaseColor(204, 0, 51));
                iTextSharp.text.Font TahomaBold = FontFactory.GetFont("Tahoma", 11F, iTextSharp.text.Font.BOLD, BaseColor.Black);
                iTextSharp.text.Font TahomaNorm = FontFactory.GetFont("Tahoma", 11F, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                iTextSharp.text.Font TahomaNorm10 = FontFactory.GetFont("Tahoma", 10F, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                iTextSharp.text.Font TahomaNorm8 = FontFactory.GetFont("Tahoma", 8F, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                iTextSharp.text.Font TahomaNorm9 = FontFactory.GetFont("Tahoma", 9F, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                iTextSharp.text.Font TahomaNorm9Bold = FontFactory.GetFont("Tahoma", 9F, iTextSharp.text.Font.BOLD, BaseColor.Black);


                var myFont = new iTextSharp.text.Font(baseFont, 9F, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                var myFontB = new iTextSharp.text.Font(baseFont, 9F, iTextSharp.text.Font.BOLD, BaseColor.Black);

                pdfDoc.NewPage();
                var title = new Paragraph("Hello World!", myFont);
                pdfDoc.Add(title);


                pdfDoc.Close();
                writer.CompressionLevel = PdfStream.BEST_COMPRESSION;
                writer.Close();
            }


            RemoveBlankPdfPages(outputPdfTuplepath);


            return outputPdfTuplepath;
        }
    }
}