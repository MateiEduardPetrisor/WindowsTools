using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DefragmentDisk
{
    class DefragUtils
    {
        private readonly String DefragFullPath;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public DefragUtils()
        {
            this.DefragFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "System32", "Defrag.exe" });
            Console.WriteLine("Defrag Path Is {0}", this.DefragFullPath);
        }

        public void DefragmentDisk(String[] DrivesToDefrag)
        {
            StringBuilder Sb = new StringBuilder();
            if (DrivesToDefrag != null)
            {
                String SystemDrive = Environment.GetEnvironmentVariable("SYSTEMDRIVE");
                Sb.Append(SystemDrive);
                Sb.Append(" ");
                foreach (String Drive in DrivesToDefrag)
                {
                    if (!Drive.Equals(SystemDrive.Remove(SystemDrive.Length - 1, 1)))
                    {
                        Sb.Append(Drive);
                        Sb.Append(":");
                        Sb.Append(" ");
                    }
                }
                Sb.Remove(Sb.Length - 1, 1);
            }
            else
            {
                Sb.Append(Environment.GetEnvironmentVariable("SYSTEMDRIVE"));
            }
            Console.WriteLine("Drives To Defragment Are {0}", Sb.ToString());
            Sb.Append(" /H /O /U /V");
            Process p = new Process();
            p.StartInfo.FileName = this.DefragFullPath;
            p.StartInfo.Arguments = Sb.ToString();
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }
    }
}