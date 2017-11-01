using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ASPNETCoreIdentitySample.BaseService
{
    public static class ValidateImageHelper
    {
        public static bool IsValid(object value)
        {
            var isValid = false;
            var file = value as IFormFile;

            if (file == null || file.Length > 1 * 1024 * 1024)
            {
                return false;
            }

            if (IsFileTypeValid(file))
            {
                isValid = true;
            }

            return isValid;
        }

        private static bool IsFileTypeValid(IFormFile file)
        {
            var isValid = false;

            try
            {
                using (var img = Image.FromStream(file.OpenReadStream()))
                {
                    if (IsOneOfValidFormats(img.RawFormat))
                    {
                        isValid = true;
                    }
                }
            }
            catch
            {
                //Image is invalid
            }
            return isValid;
        }

        private static bool IsOneOfValidFormats(ImageFormat rawFormat)
        {
            var formats = GetValidFormats();
            return formats.Contains(rawFormat);
        }

        private static IEnumerable<ImageFormat> GetValidFormats()
        {
            var formats = new List<ImageFormat> { ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Gif };
            return formats;
        }
    }
}