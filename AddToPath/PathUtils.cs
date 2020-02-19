using System;
using System.Text;

namespace AddToPath
{
    class PathUtils
    {
        public void AddToPath(String PathToAdd)
        {
            String SystemPathEnvVariable = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
            Environment.SetEnvironmentVariable("PATH_BACKUP", SystemPathEnvVariable, EnvironmentVariableTarget.Machine);
            String[] SystemPathEnvVariableTokens = SystemPathEnvVariable.Split(';');
            StringBuilder sb = new StringBuilder();
            foreach (String Tkn in SystemPathEnvVariableTokens)
            {
                sb.Append(Tkn);
                sb.Append(";");
            }
            sb.Append(PathToAdd);
            Console.WriteLine("Path Added {0}", PathToAdd);
            Environment.SetEnvironmentVariable("PATH", sb.ToString(), EnvironmentVariableTarget.Machine);
            sb.Clear();
        }
    }
}