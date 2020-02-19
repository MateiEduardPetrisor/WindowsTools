using System;
using System.Diagnostics;
using System.IO;

namespace StartService
{
    class ServiceUtils
    {
        private readonly String NetFullPath;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public ServiceUtils()
        {
            this.NetFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "net.exe" });
            Console.WriteLine("Net Path Is {0}", this.NetFullPath);
        }

        public void StartService(String ServiceName)
        {
            Console.WriteLine("Starting Service {0}",ServiceName);
            Process p = new Process();
            p.StartInfo.FileName = this.NetFullPath;
            p.StartInfo.Arguments = "start " + "\"" + ServiceName + "\"";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }
    }
}