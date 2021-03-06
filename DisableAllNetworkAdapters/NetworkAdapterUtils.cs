﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DisableAllNetworkAdapters
{
    class NetworkAdapterUtils
    {
        private readonly String NetshFullPath;
        private readonly List<String> NetworkAdapters;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public NetworkAdapterUtils()
        {
            this.NetshFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "netsh.exe" });
            Console.WriteLine("Netsh Path Is {0}", this.NetshFullPath);
            this.NetworkAdapters = this.GetNetworkInterfaces();
        }

        private String GetNetshOuput()
        {
            Process p = new Process();
            p.StartInfo.FileName = this.NetshFullPath;
            p.StartInfo.Arguments = "interface show interface";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            p.WaitForExit();
            String result = p.StandardOutput.ReadToEnd();
            p.Dispose();
            return result;
        }

        private List<String> GetNetworkInterfaces()
        {
            List<String> NetworkInterfaceNamesObj = null;
            char[] Separators = new char[] { '\r', '\n' };
            String netshOutput = this.GetNetshOuput();
            String[] tokensOutput = netshOutput.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            if (tokensOutput.Length == 2)
            {
                Console.WriteLine("No Network Adapters Found!");
            }
            else
            {
                NetworkInterfaceNamesObj = new List<String>();
                StringBuilder AdapterName = new StringBuilder();
                for (int indexTokens = 2; indexTokens < tokensOutput.Length; indexTokens++)
                {
                    String[] TokensInterface = tokensOutput[indexTokens].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int StartIndex = 3; StartIndex < TokensInterface.Length; StartIndex++)
                    {
                        AdapterName.Append(TokensInterface[StartIndex]);
                        if (StartIndex < TokensInterface.Length - 1)
                        {
                            AdapterName.Append(" ");
                        }
                    }
                    NetworkInterfaceNamesObj.Add(AdapterName.ToString());
                    AdapterName.Clear();
                }
            }
            return NetworkInterfaceNamesObj;
        }

        public void DisableAll()
        {
            if (this.NetworkAdapters != null)
            {
                foreach (String AdapterName in this.NetworkAdapters)
                {
                    Console.WriteLine(AdapterName + " is now Disabled!");
                    Process p = new Process();
                    p.StartInfo.FileName = this.NetshFullPath;
                    p.StartInfo.Arguments = "interface set interface \"" + AdapterName + "\" " + "disable";
                    p.Start();
                    p.WaitForExit();
                    p.Dispose();
                }
            }
        }
    }
}