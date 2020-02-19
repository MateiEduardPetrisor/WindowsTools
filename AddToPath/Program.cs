using System;

namespace AddToPath
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                PathUtils PathUtilsObj = new PathUtils();
                PathUtilsObj.AddToPath(args[0]);
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("AddToPath.exe " + "\"" + "PathToAdd" + "\"");
                Console.ReadKey();
            }
        }
    }
}