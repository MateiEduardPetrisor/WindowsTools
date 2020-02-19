namespace UsersCleanUp
{
    class Program
    {
        static void Main()
        {
            UserUtils UserUtilsObj = new UserUtils();
            UserUtilsObj.DeleteAllUsers();
        }
    }
}