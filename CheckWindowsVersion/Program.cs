namespace CheckWindowsVersion
{
    class Program
    {
        static int Main()
        {
            //Based on the code from this repository https://github.com/pruggitorg/detect-windows-version
            //and this article https://www.prugg.at/2019/09/09/properly-detect-windows-version-in-c-net-even-windows-10/

            OperatingSystem osVer = OSVersion.GetOperatingSystem();
            if (osVer.Equals(OperatingSystem.Windows10) || osVer.Equals(OperatingSystem.WindowsServer20162019))
            {
                return 10; //Windows 10
            }
            else if (osVer.Equals(osVer.Equals(OperatingSystem.Windows81) || osVer.Equals(OperatingSystem.WindowsServer2012R2)))
            {
                return 81; //Windows 8.1/Server 2012R2
            }
            else if (osVer.Equals(OperatingSystem.Windows8) || osVer.Equals(OperatingSystem.WindowsServer2012))
            {
                return 80; //Windows 8/Server 2012
            }
            else if (osVer.Equals(OperatingSystem.Windows7) || osVer.Equals(OperatingSystem.WindowsServer2008R2))
            {
                return 61; //Windows 7/Server 2008R2
            }
            else if (osVer.Equals(OperatingSystem.WindowsVista) || osVer.Equals(OperatingSystem.WindowsServer2008))
            {
                return 60; //Windows Vista/Server 2008
            }
            else if (osVer.Equals(OperatingSystem.Windows2000) || osVer.Equals(OperatingSystem.WindowsXP)
                || osVer.Equals(OperatingSystem.WindowsXPProx64) || osVer.Equals(OperatingSystem.WindowsServer2003)
                || osVer.Equals(OperatingSystem.WindowsServer2003R2) || osVer.Equals(OperatingSystem.WindowsHomeServer))
            {
                return 50; //Windows 2000/Windows XP 32 Bit/64 Bit/Server 2003/Server 2003R2/Home Server
            }
            else
            {
                return -1; //Unknown version of windows
            }
        }
    }
}