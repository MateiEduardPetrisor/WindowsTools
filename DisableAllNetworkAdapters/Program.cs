namespace DisableAllNetworkAdapters
{
    class Program
    {
        static void Main()
        {
            NetworkAdapterUtils NetworkAdaptersUtilsObj = new NetworkAdapterUtils();
            NetworkAdaptersUtilsObj.DisableAll();
        }
    }
}