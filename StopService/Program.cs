using System;

namespace StopService
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                ServiceUtils ServiceUtilsObj = new ServiceUtils();
                ServiceUtilsObj.StopService(args[0]);
            }
            else
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("StopService.exe " + "\"" + "Service To Stop" + "\"");
                Console.ReadKey();
            }
        }
    }
}