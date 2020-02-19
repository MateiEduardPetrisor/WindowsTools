using System;
using System.Text;

namespace RemoveFromPath
{
    class PathUtils
    {
        public void RemoveFromPath(String PathToRemove)
        {
            String SystemPathEnvVariable = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine).ToLower();
            Environment.SetEnvironmentVariable("PATH_BACKUP", SystemPathEnvVariable, EnvironmentVariableTarget.Machine);
            String[] SystemPathEnvVariableTokens = SystemPathEnvVariable.Split(';');
            StringBuilder sb = new StringBuilder();
            foreach (String Tkn in SystemPathEnvVariableTokens)
            {
                if (!Tkn.Equals(PathToRemove))
                {
                    sb.Append(Tkn);
                    sb.Append(";");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            Console.WriteLine("Path Removed {0}", PathToRemove);
            Environment.SetEnvironmentVariable("PATH", sb.ToString(), EnvironmentVariableTarget.Machine);
            sb.Clear();
        }
    }
}