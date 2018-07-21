using System.Collections.Generic;
using System.Diagnostics;
using Iql.OData.TypeScript.Generator.Definitions;

namespace Iql.OData.TypeScript.Generator.Models
{
    [DebuggerDisplay("{FileName} - {BaseClassName}")]
    public class GeneratedFile
    {
        public List<ODataTypeDefinition> References { get; set; } = new List<ODataTypeDefinition>();
        public string Contents { get; set; }
        public string FileName { get; set; }
        public string BaseClassName { get; set; }
        public bool IsEntity { get; set; }
        public string Namespace { get; set; }
    }
}