using System;

namespace ASPNETCoreIdentitySample.ViewModels
{
    public enum ImgSize
    {
        I,
        Xxs,
        Xs,
        S,
        M,
        L,
        Xl,
        Xxl,
        Xxxl,
        Full
    }
    public class BaseFileDetailsViewModel
    {
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileContentType { get; set; }
        public string FileSize { get; set; }
        public string FileOnDs { get; set; }
        public FileTypeExtension FileTypeExtension { get;set; }
        public string FileTypeCssFontAwesomeIcon { get; set; }
    }

    public enum FileTypeExtension
    {
        Unknown,
        Pdf,
        Word,
        Ppt,
        Excel,
        Jpg,
        Png,
        Gif,
        Bmp,
        Tiff,
        Zip,
        Rar,
        Mp4,
        Wmv,
        Txt,
        Mp3,
        Wav,
        Acc
    }
}