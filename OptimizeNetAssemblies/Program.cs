using System;

namespace OptimizeNetAssemblies
{
    class Program
    {
        static void Main(String[] args)
        {
            NetAssembliesUtils NetAssembliesUtilsObj = new NetAssembliesUtils();
            NetAssembliesUtilsObj.CompileAll();
        }
    }
}