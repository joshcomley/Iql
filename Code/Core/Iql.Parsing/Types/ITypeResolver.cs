namespace Iql.Parsing.Types
{
    public interface ITypeResolver
    {
        ResolvedType ResolveTypeFromTypeName(string typeName);
    }
}