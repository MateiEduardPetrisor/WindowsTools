using System;
using System.Diagnostics;
using System.IO;

namespace DeleteScheduledTask
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

        public void DeleteScheduledTask(String TaskName)
        {
            Console.WriteLine("Delete Scheduled Task {0}", TaskName);
            Process p = new Process();
            p.StartInfo.FileName = this.SchtasksFullPath;
            p.StartInfo.Arguments = "/Delete /TN " + "\"" + TaskName + "\"" + " /F";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }
    }
}