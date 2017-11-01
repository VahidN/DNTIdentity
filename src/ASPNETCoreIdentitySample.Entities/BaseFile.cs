using ASPNETCoreIdentitySample.Entities.AuditableEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASPNETCoreIdentitySample.Entities
{
    public class BaseFile : IAuditableEntity
    {
        public int Id { get; set; }
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileContentType { get; set; }
        public string FileSize { get; set; }
        public string FileOnDs { get; set; }

        public ChooseFileTypeForUploadBaseFile FileTypeFilter { get; set; }
    }

    public enum ChooseFileTypeForUploadBaseFile
    {
        Images,
        Files,
        Videoes,
        Audioes
    }
}
