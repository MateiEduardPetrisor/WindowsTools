using System;
using System.Diagnostics;
using System.IO;

namespace EjectHdd
{
    public class DiskUtils
    {
        private readonly String diskpartFullPath;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public DiskUtils()
        {
            this.diskpartFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "diskpart.exe" });
        }

        public void DiskOperation(int DiskIndex, String Command)
        {
            Process p = new Process();
            p.StartInfo.FileName = this.diskpartFullPath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.RedirectStandardInput = true;
            p.Start();
            p.StandardInput.WriteLine("select disk " + (DiskIndex - 1));
            if (Command.Equals("Online"))
            {
                p.StandardInput.WriteLine("online disk");
            }
            else
            {
                p.StandardInput.WriteLine("offline disk");
            }
            p.StandardInput.WriteLine("exit");
            p.WaitForExit();
            p.Dispose();
        }
    }
}