using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities;
using Iql.Entities.InferredValues;
using Iql.Entities.Permissions;
using Iql.Entities.Rules.Relationship;
using Iql.Parsing.Types;

namespace Iql.Data.Types
{
    public class TypeResolver : ITypeResolver
    {
        private static Dictionary<string, Type> KnownTypes { get; set; }

        static TypeResolver()
        {
            KnownTypes = new Dictionary<string, Type>();
            RegisterKnownType(typeof(RelationshipFilterContext<>));
            RegisterKnownType(typeof(InferredValueContext<>));
            RegisterKnownType(typeof(IqlEntityUserPermissionContext<,>));
            RegisterKnownType(typeof(IqlUserPermissionContext<>));
        }

        public static void RegisterKnownType(Type type)
        {
            var typeName = type.Name;
            var graveIndex = typeName.IndexOf("`");
            if (graveIndex != -1)
            {
                typeName = typeName.Substring(0, graveIndex);
            }
            KnownTypes.Add(typeName, type);
        }

        public ResolvedType ResolveTypeFromTypeName(string fullTypeName)
        {
            var typeName = fullTypeName;
            var index = typeName.IndexOf("<");
            ResolvedType[] subTypes = null;
            if (index != -1)
            {
                typeName = typeName.Substring(0, index);
                var subTypeName = fullTypeName.Substring(index + 1);
                subTypeName = subTypeName.Substring(0, subTypeName.Length - 1);
                var subTypeNames = subTypeName.Split(',');
                subTypes = subTypeNames.Select(s => s.Trim()).Select(s => ResolveTypeFromTypeName(s)).ToArray();
            }

            var resolvedType = EntityConfigurationBuilder.FindConfigurationForEntityTypeName(typeName)?.Type
                               ?? (KnownTypes.ContainsKey(typeName) ? KnownTypes[typeName] : null);
            if (subTypes != null && subTypes.Any())
            {
                resolvedType = resolvedType.MakeGenericType(subTypes.Select(_ => _.Type).ToArray());
            }
            return new ResolvedType(resolvedType, subTypes);
        }
    }
}