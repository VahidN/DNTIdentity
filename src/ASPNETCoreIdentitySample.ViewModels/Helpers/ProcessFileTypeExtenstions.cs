
namespace ASPNETCoreIdentitySample.ViewModels.Helpers.Filters
{
    public static class ProcessFileTypeExtenstions
    {
        public static FileTypeExtension ProcessExtension(string strFileType)
        {
            strFileType = strFileType.ToLower().Replace(".", "");

            switch (strFileType)
            {
                case "bmp":
                    return FileTypeExtension.Bmp;
                case "xlsx":
                    return FileTypeExtension.Excel;
                case "xls":
                    return FileTypeExtension.Excel;
                case "gif":
                    return FileTypeExtension.Gif;
                case "jpg":
                    return FileTypeExtension.Jpg;
                case "mp4":
                    return FileTypeExtension.Mp4;
                case "pdf":
                    return FileTypeExtension.Pdf;
                case "png":
                    return FileTypeExtension.Png;
                case "pptx":
                    return FileTypeExtension.Ppt;
                case "ppt":
                    return FileTypeExtension.Ppt;
                case "rar":
                    return FileTypeExtension.Rar;
                case "zip":
                    return FileTypeExtension.Zip;
                case "tiff":
                    return FileTypeExtension.Tiff;
                case "wmv":
                    return FileTypeExtension.Wmv;
                case "doc":
                    return FileTypeExtension.Word;
                case "docx":
                    return FileTypeExtension.Word;
                case "mp3":
                    return FileTypeExtension.Mp3;
                case "wav":
                    return FileTypeExtension.Wav;
                case "acc":
                    return FileTypeExtension.Acc;
                default:
                    return FileTypeExtension.Unknown;
            }
        }

        public static string ProcessExtensionCssFontAwesomeIcon(string strFileType)
        {
            var fileTypeExtension = ProcessExtension(strFileType);
            string fileType;
            switch (fileTypeExtension)
            {
                case FileTypeExtension.Pdf:
                    fileType = "fa-file-pdf-o";
                    break;
                case FileTypeExtension.Rar:
                    fileType = "fa-file-zip-o";
                    break;
                case FileTypeExtension.Zip:
                    fileType = "fa-file-zip-o";
                    break;
                case FileTypeExtension.Jpg:
                    fileType = "fa-file-image-o";
                    break;
                case FileTypeExtension.Png:
                    fileType = "fa-file-image-o";
                    break;
                case FileTypeExtension.Tiff:
                    fileType = "fa-file-image-o";
                    break;
                case FileTypeExtension.Gif:
                    fileType = "fa-file-image-o";
                    break;
                case FileTypeExtension.Bmp:
                    fileType = "fa-file-image-o";
                    break;
                case FileTypeExtension.Excel:
                    fileType = "fa-file-excel-o";
                    break;
                case FileTypeExtension.Word:
                    fileType = "fa-file-word-o";
                    break;
                case FileTypeExtension.Mp4:
                    fileType = "fa-file-movie-o";
                    break;
                case FileTypeExtension.Wmv:
                    fileType = "fa-file-movie-o";
                    break;
                case FileTypeExtension.Txt:
                    fileType = "fa-file-text-o";
                    break;
                case FileTypeExtension.Wav:
                    fileType = "fa-file-audio-o";
                    break;
                case FileTypeExtension.Acc:
                    fileType = "fa-file-audio-o";
                    break;
                case FileTypeExtension.Mp3:
                    fileType = "fa-file-audio-o";
                    break;
                default:
                    fileType = "fa-file-o";
                    break;
            }

            return fileType;
        }
    }
}