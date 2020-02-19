using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Sysprep
{
    class SysprepUtils
    {
        private readonly String SysprepFullPath;
        private readonly String OsType;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public SysprepUtils()
        {
            this.SysprepFullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "SYSTEM32", "SYSPREP", "sysprep.exe" });
            Console.WriteLine("Sysprep Path Is {0}", this.SysprepFullPath);
            if (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToLower().Equals("amd64"))
            {
                this.OsType = "amd64";
            }
            else
            {
                this.OsType = "x86";
            }
        }

        private void RunSysprep(String UnattendXml)
        {
            Console.WriteLine("Running Sysprep With Unattended {0}", UnattendXml);
            Process p = new Process();
            p.StartInfo.FileName = this.SysprepFullPath;
            p.StartInfo.Arguments = "/generalize /oobe /shutdown /unattend:" + "\"" + UnattendXml + "\"";
            p.Start();
            p.WaitForExit();
            p.Dispose();
        }

        private void CheckXmlFile(String UnattendXml)
        {
            XmlReader XmlReaderObj = XmlReader.Create(UnattendXml);
            while (XmlReaderObj.Read())
            {
                if ((XmlReaderObj.NodeType == XmlNodeType.Element) && (XmlReaderObj.Name == "component"))
                {
                    if (XmlReaderObj.HasAttributes)
                    {
                        String AttributeValue = XmlReaderObj.GetAttribute("processorArchitecture");
                        if (!AttributeValue.ToLower().Equals(this.OsType))
                        {
                            throw new ArgumentException("Xml Provided Does Not Match The Arhitecture Type. Operating System Arhitecture Type = " + this.OsType + " Xml Arhitecture Type = " + AttributeValue);
                        }
                    }
                }
            }
            XmlReaderObj.Close();
        }

        public void GeneralizeSystemOOBE()
        {
            String UnattendXmlFullPath;
            if (OsType.Equals("amd64"))
            {
                String AssemblyFullPath = Assembly.GetExecutingAssembly().Location.ToString();
                FileInfo FileInfoObj = new FileInfo(AssemblyFullPath);
                UnattendXmlFullPath = CombinePath(FileInfoObj.DirectoryName, new String[] { "Untitled.x64.xml" });
                FileInfoObj = new FileInfo(UnattendXmlFullPath);
                if (FileInfoObj.Exists)
                {
                    this.CheckXmlFile(UnattendXmlFullPath);
                    this.RunSysprep(UnattendXmlFullPath);
                }
                else
                {
                    throw new FileNotFoundException("Untitled.x64.xml Not Found In Sysprep.exe Directory!");
                }
            }
            else
            {
                String AssemblyFullPath = Assembly.GetExecutingAssembly().Location.ToString();
                FileInfo FileInfoObj = new FileInfo(AssemblyFullPath);
                UnattendXmlFullPath = CombinePath(FileInfoObj.DirectoryName, new String[] { "Untitled.x86.xml" });
                FileInfoObj = new FileInfo(UnattendXmlFullPath);
                if (FileInfoObj.Exists)
                {
                    this.CheckXmlFile(UnattendXmlFullPath);
                    this.RunSysprep(UnattendXmlFullPath);
                }
                else
                {
                    throw new FileNotFoundException("Untitled.x86.xml Not Found In Sysprep.exe Directory!");
                }
            }
        }
    }
}