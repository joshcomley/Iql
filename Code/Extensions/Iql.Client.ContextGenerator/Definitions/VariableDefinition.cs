using System.Diagnostics;
using Iql.OData.TypeScript.Generator.Models;

namespace Iql.OData.TypeScript.Generator.Definitions;

[DebuggerDisplay("{Name}")]
public class VariableDefinition : IVariable
{
    public VariableDefinition(string name = null)
    {
        TypeInfo = new TypeInfo();
        Name = name;
    }

    public string Name { get; set; }
    public bool Private { get; set; }
    public string ResolvedType { get; set; }
    public bool IsLocal { get; set; }
    public ITypeInfo TypeInfo { get; set; }
}