namespace RemoveThisPcFolders
{
    class Program
    {
        static void Main()
        {
            RegistryUtils RegistryUtilsObj = new RegistryUtils();
            RegistryUtilsObj.RemoveAllThisPcFolders();
        }
    }
}