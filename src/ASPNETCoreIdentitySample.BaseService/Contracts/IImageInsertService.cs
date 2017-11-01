using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ASPNETCoreIdentitySample.ViewModels;

namespace ASPNETCoreIdentitySample.BaseService.Contracts
{
    public interface IImageInsertService
    {
        string InsertsAndReturnIdsAsString(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false);
        Task<Tuple<string, List<string>>> InsertsAndReturnIdsAsStringReturnListAsync(IList<IFormFile> uploadfiles,
            string fileId = null, bool isUpdate = false);
        string GetBackInsertsIds(List<string> listOfFile);
        List<string> Inserts(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false);
        Task<List<string>> InsertsAsync(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false);
        string Insert(IFormFile fileUpload, string fileId = null, bool isUpdate = false);
        Task<string> InsertAsync(IFormFile fileUpload, string fileId = null, bool isUpdate = false, bool isSave = false);
        string Insert(byte[] fileBytes, string fileId = null, bool isUpdate = false);
        string GetFileName(int fileId);
        string GetFileName(string fileId);
        string GetFileNameAndRemove(string fileId);
        List<string> GetFileNameAndRemove(string[] fileIds);
        Task<string> GetFileNameAndRemoveAsync(string fileId);
        Task<List<string>> GetFileNameAndRemoveAsync(string[] fileIds);
        System.Drawing.Image GetImage(string fileId);
        void Remove(string fileId);
        void Remove(string[] fileId);
        Task<bool> RemoveAsync(string fileId);
        Task<bool> RemoveAsync(string[] fileId);
        byte[] ReadFully(Stream input);
        Task<string> GetImageUrlAsync(string guid, ImgSize imgsize);
        Task<string> GetImageUrlAsync(Guid guid, ImgSize imgsize);
        Task<Tuple<string, List<string>, string, List<string>>> InsertsAndReturnIdsAsStringReturnListWithDsNameAsync(IList<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false);
    }
}