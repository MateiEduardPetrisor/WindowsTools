using System;
using System.Diagnostics;
using System.IO;

namespace LogOff
{
    class LogOffUtils
    {
        private readonly String ShutdownFullPath;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public LogOffUtils()
        {
            this.ShutdownFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "shutdown.exe" });
            Console.WriteLine("Shutdown Path Is {0}", this.ShutdownFullPath);
        }

        public void LogOff()
        {
            Console.WriteLine("Log Off");
            Process p = new Process();
            p.StartInfo.FileName = this.ShutdownFullPath;
            p.StartInfo.Arguments = "/L /F";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }
    }
}