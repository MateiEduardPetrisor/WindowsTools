using System;
using System.IO;

namespace Sysprep
{
    class Program
    {
        static void Main(String[] args)
        {
            try
            {
                SysprepUtils SysprepUtilsObj = new SysprepUtils();
                SysprepUtilsObj.GeneralizeSystemOOBE();
            }
            catch (ArgumentException ExceptionObj)
            {
                Console.WriteLine(ExceptionObj.Message);
                Console.ReadKey();
            }
            catch (FileNotFoundException ExceptionObj)
            {
                Console.WriteLine(ExceptionObj.Message);
                Console.ReadKey();
            }
        }
    }
}