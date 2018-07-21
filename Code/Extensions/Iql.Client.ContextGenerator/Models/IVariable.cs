using Iql.OData.TypeScript.Generator.Definitions;

namespace Iql.OData.TypeScript.Generator.Models
{
    public interface IVariable
    {
        string Name { get; set; }
        ITypeInfo TypeInfo { get; set; }
        bool Private { get; set; }
        string ResolvedType { get; set; }
        bool IsLocal { get; set; }
    }
}