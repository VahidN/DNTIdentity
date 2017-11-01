using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASPNETCoreIdentitySample.ViewModels
{
    public class UploadFileViewModel
    {
        public string DeletedId { get; set; }
        public IFormFile File { get; set; }
    }

    public class UploadFilesViewModel
    {
        public string DeletedId { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
