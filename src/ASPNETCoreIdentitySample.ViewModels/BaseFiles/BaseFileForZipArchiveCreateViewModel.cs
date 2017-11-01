using System;

namespace ASPNETCoreIdentitySample.ViewModels
{
    public class BaseFileForZipArchiveCreateViewModel
    {
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileContentType { get; set; }
        public string FileSize { get; set; }
        public string FileOnDs { get; set; } 
    }
}