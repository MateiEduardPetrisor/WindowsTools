using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace DisableAdministrator
{
    class AdministratorUtils
    {
        private readonly String NetFullPath;
        private readonly String ShutdownFullPath;
        private readonly List<String> BuiltInWindowsUsers;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public AdministratorUtils()
        {
            this.NetFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "net.exe" });
            this.ShutdownFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "shutdown.exe" });
            this.BuiltInWindowsUsers = new List<String>() { "administrator", "defaultaccount", "guest", "wdagutilityaccount" };
            Console.WriteLine("Net Path Is {0}", this.NetFullPath);
            Console.WriteLine("Shutdown Path Is {0}", this.ShutdownFullPath);
            Console.WriteLine("Default Windows Users:");
            foreach (String User in this.BuiltInWindowsUsers)
            {
                Console.WriteLine(User);
            }
        }

        private void EnableUser(String UserName)
        {
            Console.WriteLine("Enable User {0}", UserName);
            Process p = new Process();
            p.StartInfo.FileName = this.NetFullPath;
            p.StartInfo.Arguments = "user " + "\"" + UserName + "\"" + " /active:yes";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        private void DisableUser(String UserName)
        {
            Console.WriteLine("Disable User {0}", UserName);
            Process p = new Process();
            p.StartInfo.FileName = this.NetFullPath;
            p.StartInfo.Arguments = "user " + "\"" + UserName + "\"" + " /active:no";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        private void SetUserBlankPassword(String UserName)
        {
            Console.WriteLine("Set Administrator Blank Password");
            Process p = new Process();
            p.StartInfo.FileName = this.NetFullPath;
            p.StartInfo.Arguments = "user " + "\"" + UserName + "\"" + " \"" + "\"";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        private List<String> GetAllUsers()
        {
            List<String> WindowsUsersList = new List<String>();
            SelectQuery query = new SelectQuery("Win32_UserAccount");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject envVar in searcher.Get())
            {
                if (!this.BuiltInWindowsUsers.Contains(envVar["Name"].ToString().ToLower()))
                {
                    WindowsUsersList.Add(envVar["Name"].ToString());
                }
            }
            return WindowsUsersList;
        }

        private void Reboot()
        {
            Console.WriteLine("Restart System");
            Process p = new Process();
            p.StartInfo.FileName = this.ShutdownFullPath;
            p.StartInfo.Arguments = "/R /F /T 00";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        public void DisableAdmin()
        {
            List<String> WindowsUserList = this.GetAllUsers();
            foreach (String User in WindowsUserList)
            {
                this.EnableUser(User);
            }
            this.SetUserBlankPassword("administrator");
            this.DisableUser("administrator");
            this.Reboot();
        }
    }
}