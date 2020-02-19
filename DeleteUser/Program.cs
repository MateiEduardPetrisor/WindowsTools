using System;

namespace DeleteUser
{
    class Program
    {
        static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                UserUtils userUtils = new UserUtils();
                userUtils.DeleteUser(args[0]);
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("DeleteUser.exe " + "\"UserToDelete\"");
                Console.ReadKey();
            }
        }
    }
}