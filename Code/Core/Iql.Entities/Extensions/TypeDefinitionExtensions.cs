using System;

namespace Iql.Entities.Extensions
{
    public static class TypeDefinitionExtensions
    {
        public static ITypeDefinition ChangeKind(this ITypeDefinition typeDefinition, IqlType kind)
        {
            return new TypeDetail(
                typeDefinition.Type, 
                typeDefinition.Nullable, 
                typeDefinition.DeclaringType, 
                typeDefinition.ConvertedFromType,
                typeDefinition.ElementType, 
                typeDefinition.IsCollection, 
                kind);
        }

        public static ITypeDefinition ChangeNullable(this ITypeDefinition typeDefinition, bool nullable)
        {
            return new TypeDetail(
                typeDefinition.Type,
                nullable,
                typeDefinition.DeclaringType,
                typeDefinition.ConvertedFromType,
                typeDefinition.ElementType,
                typeDefinition.IsCollection,
                typeDefinition.Kind);
        }

        public static ITypeDefinition ChangeType(this ITypeDefinition typeDefinition, Type type)
        {
            return new TypeDetail(
                type,
                typeDefinition.Nullable,
                typeDefinition.DeclaringType,
                typeDefinition.ConvertedFromType,
                typeDefinition.ElementType ?? type,
                typeDefinition.IsCollection,
                typeDefinition.Kind);
        }

    }
}