using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CheckArchType
{
    class CheckApplicationArchitectureType
    {
        private readonly String X64_7Z;
        private readonly String X86_7Z;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public CheckApplicationArchitectureType()
        {
            this.X64_7Z = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "7z", "x64", "7z.exe" });
            this.X86_7Z = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "7z", "x86", "7z.exe" });
            Console.WriteLine("7z x64 Path Is {0}", this.X64_7Z);
            Console.WriteLine("7z x86 Path Is {0}.", this.X86_7Z);
        }

        public void CheckAchitectureType(String EXE_PATH)
        {
            String AppDetails;
            if (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").Equals("AMD64"))
            {
                if (File.Exists(this.X64_7Z))
                {
                    if (File.Exists(EXE_PATH))
                    {
                        AppDetails = this.Check(this.X64_7Z, EXE_PATH);
                        Console.WriteLine("Application {0} details:\n {1}", EXE_PATH, AppDetails);
                    }
                    else
                    {
                        Console.WriteLine("Argument Provided Is Invalid. Provide A Full Path Like: " + "\"" + "C:\\Projects\\application.exe" + "\"");
                    }
                }
                else
                {
                    Console.WriteLine("7z.exe Not Found. Download It And Place In .\\7z\\x64 For 64 Bit OS");
                }
            }
            else
            {
                if (File.Exists(X86_7Z))
                {
                    if (File.Exists(EXE_PATH))
                    {
                        AppDetails = this.Check(this.X86_7Z, EXE_PATH);
                        Console.WriteLine("Application {0} Details:\n {1}", EXE_PATH, AppDetails);
                    }
                    else
                    {
                        Console.WriteLine("Argument Provided Is Invalid. Provide A Full Path Like: " + "\"" + "C:\\Projects\\application.exe" + "\"");
                    }
                }
                else
                {
                    Console.WriteLine("7z.exe Not Found. Download It And Place In .\\7z\\x86 For 32 Bit OS");
                }
            }
        }

        private String Check(String PATH_7Z, String EXE_PATH)
        {
            Console.WriteLine("Using 7z From {0}", PATH_7Z);
            Process p = new Process();
            p.StartInfo.FileName = PATH_7Z;
            p.StartInfo.Arguments = "l " + "\"" + EXE_PATH + "\"" + " |findstr CPU";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            String result = p.StandardOutput.ReadToEnd();
            p.Dispose();
            return result;
        }
    }
}