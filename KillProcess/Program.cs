using System;

namespace KillProcess
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                ProcessKiller ProcessKillerOj = new ProcessKiller();
                ProcessKillerOj.KillProcess(args[0]);
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("KillProcessTree " + "\"" + "Process.exe" + "\"");
                Console.ReadKey();
            }
        }
    }
}