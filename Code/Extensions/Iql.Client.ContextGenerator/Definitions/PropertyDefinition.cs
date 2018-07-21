using System.Collections.Generic;
using System.Diagnostics;
using Iql.OData.TypeScript.Generator.Models;

namespace Iql.OData.TypeScript.Generator.Definitions
{
    [DebuggerDisplay("{Name}")]
    public class PropertyDefinition : IVariable
    {
        public PropertyDefinition(string name = null, bool isLocal = false)
        {
            TypeInfo = new TypeInfo();
            Name = name;
            IsLocal = isLocal;
        }

        public string Name { get; set; }
        public bool Private { get; set; }
        public string ResolvedType { get; set; }
        public bool IsLocal { get; set; }
        public ITypeInfo TypeInfo { get; set; }
    }
}