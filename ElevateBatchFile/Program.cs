using System;
using System.Diagnostics;
using System.IO;

namespace ElevateBatchFile
{
    public class Program
    {
        public static void Main(String[] CommandLineArguments)
        {
            Process ProcessObj;
            FileInfo FileInfoObj;
            if (CommandLineArguments.Length == 1)
            {
                FileInfoObj = new FileInfo(CommandLineArguments[0]);
                if (!FileInfoObj.Exists)
                {
                    Console.WriteLine("The provided file does not exist");
                    Console.ReadKey();
                }
                else
                {
                    if ((FileInfoObj.Extension.ToLower().Equals(".bat") || FileInfoObj.Extension.ToLower().Equals(".cmd")))
                    {
                        using (ProcessObj = new Process())
                        {
                            ProcessObj.StartInfo.FileName = FileInfoObj.FullName;
                            ProcessObj.StartInfo.CreateNoWindow = true;
                            ProcessObj.Start();
                        }
                    }
                    else
                    {
                        Console.WriteLine("The file provided is not a .bat/.cmd file");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("ElevateBatchFile.exe " + "\"" + ".bat/.cmd file" + "\"");
                Console.ReadKey();
            }
        }
    }
}