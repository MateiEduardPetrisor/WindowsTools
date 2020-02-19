using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Security.Principal;

namespace UsersCleanUp
{
    class UserUtils
    {
        private readonly String RegFullPath;
        private readonly String CmdFullPath;
        private readonly String NetFullPath;
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

        public UserUtils()
        {
            this.RegFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "reg.exe" });
            this.CmdFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "cmd.exe" });
            this.NetFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "net.exe" });
            this.BuiltInWindowsUsers = new List<String>() { "administrator", "defaultaccount", "guest", "wdagutilityaccount" };
            Console.WriteLine("Reg Path is {0}", this.RegFullPath);
            Console.WriteLine("Net Path is {0}", this.NetFullPath);
            Console.WriteLine("Cmd Path is {0}", this.CmdFullPath);
            Console.WriteLine("Default Windows Users:");
            foreach (String User in this.BuiltInWindowsUsers)
            {
                Console.WriteLine(User);
            }
        }

        private String GetUserSecurityIdentifier(String UserName)
        {
            String SecurityIdentifier = null;
            try
            {
                NTAccount NtAccountObject = new NTAccount(UserName);
                SecurityIdentifier SecurityIdentifierObject = (SecurityIdentifier)NtAccountObject.Translate(typeof(SecurityIdentifier));
                SecurityIdentifier = SecurityIdentifierObject.ToString();
            }
            catch (Exception)
            {
                Console.WriteLine("User Not Found!");
            }
            return SecurityIdentifier;
        }

        private void DisableUserAccount(String UserName)
        {
            Process p = new Process();
            p.StartInfo.FileName = this.NetFullPath;
            p.StartInfo.Arguments = "user " + "\"" + UserName + "\"" + " /active:no";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        private void DeleteUserAccount(String UserName)
        {
            Process p = new Process();
            p.StartInfo.FileName = this.NetFullPath;
            p.StartInfo.Arguments = "user " + "\"" + UserName + "\"" + " /DELETE";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        private void DeleteUserSecurityIdentifierFromRegistry(String SecurityIdentifier)
        {
            if (SecurityIdentifier != null)
            {
                Process p = new Process();
                p.StartInfo.FileName = this.RegFullPath;
                p.StartInfo.Arguments = "delete " + "\"" + "HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList\\" + SecurityIdentifier + "\"" + " /f ";
                p.Start();
                p.WaitForExit();
                p.Dispose();
            }
            else
            {
                Console.WriteLine("User SID is null nothing to delete!");
            }
        }

        private void DeleteUserDataFolder(String UserName)
        {
            String CurrentUserDataFolder = Environment.GetEnvironmentVariable("USERPROFILE");
            String UsersFolder = new FileInfo(CurrentUserDataFolder).Directory.FullName;
            String UserToDeleteDataFolder = Path.Combine(UsersFolder, UserName);
            Process p = new Process();
            p.StartInfo.FileName = this.CmdFullPath;
            p.StartInfo.Arguments = "/c if exist " + "\"" + UserToDeleteDataFolder + "\"" + " (rmdir / S / Q " + "\"" + UserToDeleteDataFolder + "\"" + ") else (echo Folder " + "\"" + UserToDeleteDataFolder + "\"" + " Not Found! )";
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

        public void DeleteAllUsers()
        {
            List<String> WindowsUserList = this.GetAllUsers();
            foreach (String User in WindowsUserList)
            {
                Console.WriteLine("Disable User {0}", User);
                this.DisableUserAccount(User);
                String UserSID = this.GetUserSecurityIdentifier(User);
                Console.WriteLine("Delete User SID {0}", UserSID);
                this.DeleteUserSecurityIdentifierFromRegistry(UserSID);
                Console.WriteLine("Delete User {0}", User);
                this.DeleteUserAccount(User);
                Console.WriteLine("Delete User Data Folder {0}", UserSID);
                this.DeleteUserDataFolder(User);
            }
        }
    }
}