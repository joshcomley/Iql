using System.Diagnostics;

namespace Iql.OData.TypeScript.Generator.DataContext
{
    [DebuggerDisplay("{Path}")]
    public class GeneratedFile
    {
        public OutputKind Kind { get; set; }
        public string Path { get; set; }
        public string Contents { get; set; }

        public GeneratedFile(OutputKind kind, string path, string contents)
        {
            Kind = kind;
            Path = path;
            Contents = contents;
        }
    }
}