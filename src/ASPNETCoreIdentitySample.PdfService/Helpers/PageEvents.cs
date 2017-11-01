using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASPNETCoreIdentitySample.PdfService.Helpers
{
    public class PageEvents : PdfPageEventHelper
    {
        PdfTemplate _backgroundImageTemplate;
        private readonly IOptions<SiteSettings> _settings;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PageEvents(IOptions<SiteSettings> settings,
            IHostingEnvironment hostingEnvironment)
        {
            _settings = settings;
            _hostingEnvironment = hostingEnvironment;
        }


        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
            writer.DirectContent.AddTemplate(_backgroundImageTemplate, 0, 0);
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            _backgroundImageTemplate = writer.DirectContent.CreateTemplate(document.PageSize.Width, document.PageSize.Height);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            var pdfBackgroundImage = _settings.Value.PdfServiceBackgroundImage;
            if(!string.IsNullOrEmpty(pdfBackgroundImage))
            {
                var pdfBackgroundImageFileAddress = Path.Combine(_hostingEnvironment.WebRootPath, "images", pdfBackgroundImage);

                if (File.Exists(pdfBackgroundImageFileAddress))
                {
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(pdfBackgroundImageFileAddress);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;
                    var width = (document.PageSize.Width - img.Width) / 2;
                    var height = (document.PageSize.Height - img.Height) / 2;
                    img.SetAbsolutePosition(width, height);

                    _backgroundImageTemplate.AddImage(img);
                }
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            var content = writer.DirectContent;
            var pageBorderRect = new Rectangle(document.PageSize);

            pageBorderRect.Left += document.LeftMargin;
            pageBorderRect.Right -= document.RightMargin;
            pageBorderRect.Top -= document.TopMargin;
            pageBorderRect.Bottom += document.BottomMargin;

            content.SetColorStroke(BaseColor.Black);
            content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
            content.Stroke();
        }
    }
}
