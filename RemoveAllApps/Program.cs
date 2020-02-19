namespace RemoveAllApps
{
    class Program
    {
        static void Main()
        {
            AppsUtils AppsUtilsObj = new AppsUtils();
            AppsUtilsObj.DeleteAllApps();
        }
    }
}