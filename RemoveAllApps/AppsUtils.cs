using System;
using System.Diagnostics;
using System.IO;

namespace RemoveAllApps
{
    class AppsUtils
    {
        private readonly String PowerShellFullPath;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public AppsUtils()
        {
            this.PowerShellFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "System32", "WindowsPowerShell", "v1.0", "powershell.exe" });
            Console.WriteLine("Powershell Path Is {0}", this.PowerShellFullPath);
        }

        public void DeleteAllApps()
        {
            Process p = new Process();
            p.StartInfo.FileName = this.PowerShellFullPath;
            p.StartInfo.Arguments = "-Command Get-AppxPackage -AllUsers | where-object {$_.name –notlike '*store*' } | where-object {$_.name –notlike '*edge*' } | Remove-AppxPackage";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }
    }
}