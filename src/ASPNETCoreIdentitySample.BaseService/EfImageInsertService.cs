using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using ASPNETCoreIdentitySample.BaseService.Contracts;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.Entities;
using ASPNETCoreIdentitySample.ViewModels;
using ASPNETCoreIdentitySample.Common.StringToolKit;

namespace ASPNETCoreIdentitySample.BaseService
{
    public class EfImageInsertService : IImageInsertService
    {
        private readonly IOptionsSnapshot<SiteSettings> _settingsAppPathConfig;
        private readonly IBaseFileService _baseFileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUnitOfWork _uow;
        public EfImageInsertService(IOptionsSnapshot<SiteSettings> settingsAppPathConfig,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment,
            IBaseFileService baseFileService,
            IUnitOfWork uow)
        {
            _settingsAppPathConfig = settingsAppPathConfig;
            _baseFileService = baseFileService;
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }
        

        /// <summary>
        /// ذخیره فایل و خروجی لیست آی دی فایل های با اسپلیتر
        /// </summary>
        /// <param name="uploadfiles"></param>
        /// <param name="fileId"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        public  string InsertsAndReturnIdsAsString(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false)
        {
            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return string.Empty;

            if (httpPostedFileBases.Count > 1)
            {
                var intlist = Inserts(httpPostedFileBases);
                return GetBackInsertsIds(intlist);
            }
            else
            {
                var oneid = Insert(httpPostedFileBases.First());
                return oneid;
            }
        }

        public async Task<Tuple<string, List<string>>> InsertsAndReturnIdsAsStringReturnListAsync(IList<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false)
        {
            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return new Tuple<string, List<string>>(string.Empty, null);

            if (httpPostedFileBases.Count > 1)
            {
                var intlist = await InsertsAsync(httpPostedFileBases, fileId, isUpdate);
                return new Tuple<string, List<string>>(GetBackInsertsIds(intlist), intlist);
            }
            else
            {
                var oneid = await InsertAsync(httpPostedFileBases.First());
                return new Tuple<string, List<string>>(oneid, null);
            }
        }

        public async Task<Tuple<string, List<string>, string, List<string>>> InsertsAndReturnIdsAsStringReturnListWithDsNameAsync(IList<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false)
        {
            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return new Tuple<string, List<string>, string, List<string>>(string.Empty, null, string.Empty, null);

            if (httpPostedFileBases.Count > 1)
            {
                var intlist = await InsertsGetGuidAndDsNameAsync(httpPostedFileBases, fileId, isUpdate);
                return new Tuple<string, List<string>, string, List<string>>(GetBackInsertsIds(intlist.Item1), intlist.Item1, GetBackInsertsIds(intlist.Item2), intlist.Item2);
            }
            else
            {
                var oneid = await InsertGetFileGuidAndDsNameAsync(httpPostedFileBases.First());
                return new Tuple<string, List<string>, string, List<string>>(oneid.Item1, null,oneid.Item2,null);
            }
        }

        public  string GetBackInsertsIds(List<string> listOfFile)
        {
            var baseFileIds = string.Empty;

            if (listOfFile.Any())
            {
                foreach (var i in listOfFile)
                {
                    if (!string.IsNullOrEmpty(baseFileIds))
                    {
                        baseFileIds = baseFileIds + "|" + i;
                    }
                    else
                    {
                        baseFileIds = i;
                    }
                }
            }
            return baseFileIds;
        }

        public  List<string> Inserts(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false)
        {
            var listOfFile = new List<string>();

            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return listOfFile;
            listOfFile.AddRange(httpPostedFileBases.Where(httpPostedFileBase => httpPostedFileBase != null).Select(httpPostedFileBase => Insert(httpPostedFileBase)));

            return listOfFile;
        }

        public async Task<List<string>> InsertsAsync(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false)
        {
            var listOfFile = new List<string>();

            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return listOfFile;
            foreach (var httpPostedFileBase in httpPostedFileBases)
            {
                if (httpPostedFileBase != null)
                    listOfFile.Add(await InsertAsync(httpPostedFileBase));
            }

            return listOfFile;
        }

        public async Task<Tuple<List<string>, List<string>>> InsertsGetGuidAndDsNameAsync(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false)
        {
            var listOfFile = new List<string>();
            var listOfFileDs = new List<string>();

            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return new Tuple<List<string>, List<string>>(listOfFile, listOfFileDs);
            foreach (var httpPostedFileBase in httpPostedFileBases)
            {
                if (httpPostedFileBase != null)
                {
                    var ss = await InsertGetFileGuidAndDsNameAsync(httpPostedFileBase);
                    listOfFile.Add(ss.Item1);
                    listOfFileDs.Add(ss.Item2);
                }
            }

           return new Tuple<List<string>, List<string>>(listOfFile, listOfFileDs);
        }

        public  string Insert(IFormFile fileUpload,string fileId = null, bool isUpdate = false)
        {
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerImagesRootPath);
            var newGuid = Guid.NewGuid();
            var fileUploadStream = fileUpload.OpenReadStream();
            var imgProc = new ImageProcess
            {
                FileByte = ReadFully(fileUploadStream),
                FileContentLength = fileUpload.Length,
                FileStreamImg = fileUploadStream,
                FilePath = fileUpload.FileName,
                FileName = System.IO.Path.GetFileName(fileUpload.FileName),
                FileContentType = System.IO.Path.GetExtension(fileUpload.FileName).ToLower(),
                IsDeferentSize = true,
                SavePath = pathImages
            };

            imgProc.Proccess();
            var photoName = imgProc.GetFileForDs();

            if (isUpdate)
            {
                //Remove Old Image From Disk
                var basefilestrIds = StringUtil.SplitString(fileId);
                foreach (var basefilestrId in basefilestrIds)
                {
                    Remove(basefilestrId);
                }
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                 ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                 : null;

            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = imgProc.FileName,
                FileContentType = imgProc.FileContentType,
                FileSize = imgProc.FileSize,
                FileOnDs = photoName,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Images
            };
            _baseFileService.Insert(filebase);
            _uow.SaveChanges();
            return newGuid.ToString();
        }

        public async Task<string> InsertAsync(IFormFile fileUpload, string fileId = null, bool isUpdate = false, bool isSave = false)
        {
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerImagesRootPath);
            var newGuid = Guid.NewGuid();
            var fileUploadStream = fileUpload.OpenReadStream();
            var imgProc = new ImageProcess
            {
                FileByte = ReadFully(fileUploadStream),
                FileContentLength = fileUpload.Length,
                FileStreamImg = fileUploadStream,
                FilePath = fileUpload.FileName,
                FileName = System.IO.Path.GetFileName(fileUpload.FileName),
                FileContentType = System.IO.Path.GetExtension(fileUpload.FileName).ToLower(),
                IsDeferentSize = true,
                SavePath = pathImages
            };

            imgProc.Proccess();
            var photoName = imgProc.GetFileForDs();

            if (isUpdate)
            {
                //Remove Old Image From Disk
                var basefilestrIds = StringUtil.SplitString(fileId);
                foreach (var basefilestrId in basefilestrIds)
                {
                    await RemoveAsync(basefilestrId);
                }
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                 ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                 : null;

            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = imgProc.FileName,
                FileContentType = imgProc.FileContentType,
                FileSize = imgProc.FileSize,
                FileOnDs = photoName,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Images
            };
            _baseFileService.Insert(filebase);
            if (isSave)
                await _uow.SaveChangesAsync();
            return newGuid.ToString();
        }

        public async Task<Tuple<string,string>> InsertGetFileGuidAndDsNameAsync(IFormFile fileUpload, string fileId = null, bool isUpdate = false, bool isSave = false)
        {
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerImagesRootPath);
            var newGuid = Guid.NewGuid();
            var fileUploadStream = fileUpload.OpenReadStream();
            var imgProc = new ImageProcess
            {
                FileByte = ReadFully(fileUploadStream),
                FileContentLength = fileUpload.Length,
                FileStreamImg = fileUploadStream,
                FilePath = fileUpload.FileName,
                FileName = System.IO.Path.GetFileName(fileUpload.FileName),
                FileContentType = System.IO.Path.GetExtension(fileUpload.FileName).ToLower(),
                IsDeferentSize = true,
                SavePath = pathImages
            };

            imgProc.Proccess();
            var photoName = imgProc.GetFileForDs();

            if (isUpdate)
            {
                //Remove Old Image From Disk
                var basefilestrIds = StringUtil.SplitString(fileId);
                foreach (var basefilestrId in basefilestrIds)
                {
                    await RemoveAsync(basefilestrId);
                }
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                 ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                 : null;

            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = imgProc.FileName,
                FileContentType = imgProc.FileContentType,
                FileSize = imgProc.FileSize,
                FileOnDs = photoName,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Images
            };
            _baseFileService.Insert(filebase);
            if (isSave)
                await _uow.SaveChangesAsync();
            return new Tuple<string,string>(newGuid.ToString(), filebase.FileOnDs);
        }

        public  string Insert(byte[] fileBytes, string fileId = null, bool isUpdate = false)
        {
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerImagesRootPath);
            var newGuid = Guid.NewGuid();
            var imgProc = new ImageProcess
            {
                FileByte = fileBytes,
                FileContentLength = fileBytes.Length,
                FileStreamImg = new MemoryStream(fileBytes),
                FilePath = "1.jpg",
                FileName = System.IO.Path.GetFileName("1.jpg"),
                FileContentType = System.IO.Path.GetExtension("1.jpg").ToLower(),
                IsDeferentSize = true,
                SavePath = pathImages
            };

            imgProc.Proccess();
            var photoName = imgProc.GetFileForDs();

            if (isUpdate)
            {
                //Remove Old Image From Disk
                var basefilestrIds = StringUtil.SplitString(fileId);
                foreach (var basefilestrId in basefilestrIds)
                {
                    Remove(basefilestrId);
                }
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                 ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                 : null;

            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = imgProc.FileName,
                FileContentType = imgProc.FileContentType,
                FileSize = imgProc.FileContentType,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Images,
                FileOnDs = photoName
            };

            _baseFileService.Insert(filebase);
            _uow.SaveChanges();
            return newGuid.ToString();
        }

        public  string GetFileName(int fileId)
        {
            
            return _baseFileService.GetFileNameOnDs(fileId);
        }
        public  string GetFileName(string fileId)
        {
            
            return _baseFileService.GetFileNameOnDs(fileId);
        }

        public  string GetFileNameAndRemove(string fileId)
        {
            return _baseFileService.GetFileNameOnDsAndRemove(fileId);
        }
        public List<string> GetFileNameAndRemove(string[] fileIds)
        {
            return _baseFileService.GetFileNameOnDsAndRemove(fileIds);
        }

        public async Task<string> GetFileNameAndRemoveAsync(string fileId)
        {
            return await _baseFileService.GetFileNameOnDsAndRemoveAsync(fileId);
        }

        public async Task<List<string>> GetFileNameAndRemoveAsync(string[] fileIds)
        {
            return await _baseFileService.GetFileNameOnDsAndRemoveAsync(fileIds);
        }

        public  System.Drawing.Image GetImage(string fileId)
        {
            var pathImages = _settingsAppPathConfig.Value.ServerImagesRootPath;
            var fileOnDs = GetFileName(fileId);
            if (string.IsNullOrEmpty(fileOnDs)) return null;
            var pathOfFile = Path.Combine(_hostingEnvironment.WebRootPath, pathImages + "/FULL", fileOnDs);
            var img = System.Drawing.Image.FromFile(pathOfFile);
            return img;
        }

        public  void Remove(string fileId)
        {
            var items = StringUtil.SplitString(fileId);
            if(items == null) return;
            Remove(items);
            //var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerImagesRootPath);
            //var fileOnDs = GetFileNameAndRemove(fileId);
            //if (fileOnDs == null || string.IsNullOrEmpty(fileOnDs)) return;
            //var imgProc = new ImageProcess
            //{
            //    SavePath = pathImages
            //};
            //if (string.IsNullOrEmpty(fileOnDs)) return;
            //imgProc.RemovePic(fileOnDs);
        }

        public void Remove(string[] fileId)
        {
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerImagesRootPath);
            var fileOnDs = GetFileNameAndRemove(fileId);
            if (fileOnDs == null || !fileOnDs.Any()) return;
            var imgProc = new ImageProcess
            {
                SavePath = pathImages
            };
            foreach (var fileOnD in fileOnDs)
            {
                imgProc.RemovePic(fileOnD);
            }
            
        }

        public async Task<bool> RemoveAsync(string fileId)
        {
            var items = StringUtil.SplitString(fileId);
            if (items == null) return false;
            return await RemoveAsync(items);
            //var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerImagesRootPath);
            //var fileOnDs = await GetFileNameAndRemoveAsync(fileId);
            //if (fileOnDs == null || string.IsNullOrEmpty(fileOnDs)) return false;
            //var imgProc = new ImageProcess
            //{
            //    SavePath = pathImages
            //};
            //if (string.IsNullOrEmpty(fileOnDs)) return false;
            //imgProc.RemovePic(fileOnDs);
            //return true;
        }

        public async Task<bool> RemoveAsync(string[] fileId)
        {
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerImagesRootPath);
            var fileOnDs = await GetFileNameAndRemoveAsync(fileId);
            if (fileOnDs == null || !fileOnDs.Any()) return false;
            var imgProc = new ImageProcess
            {
                SavePath = pathImages
            };
            foreach (var fileOnD in fileOnDs)
            {
                imgProc.RemovePic(fileOnD);
            }
            return true;
        }

        public  byte[] ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
        public async Task<string> GetImageUrlAsync(string guid,ImgSize imgsize)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return await GetImageUrlAsync(baseGuid, imgsize);
        }
        public async Task<string> GetImageUrlAsync(Guid guid, ImgSize imgsize)
        {
            var result = await _baseFileService.GetFileNameAndFileNameOnDsAndFileTypeAsync(guid);
            return "/" + _settingsAppPathConfig.Value.ServerImagesRootPath + "/" + imgsize.ToString().ToLower() + "/" + result.Item2;
        }
    }
}