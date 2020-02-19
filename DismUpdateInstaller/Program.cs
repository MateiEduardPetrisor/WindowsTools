using System;

namespace DismUpdateInstaller
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                try
                {
                    MsuFileUtils MsuFileUtilsObj = new MsuFileUtils();
                    MsuFileUtilsObj.InstallUpdates(args[0]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("DismUpdateInstaller.exe " + "\"" + "Folder With Msu Files Inside" + "\"");
                Console.ReadKey();
            }
        }
    }
}