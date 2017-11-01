using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ASPNETCoreIdentitySample.BaseService.Contracts
{
    public interface IFileInsertService
    {
        string InsertsAndReturnIdsAsString(IList<IFormFile> uploadfiles, string fileId = null,
            bool isUpdate = false, bool isCompress = false);

        Task<string> InsertsAndReturnIdsAsStringAsync(IList<IFormFile> uploadfiles, string fileId = null,
            bool isUpdate = false, bool isCompress = false);

        Task<Tuple<string, List<string>>> InsertsAndReturnIdsAsStringReturnListAsync(IList<IFormFile> uploadfiles,
            string fileId = null, bool isUpdate = false, bool isCompress = false);
        Tuple<string, List<string>> InsertsAndReturnIdsAsStringReturnList(IList<IFormFile> uploadfiles,
            string fileId = null, bool isUpdate = false, bool isCompress = false);
        string GetBackInsertsIds(List<string> listOfFile);

        Tuple<string, Guid> InsertAndArchivedWithbaseFilesGuids(List<Guid> basefileGuids,
            List<string> basefileNames = null);

        List<string> Inserts(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false,
            bool isCompress = false);
        Tuple<string, Guid> InsertZip(Stream fileBytes, string fileId = null, bool isUpdate = false);

        Task<Tuple<string, Guid, string>> InsertStreamAsync(Stream fileBytes, string fileExtension, string fileId = null, bool isUpdate = false, bool isSave = false);

        Tuple<string, Guid, string> InsertStream(Stream fileBytes, string fileExtension, string fileId = null, bool isUpdate = false);

        string InsertWithoutFileUpload(IFormFile fileBytes, string fileId = null,
            bool isUpdate = false);

        string Insert(IFormFile fileUpload, string fileId = null, bool isUpdate = false);
        Task<string> InsertAsync(IFormFile fileUpload, string fileId = null, bool isUpdate = false, bool isSave = false);
        Task<Tuple<string, string>> InsertAndGetGuidAndFileNameOnDsAsync(IFormFile fileUpload, string fileId = null, bool isUpdate = false, bool isSave = false);
        Task<Tuple<string, string, List<Tuple<string, string>>>> InsertsAndGetGuidsAndFileNamesOnDsAsync(IList<IFormFile> uploadfiles, string[] fileIds = null, bool isUpdate = false, bool isSave = false);

        string GetFileName(int fileId);
        string GetFileName(string fileId);
        string GetFileNameOnDs(int fileId);
        string GetFileNameOnDs(string fileId);
        string GetFileSize(int fileId);
        string GetFileSize(string fileId);
        Task<List<string>> GetFileNameAndRemoveRangeAsync(string[] fileIds);
        Task<string> GetFileNameAndRemoveAsync(string fileId);
        Task<bool> RemoveRangeAsync(string[] fileIds, bool isSaveChanges = false, bool deleteFromDisk = true);
        bool RemoveRange(string[] fileIds, bool isSaveChanges = false, bool deleteFromDisk = true);
        bool RemoveRange(string[] fileIds);
        void Remove(int fileId, bool isSaveChanges = false);
        void Remove(string fileId, bool isSaveChanges = false);
        bool RemoveOnDiskWithFileNameOnDisk(string onDiskFileName);
        bool RemoveOnDiskWithFileNameOnDisk(string[] onDiskFileNames);
        Task<bool> RemoveAsync(string fileId, bool isSaveChanges = false);
        byte[] ReadFully(Stream input);
        byte[] ReadFileFromDs(string fileOnDs);

        Task<string> GetFileUrlAsync(string guid);

        Task<string> GetFileUrlAsync(Guid guid);
        Task<Tuple<string, string>> GetFileUrlAndSizeAsync(string guid);
        Task<Tuple<string, string>> GetFileUrlAndSizeAsync(Guid guid);

        Task<List<Tuple<string, string>>> GetFilesGuidAndUrlAsync(List<string> guids);
        Task<List<Tuple<string, string>>> GetFilesGuidAndUrlAsync(List<Guid> guids);

        Task<List<string>> GetFilesUrlAsync(List<Guid> guids);
        Task<List<string>> GetFilesUrlAsync(List<string> guids);

        Task<List<Tuple<string, string>>> GetFilesGuidAndPhysicalUrlAsync(List<Guid> guids);
        Task<List<Tuple<string, string>>> GetFilesGuidAndPhysicalUrlAsync(List<string> guids);
    }
}