using System;
using System.Diagnostics;
using System.IO;

namespace KillProcess
{
    class ProcessKiller
    {
        private readonly String TaskKillFullPath;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public ProcessKiller()
        {
            this.TaskKillFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "taskkill.exe" });
            Console.WriteLine("Taskkill Path Is {0}",this.TaskKillFullPath);
        }

        public void KillProcess(String ProcessName)
        {
            Process p = new Process();
            p.StartInfo.FileName = this.TaskKillFullPath;
            p.StartInfo.Arguments = "/IM " + "\"" + ProcessName + "\"" + " /F";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }
    }
}