using System;
using System.Diagnostics;
using System.IO;

namespace StopService
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

        public void StopService(String ServiceName)
        {
            Console.WriteLine("Stopping Service {0}", ServiceName);
            Process p = new Process();
            p.StartInfo.FileName = this.NetFullPath;
            p.StartInfo.Arguments = "stop " + "\"" + ServiceName + "\"";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }
    }
}