using System;

namespace SetNetState
{
    public class Program
    {
        public static void Main(String[] args)
        {

            if (args.Length == 1)
            {
                NetworkUtils networkUtils;
                if (args[0].ToLower().Equals("disable"))
                {
                    networkUtils = new NetworkUtils();
                    networkUtils.SetNetState("disable");
                }
                else if (args[0].ToLower().Equals("enable"))
                {
                    networkUtils = new NetworkUtils();
                    networkUtils.SetNetState("enable");
                }
                else
                {
                    Console.WriteLine("Usage");
                    Console.WriteLine("SetNetState.exe " + "\"" + "disable" + "\"" + " -> Disable all network adapters");
                    Console.WriteLine("SetNetState.exe " + "\"" + "enable" + "\"" + "  -> Enable  all network adapters");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("SetNetState.exe " + "\"" + "disable" + "\"" + " -> Disable all network adapters");
                Console.WriteLine("SetNetState.exe " + "\"" + "enable" + "\"" + "  -> Enable  all network adapters");
                Console.ReadKey();
            }
        }
    }
}