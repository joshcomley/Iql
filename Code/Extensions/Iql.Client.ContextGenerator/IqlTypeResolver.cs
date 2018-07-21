using Iql.OData.TypeScript.Generator.Definitions;

namespace Iql.OData.TypeScript.Generator
{
    public static class IqlTypeResolver
    {
        public static IqlType ResolveIqlType(
            this ITypeInfo type)

        {
            if (type.EdmType.StartsWith("Collection("))
            {
                return IqlType.Collection;
            }

            switch (type.EdmType)
            {
                case "String":
                case "Edm.String":
                case "Guid":
                case "Edm.Guid":
                    return IqlType.String;
                case "Int32":
                case "Edm.Int32":
                case "Int64":
                case "Edm.Int64":
                    return IqlType.Integer;
                case "Double":
                case "Edm.Double":
                case "Single":
                case "Edm.Single":
                case "Decimal":
                case "Edm.Decimal":
                    return IqlType.Decimal;
                case "Boolean":
                case "Edm.Boolean":
                    return IqlType.Boolean;
                case "DateTimeOffset":
                case "Edm.DateTimeOffset":
                    return IqlType.Date;
            }

            if (type.IsEnum)
            {
                return IqlType.Enum;
            }
            return IqlType.Unknown;
        }
    }
}