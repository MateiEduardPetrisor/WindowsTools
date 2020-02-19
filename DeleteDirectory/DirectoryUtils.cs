using System;
using System.Diagnostics;
using System.IO;

namespace DeleteDirectory
{
    class DirectoyUtils
    {
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

        public DirectoyUtils()
        {
            this.CmdFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "cmd.exe" });
            Console.WriteLine("Cmd Path Is {0}", this.CmdFullPath);
        }

        public void DeleteDirectory(String FolderToDelete)
        {
            Console.WriteLine("Delete Directory {0}", FolderToDelete);
            Process p = new Process();
            p.StartInfo.FileName = this.CmdFullPath;
            p.StartInfo.Arguments = "/c if exist " + "\"" + FolderToDelete + "\"" + " (rmdir / S / Q " + "\"" + FolderToDelete + "\"" + ") else (echo Folder " + "\"" + FolderToDelete + "\"" + " Not Found!)";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }
    }
}