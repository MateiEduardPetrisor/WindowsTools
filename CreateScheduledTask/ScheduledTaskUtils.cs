using System;
using System.Diagnostics;
using System.IO;

namespace CreateScheduledTask
{
    class ScheduledTaskUtils
    {
        private readonly String SchtasksFullPath;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public ScheduledTaskUtils()
        {
            this.SchtasksFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "schtasks.exe" });
            Console.WriteLine("Schtasks Path Is {0}", this.SchtasksFullPath);
        }

        public void CreateScheduledTask(String TaskName, String FileToRun)
        {
            Console.WriteLine("Create Scheduled Task {0}", TaskName);
            Process p = new Process();
            p.StartInfo.FileName = this.SchtasksFullPath;
            p.StartInfo.Arguments = "/Create /TN " + "\"" + TaskName + "\"" + " /TR " + "\"" + FileToRun + "\"" + " /SC ONLOGON /RL HIGHEST" + " /F";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }
    }
}