using System;
using System.Collections.Generic;
using Iql.Extensions;

namespace Iql.Entities.Extensions
{
    public static class TypeDefinitionExtensions
    {
        private static readonly Dictionary<ITypeDefinition, IqlType> ToIqlTypeCache = new Dictionary<ITypeDefinition, IqlType>();
        public static IqlType ToIqlType(this ITypeDefinition type)
        {
            if (!ToIqlTypeCache.ContainsKey(type))
            {
                ToIqlTypeCache.Add(type, ToIqlTypeInternal(type));
            }

            return ToIqlTypeCache[type];
        }

        private static IqlType ToIqlTypeInternal(ITypeDefinition type)
        {
            if (type == null)
            {
                return IqlType.Unknown;
            }

            if (type.ConvertedFromType == KnownPrimitiveTypes.Guid)
            {
                return IqlType.Guid;
            }

            if (type.Kind != IqlType.Unknown)
            {
                return type.Kind;
            }

            return type.Type.ToIqlType();
        }

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

        public static ITypeDefinition ChangeConvertedFromType(this ITypeDefinition typeDefinition, string convertedFromType)
        {
            return new TypeDetail(
                typeDefinition.Type,
                typeDefinition.Nullable,
                typeDefinition.DeclaringType,
                convertedFromType,
                typeDefinition.ElementType,
                typeDefinition.IsCollection,
                typeDefinition.Kind);
        }
    }
}