using System;

namespace DeleteScheduledTask
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                ScheduledTaskUtils ScheduledTaskUtilsObj = new ScheduledTaskUtils();
                ScheduledTaskUtilsObj.DeleteScheduledTask(args[0]);
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("DeleteScheduledTask.exe " + "\"" + "Task Name" + "\"");
                Console.ReadKey();
            }
        }
    }
}