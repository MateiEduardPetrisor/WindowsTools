using System;
using System.IO;

namespace CreateScheduledTask
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 2)
            {
                ScheduledTaskUtils ScheduledTaskUtilsObj = new ScheduledTaskUtils();
                FileInfo FileInfoObj = new FileInfo(args[1]);
                if (FileInfoObj.Exists)
                {
                    if (FileInfoObj.Extension.Equals(".exe") || FileInfoObj.Extension.Equals(".cmd") || FileInfoObj.Extension.Equals(".bat"))
                    {
                        ScheduledTaskUtilsObj.CreateScheduledTask(args[0], args[1]);
                    }
                    else
                    {
                        Console.WriteLine("The File Provided Is Not .exe .bat .cmd");
                        Console.ReadKey();
                    }

                }
                else
                {
                    Console.WriteLine("The Provided Path Is Not A File Or Is A Directory");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("CreateScheduledTask.exe " + "\"" + "Task Name" + "\"" + " " + "\"" + "File To Run" + "\"");
                Console.ReadKey();
            }
        }
    }
}