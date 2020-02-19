namespace RestoreThisPcFolders
{
    class Program
    {
        static void Main()
        {
            RegistryUtils RegistryUtilsObj = new RegistryUtils();
            RegistryUtilsObj.RestoreAllThisPcFolders();
        }
    }
}