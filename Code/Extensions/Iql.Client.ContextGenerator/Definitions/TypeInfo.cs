namespace Iql.OData.TypeScript.Generator.Definitions
{
    public class TypeInfo : ITypeInfo
    {
        public TypeInfo(string name = null)
        {
            EdmType = name;
        }

        public bool IsEnum { get; set; }
        public string EdmType { get; set; }
        public bool Nullable { get; set; }
        public string ResolvedType { get; set; }
        public string ConstructorType { get; set; }
    }
}