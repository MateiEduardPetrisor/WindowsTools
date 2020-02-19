using System;

namespace WusaUpdateInstaller
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                try
                {
                    WusaUtils WusaUtilsObj = new WusaUtils();
                    WusaUtilsObj.InstallUpdates(args[0]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("WusaUpdateInstaller.exe " + "\"" + "Folder With Msu Files Inside" + "\"");
                Console.ReadKey();
            }
        }
    }
}