using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ASPNETCoreIdentitySample.Common.WebToolkit
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2555
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class UploadFileExtensionsAttribute : ValidationAttribute
    {
        private readonly IList<string> _allowedExtensions;
        public UploadFileExtensionsAttribute(string fileExtensions)
        {
            _allowedExtensions = fileExtensions.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return true; // returning false, makes this field required.
            }

            var file = value as IFormFile;
            if (file != null)
            {
                return isValidFile(file);
            }

            var files = value as IList<IFormFile>;
            if (files == null)
            {
                return false;
            }

            foreach (var postedFile in files)
            {
                if (!isValidFile(postedFile)) return false;
            }

            return true;
        }

        private bool isValidFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return true; // returning false, makes this field required.
            }

            var fileExtension = Path.GetExtension(file.FileName);
            return !string.IsNullOrWhiteSpace(fileExtension) &&
                   _allowedExtensions.Any(ext => fileExtension.Equals(ext, StringComparison.OrdinalIgnoreCase));
        }
    }
}