using System;
using System.Diagnostics;
using System.IO;

namespace DismCleanUpResetBase
{
    class DismUtils
    {
        private readonly String DismFullPath;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public DismUtils()
        {
            this.DismFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "dism.exe" });
            Console.WriteLine("Dism Path Is {0}", this.DismFullPath);
        }

        public void CleanUpResetBase()
        {
            Console.WriteLine("Cleanup WinSxS ResetBase");
            Process p = new Process();
            p.StartInfo.FileName = this.DismFullPath;
            p.StartInfo.Arguments = "/online /Cleanup-Image /StartComponentCleanup /ResetBase";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }
    }
}