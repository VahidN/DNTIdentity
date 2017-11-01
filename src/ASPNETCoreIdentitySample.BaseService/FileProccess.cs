using System;
using System.Globalization;
using System.IO;

namespace ASPNETCoreIdentitySample.BaseService
{
    /// <summary>
    /// Summary description for FileProccess
    /// </summary>
    public class FileProccess
    {
        //Set
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public string FileContentType { get; set; }
        public long FileContentLength { get; set; }
        public Stream FileStreamFile { get; set; }
        public FileStream FileInputStream { get; set; }
        public Byte[] FileByte { get; set; }

        //Get
        public string SavePath { get; set; }
        private static readonly object Lock = new object();



        public void Proccess()
        {
            FileSize = GenerateFileSize();

        }

        #region Save To Database
        public byte[] GetFileForDb()
        {
            if (!IsValidFileType(FileContentType)) throw new Exception("پسوند فایل غیر مجاز می باشد");
            var br = new BinaryReader(FileStreamFile);
            Byte[] bytes = br.ReadBytes((Int32)FileStreamFile.Length);

            return bytes;
        }

        #endregion

        #region Save To Disk
        public string GetFileForDs(bool isSaveAsStream=false)
        {
            if (!IsValidFileType(FileContentType)) throw new Exception("فایل مجاز : JPG,PNG,GIF,BMP می باشند");
            CreateDirectoryIfMissing(SavePath);
            var name = CreateName() + FileContentType;

            if (isSaveAsStream)
            {
                var ms = new MemoryStream(FileByte);
                SaveFileToDisk(ms, SavePath, name);
            }
            else
            {
                SaveFileToDisk(FileByte, SavePath, name);
            }
            return name;
        }

        #endregion

        #region Save Different Thumbline To Disk
        public void SaveFileToDisk(byte[] file, string path, string name)
        {
            CreateDirectoryIfMissing(path);
            try
            {
                var ms = new MemoryStream(file);

                var pathStr = Path.Combine(path, name);

                SaveMemoryStream(ms, pathStr);

            }
            catch (Exception ex)
            {
                throw new Exception("خطا: در ذخیره فایل روی دیسک از نوع بایت");
            }
        }

        public MemoryStream GetMs(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms;
            }
        }

        public void SaveFileToDisk(MemoryStream ms, string path, string name)
        {
            CreateDirectoryIfMissing(path);
            try
            {
                path = Path.Combine(path, name);
                SaveMemoryStream(ms, path);
            }
            catch (Exception)
            {
                throw new Exception("خطا: در ذخیره فایل روی دیسک از نوع استریم");
            }
        }

        public static void SaveMemoryStream(MemoryStream ms, string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                ms.Seek(0, SeekOrigin.Begin);
                ms.CopyTo(fileStream);
            }
        }
        #endregion

        #region Remove From Disk
        public void Removefile(string fileName)
        {
            try
            {
                var pathOfFile = Path.Combine(SavePath, fileName);

                if (fileName == "noimage.jpg") return;

                if (File.Exists(pathOfFile))
                {
                    lock (Lock)
                    {
                        File.Delete(pathOfFile);
                    }
                }
                    

            }
            catch (Exception ex)
            {
                throw new Exception("خطا: در حذف فایل از دیسک", ex);
            }
        }
        #endregion

        #region Get Back File Size
        private string GenerateFileSize()
        {
            // ReSharper disable PossibleLossOfFraction
            float filesizekb = FileContentLength / 1024;
            float filesizemb = filesizekb / 1024;
            return (string.Format("{0:0.##}", filesizemb));
        }
        #endregion

        #region Check Directory is Missing Create
        public static void CreateDirectoryIfMissing(string path)
        {
            var folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        #endregion

        #region چک کردن پسوند فایل ها
        public static bool IsValidFileType(string fileType)
        {
            var fileExtensions = fileType;
            string[] validExtensions = { ".bmp", ".gif", ".png", ".jpg", ".jpeg", ".doc",
                ".docx", ".xls", ".xlsx", ".zip", ".rar", ".pdf", ".ppt", ".pptx", ".pps",
                ".ppsx", ".mp3", ".acc", ".mp4",".m4a", ".wmv" };
            return Array.IndexOf(validExtensions, fileExtensions) >= 0;
        }
        #endregion

        #region Generate Time + Random Name
        /// <summary>
        /// ساخت یک رشته با تاریخ،ساعت،دقیقه،ثانیه،میلی ثانیه و...
        /// </summary>
        internal string CreateName()
        {
            DateTime now = DateTime.Now;
            string sal = now.Year.ToString(CultureInfo.InvariantCulture);

            string mah = now.Month.ToString(CultureInfo.InvariantCulture);
            if (mah.Length < 2)
                mah = "0" + mah;

            string rooz = now.Day.ToString(CultureInfo.InvariantCulture);
            if (rooz.Length < 2)
                rooz = "0" + rooz;

            string saat = now.Hour.ToString(CultureInfo.InvariantCulture);
            if (saat.Length < 2)
                saat = "0" + saat;

            string daghighe = now.Minute.ToString(CultureInfo.InvariantCulture);
            if (daghighe.Length < 2)
                daghighe = "0" + daghighe;

            string sanie = now.Second.ToString(CultureInfo.InvariantCulture);
            if (sanie.Length < 2)
                sanie = "0" + sanie;

            string milisanie = now.Millisecond.ToString(CultureInfo.InvariantCulture);
            if (milisanie.Length < 2)
                milisanie = "0" + milisanie;

            string fileName = sal + mah + rooz + saat + daghighe + sanie + milisanie + Guid.NewGuid().ToString().Replace("-", string.Empty);

            return fileName;
        }
        #endregion
    }
}