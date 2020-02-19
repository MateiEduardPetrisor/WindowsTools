using System;
using System.Linq;
using System.IO;
using System.Security.Cryptography;

namespace ShuffleCopyFiles
{
    class FilesShuffler
    {
        private readonly String SourceFullPath;
        private readonly String DestinationFullPath;

        public FilesShuffler(String SourceFullPth, String DestinationFullPth)
        {
            this.SourceFullPath = SourceFullPth;
            this.DestinationFullPath = DestinationFullPth;
            Console.WriteLine("Soruce Folder Is {0}", this.SourceFullPath);
            Console.WriteLine("Destination Folder Is {0}", this.DestinationFullPath);
            DirectoryInfo SourceDirInfo = new DirectoryInfo(this.SourceFullPath);
            DirectoryInfo DestinationDirInfo = new DirectoryInfo(this.DestinationFullPath);
            if (SourceDirInfo.Exists)
            {
                if (!DestinationDirInfo.Exists)
                {
                    Console.WriteLine("Create Directory {0}", this.DestinationFullPath);
                    Directory.CreateDirectory(this.DestinationFullPath);
                }
            }
            else
            {
                Console.WriteLine("Source Directory {0} Doesn't Exist!", this.SourceFullPath);
            }
        }

        private String[] ShuffleFileList()
        {
            String[] FilesArrayObj = Directory.GetFiles(SourceFullPath, "*.mp3", SearchOption.AllDirectories);
            RNGCryptoServiceProvider RngProvider = new RNGCryptoServiceProvider();
            Int32 ArraySize = FilesArrayObj.Count();
            Console.WriteLine("Found {0} .mp3 Files", ArraySize);
            while (ArraySize > 1)
            {
                Byte[] ByteArrayObj = new Byte[4];
                do
                {
                    RngProvider.GetBytes(ByteArrayObj);
                }
                while (!(BitConverter.ToInt32(ByteArrayObj, 0) < ArraySize * (Int32.MaxValue / ArraySize)));
                Int32 NewIndex = BitConverter.ToInt32(ByteArrayObj, 0) % ArraySize;
                if (NewIndex <= 0)
                {
                    NewIndex *= (-1);
                }
                --ArraySize;
                String TempFileName = FilesArrayObj[NewIndex];
                FilesArrayObj[NewIndex] = FilesArrayObj[ArraySize];
                FilesArrayObj[ArraySize] = TempFileName;
            }
            RngProvider.Dispose();
            Console.WriteLine("FilesArray Shuffled!");
            return FilesArrayObj;
        }

        public void CopyFiles(Boolean Overwrite)
        {
            String[] FileArrayObj = this.ShuffleFileList();
            if (FileArrayObj != null)
            {
                for (Int32 i = 0; i < FileArrayObj.Count(); i++)
                {
                    Console.WriteLine("Copy {0} File {1} To {2}", i + 1, FileArrayObj.ElementAt(i), this.DestinationFullPath);
                    FileInfo FileInfoObj = new FileInfo(FileArrayObj.ElementAt(i));
                    if (FileInfoObj.Exists)
                    {
                        File.Copy(FileArrayObj.ElementAt(i), Path.Combine(this.DestinationFullPath, FileInfoObj.Name), Overwrite);
                    }
                    else
                    {
                        Console.WriteLine("File {0} Doesn't Exist!", FileArrayObj.ElementAt(i));
                    }
                }
                Console.WriteLine("Copy Of Files Completed!");
            }
            else
            {
                Console.WriteLine("FilesArray Is Null Nothing To Copy!");
            }
        }
    }
}