using System;
using System.Diagnostics;
using System.IO;

namespace OptimizeNetAssemblies
{
    class NetAssembliesUtils
    {
        private readonly String ngen4x86FullPath;
        private readonly String ngen4x64FullPath;
        private readonly String ngen2x86FullPath;
        private readonly String ngen2x64FullPath;

        private String CombinePath(String Path1, String[] Paths)
        {
            String FinalPath = Path1;
            foreach (String Pth in Paths)
            {
                FinalPath = Path.Combine(FinalPath, Pth);
            }
            return FinalPath;
        }

        public NetAssembliesUtils()
        {
            this.ngen4x64FullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "Microsoft.NET", "Framework64", "v4.0.30319", "ngen.exe" });
            this.ngen2x64FullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "Microsoft.NET", "Framework64", "v2.0.50727", "ngen.exe" });
            this.ngen4x86FullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "Microsoft.NET", "Framework", "v4.0.30319", "ngen.exe" });
            this.ngen2x86FullPath = CombinePath(Environment.GetEnvironmentVariable("SYSTEMROOT"), new String[] { "Microsoft.NET", "Framework", "v2.0.50727", "ngen.exe" });
            Console.WriteLine("ngen .NET 4 X64 Path Is {0}", this.ngen4x64FullPath);
            Console.WriteLine("ngen .NET 4 X86 Path Is {0}", this.ngen4x86FullPath);
            Console.WriteLine("ngen .NET 2 X64 Path Is {0}", this.ngen2x64FullPath);
            Console.WriteLine("ngen .NET 2 X86 Path Is {0}", this.ngen2x86FullPath);
        }

        private void CompileNetAssembly(String ngenFullPath)
        {
            if (File.Exists(ngenFullPath))
            {
                Process p = new Process();
                p.StartInfo.FileName = ngenFullPath;
                p.StartInfo.Arguments = "executequeueditems";
                p.Start();
                p.WaitForExit();
                p.Dispose();
            }
            else
            {
                Console.WriteLine(ngenFullPath + "Not Found!");
            }
        }

        public void CompileAll()
        {
            if (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToLower().Equals("amd64"))
            {
                Console.WriteLine("Compile .NET 4 X64 Assemblies");
                this.CompileNetAssembly(this.ngen4x64FullPath);
                Console.WriteLine("Compile .NET 4 X86 Assemblies");
                this.CompileNetAssembly(this.ngen4x86FullPath);
                Console.WriteLine("Compile .NET 2 X64 Assemblies");
                this.CompileNetAssembly(this.ngen2x64FullPath);
                Console.WriteLine("Compile .NET 2 X86 Assemblies");
                this.CompileNetAssembly(this.ngen2x86FullPath);
            }
            else
            {
                Console.WriteLine("Compile .NET 4 X86 Assemblies");
                this.CompileNetAssembly(this.ngen4x86FullPath);
                Console.WriteLine("Compile .NET 2 X86 Assemblies");
                this.CompileNetAssembly(this.ngen2x86FullPath);
            }
        }
    }
}