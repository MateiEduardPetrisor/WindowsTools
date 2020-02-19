using System;

namespace DefragmentDisk
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 0)
            {
                DefragUtils DefragUtilsObj = new DefragUtils();
                DefragUtilsObj.DefragmentDisk(null);
            }
            else
            {
                String[] Drives = new String[args.Length];
                for (int IndexArgs = 0; IndexArgs < args.Length; IndexArgs++)
                {
                    if (args[IndexArgs].Length == 1)
                    {
                        Drives[IndexArgs] = args[IndexArgs];
                    }
                    else
                    {
                        Console.WriteLine("Usage");
                        Console.WriteLine("DefragmentDisk.exe");
                        Console.WriteLine("DefragmentDisk.exe" + " C" + " D");
                        Console.ReadKey();
                        return;
                    }
                }
                DefragUtils DefragUtilsObj = new DefragUtils();
                DefragUtilsObj.DefragmentDisk(Drives);
            }
        }
    }
}