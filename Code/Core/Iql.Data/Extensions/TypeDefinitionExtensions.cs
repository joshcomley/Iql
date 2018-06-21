using Iql.Entities;
using Iql.Extensions;

namespace Iql.Data.Extensions
{
    public static class TypeDefinitionExtensions
    {
        public static IqlType ToIqlType(this ITypeDefinition type)
        {
            if (type == null)
            {
                return IqlType.Unknown;
            }

            if (type.ConvertedFromType == KnownPrimitiveTypes.Guid)
            {
                return IqlType.Guid;
            }

            return type.Type.ToIqlType();
        }
    }
}