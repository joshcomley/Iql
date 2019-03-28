using System;
using System.Linq;
using Iql.Entities;
using Iql.Entities.SpecialTypes;

namespace Iql.Parsing.Types
{
    public class ResolvedType : IIqlTypeMetadata
    {
        public object UnderlyingObject => TypeMetadata.UnderlyingObject;
        public IIqlTypeMetadata TypeMetadata { get; }

        public ResolvedType(IIqlTypeMetadata typeMetadata, ResolvedType[] genericTypeParameters = null)
        {
            TypeMetadata = typeMetadata;
            GenericTypeParameters = genericTypeParameters ?? new ResolvedType[] { };
            var type = TypeMetadata.Type;
            if (GenericTypeParameters.Any())
            {
                type = type.MakeGenericType(genericTypeParameters.Select(_ => _.TypeMetadata.Type).ToArray());
            }
            Type = type;
        }

        public IIqlTypeMetadata[] GenericTypeParameters {get;}

        public Type Type { get; }

        public ITypeProperty FindProperty(string name)
        {
            var property = TypeMetadata.FindProperty(name);
            return property == null ? null : new ResolvedProperty(this, property);
        }
    }

    public class ResolvedProperty : ITypeProperty
    {
        public ResolvedProperty(ResolvedType type, ITypeProperty property)
        {
            Property = property;
            ResolvedType = type;
        }
        public ResolvedType ResolvedType { get; }
        public ITypeProperty Property { get; }
        public bool IsCollection => Property.IsCollection;

        public Type ElementType => Property.ElementType;

        public Type Type => Property.Type;

        public IIqlTypeMetadata TypeMetadata => Property.TypeMetadata;

        public IqlType ToIqlType()
        {
            return Property.ToIqlType();
        }


        public Func<object, object> GetValue => Property.GetValue;

        public Func<object, object, object> SetValue => Property.SetValue;

        public PropertyKind Kind => Property.Kind;

        public string PropertyName => Property.PropertyName;
        public object UnderlyingObject => Property.UnderlyingObject;
    }
}