namespace Iql.OData.TypeScript.Generator.Definitions
{
    public interface ITypeInfo
    {
        bool IsEnum { get; set; }
        string EdmType { get; set; }
        bool Nullable { get; set; }
        string ResolvedType { get; set; }
        string ConstructorType { get; set; }
    }
}