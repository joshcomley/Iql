using System;
using System.IO;
using System.Threading;

namespace Iql.OData.TypeScript.Generator.DataContext
{
    public class GeneratedContext
    {
        public OutputKind Kind { get; }
        public GeneratedFile[] Files { get; }

        public GeneratedContext(OutputKind kind, GeneratedFile[] files)
        {
            Kind = kind;
            Files = files;
        }

        public void SaveTo(string path, bool clearDirectory = false)
        {
            if (clearDirectory)
            {
                ClearFolder(path);
            }
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                foreach (var file in Files)
                {
                    var newFilePath = Path.Combine(path, file.Path);
                    var directoryName = Path.GetDirectoryName(newFilePath);
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    File.WriteAllText(newFilePath, file.Contents);
                }
            }
        }

        private static void ClearFolder(string path)
        {
            while (true)
            {
                try
                {
                    if (Directory.Exists(path))
                    {
                        var di = new DirectoryInfo(path);
                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }

                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                    }

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    break;
                }
                catch (IOException e)
                {
                    Thread.Sleep(5000);
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Trying again in 5 seconds");
                }
            }
        }
    }
}