using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Iql.ExpressionMethodGenerator.ConsoleApp
{
    public class MethodGenerator
    {
        private static string _iqlSolutionDirectory;

        public static string ResolveIqlSolutionDirectory()
        {
            if (_iqlSolutionDirectory == null)
            {
                var dir = Path.GetDirectoryName(new Uri(
                        Assembly.GetEntryAssembly().CodeBase)
                    .AbsolutePath
                    .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar));
                while (!File.Exists(Path.Combine(dir, "Iql.sln")))
                {
                    dir = Path.GetDirectoryName(dir);
                }

                _iqlSolutionDirectory = dir;
            }

            return _iqlSolutionDirectory;
        }

        private static string _iqlDirectory;
        public static string ResolveIqlDirectory()
        {
            if (_iqlDirectory == null)
            {
                var dir = ResolveIqlSolutionDirectory();
                var path = Directory.EnumerateFiles(dir, "Iql.csproj", SearchOption.AllDirectories).First();
                dir = Path.GetDirectoryName(path);
                _iqlDirectory = dir;
            }
            return _iqlDirectory;
        }
    }
}