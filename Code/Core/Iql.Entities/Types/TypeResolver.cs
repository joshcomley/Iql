using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Entities;
using Iql.Entities.InferredValues;
using Iql.Entities.Permissions;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.SpecialTypes;
using Iql.Extensions;
using Iql.Parsing.Types;

namespace Iql.Data.Types
{
    public class TypeResolver : ITypeResolver
    {
        public TypeResolver()
        {
            
        }

        private static Dictionary<string, IIqlTypeMetadata> KnownTypes { get; set; }

        static TypeResolver()
        {
            KnownTypes = new Dictionary<string, IIqlTypeMetadata>();
            RegisterKnownType(typeof(RelationshipFilterContext<>));
            RegisterKnownType(typeof(InferredValueContext<>));
            RegisterKnownType(typeof(IqlEntityUserPermissionContext<,>));
            RegisterKnownType(typeof(IqlUserPermissionContext<>));
        }

        public static void RegisterKnownType(Type type)
        {
            KnownTypes.Add(CleanTypeName(type.Name), new StandardTypeMetadata(type));
        }

        protected static string CleanTypeName(string typeName)
        {
            var graveIndex = typeName.IndexOf("`");
            if (graveIndex != -1)
            {
                typeName = typeName.Substring(0, graveIndex);
            }

            return typeName;
        }

        public virtual IIqlTypeMetadata FindType<T>()
        {
            return FindTypeByType(typeof(T));
        }

        public virtual IIqlTypeMetadata FindTypeByType(Type type)
        {
            return ResolveTypeFromTypeName(type.GetFullName());
        }

        public virtual IIqlTypeMetadata ResolveTypeFromTypeName(string fullTypeName)
        {
            var typeName = CleanTypeName(fullTypeName);
            var index = typeName.IndexOf("<");
            ResolvedType[] subTypes = null;
            if (index != -1)
            {
                typeName = typeName.Substring(0, index);
                var subTypeName = fullTypeName.Substring(index + 1);
                subTypeName = subTypeName.Substring(0, subTypeName.Length - 1);
                var subTypeNames = subTypeName.Split(',');
                subTypes = subTypeNames.Select(s => s.Trim()).Select(s => (ResolvedType)ResolveTypeFromTypeName(s)).ToArray();
            }
            var resolvedType = KnownTypes.ContainsKey(typeName) ? KnownTypes[typeName] : null;
            return new ResolvedType(resolvedType, subTypes);
        }
    }

    public class StandardTypeMetadata : IIqlTypeMetadata
    {
        public object UnderlyingObject => Type;
        public StandardTypeMetadata(Type type)
        {
            Type = type;
        }

        public IIqlTypeMetadata[] GenericTypeParameters { get; } = new IIqlTypeMetadata[] { };
        public Type Type { get; }
        public ITypeProperty FindProperty(string name)
        {
            return new StandardPropertyMetadata(this, Type.GetProperties().FirstOrDefault(_ => _.Name == name));
        }

        public SpecialTypeDefinition SpecialTypeDefinition => null;
        public IEntityConfiguration EntityConfiguration => null;
    }

    public class StandardPropertyMetadata : ITypeProperty
    {
        public PropertyInfo PropertyInfo { get; }

        public StandardPropertyMetadata(IIqlTypeMetadata typeMetadata, PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
            TypeMetadata = typeMetadata;
            UnderlyingObject = PropertyInfo;
        }

        public bool IsCollection => PropertyInfo.PropertyType is IEnumerable;
        public Type ElementType => PropertyInfo.PropertyType;
        public Type Type => PropertyInfo.PropertyType;
        public IIqlTypeMetadata TypeMetadata { get; }

        public IqlType ToIqlType()
        {
            return Type.ToIqlType();
        }

        public IProperty EntityProperty => null;
        public Func<object, object> GetValue => entity => entity.GetPropertyValueByName(PropertyName);

        public Func<object, object, object> SetValue =>
            (entity, value) => entity.SetPropertyValueByName(PropertyName, value);

        public PropertyKind Kind => PropertyKind.Primitive;
        public string PropertyName => PropertyInfo.Name;
        public object UnderlyingObject { get; }
        public EntityRelationship Relationship => null;
    }
}