using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace RemoveAllApps
{
    class AppsUtils
    {
        private readonly String PowerShellFullPath;
        private readonly String AppsToKeepPath;

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
            String AssemblyPath = Assembly.GetExecutingAssembly().Location.ToString();
            FileInfo AppsToKeepFileInfo = new FileInfo(AssemblyPath);
            this.AppsToKeepPath = CombinePath(AppsToKeepFileInfo.DirectoryName, new String[] { "AppsToKeep.txt" });
            Console.WriteLine("AppsToKeepPath Path Is {0}", this.AppsToKeepPath);
        }

        private List<String> ReadAppsToKeep()
        {
            StreamReader Sr = new StreamReader(this.AppsToKeepPath);
            List<String> Lines = new List<String>();
            String CurrentLine = Sr.ReadLine();
            Lines.Add(CurrentLine);
            while (CurrentLine != null)
            {
                CurrentLine = Sr.ReadLine();
                Lines.Add(CurrentLine);
            }
            return Lines;
        }

        private String BuildCommand(List<String> ModuleNames)
        {
            StringBuilder StringBuilderObj = new StringBuilder();
            StringBuilderObj.Append("-Command Get-AppxPackage -AllUsers ");
            foreach (String Module in ModuleNames)
            {
                StringBuilderObj.Append("| where-object {$_.name –notlike" + "\'" + Module + "\'" + "} ");
            }
            StringBuilderObj.Append("| Remove-AppxPackage");
            String Command = StringBuilderObj.ToString();
            StringBuilderObj.Clear();
            return Command;
        }

        public void DeleteAllApps()
        {
            FileInfo FileInfoObj = new FileInfo(this.AppsToKeepPath);
            if (FileInfoObj.Exists)
            {
                Process p = new Process();
                p.StartInfo.FileName = this.PowerShellFullPath;
                List<String> Modules = this.ReadAppsToKeep();
                String Arguments = this.BuildCommand(Modules);
                p.StartInfo.Arguments = Arguments;
                p.Start();
                p.WaitForExit();
                p.Dispose();
            }
            else
            {
                Console.WriteLine("File {0} + Not Found!", this.AppsToKeepPath);
            }
        }
    }
}