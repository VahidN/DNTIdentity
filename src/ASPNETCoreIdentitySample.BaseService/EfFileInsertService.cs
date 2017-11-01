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
using ASPNETCoreIdentitySample.Common.StringToolKit;

namespace ASPNETCoreIdentitySample.BaseService
{
    public class EfFileInsertService : IFileInsertService
    {
        private readonly IOptionsSnapshot<SiteSettings> _settingsAppPathConfig;
        private readonly IBaseCompressHelper _baseCompressHelper;
        private readonly IBaseFileService _baseFileService;
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EfFileInsertService(IOptionsSnapshot<SiteSettings> settingsAppPathConfig,
            IBaseCompressHelper baseCompressHelper,
            IBaseFileService baseFileService,
            IUnitOfWork uow,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment)
        {
            _settingsAppPathConfig = settingsAppPathConfig;
            _baseFileService = baseFileService;
            _baseCompressHelper = baseCompressHelper;
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
        /// <param name="isCompress"></param>
        /// <returns></returns>
        public  string InsertsAndReturnIdsAsString(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false,bool isCompress = false)
        {
            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return string.Empty;
                
                if (httpPostedFileBases.Count > 1)
                {
                    var intlist = Inserts(httpPostedFileBases, fileId, isUpdate, isCompress);
                    return GetBackInsertsIds(intlist);
                }
                else
                {
                    var oneid = InsertWithoutFileUpload(httpPostedFileBases.First());
                    return oneid;
                }
        }

        public string InsertsAndReturnIdsAsString(IList<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false, bool isCompress = false)
        {
            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return string.Empty;

            if (httpPostedFileBases.Count > 1)
            {
                var intlist = Inserts(httpPostedFileBases, fileId, isUpdate, isCompress);
                return GetBackInsertsIds(intlist);
            }
            else
            {
                var oneid = InsertWithoutFileUpload(httpPostedFileBases.First());
                return oneid;
            }
        }

        public async Task<string> InsertsAndReturnIdsAsStringAsync(IList<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false,
            bool isCompress = false)
        {
            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return string.Empty;

            if (httpPostedFileBases.Count > 1)
            {
                var intlist = await InsertsAsync(httpPostedFileBases, fileId, isUpdate, isCompress);
                return GetBackInsertsIds(intlist);
            }
            else
            {
                var oneid = await InsertWithoutFileUploadAsync(httpPostedFileBases.First());
                return oneid;
            }
        }

        public async Task<Tuple<string, List<string>>> InsertsAndReturnIdsAsStringReturnListAsync(IList<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false,
            bool isCompress = false)
        {
            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return new Tuple<string, List<string>>(string.Empty, null);

            if (httpPostedFileBases.Count > 1)
            {
                var intlist = await InsertsAsync(httpPostedFileBases, fileId, isUpdate, isCompress);
                return new Tuple<string, List<string>>(GetBackInsertsIds(intlist), intlist);
            }
            else
            {
                var oneid = await InsertWithoutFileUploadAsync(httpPostedFileBases.First());
                return new Tuple<string, List<string>>(oneid, null);
            }
        }

        public Tuple<string,List<string>> InsertsAndReturnIdsAsStringReturnList(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false, bool isCompress = false)
        {
            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return new Tuple<string, List<string>>(string.Empty,null);

            if (httpPostedFileBases.Count > 1)
            {
                var intlist = Inserts(httpPostedFileBases, fileId, isUpdate, isCompress);
                return new Tuple<string, List<string>>(GetBackInsertsIds(intlist), intlist);
            }
            else
            {
                var oneid = InsertWithoutFileUpload(httpPostedFileBases.First());
                return new Tuple<string, List<string>>(oneid,null);
            }
        }

        public Tuple<string, List<string>> InsertsAndReturnIdsAsStringReturnList(IList<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false, bool isCompress = false)
        {
            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return new Tuple<string, List<string>>(string.Empty, null);

            if (httpPostedFileBases.Count > 1)
            {
                var intlist = Inserts(httpPostedFileBases, fileId, isUpdate, isCompress);
                return new Tuple<string, List<string>>(GetBackInsertsIds(intlist), intlist);
            }
            else
            {
                var oneid = InsertWithoutFileUpload(httpPostedFileBases.First());
                return new Tuple<string, List<string>>(oneid, null);
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

        public  Tuple<string,Guid> InsertAndArchivedWithbaseFilesGuids(List<Guid> basefileGuids, List<string> basefileNames = null)
        {
            var listOfFile = _baseFileService.GetFileNameAndFileNameOnDsList(basefileGuids);
            var pathFiles = _settingsAppPathConfig.Value.ServerFilesRootPath;
            var serverPathFiles = Path.Combine(_hostingEnvironment.WebRootPath, pathFiles);
            var filesForArchiveList = listOfFile.Select(p => new BaseCompressHelper.ZipItem(Path.Combine(serverPathFiles, p.FileOnDs), p.FileName)).ToList();
            var zipFileResult = _baseCompressHelper.AddFileToArchive(filesForArchiveList);
            return InsertZip(zipFileResult);

        }

        public  List<string> Inserts(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false, bool isCompress = false)
        {
            var listOfFile = new List<string>();
            if (isCompress)
            {
                var filesForArchiveList = uploadfiles.Select(p => new BaseCompressHelper.ZipItem(ReadFully(p.OpenReadStream()), p.FileName)).ToList();
                var zipFileResult = _baseCompressHelper.AddFileToArchive_InputByte(filesForArchiveList);
                
                listOfFile.Add(InsertZip(zipFileResult).Item1);
            }
            else
            {
                var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
                if (uploadfiles == null || httpPostedFileBases.First() == null) return listOfFile;
                listOfFile.AddRange(httpPostedFileBases.Where(httpPostedFileBase => httpPostedFileBase != null)
                    .Select(httpPostedFileBase => InsertWithoutFileUpload(httpPostedFileBase)));
            }
            

            return listOfFile;
        }


        public async Task<List<string>> InsertsAsync(IEnumerable<IFormFile> uploadfiles, string fileId = null, bool isUpdate = false, bool isCompress = false)
        {
            var listOfFile = new List<string>();
            if (isCompress)
            {
                var filesForArchiveList = uploadfiles.Select(p => new BaseCompressHelper.ZipItem(ReadFully(p.OpenReadStream()), p.FileName)).ToList();
                var zipFileResult = _baseCompressHelper.AddFileToArchive_InputByte(filesForArchiveList);
                var filesResult = await InsertZipAsync(zipFileResult);
                listOfFile.Add(filesResult.Item1);
            }
            else
            {
                var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
                if (uploadfiles == null || httpPostedFileBases.First() == null) return listOfFile;

                foreach (var httpPostedFileBase in httpPostedFileBases)
                {
                    if (httpPostedFileBase != null)
                        listOfFile.Add(await InsertWithoutFileUploadAsync(httpPostedFileBase));
                }
            }


            return listOfFile;
        }

        public async Task<Tuple<string, Guid>> InsertZipAsync(Stream fileBytes, string fileId = null, bool isUpdate = false)
        {
            var pathFiles = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            const string extension = ".zip";
            var newGuid = Guid.NewGuid();
            var filebyte = ReadFully(fileBytes);
            var proc = new FileProccess
            {
                FileByte = filebyte,
                FileInputStream = null,
                FileContentLength = filebyte.Length,
                FileStreamFile = fileBytes,
                FilePath = newGuid.ToString(),
                FileName = newGuid.ToString(),
                FileContentType = extension == string.Empty ? string.Empty : extension,
                SavePath = pathFiles
            };

            proc.Proccess();
            var photoName = proc.GetFileForDs(true);

            if (isUpdate)
            {
                //Remove Old Image From Disk
                await RemoveAsync(fileId);
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                 ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                 : null;
            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = proc.FileName,
                FileContentType = proc.FileContentType,
                FileSize = proc.FileSize,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Files,
                FileOnDs = photoName
            };

            _baseFileService.Insert(filebase);
            return new Tuple<string, Guid>(newGuid.ToString(), newGuid);
        }

        public  Tuple<string, Guid> InsertZip(Stream fileBytes, string fileId = null, bool isUpdate = false)
        {
            var pathFiles = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            const string extension = ".zip";
            var newGuid = Guid.NewGuid();
            var filebyte = ReadFully(fileBytes);
            var proc = new FileProccess
            {
                FileByte = filebyte,
                FileInputStream = null,
                FileContentLength = filebyte.Length,
                FileStreamFile = fileBytes,
                FilePath = newGuid.ToString(),
                FileName = newGuid.ToString(),
                FileContentType = extension == string.Empty ? string.Empty : extension,
                SavePath = pathFiles
            };

            proc.Proccess();
            var photoName = proc.GetFileForDs(true);

            if (isUpdate)
            {
                //Remove Old Image From Disk
                Remove(fileId);
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated 
                 ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                 : null ;
            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = proc.FileName,
                FileContentType = proc.FileContentType,
                FileSize = proc.FileSize,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Files,
                FileOnDs = photoName
            };

            _baseFileService.Insert(filebase);
            _uow.SaveChanges();
            return new Tuple<string, Guid>(newGuid.ToString(),newGuid);
        }

        public Tuple<string, Guid,string> InsertStream(Stream fileBytes, string fileExtension, string fileId = null, bool isUpdate = false)
        {
            var pathFiles = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            var extension = fileExtension;
            var newGuid = Guid.NewGuid();
            var filebyte = ReadFully(fileBytes);
            var proc = new FileProccess
            {
                FileByte = filebyte,
                FileInputStream = null,
                FileContentLength = filebyte.Length,
                FileStreamFile = fileBytes,
                FilePath = newGuid.ToString(),
                FileName = newGuid.ToString(),
                FileContentType = extension == string.Empty ? string.Empty : extension,
                SavePath = pathFiles
            };

            proc.Proccess();
            var fileName = proc.GetFileForDs(true);

            if (isUpdate)
            {
                //Remove Old Image From Disk
                Remove(fileId);
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                 ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                 : null;

            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = proc.FileName,
                FileContentType = proc.FileContentType,
                FileSize = proc.FileSize,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Files,
                FileOnDs = fileName
            };

            _baseFileService.Insert(filebase);
            _uow.SaveChanges();
            return new Tuple<string, Guid, string>(newGuid.ToString(), newGuid, fileName);
        }

        public async Task<Tuple<string, Guid, string>> InsertStreamAsync(Stream fileBytes, string fileExtension, string fileId = null, bool isUpdate = false,bool isSave = false)
        {
            var pathFiles = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            var extension = fileExtension;
            var newGuid = Guid.NewGuid();
            var filebyte = ReadFully(fileBytes);
            var proc = new FileProccess
            {
                FileByte = filebyte,
                FileInputStream = null,
                FileContentLength = filebyte.Length,
                FileStreamFile = fileBytes,
                FilePath = newGuid.ToString(),
                FileName = newGuid.ToString(),
                FileContentType = extension == string.Empty ? string.Empty : extension,
                SavePath = pathFiles
            };

            proc.Proccess();
            var fileName = proc.GetFileForDs(true);

            if (isUpdate)
            {
                //Remove Old Image From Disk
                await RemoveAsync(fileId);
            }

            var userId = _httpContextAccessor.HttpContext != null ?( _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                 ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                 : null) : null;

            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = proc.FileName,
                FileContentType = proc.FileContentType,
                FileSize = proc.FileSize,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Files,
                FileOnDs = fileName
            };

            _baseFileService.Insert(filebase);
            if (isSave)
                await _uow.SaveChangesAsync();
            return new Tuple<string, Guid, string>(newGuid.ToString(), newGuid, fileName);
        }
        public async Task<string> InsertWithoutFileUploadAsync(IFormFile fileBytes, string fileId = null, bool isUpdate = false)
        {
            var pathFiles = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            var extension = System.IO.Path.GetExtension(fileBytes.FileName);
            var newGuid = Guid.NewGuid();
            var fileBytesStream = fileBytes.OpenReadStream();
            var proc = new FileProccess
            {
                FileByte = ReadFully(fileBytesStream),
                FileInputStream = null,
                FileContentLength = fileBytes.Length,
                FileStreamFile = fileBytesStream,
                FilePath = fileBytes.FileName,
                FileName = System.IO.Path.GetFileName(fileBytes.FileName),
                FileContentType = extension == string.Empty ? string.Empty : extension,
                SavePath = pathFiles
            };

            proc.Proccess();
            var photoName = proc.GetFileForDs(true);

            if (isUpdate)
            {
                //Remove Old Image From Disk
               await  RemoveAsync(fileId);
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                 ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                 : null;

            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = proc.FileName,
                FileContentType = proc.FileContentType,
                FileSize = proc.FileSize,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Files,
                FileOnDs = photoName
            };

            _baseFileService.Insert(filebase);

            return newGuid.ToString();
        }
        public  string InsertWithoutFileUpload(IFormFile fileBytes, string fileId = null, bool isUpdate = false)
        {
            var pathFiles = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            var extension = System.IO.Path.GetExtension(fileBytes.FileName);
            var newGuid = Guid.NewGuid();
            var fileBytesStream = fileBytes.OpenReadStream();
            var proc = new FileProccess
            {
                FileByte = ReadFully(fileBytesStream),
                FileInputStream = null,
                FileContentLength = fileBytes.Length,
                FileStreamFile = fileBytesStream,
                FilePath = fileBytes.FileName,
                FileName = System.IO.Path.GetFileName(fileBytes.FileName),
                FileContentType = extension == string.Empty ? string.Empty : extension,
                SavePath = pathFiles
            };

            proc.Proccess();
            var photoName = proc.GetFileForDs(true);

            if (isUpdate)
            {
                //Remove Old Image From Disk
                Remove(fileId);
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                 ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                 : null;

            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = proc.FileName,
                FileContentType = proc.FileContentType,
                FileSize = proc.FileSize,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Files,
                FileOnDs = photoName
            };

            _baseFileService.Insert(filebase);
            _uow.SaveChanges();

            return newGuid.ToString();
        }

        public async Task<string> InsertAsync(IFormFile fileUpload, string fileId = null, bool isUpdate = false,bool isSave=false)
        {
            var pathFiles = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            var newGuid = Guid.NewGuid();
            var fileBytesStream = fileUpload.OpenReadStream();
            var proc = new FileProccess
            {
                FileByte = ReadFully(fileBytesStream),
                FileInputStream = null,
                FileContentLength = fileUpload.Length,
                FileStreamFile = fileUpload.OpenReadStream(),
                FilePath = fileUpload.FileName,
                FileName = System.IO.Path.GetFileName(fileUpload.FileName),
                FileContentType = System.IO.Path.GetExtension(fileUpload.FileName).ToLower(),
                SavePath = pathFiles
            };

            proc.Proccess();
            var fileName = proc.GetFileForDs();

            if (isUpdate)
            {
                //Remove Old Image From Disk
                await RemoveAsync(fileId);
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                : null;

            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = proc.FileName,
                FileContentType = proc.FileContentType,
                FileSize = proc.FileSize,
                FileOnDs = fileName,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Files
            };

            _baseFileService.Insert(filebase);

            if (isSave)
              await _uow.SaveChangesAsync();

            return newGuid.ToString();
        }

        public async Task<Tuple<string,string,List<Tuple<string, string>>>> InsertsAndGetGuidsAndFileNamesOnDsAsync(IList<IFormFile> uploadfiles, string[] fileIds = null, bool isUpdate = false, bool isSave = false)
        {
            var httpPostedFileBases = uploadfiles as IList<IFormFile> ?? uploadfiles.ToList();
            if (uploadfiles == null || httpPostedFileBases.First() == null) return new Tuple<string, string, List<Tuple<string, string>>>(string.Empty, string.Empty, null);

            
            if (httpPostedFileBases.Count > 1)
            {
                var lst = new List<Tuple<string, string>>();
                var i = 0;
                foreach (var item in httpPostedFileBases)
                {
                    var result = await InsertAndGetGuidAndFileNameOnDsAsync(item, fileIds == null ? null : fileIds[i], isUpdate);
                    lst.Add(new Tuple<string, string>(result.Item1, result.Item2));
                    i++;
                }

                return new Tuple<string, string, List<Tuple<string, string>>>(GetBackInsertsIds(lst.Select(p=>p.Item1).ToList()), GetBackInsertsIds(lst.Select(p => p.Item2).ToList()), lst);
            }
            else
            {
                var oneid = await InsertAndGetGuidAndFileNameOnDsAsync(httpPostedFileBases.First(), fileIds == null ? null : fileIds[0], isUpdate);
                return new Tuple<string, string, List<Tuple<string, string>>>(oneid.Item1, oneid.Item2, null);
            }
        }

        public async Task<Tuple<string,string>> InsertAndGetGuidAndFileNameOnDsAsync(IFormFile fileUpload, string fileId = null, bool isUpdate = false, bool isSave = false)
        {
            var pathFiles = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            var newGuid = Guid.NewGuid();
            var fileBytesStream = fileUpload.OpenReadStream();
            var proc = new FileProccess
            {
                FileByte = ReadFully(fileBytesStream),
                FileInputStream = null,
                FileContentLength = fileUpload.Length,
                FileStreamFile = fileUpload.OpenReadStream(),
                FilePath = fileUpload.FileName,
                FileName = System.IO.Path.GetFileName(fileUpload.FileName),
                FileContentType = System.IO.Path.GetExtension(fileUpload.FileName).ToLower(),
                SavePath = pathFiles
            };

            proc.Proccess();
            var fileName = proc.GetFileForDs();

            if (isUpdate)
            {
                //Remove Old Image From Disk
                await RemoveAsync(fileId);
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                : null;

            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = proc.FileName,
                FileContentType = proc.FileContentType,
                FileSize = proc.FileSize,
                FileOnDs = fileName,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Files
            };

            _baseFileService.Insert(filebase);

            if (isSave)
                await _uow.SaveChangesAsync();

            return new Tuple<string, string>(newGuid.ToString(), filebase.FileOnDs);
        }

        public  string Insert(IFormFile fileUpload, string fileId = null, bool isUpdate = false)
        {
            var pathFiles = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            var newGuid = Guid.NewGuid();
            var fileBytesStream = fileUpload.OpenReadStream();
            var proc = new FileProccess
            {
                FileByte = ReadFully(fileBytesStream),
                FileInputStream = null,
                FileContentLength = fileUpload.Length,
                FileStreamFile = fileUpload.OpenReadStream(),
                FilePath = fileUpload.FileName,
                FileName = System.IO.Path.GetFileName(fileUpload.FileName),
                FileContentType = System.IO.Path.GetExtension(fileUpload.FileName).ToLower(),
                SavePath = pathFiles
            };

            proc.Proccess();
            var fileName = proc.GetFileForDs();

            if (isUpdate)
            {
                //Remove Old Image From Disk
                Remove(fileId);
            }

            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated 
                ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value 
                : null;

            var filebase = new BaseFile
            {
                FileGuid = newGuid,
                FileName = proc.FileName,
                FileContentType = proc.FileContentType,
                FileSize = proc.FileSize,
                FileOnDs = fileName,
                FileTypeFilter = ChooseFileTypeForUploadBaseFile.Files
            };

            _baseFileService.Insert(filebase);
            _uow.SaveChanges();

            return newGuid.ToString();
        }

        public  string GetFileName(int fileId)
        {
            var c = _baseFileService.GetFileName(fileId);
            return c;
        }

        public  string GetFileName(string fileId)
        {
            var c = _baseFileService.GetFileName(fileId);
            return c;
        }

        public string GetFileNameAndRemove(string fileId)
        {
            return _baseFileService.GetFileNameOnDsAndRemove(fileId);
        }

        public string GetFileNameOnDs(int fileId)
        {
            var c = _baseFileService.GetFileNameOnDs(fileId);
            return c;
        }
        public  string GetFileNameOnDs(string fileId)
        {
            var c = _baseFileService.GetFileNameOnDs(fileId);
            return c;
        }
        public  string GetFileSize(int fileId)
        {
            var c = _baseFileService.GetFileSize(fileId);
            return c;
        }
        public  string GetFileSize(string fileId)
        {
            var c = _baseFileService.GetFileSize(fileId);
            return c;
        }


        public  void Remove(int fileId, bool isSaveChanges = false)
        {
            var c = _baseFileService.GetById(fileId);
            var pathFiles = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            if (c == null) return;
            var proc = new FileProccess
            {
                SavePath = pathFiles
            };
            if (string.IsNullOrEmpty(c.FileOnDs)) return;
            proc.Removefile(c.FileOnDs);
                if (isSaveChanges)
                    _uow.SaveChanges();
        }

        public  void Remove(string fileId, bool isSaveChanges = false)
        {
            var fileIds = StringUtil.SplitString('|', fileId);
            var pathFiles = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            foreach (var id in fileIds)
            {
                var fileOnDs = GetFileNameAndRemove(id);
                if (fileOnDs == null) return;
                var proc = new FileProccess
                {
                    SavePath = pathFiles
                };
                if (string.IsNullOrEmpty(fileOnDs)) return;
                proc.Removefile(fileOnDs);
                if (isSaveChanges)
                    _uow.SaveChanges();
            }
            
        }

        public async Task<bool> RemoveAsync(string fileId,bool isSaveChanges=false)
        {
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            var fileOnDs = await GetFileNameAndRemoveAsync(fileId);
            if (string.IsNullOrEmpty(fileOnDs)) return false;
            var imgProc = new FileProccess
            {
                SavePath = pathImages
            };
            if (string.IsNullOrEmpty(fileOnDs)) return false;
            imgProc.Removefile(fileOnDs);
            if (isSaveChanges)
                await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRangeAsync(string[] fileIds, bool isSaveChanges = false,bool deleteFromDisk=true)
        {
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            var fileOnDs = await GetFileNameAndRemoveRangeAsync(fileIds);
            if (fileOnDs == null || !fileOnDs.Any()) return false;
            var imgProc = new FileProccess
            {
                SavePath = pathImages
            };
            if (fileOnDs == null || !fileOnDs.Any()) return false;
            if(deleteFromDisk)
            {
                foreach (var item in fileOnDs)
                {
                    imgProc.Removefile(item);
                }
            }
            if (isSaveChanges)
                await _uow.SaveChangesAsync();
            return true;
        }

        public bool RemoveRange(string[] fileIds, bool isSaveChanges = false, bool deleteFromDisk = true)
        {
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            var fileOnDs = GetFileNameAndRemoveRangeAsync(fileIds).Result;
            if (fileOnDs == null || !fileOnDs.Any()) return false;
            var imgProc = new FileProccess
            {
                SavePath = pathImages
            };
            if (fileOnDs == null || !fileOnDs.Any()) return false;
            if (deleteFromDisk)
            {
                foreach (var item in fileOnDs)
                {
                    imgProc.Removefile(item);
                }
            }
            if (isSaveChanges)
                _uow.SaveChanges();
            return true;
        }

        public bool RemoveRange(string[] fileIds)
        {
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            var fileOnDs = GetFileNameAndRemoveRangeAsync(fileIds).Result;
            if (fileOnDs == null || !fileOnDs.Any()) return false;
            var imgProc = new FileProccess
            {
                SavePath = pathImages
            };
            if (fileOnDs == null || !fileOnDs.Any()) return false;

            foreach (var item in fileOnDs)
            {
                imgProc.Removefile(item);
            }
            _uow.SaveChanges();
            return true;
        }

        public bool RemoveOnDiskWithFileNameOnDisk(string onDiskFileName)
        {
            if (string.IsNullOrEmpty(onDiskFileName)) return false;
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            
            var imgProc = new FileProccess
            {
                SavePath = pathImages
            };
            if (string.IsNullOrEmpty(onDiskFileName)) return false;
            imgProc.Removefile(onDiskFileName);
            return true;
        }
        
        public bool RemoveOnDiskWithFileNameOnDisk(string[] onDiskFileNames)
        {
            if (onDiskFileNames ==null || !onDiskFileNames.Any()) return false;
            var pathImages = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);

            var imgProc = new FileProccess
            {
                SavePath = pathImages
            };
            if (onDiskFileNames == null || !onDiskFileNames.Any()) return false;
            foreach(var item in onDiskFileNames)
            {
                imgProc.Removefile(item);
            }
            return true;
        }

        public async Task<string> GetFileNameAndRemoveAsync(string fileId)
        {
            return await _baseFileService.GetFileNameOnDsAndRemoveAsync(fileId);
        }

        public async Task<List<string>> GetFileNameAndRemoveRangeAsync(string[] fileIds)
        {
            return await _baseFileService.GetFileNameOnDsAndRemoveAsync(fileIds);
        }

        public  byte[] ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public  byte[] ReadFileFromDs(string fileOnDs)
        {
            var pathFiles = _settingsAppPathConfig.Value.ServerFilesRootPath;
            var file = Path.Combine(_hostingEnvironment.WebRootPath ,pathFiles, fileOnDs);
            return File.ReadAllBytes(file);
        }
        public async Task<string> GetFileUrlAsync(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return await GetFileUrlAsync(baseGuid);
        }

        public async Task<string> GetFileUrlAsync(Guid guid)
        {
            var result = await _baseFileService.GetFileNameAndFileNameOnDsAndFileTypeAsync(guid);
            return "/" + _settingsAppPathConfig.Value.ServerFilesRootPath + "/" + result.Item2;
        }

        public async Task<List<string>> GetFilesUrlAsync(List<string> guids)
        {
            var baseGuids = StringUtil.ConvertStringsToGuids(guids.ToArray());
            return await GetFilesUrlAsync(baseGuids.ToList());
        }
        public async Task<List<string>> GetFilesUrlAsync(List<Guid> guids)
        {
            var list = new List<string>();
            var result = await _baseFileService.GetAllFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(guids);
            if (result == null) return list;
            var basePath = _settingsAppPathConfig.Value.ServerFilesRootPath;
            foreach (var item in result)
            {
                list.Add("/" + basePath + "/" + item.Item2);
            }
            return list;
        }

        public async Task<List<Tuple<string, string>>> GetFilesGuidAndUrlAsync(List<string> guids)
        {
            var baseGuids = StringUtil.ConvertStringsToGuids(guids.ToArray());
            return await GetFilesGuidAndUrlAsync(baseGuids.ToList());
        }
        public async Task<List<Tuple<string, string>>> GetFilesGuidAndUrlAsync(List<Guid> guids)
        {
            var list = new List<Tuple<string, string>>();
            var result = await _baseFileService.GetAllFileNameOnDsAsync(guids);
            if (result == null) return list;
            var basePath = _settingsAppPathConfig.Value.ServerFilesRootPath;
            foreach (var item in result)
            {
                list.Add(new Tuple<string, string>(item.Item1, "/" + basePath + "/" + item.Item2));
            }
            return list;
        }

        public async Task<List<Tuple<string, string>>> GetFilesGuidAndPhysicalUrlAsync(List<string> guids)
        {
            var baseGuids = StringUtil.ConvertStringsToGuids(guids.ToArray());
            return await GetFilesGuidAndPhysicalUrlAsync(baseGuids.ToList());
        }
        public async Task<List<Tuple<string, string>>> GetFilesGuidAndPhysicalUrlAsync(List<Guid> guids)
        {
            var list = new List<Tuple<string, string>>();
            var result = await _baseFileService.GetAllFileNameOnDsAsync(guids);
            if (result == null) return list;
            var basePath = Path.Combine(_hostingEnvironment.WebRootPath, _settingsAppPathConfig.Value.ServerFilesRootPath);
            foreach (var item in result)
            {
                list.Add(new Tuple<string, string>(item.Item1, Path.Combine(basePath, item.Item2)));
            }
            return list;
        }

        public async Task<Tuple<string, string>> GetFileUrlAndSizeAsync(string guid)
        {
            var baseGuid = StringUtil.ConvertStringToGuid(guid);
            return await GetFileUrlAndSizeAsync(baseGuid);
        }
        public async Task<Tuple<string,string>> GetFileUrlAndSizeAsync(Guid guid)
        {
            var result = await _baseFileService.GetFileNameAndFileNameOnDsAndFileTypeAndFileSizeAsync(guid);
            return new Tuple<string, string>("/" + _settingsAppPathConfig.Value.ServerFilesRootPath + "/" + result.Item2, result.Item4);
        }

        
    }
}