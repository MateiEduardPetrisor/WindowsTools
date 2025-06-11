using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WusaUpdateInstaller
{
    class WusaUtils
    {
        private readonly String WusaFullPath;
        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public WusaUtils()
        {
            this.WusaFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "wusa.exe" });
            Console.WriteLine("Wusa Path Is {0}", this.WusaFullPath);
        }

        private void InstallSingleUpdate(String UpdateFullPath)
        {
            FileInfo FileInfoObj = new FileInfo(UpdateFullPath);
            if (FileInfoObj.Exists)
            {
                Console.WriteLine("Installing Msu File {0}", FileInfoObj.FullName);
                StringBuilder Sb = new StringBuilder();
                Sb.Append(UpdateFullPath);
                Sb.Append(" /quiet /norestart");
                Process p = new Process();
                p.StartInfo.FileName = this.WusaFullPath;
                p.StartInfo.Arguments = Sb.ToString();
                Sb.Clear();
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
                foreach (String MsuFile in FileList)
                {
                    this.InstallSingleUpdate(MsuFile);
                }
            }
        }
    }
}
