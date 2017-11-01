using System;
using System.Globalization;
using System.IO;

namespace ASPNETCoreIdentitySample.BaseService
{
    /// <summary>
    /// Insert : Pass FileUpload and Root SavePath and IsDeferentSize for SaveToDisk (True Or False) Then Run Proccess() : Return byte[] by GetFileForDb() , Return fileName by GetFileForDs()
    /// Remove : Pass FileName And Root SavePath Then Run Remove()
    /// </summary>
    public class ImageProcess
    {
        //Set 
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public string FileContentType { get; set; }
        public long FileContentLength { get; set; }
        public Stream FileStreamImg { get; set; }
        public Byte[] FileByte { get; set; }

        //Get
        public bool IsDeferentSize { get; set; }
        public string SavePath { get; set; }

        public void Proccess()
        {
            
            FileSize = GenerateFileSize();
            
        }

        #region Save To Database
        public byte[] GetFileForDb()
        {
            if (!IsValidImageType(FileContentType)) throw new Exception("تصاویر مجاز : JPG,PNG,GIF,BMP می باشند");
            var br = new BinaryReader(FileStreamImg);
            var bytes = br.ReadBytes((Int32)FileStreamImg.Length);

            return bytes;
        }

        #endregion

        #region Save To Disk
        public string GetFileForDs()
        {
            if (!IsValidImageType(FileContentType)) throw new Exception("تصاویر مجاز : JPG,PNG,GIF,BMP می باشند");
            CreateIfMissing(SavePath);
            var name = CreateName() + FileContentType;
            var sourceImage = FileByte;
            var imageFormat = sourceImage.FindImageFormat();
            if (IsDeferentSize)
            {
                ////I
                //var i =
                //    sourceImage.ResizeImageFile(24, imageFormat);
                //SaveImageToDisk(i, Path.Combine(SavePath , @"i"), name);
                ////XXS
                //var xxs =
                //    sourceImage.ResizeImageFile(30, imageFormat);
                //SaveImageToDisk(xxs, Path.Combine(SavePath, @"xxs"), name);

                ////XS
                //var xs =
                //    sourceImage.ResizeImageFile(50, imageFormat);
                //SaveImageToDisk(xs, Path.Combine(SavePath, @"xs"), name);

                //S
                var s =
                    sourceImage.ResizeImageFile(150, imageFormat);
                SaveImageToDisk(s, Path.Combine(SavePath, @"s"), name);

                //M
                var m =
                    sourceImage.ResizeImageFile(198, imageFormat);
                SaveImageToDisk(m, Path.Combine(SavePath ,  @"m"), name);

                ////L
                //var l =
                //    sourceImage.ResizeImageFile(500, imageFormat);
                //SaveImageToDisk(l, Path.Combine(SavePath ,  @"l"), name);

                ////XL
                //var xl =
                //    sourceImage.ResizeImageFile(1200, imageFormat);
                //SaveImageToDisk(xl, Path.Combine(SavePath ,  @"xl"), name);

                ////XXL
                //var xxl =
                //    sourceImage.ResizeImageFile(1800, imageFormat);
                //SaveImageToDisk(xxl, Path.Combine(SavePath ,  @"xxl"), name);

                //Full
                var full = sourceImage;
                SaveImageToDisk(full, Path.Combine(SavePath ,  @"full"), name);
                return name;
            }
            else
            {
                //Full
                var full = sourceImage;
                SaveImageToDisk(full, Path.Combine(SavePath ,  @"full"), name);
                return name;
            }
        }

        #endregion

        #region Remove From Disk
        public void RemovePic(string fileName)
        {
            var pathorg = SavePath;

            if (fileName == "noimage.jpg") return;

            //if (File.Exists(Path.Combine(pathorg, @"i" ,fileName)))
            //    File.Delete(Path.Combine(pathorg, @"i", fileName));

            if (File.Exists(Path.Combine(pathorg, @"s", fileName)))
                File.Delete(Path.Combine(pathorg, @"s", fileName));

            if (File.Exists(Path.Combine(pathorg, @"m", fileName)))
                File.Delete(Path.Combine(pathorg, @"m", fileName));

            //if (File.Exists(Path.Combine(pathorg, @"l", fileName)))
            //    File.Delete(Path.Combine(pathorg, @"l", fileName));

            //if (File.Exists(Path.Combine(pathorg, @"xl", fileName)))
            //    File.Delete(Path.Combine(pathorg, @"xl", fileName));

            //if (File.Exists(Path.Combine(pathorg, @"xxl", fileName)))
            //    File.Delete(Path.Combine(pathorg, @"xxl", fileName));

            if (File.Exists(Path.Combine(pathorg, @"full", fileName)))
                File.Delete(Path.Combine(pathorg, @"full", fileName));
            

        }
        #endregion

        #region Save Different Thumbline To Disk
        private void SaveImageToDisk(byte[] file, string path, string name)
        {
            CreateIfMissing(path);
            try
            {
                var ms = new MemoryStream(file);
                path = Path.Combine(path, name);
                var imga = System.Drawing.Image.FromStream(ms);
                imga.Save(path, ImageHelper.FindImageFormat(imga));
                //img.Save(path + name, ImageFormat.Jpeg);
            }
            catch (Exception)
            {
                    
                throw;
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
        private void CreateIfMissing(string path)
        {
            var folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        #endregion

        #region Convert Byte[] To Image(system.Drawing)
        private System.Drawing.Image ConvertByteToImg(byte[] file)
        {
            if (file != null)
            {
                System.Drawing.Image newImage;
                using (var stream = new MemoryStream(file))
                {
                    
                    using (newImage = System.Drawing.Image.FromStream(stream))
                    {
                        newImage.Save(FileName);
                    }
                }

                return newImage;
            }

            else
            {
                throw new Exception("تبدیل تصویر byte[] به image با مشکل مواجه شد");
            }
        }
        #endregion

        #region Checked ContentType Images
        private bool IsValidImageType(string fileType)
        {
            var fileExtensions = fileType;
            string[] validExtensions = { ".bmp", ".gif", ".png", ".jpg", ".jpeg" };
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