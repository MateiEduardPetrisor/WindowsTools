using System;

namespace CheckArchType
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                CheckApplicationArchitectureType CheckApplicationArchitectureTypeObj = new CheckApplicationArchitectureType();
                CheckApplicationArchitectureTypeObj.CheckAchitectureType(args[0]);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("CheckArchType.exe " + "\"" + "ApplicationFullPath" + "\"");
                Console.ReadKey();
            }
        }
    }
}