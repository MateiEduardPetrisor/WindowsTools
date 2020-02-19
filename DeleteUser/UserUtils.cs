using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace DeleteUser
{
    class UserUtils
    {
        private readonly String RegFullPath;
        private readonly String CmdFullPath;
        private readonly String NetFullPath;

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
            Console.WriteLine("Reg Path is {0}", this.RegFullPath);
            Console.WriteLine("Net Path is {0}", this.NetFullPath);
            Console.WriteLine("Cmd Path is {0}", this.CmdFullPath);
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
            Console.WriteLine("Disable User {0}", UserName);
            Process p = new Process();
            p.StartInfo.FileName = this.NetFullPath;
            p.StartInfo.Arguments = "user " + "\"" + UserName + "\"" + " /active:no";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        private void DeleteUserAccount(String UserName)
        {
            Console.WriteLine("Delete User Account {0}", UserName);
            Process p = new Process();
            p.StartInfo.FileName = this.NetFullPath;
            p.StartInfo.Arguments = "user " + "\"" + UserName + "\"" + " /DELETE";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        private void DeleteUserSecurityIdentifierFromRegistry(String SecurityIdentifier)
        {
            Console.WriteLine("Delete SecurityIdentifier {0} From Registry", SecurityIdentifier);
            Process p = new Process();
            p.StartInfo.FileName = this.RegFullPath;
            p.StartInfo.Arguments = "delete " + "\"" + "HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList\\" + SecurityIdentifier + "\"" + " /f ";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        private void DeleteUserDataFolder(String UserName)
        {
            String CurrentUserDataFolder = Environment.GetEnvironmentVariable("USERPROFILE");
            String UsersFolder = new FileInfo(CurrentUserDataFolder).Directory.FullName;
            String UserToDeleteDataFolder = Path.Combine(UsersFolder, UserName);
            Console.WriteLine("Delete User {0} Data Folder", UserToDeleteDataFolder);
            Process p = new Process();
            p.StartInfo.FileName = this.CmdFullPath;
            p.StartInfo.Arguments = "/c if exist " + "\"" + UserToDeleteDataFolder + "\"" + " (rmdir / S / Q " + "\"" + UserToDeleteDataFolder + "\"" + ") else (echo Folder " + "\"" + UserToDeleteDataFolder + "\"" + " Not Found! )";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        public void DeleteUser(String UserName)
        {
            String UserSid = this.GetUserSecurityIdentifier(UserName);
            this.DisableUserAccount(UserName);
            this.DeleteUserAccount(UserName);
            if (UserSid != null)
            {
                this.DeleteUserSecurityIdentifierFromRegistry(UserSid);
            }
            else
            {
                Console.WriteLine("UserSid Is null");
            }
            this.DeleteUserDataFolder(UserName);
        }
    }
}