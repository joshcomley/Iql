using Iql.OData.TypeScript.Generator.Definitions;

namespace Iql.OData.TypeScript.Generator.Models
{
    public class GenericTypeParameter : ITypeInfo
    {
        public string Name { get; set; }
        public bool IsEnum { get; set; }
        public string EdmType { get; set; }
        public bool Nullable { get; set; }
        public string ResolvedType { get; set; }
        public string ConstructorType { get; set; }
    }
}