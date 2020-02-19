using System;

namespace ShutDown
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                ShutDownUtils ShutdownUtilsObj = new ShutDownUtils();
                ShutDownOperations ShutDownOperationsObj;
                if (!Enum.TryParse(args[0], true, out ShutDownOperationsObj))
                {
                    ShutDownOperationsObj = ShutDownOperations.INVALID;
                }
                else
                {
                    ShutDownOperationsObj = (ShutDownOperations)Enum.Parse(typeof(ShutDownOperations), args[0], true);
                }
                switch (ShutDownOperationsObj)
                {
                    case (ShutDownOperations.RESTART):
                        ShutdownUtilsObj.Restart();
                        break;
                    case (ShutDownOperations.SHUTDOWN):
                        ShutdownUtilsObj.ShutDown();
                        break;
                    case (ShutDownOperations.HIBERNATE):
                        ShutdownUtilsObj.Hibernate();
                        break;
                    case (ShutDownOperations.LOGOFF):
                        ShutdownUtilsObj.LogOff();
                        break;
                    default:
                        Console.WriteLine("Invalid Operation");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("ShutDownUtils.exe " + "\"" + "restart" + "\"" + " to restart the computer");
                Console.WriteLine("ShutDownUtils.exe " + "\"" + "shutdown" + "\"" + " to poweroff the computer");
                Console.WriteLine("ShutDownUtils.exe " + "\"" + "hibernate" + "\"" + " to hibernate the computer");
                Console.WriteLine("ShutDownUtils.exe " + "\"" + "logoff" + "\"" + " to log off the user");
                Console.ReadKey();
            }
        }
    }
}