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
            RegisterKnownType(typeof(RelationshipFilterContext<>), "TOwner");
            RegisterKnownType(typeof(InferredValueContext<>), "T");
            RegisterKnownType(typeof(IqlEntityUserPermissionContext<,>), "TEntity", "TUser");
            RegisterKnownType(typeof(IqlUserPermissionContext<>), "TUser");
        }

        public static void RegisterKnownType(Type type, params string[] genericParameters)
        {
            KnownTypes.Add(CleanTypeName(type.Name), new StandardTypeMetadata(type, type.GetTypeInfo().GenericTypeParameters.Select(gt => new GenericTypeParameter(gt.Name, null)).ToArray()));
        }

        protected static string CleanTypeName(string typeName)
        {
            if (typeName == null)
            {
                return typeName;
            }
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
            if (string.IsNullOrWhiteSpace(typeName))
            {
                return null;
            }

            var typeNameParsed = TypeName.Parse(typeName);
            GenericTypeParameter[] subTypes = null;
            if (typeNameParsed.Generics?.Any() == true)
            {
                subTypes = typeNameParsed.Generics.Select(s => s.Trim()).Select(s => new GenericTypeParameter(s, ResolveTypeFromTypeName(s))).ToArray();
            }
            var resolvedType = KnownTypes.ContainsKey(typeNameParsed.Name) ? KnownTypes[typeNameParsed.Name] : null;
            return resolvedType == null ? null : new ResolvedType(resolvedType, subTypes);
        }
    }

    public class TypeName
    {
        public string Name { get; set; }
        public string[] Generics { get; set; }

        public static TypeName Parse(string fullName)
        {
            var type = new TypeName();
            var isInBrackets = 0;
            var part = "";
            var genericParts = new List<string>();
            for (var i = 0; i < fullName.Length; i++)
            {
                if (fullName[i] == '<')
                {
                    if (isInBrackets == 0)
                    {
                        type.Name = part;
                        part = "";
                        isInBrackets++;
                        continue;
                    }
                    isInBrackets++;
                }
                else if (fullName[i] == '>')
                {
                    isInBrackets--;
                    if (isInBrackets == 0)
                    {
                        genericParts.Add(part.Trim());
                        part = "";
                        continue;
                    }
                }
                if (isInBrackets == 1 && fullName[i] == ',')
                {
                    genericParts.Add(part.Trim());
                    part = "";
                    continue;
                }
                part += fullName[i];
            }
            if (!string.IsNullOrWhiteSpace(part))
            {
                type.Name = part;
            }
            type.Generics = genericParts.ToArray();
            return type;
        }
    }


    public class GenericTypeParameter : IGenericTypeParameter
    {
        public string Name { get; }
        public IIqlTypeMetadata Type { get; }

        public GenericTypeParameter(string name, IIqlTypeMetadata type)
        {
            Name = name;
            Type = type;
        }
    }
    public class StandardTypeMetadata : IIqlTypeMetadata
    {
        public object UnderlyingObject => Type;
        public StandardTypeMetadata(Type type, IGenericTypeParameter[] genericTypeParameters)
        {
            Type = type;
            GenericTypeParameters = genericTypeParameters ?? new IGenericTypeParameter[] { };
        }

        public IGenericTypeParameter[] GenericTypeParameters { get; } = new IGenericTypeParameter[] { };
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