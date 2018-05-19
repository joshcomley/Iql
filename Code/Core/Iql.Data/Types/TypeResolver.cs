using System;
using System.Linq;
using Iql.Data.Configuration;
using Iql.Data.Configuration.Rules.Relationship;
using Iql.Parsing.Types;

namespace Iql.Data.Types
{
    public class TypeResolver : ITypeResolver
    {
        public Type ResolveTypeFromTypeName(string fullTypeName)
        {
            var typeName = fullTypeName;
            var index = typeName.IndexOf("<");
            Type[] subTypes = null;
            if (index != -1)
            {
                typeName = typeName.Substring(0, index);
                var subTypeName = fullTypeName.Substring(index + 1);
                subTypeName = subTypeName.Substring(0, subTypeName.Length - 1);
                var subTypeNames = subTypeName.Split(',');
                subTypes = subTypeNames.Select(s => s.Trim()).Select(s => ResolveTypeFromTypeName(s)).ToArray();
            }
            var entityType = EntityConfigurationBuilder.FindConfigurationForEntityTypeName(typeName);
            if (entityType != null)
            {
                return entityType.Type;
            }

            Type resolvedType = null;
            if (typeName.StartsWith(nameof(RelationshipFilterContext<int>)))
            {
                resolvedType = typeof(RelationshipFilterContext<>);
                if (subTypes != null && subTypes.Any())
                {
                    resolvedType = resolvedType.MakeGenericType(subTypes);
                }
            }

            return resolvedType;
        }
    }
}