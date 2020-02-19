using System;

namespace DeleteDirectory
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                DirectoyUtils DirectoyUtilsObj = new DirectoyUtils();
                DirectoyUtilsObj.DeleteDirectory(args[0]);
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("DeleteDirectory.exe " + "\"" + "Directory To Delete" + "\"");
                Console.ReadKey();
            }
        }
    }
}