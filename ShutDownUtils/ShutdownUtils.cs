using System;
using System.Diagnostics;
using System.IO;

namespace ShutDown
{
    class ShutDownUtils
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

        public ShutDownUtils()
        {
            this.ShutdownFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "shutdown.exe" });
            Console.WriteLine("Shutdown Path Is {0}", this.ShutdownFullPath);
        }

        public void Restart()
        {
            Console.WriteLine("Restart System");
            Process p = new Process();
            p.StartInfo.FileName = this.ShutdownFullPath;
            p.StartInfo.Arguments = "/R /F /T 00";
            p.Start();
            p.WaitForExit();
            p.Dispose();
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

        public void Hibernate()
        {
            Console.WriteLine("Hibernate System");
            Process p = new Process();
            p.StartInfo.FileName = this.ShutdownFullPath;
            p.StartInfo.Arguments = "/H";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        public void ShutDown()
        {
            Console.WriteLine("Shutdown System");
            Process p = new Process();
            p.StartInfo.FileName = this.ShutdownFullPath;
            p.StartInfo.Arguments = "/S /F /T 00";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }
    }
}