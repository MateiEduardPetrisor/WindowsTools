using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DismUpdateInstaller
{
    class MsuFileUtils
    {
        private readonly String ExpandFullPath;
        private readonly String DismFullPath;
        private readonly String CmdFullPath;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public MsuFileUtils()
        {
            this.ExpandFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "expand.exe" });
            this.DismFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "dism.exe" });
            this.CmdFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "cmd.exe" });
            Console.WriteLine("Expand Path is {0}", this.ExpandFullPath);
            Console.WriteLine("Dism Path is {0}", this.DismFullPath);
            Console.WriteLine("Cmd Path is {0}", this.CmdFullPath);
        }

        private void ExtractMsuFile(String MsuFileFullPath)
        {
            FileInfo fileInfo = new FileInfo(MsuFileFullPath);
            if (fileInfo.Exists)
            {
                Console.WriteLine("Extracting msu file {0}", fileInfo.FullName);
                Process p = new Process();
                p.StartInfo.FileName = this.ExpandFullPath;
                p.StartInfo.Arguments = "-F:* " + "\"" + fileInfo.FullName + "\"" + " " + "\"" + fileInfo.Directory.FullName + "\"";
                p.Start();
                p.WaitForExit();
                p.Dispose();
            }
        }

        private void InstallSingleUpdate(String CabFileFullPath)
        {
            FileInfo fileInfo = new FileInfo(CabFileFullPath);
            if (fileInfo.Exists)
            {
                Console.WriteLine("Installing package file {0}", fileInfo.FullName);
                Process p = new Process();
                p.StartInfo.FileName = this.DismFullPath;
                p.StartInfo.Arguments = "/Online /Add-Package /PackagePath:" + "\"" + fileInfo.FullName + "\"" + " /NoRestart";
                p.Start();
                p.WaitForExit();
                p.Dispose();
            }
        }

        public void InstallUpdates(String WorkingDirectoryFullPath)
        {
            FileInfo FileInfoObj = new FileInfo(WorkingDirectoryFullPath);
            if (!FileInfoObj.Exists && FileInfoObj.Attributes.HasFlag(FileAttributes.Directory))
            {
                List<String> FileList = new List<String>(Directory.GetFiles(WorkingDirectoryFullPath, "*.msu", SearchOption.TopDirectoryOnly));
                FileList.Sort();
                Process p = new Process();
                p.StartInfo.FileName = "CheckWindowsVersion.exe";
                p.Start();
                p.WaitForExit();
                if (p.ExitCode == 11)
                {
                    Console.WriteLine("Widows 11 detected calling dism directly with msu package");
                    foreach (String fname in FileList)
                    {
                        Console.WriteLine("Calling dism with msu file {0}", fname);
                        this.InstallSingleUpdate(fname);
                    }
                }
                else
                {
                    Console.WriteLine("Windows 10 or older detected need to extract the msu package first");
                    foreach (String fname in FileList)
                    {
                        this.ExtractMsuFile(fname);
                        List<String> CabFileList = new List<String>(Directory.GetFiles(WorkingDirectoryFullPath, "*.cab", SearchOption.TopDirectoryOnly));
                        foreach (String CabFilePath in CabFileList)
                        {
                            if (!CabFilePath.ToLower().Contains("wsusscan.cab"))
                            {
                                Console.WriteLine("Calling dism with cab file {0}", CabFilePath);
                                this.InstallSingleUpdate(CabFilePath);
                            }
                        }
                    }
                }
            }
        }
    }
}
