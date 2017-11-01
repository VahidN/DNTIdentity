using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using ASPNETCoreIdentitySample.BaseService.Contracts;

namespace ASPNETCoreIdentitySample.BaseService
{
    /// <summary>
    ///     Summary description for CompressHelper
    /// </summary>
    public class BaseCompressHelper : IBaseCompressHelper
    {

        ////create With Input FileNames
        //    CompressHelper.AddFileToArchive_InputByte(new CompressHelper.ZipItem[]{ new CompressHelper.ZipItem( @"E:\b\1.jpg",@"images\1.jpg"),
        //        new CompressHelper.ZipItem(@"E:\b\2.txt",@"text\2.txt")}, @"C:\test.zip");

        //    //create with input stream
        //    CompressHelper.AddFileToArchive_InputByte(new CompressHelper.ZipItem[]{ new CompressHelper.ZipItem(File.ReadAllBytes( @"E:\b\1.jpg"),@"images\1.jpg"),
        //        new CompressHelper.ZipItem(File.ReadAllBytes(@"E:\b\2.txt"),@"text\2.txt")}, @"C:\test.zip");

        //    //Create Archive And Return StreamZipFile
        //    var getStreamZipFile = CompressHelper.AddFileToArchive(new CompressHelper.ZipItem[]{ new CompressHelper.ZipItem( @"E:\b\1.jpg",@"images\1.jpg"),
        //        new CompressHelper.ZipItem(@"E:\b\2.txt",@"text\2.txt")});


        //    //Extract in memory
        //    CompressHelper.ZipItem[] ListitemsWithBytes = CompressHelper.ExtractItems(@"C:\test.zip");

        //    //Choese Files For Extract To memory
        //    var listFileNameForExtract = new List<string>(new string[] { @"images\1.jpg", @"text\2.txt" });
        //    ListitemsWithBytes = CompressHelper.ExtractItems(@"C:\test.zip", listFileNameForExtract);


        //    // Choese Files For Extract To Directory
        //    CompressHelper.ExtractItems(@"C:\test.zip", listFileNameForExtract, "c:\\extractFiles");

        public void AddFileToArchive(List<ZipItem> zipItems, string seveToFile)
        {
            var memoryStream = new MemoryStream();

            //Create Empty Archive
            var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

            foreach (var item in zipItems)
            {
                //Create Path File in Archive
                var fileInArchive = archive.CreateEntry(item.PathinArchive);


                //Open File in Archive For Write
                var openFileInArchive = fileInArchive.Open();

                //Read Stream
                var fsReader = new FileStream(item.FileNameSource, FileMode.Open, FileAccess.Read);

                var readAllbytes = new byte[4096]; //Capcity buffer
                while (fsReader.Position != fsReader.Length)
                {
                    //Read Bytes
                    var readByte = fsReader.Read(readAllbytes, 0, readAllbytes.Length);

                    //Write Bytes
                    openFileInArchive.Write(readAllbytes, 0, readByte);
                }
                fsReader.Dispose();
                openFileInArchive.Dispose();
            }
            archive.Dispose();

            using (var fileStream = new FileStream(seveToFile, FileMode.Create))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.CopyTo(fileStream);
            }
        }

        public MemoryStream AddFileToArchive(List<ZipItem> zipItems)
        {
            var memoryStream = new MemoryStream();

            //Create Empty Archive
            var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

            foreach (var item in zipItems)
            {
                //Create Path File in Archive
                var fileInArchive = archive.CreateEntry(item.PathinArchive);


                //Open File in Archive For Write
                var openFileInArchive = fileInArchive.Open();

                //Read Stream
                var fsReader = new FileStream(item.FileNameSource, FileMode.Open, FileAccess.Read);

                var readAllbytes = new byte[4096]; //Capcity buffer
                while (fsReader.Position != fsReader.Length)
                {
                    //Read Bytes
                    var readByte = fsReader.Read(readAllbytes, 0, readAllbytes.Length);

                    //Write Bytes
                    openFileInArchive.Write(readAllbytes, 0, readByte);
                }
                fsReader.Dispose();
                openFileInArchive.Dispose();
            }
            archive.Dispose();

            return memoryStream;
        }

        public void AddFileToArchive_InputByte(List<ZipItem> zipItems, string seveToFile)
        {
            var memoryStream = new MemoryStream();

            //Create Empty Archive
            var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

            foreach (var item in zipItems)
            {
                //Create Path File in Archive
                var fileInArchive = archive.CreateEntry(item.PathinArchive);


                //Open File in Archive For Write
                var openFileInArchive = fileInArchive.Open();

                //Read Stream
                //  FileStream fsReader = new FileStream(item.FileNameSource, FileMode.Open, FileAccess.Read);

                var readAllbytes = new byte[4096]; //Capcity buffer
                var readByte = 4096;
                var totalWrite = 0;
                while (totalWrite != item.Bytes.Length)
                {
                    if (totalWrite + 4096 > item.Bytes.Length)
                        readByte = item.Bytes.Length - totalWrite;


                    Array.Copy(item.Bytes, totalWrite, readAllbytes, 0, readByte);


                    //Write Bytes
                    openFileInArchive.Write(readAllbytes, 0, readByte);
                    totalWrite += readByte;
                }

                openFileInArchive.Dispose();
            }
            archive.Dispose();

            using (var fileStream = new FileStream(seveToFile, FileMode.Create))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.CopyTo(fileStream);
            }
        }

        public MemoryStream AddFileToArchive_InputByte(List<ZipItem> zipItems)
        {
            var memoryStream = new MemoryStream();

            //Create Empty Archive
            var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

            foreach (var item in zipItems)
            {
                //Create Path File in Archive
                var fileInArchive = archive.CreateEntry(item.PathinArchive);


                //Open File in Archive For Write
                var openFileInArchive = fileInArchive.Open();

                //Read Stream
                //  FileStream fsReader = new FileStream(item.FileNameSource, FileMode.Open, FileAccess.Read);

                var readAllbytes = new byte[4096]; //Capcity buffer
                var readByte = 4096;
                var totalWrite = 0;
                while (totalWrite != item.Bytes.Length)
                {
                    if (totalWrite + 4096 > item.Bytes.Length)
                        readByte = item.Bytes.Length - totalWrite;


                    Array.Copy(item.Bytes, totalWrite, readAllbytes, 0, readByte);


                    //Write Bytes
                    openFileInArchive.Write(readAllbytes, 0, readByte);
                    totalWrite += readByte;
                }

                openFileInArchive.Dispose();
            }
            archive.Dispose();

            return memoryStream;
        }

        public void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
        {
            //Opens the zip file up to be read
            using (var archive = ZipFile.OpenRead(sourceArchiveFileName))
            {
                if (Directory.Exists(destinationDirectoryName) == false)
                    Directory.CreateDirectory(destinationDirectoryName);

                //Loops through each file in the zip file
                archive.ExtractToDirectory(destinationDirectoryName);
            }
        }

        public void ExtractItems(string sourceArchiveFileName, List<string> pathFilesinArchive,
            string destinationDirectoryName)
        {
            //Opens the zip file up to be read
            using (var archive = ZipFile.OpenRead(sourceArchiveFileName))
            {
                //Loops through each file in the zip file
                foreach (var file in archive.Entries)
                {
                    var posResult = pathFilesinArchive.IndexOf(file.FullName);
                    if (posResult != -1)
                    {
                        //Create Folder
                        if (
                            Directory.Exists(destinationDirectoryName + "\\" +
                                             Path.GetDirectoryName(pathFilesinArchive[posResult])) == false)
                            Directory.CreateDirectory(destinationDirectoryName + "\\" +
                                                      Path.GetDirectoryName(pathFilesinArchive[posResult]));

                        var openFileGetBytes = file.Open();

                        var fileStreamOutput =
                            new FileStream(destinationDirectoryName + "\\" + pathFilesinArchive[posResult], FileMode.Create);

                        var readAllbytes = new byte[4096]; //Capcity buffer
                        var totalRead = 0;
                        while (totalRead != file.Length)
                        {
                            //Read Bytes
                            var readByte = openFileGetBytes.Read(readAllbytes, 0, readAllbytes.Length);
                            totalRead += readByte;

                            //Write Bytes
                            fileStreamOutput.Write(readAllbytes, 0, readByte);
                        }

                        fileStreamOutput.Dispose();
                        openFileGetBytes.Dispose();


                        pathFilesinArchive.RemoveAt(posResult);
                    }

                    if (pathFilesinArchive.Count == 0)
                        break;
                }
            }
        }

        public List<ZipItem> ExtractItems(string sourceArchiveFileName)
        {
            var zipItemsReading = new List<ZipItem>();
            //Opens the zip file up to be read
            using (var archive = ZipFile.OpenRead(sourceArchiveFileName))
            {
                //Loops through each file in the zip file
                foreach (var file in archive.Entries)
                {
                    var openFileGetBytes = file.Open();

                    var memstreams = new MemoryStream();
                    var readAllbytes = new byte[4096]; //Capcity buffer
                    var totalRead = 0;
                    while (totalRead != file.Length)
                    {
                        //Read Bytes
                        var readByte = openFileGetBytes.Read(readAllbytes, 0, readAllbytes.Length);
                        totalRead += readByte;

                        //Write Bytes
                        memstreams.Write(readAllbytes, 0, readByte);
                    }

                    memstreams.Position = 0;
                    openFileGetBytes.Dispose();
                    memstreams.Dispose();

                    zipItemsReading.Add(new ZipItem(memstreams.ToArray(), file.FullName));
                }
            }

            return zipItemsReading;
        }

        public List<ZipItem> ExtractItems(string sourceArchiveFileName, List<string> pathFilesinArchive)
        {
            var zipItemsReading = new List<ZipItem>();
            //Opens the zip file up to be read
            using (var archive = ZipFile.OpenRead(sourceArchiveFileName))
            {
                //Loops through each file in the zip file
                foreach (var file in archive.Entries)
                {
                    var posResult = pathFilesinArchive.IndexOf(file.FullName);
                    if (posResult != -1)
                    {
                        var openFileGetBytes = file.Open();

                        var memstreams = new MemoryStream();
                        var readAllbytes = new byte[4096]; //Capcity buffer
                        var totalRead = 0;
                        while (totalRead != file.Length)
                        {
                            //Read Bytes
                            var readByte = openFileGetBytes.Read(readAllbytes, 0, readAllbytes.Length);
                            totalRead += readByte;

                            //Write Bytes
                            memstreams.Write(readAllbytes, 0, readByte);
                        }

                        //Create item
                        zipItemsReading.Add(new ZipItem(memstreams.ToArray(), file.FullName));

                        openFileGetBytes.Dispose();
                        memstreams.Dispose();


                        pathFilesinArchive.RemoveAt(posResult);
                    }

                    if (pathFilesinArchive.Count == 0)
                        break;
                }
            }

            return zipItemsReading;
        }

        public struct ZipItem
        {
            private readonly string _fileNameSource;
            private byte[] _bytes;
            private string _pathinArchive;

            public ZipItem(string fileNameSource, string pathinArchive)
            {
                _bytes = null;
                _fileNameSource = fileNameSource;
                _pathinArchive = pathinArchive;
            }

            public ZipItem(byte[] bytes, string pathinArchive)
            {
                _bytes = bytes;
                _fileNameSource = "";
                _pathinArchive = pathinArchive;
            }

            public string FileNameSource
            {
                set
                {
                    if (value == null) throw new ArgumentNullException("value");
                    FileNameSource = value;
                }
                get { return _fileNameSource; }
            }

            public string PathinArchive
            {
                set { _pathinArchive = value; }
                get { return _pathinArchive; }
            }

            public byte[] Bytes
            {
                set { _bytes = value; }
                get { return _bytes; }
            }
        }
    }
}