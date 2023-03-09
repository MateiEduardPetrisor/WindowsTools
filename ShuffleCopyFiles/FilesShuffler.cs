using System;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace ShuffleCopyFiles
{
    class FilesShuffler
    {
        private readonly String SourceFullPath;
        private readonly String DestinationFullPath;
        private readonly Int32 FilesPerFolder;

        public FilesShuffler(String SourceFullPth, String DestinationFullPth, Int32 FilesPerFolder)
        {
            this.SourceFullPath = SourceFullPth;
            this.DestinationFullPath = DestinationFullPth;
            this.FilesPerFolder = FilesPerFolder;

            Console.WriteLine("Soruce Folder Is {0}", this.SourceFullPath);
            Console.WriteLine("Destination Folder Is {0}", this.DestinationFullPath);
            Console.WriteLine("FilesPerFolder Is {0}", this.FilesPerFolder);
        }

        private bool CheckIfFolderExist(String FullPath)
        {
            DirectoryInfo FolderToCheck = new DirectoryInfo(FullPath);
            if (FolderToCheck.Exists)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Direcory {0} does not exist", FolderToCheck.FullName);
                return false;
            }
        }

        private Int32 GetNumberOfFilesInSourceFolder()
        {
            Int32 NumberOfFiles = 0;
            if (this.CheckIfFolderExist(this.SourceFullPath))
            {
                NumberOfFiles = Directory.GetFiles(SourceFullPath, "*.mp3", SearchOption.AllDirectories).Count();
            }
            return NumberOfFiles;
        }

        private String[] GetFileArray()
        {
            String[] FilesArrayObj = null;
            if (this.CheckIfFolderExist(this.SourceFullPath))
            {
                FilesArrayObj = Directory.GetFiles(SourceFullPath, "*.mp3", SearchOption.AllDirectories);
            }
            return FilesArrayObj;
        }

        private String[] ShuffleFileList()
        {
            Int32 NumberOfFiles = this.GetNumberOfFilesInSourceFolder();
            String[] FilesArrayObj = this.GetFileArray();
            RNGCryptoServiceProvider RngProvider = new RNGCryptoServiceProvider();
            Console.WriteLine("Found {0} .mp3 Files", NumberOfFiles);
            while (NumberOfFiles > 1)
            {
                Byte[] ByteArrayObj = new Byte[4];
                do
                {
                    RngProvider.GetBytes(ByteArrayObj);
                }
                while (!(BitConverter.ToInt32(ByteArrayObj, 0) < NumberOfFiles * (Int32.MaxValue / NumberOfFiles)));
                Int32 NewIndex = BitConverter.ToInt32(ByteArrayObj, 0) % NumberOfFiles;
                if (NewIndex <= 0)
                {
                    NewIndex *= (-1);
                }
                --NumberOfFiles;
                String TempFileName = FilesArrayObj[NewIndex];
                FilesArrayObj[NewIndex] = FilesArrayObj[NumberOfFiles];
                FilesArrayObj[NumberOfFiles] = TempFileName;
            }
            RngProvider.Dispose();
            Console.WriteLine("FilesArray Shuffled!");
            return FilesArrayObj;
        }

        private List<String> CreateFolders()
        {
            List<String> Folders = new List<String>();
            Int32 NumberOfFoldersToCreate = 0;
            if (this.GetNumberOfFilesInSourceFolder() % this.FilesPerFolder != 0)
            {
                NumberOfFoldersToCreate++;
            }
            NumberOfFoldersToCreate += this.GetNumberOfFilesInSourceFolder() / this.FilesPerFolder;
            if (!this.CheckIfFolderExist(this.DestinationFullPath))
            {
                for (Int32 i = 1; i <= NumberOfFoldersToCreate; i++)
                {
                    String NewDirectoryFullPath = this.DestinationFullPath + "_" + i;
                    Directory.CreateDirectory(NewDirectoryFullPath);
                    Folders.Add(NewDirectoryFullPath);
                }
            }
            return Folders;
        }

        public void CopyFiles()
        {
            List<String> Folders = CreateFolders();
            String[] FileArrayObj = this.ShuffleFileList();
            Int32 IndexCurrentFolder = 0;
            Int32 NumberOfCopiedFiles = 0;
            while (IndexCurrentFolder < Folders.Count)
            {
                if (FileArrayObj != null)
                {
                    for (Int32 IndexCurrentFile = 0; IndexCurrentFile < FileArrayObj.Count(); IndexCurrentFile++)
                    {
                        Console.WriteLine("Copy {0} File {1} To {2}", IndexCurrentFile + 1, FileArrayObj.ElementAt(IndexCurrentFile), Folders.ElementAt(IndexCurrentFolder));
                        FileInfo FileInfoObj = new FileInfo(FileArrayObj.ElementAt(IndexCurrentFile));
                        if (FileInfoObj.Exists)
                        {
                            FileInfo FileInfoDest = new FileInfo(Path.Combine(Folders.ElementAt(IndexCurrentFolder), FileInfoObj.Name));
                            if (FileInfoDest.Exists)
                            {
                                FileInfoDest.Attributes = FileAttributes.Normal;
                            }
                            File.Copy(FileArrayObj.ElementAt(IndexCurrentFile), Path.Combine(Folders.ElementAt(IndexCurrentFolder), FileInfoObj.Name), true);
                            NumberOfCopiedFiles++;

                            if (NumberOfCopiedFiles == this.FilesPerFolder)
                            {
                                IndexCurrentFolder += 1;
                                NumberOfCopiedFiles = 0;
                            }
                            else if (IndexCurrentFile == FileArrayObj.Count() - 1)
                            {
                                NumberOfCopiedFiles = 0;
                                IndexCurrentFolder += 1;
                            }
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
}
