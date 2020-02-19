using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateDirectory
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                DirectoyUtils DirectoyUtilsObj = new DirectoyUtils();
                DirectoyUtilsObj.CreateDirectory(args[0]);
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("CreateDirectory.exe " + "\"" + "Directory To Create" + "\"");
                Console.ReadKey();
            }
        }
    }
}
