using System;

namespace StartService
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                ServiceUtils ServiceUtilsObj = new ServiceUtils();
                ServiceUtilsObj.StartService(args[0]);
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("StartService.exe " + "\"" + "Service To Start" + "\"");
                Console.ReadKey();
            }
        }
    }
}