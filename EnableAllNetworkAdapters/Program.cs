namespace EnableAllNetworkAdapters
{
    class Program
    {
        static void Main()
        {
            NetworkAdapterUtils NetworkAdaptersUtilsObj = new NetworkAdapterUtils();
            NetworkAdaptersUtilsObj.EnableAll();
        }
    }
}