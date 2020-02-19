using System;

namespace RemoveFromPath
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                PathUtils PathUtilsObj = new PathUtils();
                PathUtilsObj.RemoveFromPath(args[0].ToLower());
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("RemoveFromPath.exe " + "\"" + "PathToRemove" + "\"");
                Console.ReadKey();
            }
        }
    }
}