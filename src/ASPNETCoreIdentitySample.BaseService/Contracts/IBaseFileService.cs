using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNETCoreIdentitySample.Entities;
using ASPNETCoreIdentitySample.ViewModels;

namespace ASPNETCoreIdentitySample.BaseService.Contracts
{
    public interface IBaseFileService
    {
        List<BaseFile> GetAll();

        List<BaseFile> GetAllFilterTypeInsert(ChooseFileTypeForUploadBaseFile fileOrImg);

        int GetAllFilterTypeCount(ChooseFileTypeForUploadBaseFile fileOrImg);
        BaseFile GetById(int i);
        BaseFile GetByGuid(Guid guid);
        BaseFile GetByGuid(string guid);

        string GetFileName(int i);
        string GetFileName(Guid guid);
        string GetFileName(string guid);
        string GetFileNameOnDs(int i);
        string GetFileNameOnDs(Guid guid);
        string GetFileNameOnDs(string guid);
        string GetFileNameOnDsAndRemove(int i);
        string GetFileNameOnDsAndRemove(Guid guid);
        List<string> GetFileNameOnDsAndRemove(Guid[] guids);
        string GetFileNameOnDsAndRemove(string guid);
        List<string> GetFileNameOnDsAndRemove(string[] guid);
        Task<string> GetFileNameOnDsAndRemoveAsync(int i);
        Task<string> GetFileNameOnDsAndRemoveAsync(Guid guid);
        Task<string> GetFileNameOnDsAndRemoveAsync(string guid);
        Task<List<string>> GetFileNameOnDsAndRemoveAsync(string[] guid);
        Task<BaseFileDetailsViewModel> BaseFileDetailsAsync(string fileId);
        Task<BaseFileDetailsViewModel> BaseFileDetailsAsync(int fileId);

        Task<List<BaseFileDetailsViewModel>> BaseFileDetailsAsync(string[] fileId);
        Task<List<BaseFileDetailsViewModel>> BaseFileDetailsAsync(int[] fileId);
        string GetFileSize(int i);
        string GetFileSize(Guid guid);
        string GetFileSize(string guid);
        int Insert(BaseFile a);
        void Delete(int i);
        void Delete(Guid guid);
        void Delete(string guid);
        void Update(BaseFile a);
        List<BaseFileForZipArchiveCreateViewModel> GetFileNameAndFileNameOnDsList(List<Guid> basefileGuids);
        Tuple<string,string> GetFileNameAndFileNameOnDs(string guid);
        Tuple<string, string> GetFileNameAndFileNameOnDs(int id);
        Tuple<string, string> GetFileNameAndFileNameOnDs(Guid guid);
        Tuple<string, string,string> GetFileNameAndFileNameOnDsAndFileType(int id);
        Tuple<string, string, string> GetFileNameAndFileNameOnDsAndFileType(Guid guid);
        Tuple<string, string, string> GetFileNameAndFileNameOnDsAndFileType(string guid);
        Task<Tuple<string, string, string>> GetFileNameAndFileNameOnDsAndFileTypeAsync(int id);
        Task<Tuple<string, string, string>> GetFileNameAndFileNameOnDsAndFileTypeAsync(Guid guid);
        Task<Tuple<string, string, string>> GetFileNameAndFileNameOnDsAndFileTypeAsync(string guid);
        Task<Tuple<string, string, string, string>> GetFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(int id);
        Task<Tuple<string, string, string, string>> GetFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(Guid guid);
        Task<Tuple<string, string, string, string>> GetFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(string guid);

        Task<List<Tuple<string, string, string, string>>> GetAllFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(List<Guid> guids);
        Task<List<Tuple<string, string, string, string>>> GetAllFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(List<string> guids);

        Task<List<Tuple<string, string>>> GetAllFileNameOnDsAsync(List<Guid> guids);
        Task<List<Tuple<string, string>>> GetAllFileNameOnDsAsync(List<string> guids);


    }
}