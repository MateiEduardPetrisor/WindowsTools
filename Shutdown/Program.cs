using System;

namespace Shutdown
{
    class Program
    {
        static void Main(String[] args)
        {
            ShutdownUtils ShutdownUtilsObj = new ShutdownUtils();
            ShutdownUtilsObj.ShutDown();
        }
    }
}