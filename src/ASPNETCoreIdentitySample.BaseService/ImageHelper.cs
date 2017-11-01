using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ASPNETCoreIdentitySample.BaseService
{
    public static class ImageHelper
    {
        public static ImageFormat FindImageFormat(System.Drawing.Image img)
        {
            if (ImageFormat.Jpeg.Equals(img.RawFormat))
            {
                return ImageFormat.Jpeg;
            }
            else if (ImageFormat.Bmp.Equals(img.RawFormat))
            {
                return ImageFormat.Bmp;
            }
            else if (ImageFormat.Emf.Equals(img.RawFormat))
            {
                return ImageFormat.Emf;
            }
            else if (ImageFormat.Exif.Equals(img.RawFormat))
            {
                return ImageFormat.Exif;
            }
            else if (ImageFormat.Gif.Equals(img.RawFormat))
            {
                return ImageFormat.Gif;
            }
            else if (ImageFormat.Icon.Equals(img.RawFormat))
            {
                return ImageFormat.Icon;
            }
            else if (ImageFormat.MemoryBmp.Equals(img.RawFormat))
            {
                return ImageFormat.MemoryBmp;
            }
            else if (ImageFormat.Png.Equals(img.RawFormat))
            {
                return ImageFormat.Png;
            }
            else if (ImageFormat.Tiff.Equals(img.RawFormat))
            {
                return ImageFormat.Tiff;
            }
            else if (ImageFormat.Wmf.Equals(img.RawFormat))
            {
                return ImageFormat.Wmf;
            }
            else
            {
                return ImageFormat.Jpeg;
            }
        }

        public static ImageFormat FindImageFormat(this byte[] imageFile)
        {
            var img = Image.FromStream(new MemoryStream(imageFile));
            if (ImageFormat.Jpeg.Equals(img.RawFormat))
            {
                return ImageFormat.Jpeg;
            }
            else if (ImageFormat.Bmp.Equals(img.RawFormat))
            {
                return ImageFormat.Bmp;
            }
            else if (ImageFormat.Emf.Equals(img.RawFormat))
            {
                return ImageFormat.Emf;
            }
            else if (ImageFormat.Exif.Equals(img.RawFormat))
            {
                return ImageFormat.Exif;
            }
            else if (ImageFormat.Gif.Equals(img.RawFormat))
            {
                return ImageFormat.Gif;
            }
            else if (ImageFormat.Icon.Equals(img.RawFormat))
            {
                return ImageFormat.Icon;
            }
            else if (ImageFormat.MemoryBmp.Equals(img.RawFormat))
            {
                return ImageFormat.MemoryBmp;
            }
            else if (ImageFormat.Png.Equals(img.RawFormat))
            {
                return ImageFormat.Png;
            }
            else if (ImageFormat.Tiff.Equals(img.RawFormat))
            {
                return ImageFormat.Tiff;
            }
            else if (ImageFormat.Wmf.Equals(img.RawFormat))
            {
                return ImageFormat.Wmf;
            }
            else
            {
                return ImageFormat.Jpeg;
            }
        }
        public static byte[] ResizeImageFile(this byte[] imageFile, Int32 targetSize, ImageFormat format)
        {
            if (imageFile == null)
                throw new Exception("لطفا تصویر اصلی را مشخص نمایید");
            //باز کردن تصویر اصلی به عنوان یک جریان
            using (var oldImage = Image.FromStream(new MemoryStream(imageFile)))
            {
                //محاسبه اندازه تصویر خروجی با توجه به اندازه داده شده
                var newSize = CalculateDimensions(oldImage.Size, targetSize);
                //ایجاد تصویر جدید
                using (var newImage = new Bitmap(newSize.Width, newSize.Height, oldImage.PixelFormat))
                {
                    using (var canvas = Graphics.FromImage(newImage))
                    {
                        canvas.SmoothingMode = SmoothingMode.AntiAlias;
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        //تغییر اندازه تصویر اصلی و قرار دادن آن در تصویر جدید
                        canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
                        var m = new MemoryStream();
                        //ذخیره تصویر جدید با فرمت وارد شده

                        newImage.Save(m, format);
                        return m.ToArray();
                    }
                }
            }
        }

        private static Size CalculateDimensions(Size oldSize, Int32 targetSize)
        {
            var newSize = new Size();
            if (oldSize.Height > oldSize.Width)
            {
                newSize.Width = Convert.ToInt32(oldSize.Width * (targetSize / (float)oldSize.Height));
                newSize.Height = targetSize;
            }
            else
            {
                newSize.Width = targetSize;
                newSize.Height = Convert.ToInt32(oldSize.Height * (targetSize / (float)oldSize.Width));
            }
            return newSize;
        }
    }
}