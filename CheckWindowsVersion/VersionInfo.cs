using System;

namespace CheckWindowsVersion
{
    public class VersionInfo
    {
        public Version Version { get; private set; }

        public VersionInfo(int major, int minor, int build)
        {
            this.Version = new Version(major, minor, build);
        }
    }
}