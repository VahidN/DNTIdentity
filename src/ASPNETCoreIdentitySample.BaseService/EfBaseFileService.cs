using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.Entities;
using ASPNETCoreIdentitySample.ViewModels;
using ASPNETCoreIdentitySample.BaseService.Contracts;
using ASPNETCoreIdentitySample.Common.StringToolKit;

namespace ASPNETCoreIdentitySample.BaseService
{
    public class EfBaseFileService : IBaseFileService
    {
        readonly IUnitOfWork _uow;
        readonly DbSet<BaseFile> _baseFiles;
        public EfBaseFileService(IUnitOfWork uow)
        {
            _uow = uow;
            _baseFiles = _uow.Set<BaseFile>();
        }

        public List<BaseFile> GetAll()
        {
            return _baseFiles.ToList();
        }

        public List<BaseFile> GetAllFilterTypeInsert(ChooseFileTypeForUploadBaseFile fileOrImg)
        {
            return _baseFiles.Where(p => p.FileTypeFilter == fileOrImg).ToList();
        }

        public int GetAllFilterTypeCount(ChooseFileTypeForUploadBaseFile fileOrImg)
        {
            return _baseFiles.Count(p => p.FileTypeFilter == fileOrImg);
        }

        public BaseFile GetById(int i)
        {
            return _baseFiles.FirstOrDefault(p => p.Id == i);
        }

        public BaseFile GetByGuid(Guid guid)
        {
            return _baseFiles.FirstOrDefault(p => p.FileGuid == guid);
        }

        public BaseFile GetByGuid(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return _baseFiles.FirstOrDefault(p => p.FileGuid == baseGuid);
        }

        public string GetFileName(int i)
        {
            var a = _baseFiles.AsNoTracking().Select(order => new { order.FileName, order.Id }).FirstOrDefault(p => p.Id == i);
            if (a == null) return string.Empty;
            return string.IsNullOrEmpty(a.FileName) ? string.Empty : a.FileName;
        }

        public string GetFileName(Guid guid)
        {
            var a = _baseFiles.AsNoTracking().Select(order => new { order.FileName, order.Id,order.FileGuid }).FirstOrDefault(p => p.FileGuid == guid);
            if (a == null) return string.Empty;
            return string.IsNullOrEmpty(a.FileName) ? string.Empty : a.FileName;
        }

        public string GetFileName(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return GetFileName(baseGuid);
        }

        public string GetFileNameOnDs(int i)
        {
            var a = _baseFiles.AsNoTracking().Select(order => new { order.FileOnDs, order.Id }).FirstOrDefault(p => p.Id == i);
            if (a == null) return string.Empty;
            return string.IsNullOrEmpty(a.FileOnDs) ? string.Empty : a.FileOnDs;
        }

        public string GetFileNameOnDs(Guid guid)
        {
            var a = _baseFiles.AsNoTracking().Select(order => new { order.FileOnDs, order.Id, order.FileGuid }).FirstOrDefault(p => p.FileGuid == guid);
            if (a == null) return string.Empty;
            return string.IsNullOrEmpty(a.FileOnDs) ? string.Empty : a.FileOnDs;
        }

        public string GetFileNameOnDs(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return GetFileNameOnDs(baseGuid);
        }

        public string GetFileNameOnDsAndRemove(int i)
        {
            var a = _baseFiles.AsNoTracking().Select(order => new { order.FileOnDs, order.Id }).FirstOrDefault(p => p.Id == i);
            if (a == null) return string.Empty;
            Delete(i);
            return string.IsNullOrEmpty(a.FileOnDs) ? string.Empty : a.FileOnDs;
        }

        public string GetFileNameOnDsAndRemove(Guid guid)
        {
            var a = _baseFiles.AsNoTracking().Select(order => new { order.FileOnDs, order.Id, order.FileGuid }).FirstOrDefault(p => p.FileGuid == guid);
            if (a == null) return string.Empty;
            Delete(guid);
            return string.IsNullOrEmpty(a.FileOnDs) ? string.Empty : a.FileOnDs;
        }

        public List<string> GetFileNameOnDsAndRemove(Guid[] guids)
        {
            var items = _baseFiles.AsNoTracking().Select(order => new { order.FileOnDs, order.Id, order.FileGuid }).Where(p =>   guids.Contains(p.FileGuid)).ToList();
            if (items == null) return new List<string>();
            var listFileOnDs=new List<string>();
            foreach (var item in items)
            {
                listFileOnDs.Add(item.FileOnDs);
                Delete(item.FileGuid);
            }
            
            return listFileOnDs;
        }

        public string GetFileNameOnDsAndRemove(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return GetFileNameOnDsAndRemove(baseGuid);
        }

        public List<string> GetFileNameOnDsAndRemove(string[] guid)
        {
            var baseGuid = StringUtil.ConvertStringsToGuids(guid);
            return GetFileNameOnDsAndRemove(baseGuid);
        }


        public async Task<string> GetFileNameOnDsAndRemoveAsync(int i)
        {
            var a = await _baseFiles.AsNoTracking().Select(order => new { order.FileOnDs, order.Id }).FirstOrDefaultAsync(p => p.Id == i);
            if (a == null) return string.Empty;
            Delete(i);
            return string.IsNullOrEmpty(a.FileOnDs) ? string.Empty : a.FileOnDs;
        }

        public async Task<string> GetFileNameOnDsAndRemoveAsync(Guid guid)
        {
            var a = await _baseFiles.AsNoTracking().Select(order => new { order.FileOnDs, order.Id, order.FileGuid }).FirstOrDefaultAsync(p => p.FileGuid == guid);
            if (a == null) return string.Empty;
            Delete(guid);
            return string.IsNullOrEmpty(a.FileOnDs) ? string.Empty : a.FileOnDs;
        }
        public async Task<List<string>> GetFileNameOnDsAndRemoveAsync(Guid[] guids)
        {
            var items = await _baseFiles.AsNoTracking().Select(order => new { order.FileOnDs, order.Id, order.FileGuid }).Where(p => guids.Contains(p.FileGuid)).ToListAsync();
            if (items == null) return new List<string>();
            var listFileOnDs = new List<string>();
            foreach (var item in items)
            {
                listFileOnDs.Add(item.FileOnDs);
                Delete(item.FileGuid);
            }

            return listFileOnDs;
        }

        public async Task<string> GetFileNameOnDsAndRemoveAsync(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return await GetFileNameOnDsAndRemoveAsync(baseGuid);
        }

        public async Task<List<string>> GetFileNameOnDsAndRemoveAsync(string[] guid)
        {
            var baseGuid = StringUtil.ConvertStringsToGuids(guid);
            return await GetFileNameOnDsAndRemoveAsync(baseGuid);
        }

        public async Task<BaseFileDetailsViewModel> BaseFileDetailsAsync(string fileId)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(fileId);
            return
                await _baseFiles.AsNoTracking()
                    .ProjectTo<BaseFileDetailsViewModel>()
                    .FirstOrDefaultAsync(p => p.FileGuid == baseGuid);
        }

        public async Task<BaseFileDetailsViewModel> BaseFileDetailsAsync(int fileId)
        {
            var aa=
                await _baseFiles.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == fileId);
            return aa == null ? null : AutoMapper.Mapper.Map(aa, new BaseFileDetailsViewModel());
        }

        public async Task<List<BaseFileDetailsViewModel>> BaseFileDetailsAsync(string[] fileId)
        {
            var baseGuids = StringUtil.ConvertStringsToGuids(fileId);
            return
                await _baseFiles.AsNoTracking()
                .Where(p=> baseGuids.Contains(p.FileGuid))
                    .ProjectTo<BaseFileDetailsViewModel>().ToListAsync();
        }

        public async Task<List<BaseFileDetailsViewModel>> BaseFileDetailsAsync(int[] fileId)
        {
            var aa =
                await _baseFiles.AsNoTracking().Where(p => fileId.Contains(p.Id)).ToListAsync();
            return aa == null ? null : AutoMapper.Mapper.Map(aa, new List<BaseFileDetailsViewModel>());
        }


        public string GetFileSize(int i)
        {
            var a = _baseFiles.Select(order => new { order.FileSize, order.Id }).FirstOrDefault(p => p.Id == i);
            if (a == null) return string.Empty;
            return string.IsNullOrEmpty(a.FileSize) ? string.Empty : a.FileSize;
        }

        public string GetFileSize(Guid guid)
        {
            var a = _baseFiles.Select(order => new { order.FileSize, order.Id, order.FileGuid }).FirstOrDefault(p => p.FileGuid == guid);
            if (a == null) return string.Empty;
            return string.IsNullOrEmpty(a.FileSize) ? string.Empty : a.FileSize;
        }

        public string GetFileSize(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return GetFileSize(baseGuid);
        }

        public int Insert(BaseFile b)
        {
            _baseFiles.Add(b);
            
            return b.Id;
        }

        //public void Inserts(BaseFile[] c)
        //{
        //    //_context.BaseFiles.AddRange(c);
        //    _uow.RemoveRange(c);
        //}


        public void Delete(int i)
        {
            var c = _baseFiles.FirstOrDefault(p => p.Id == i);
            _baseFiles.Remove(c);
            
        }

        public void Delete(Guid guid)
        {
            var c = _baseFiles.FirstOrDefault(p => p.FileGuid == guid);
            _baseFiles.Remove(c);
        }

        public void Delete(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            var c = _baseFiles.FirstOrDefault(p => p.FileGuid == baseGuid);
            _baseFiles.Remove(c);
        }


        public void Update(BaseFile b)
        {
            _uow.Entry(b).State = EntityState.Modified;
            
        }

        public List<BaseFileForZipArchiveCreateViewModel> GetFileNameAndFileNameOnDsList(List<Guid> basefileGuids)
        {
            return _baseFiles.Where(p => basefileGuids.Contains(p.FileGuid)).ProjectTo<BaseFileForZipArchiveCreateViewModel>().ToList();
        }

        public Tuple<string, string> GetFileNameAndFileNameOnDs(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return GetFileNameAndFileNameOnDs(baseGuid);
        }

        public Tuple<string, string> GetFileNameAndFileNameOnDs(int id)
        {
            var queryResult =
                _baseFiles.AsNoTracking().Select(p => new { p.FileName, p.FileOnDs, p.Id }).FirstOrDefault(p => p.Id == id);
            return queryResult == null
                ? new Tuple<string, string>(string.Empty, string.Empty)
                : new Tuple<string, string>(queryResult.FileName, queryResult.FileOnDs);
        }

        public Tuple<string, string> GetFileNameAndFileNameOnDs(Guid guid)
        {
            var queryResult =
                _baseFiles.AsNoTracking().Select(p => new {p.FileName, p.FileOnDs, p.FileGuid}).FirstOrDefault(p => p.FileGuid == guid);
            return queryResult == null
                ? new Tuple<string, string>(string.Empty, string.Empty)
                : new Tuple<string, string>(queryResult.FileName, queryResult.FileOnDs);
        }

        public Tuple<string, string, string> GetFileNameAndFileNameOnDsAndFileType(int id)
        {
            var queryResult =
                _baseFiles.AsNoTracking().Select(p => new { p.FileName, p.FileOnDs, p.Id, p.FileContentType }).FirstOrDefault(p => p.Id == id);
            return queryResult == null
                ? new Tuple<string, string, string>(string.Empty, string.Empty, string.Empty)
                : new Tuple<string, string, string>(queryResult.FileName, queryResult.FileOnDs, queryResult.FileContentType);
        }

        public Tuple<string, string, string> GetFileNameAndFileNameOnDsAndFileType(Guid guid)
        {
            var queryResult =
                _baseFiles.AsNoTracking().Select(p => new { p.FileName, p.FileOnDs, p.FileGuid,p.FileContentType }).FirstOrDefault(p => p.FileGuid == guid);
            return queryResult == null
                ? new Tuple<string, string, string>(string.Empty, string.Empty, string.Empty)
                : new Tuple<string, string, string>(queryResult.FileName, queryResult.FileOnDs,queryResult.FileContentType);
        }

        public Tuple<string, string, string> GetFileNameAndFileNameOnDsAndFileType(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return GetFileNameAndFileNameOnDsAndFileType(baseGuid);
        }


        public async Task<Tuple<string, string, string>> GetFileNameAndFileNameOnDsAndFileTypeAsync(int id)
        {
            var queryResult =await 
                _baseFiles.AsNoTracking().Select(p => new { p.FileName, p.FileOnDs, p.Id, p.FileContentType }).FirstOrDefaultAsync(p => p.Id == id);
            return queryResult == null
                ? new Tuple<string, string, string>(string.Empty, string.Empty, string.Empty)
                : new Tuple<string, string, string>(queryResult.FileName, queryResult.FileOnDs, queryResult.FileContentType);
        }

        public async Task<Tuple<string, string, string>> GetFileNameAndFileNameOnDsAndFileTypeAsync(Guid guid)
        {
            var queryResult = await
                _baseFiles.AsNoTracking().Select(p => new { p.FileName, p.FileOnDs, p.FileGuid, p.FileContentType }).FirstOrDefaultAsync(p => p.FileGuid == guid);
            return queryResult == null
                ? new Tuple<string, string, string>(string.Empty, string.Empty, string.Empty)
                : new Tuple<string, string, string>(queryResult.FileName, queryResult.FileOnDs, queryResult.FileContentType);
        }

        




        public async Task<Tuple<string, string, string>> GetFileNameAndFileNameOnDsAndFileTypeAsync(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return await GetFileNameAndFileNameOnDsAndFileTypeAsync(baseGuid);
        }

        public async Task<Tuple<string, string, string, string>> GetFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(int id)
        {
            var queryResult = await
                _baseFiles.AsNoTracking().Select(p => new { p.FileName, p.FileOnDs, p.Id, p.FileContentType,p.FileSize }).FirstOrDefaultAsync(p => p.Id == id);
            return queryResult == null
                ? new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty)
                : new Tuple<string, string, string, string>(queryResult.FileName, queryResult.FileOnDs, queryResult.FileContentType, queryResult.FileSize);
        }

        public async Task<Tuple<string, string, string, string>> GetFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(Guid guid)
        {
            var queryResult = await
                _baseFiles.AsNoTracking().Select(p => new { p.FileName, p.FileOnDs, p.FileGuid, p.FileContentType, p.FileSize }).FirstOrDefaultAsync(p => p.FileGuid == guid);
            return queryResult == null
                ? new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty)
                : new Tuple<string, string, string, string>(queryResult.FileName, queryResult.FileOnDs, queryResult.FileContentType, queryResult.FileSize);
        }

        public async Task<Tuple<string, string, string,string>> GetFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return await GetFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(baseGuid);
        }
        /// <summary>
        /// برگشت اطلاعات فایل
        /// </summary>
        /// <param name="guids"></param>
        /// <returns>
        /// 1- FileNameOnDatabase
        /// 2- FileNameOnDs
        /// 3- FileContentType
        /// 4- FileSize MB
        /// </returns>
        public async Task<List<Tuple<string, string, string, string>>> GetAllFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(List<Guid> guids)
        {
            var mylist = new List<Tuple<string, string, string, string>>();
               var queryResult = await
                _baseFiles.AsNoTracking().Select(p => new { p.FileName, p.FileOnDs, p.FileGuid, p.FileContentType, p.FileSize }).Where(p => guids.Contains(p.FileGuid)).ToListAsync();
            if (queryResult == null)
                return mylist;
            
            foreach(var q in queryResult)
            {
                mylist.Add(new Tuple<string, string, string, string>(q.FileName, q.FileOnDs, q.FileContentType, q.FileSize));
            }

            return mylist;
        }
        /// <summary>
        /// برگشت اطلاعات فایل
        /// </summary>
        /// <param name="guids"></param>
        /// <returns>
        /// 1- FileNameOnDatabase
        /// 2- FileNameOnDs
        /// 3- FileContentType
        /// 4- FileSize MB
        /// </returns>
        public async Task<List<Tuple<string, string, string, string>>> GetAllFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(List<string> guids)
        {
            var baseGuids = StringUtil.ConvertStringsToGuids(guids.ToArray());
            return await GetAllFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(baseGuids.ToList());
        }

        /// <summary>
        /// برگشت اطلاعات فایل
        /// </summary>
        /// <param name="guids"></param>
        /// <returns>
        /// 1- FileNameOnDatabase
        /// 2- FileNameOnDs
        /// 3- FileContentType
        /// 4- FileSize MB
        /// </returns>
        public async Task<List<Tuple<string, string>>> GetAllFileNameOnDsAsync(List<Guid> guids)
        {
            var mylist = new List<Tuple<string, string>>();
            var queryResult = await
             _baseFiles.AsNoTracking().Select(p => new { p.FileOnDs, p.FileGuid }).Where(p => guids.Contains(p.FileGuid)).ToListAsync();
            if (queryResult == null)
                return mylist;

            foreach (var q in queryResult)
            {
                mylist.Add(new Tuple<string, string>(q.FileGuid.ToString(), q.FileOnDs));
            }

            return mylist;
        }
        /// <summary>
        /// برگشت اطلاعات فایل
        /// </summary>
        /// <param name="guids"></param>
        /// <returns>
        /// 1- FileNameOnDatabase
        /// 2- FileNameOnDs
        /// 3- FileContentType
        /// 4- FileSize MB
        /// </returns>
        public async Task<List<Tuple<string, string>>> GetAllFileNameOnDsAsync(List<string> guids)
        {
            var baseGuids = StringUtil.ConvertStringsToGuids(guids.ToArray());
            return await GetAllFileNameOnDsAsync(baseGuids.ToList());
        }
    }
}