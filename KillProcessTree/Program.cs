using System;

namespace KillProcessTree
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                ProcessKiller ProcessKillerOj = new ProcessKiller();
                ProcessKillerOj.KillProcessTree(args[0]);
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("KillProcess " + "\"" + "Process.exe" + "\"");
                Console.ReadKey();
            }
        }
    }
}