using System.Collections.Generic;
using System.IO;

namespace ASPNETCoreIdentitySample.BaseService.Contracts
{
    public interface IBaseCompressHelper
    {
        void AddFileToArchive(List<BaseCompressHelper.ZipItem> zipItems, string seveToFile);
        MemoryStream AddFileToArchive(List<BaseCompressHelper.ZipItem> zipItems);
        void AddFileToArchive_InputByte(List<BaseCompressHelper.ZipItem> zipItems, string seveToFile);
        MemoryStream AddFileToArchive_InputByte(List<BaseCompressHelper.ZipItem> zipItems);
        void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName);

        void ExtractItems(string sourceArchiveFileName, List<string> pathFilesinArchive,
            string destinationDirectoryName);

        List<BaseCompressHelper.ZipItem> ExtractItems(string sourceArchiveFileName);
        List<BaseCompressHelper.ZipItem> ExtractItems(string sourceArchiveFileName, List<string> pathFilesinArchive);
    }
}