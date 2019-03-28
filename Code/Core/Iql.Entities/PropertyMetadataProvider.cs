using System;
using Iql.Entities.Extensions;

namespace Iql.Entities
{
    public class PropertyMetadataProvider : ITypeProperty
    {
        public bool IsCollection => EntityProperty.TypeDefinition.IsCollection;
        public Type ElementType => EntityProperty.TypeDefinition.ElementType;
        public Type Type => EntityProperty.TypeDefinition.Type;
        public IIqlTypeMetadata TypeMetadata { get; }
        public IqlType ToIqlType()
        {
            return EntityProperty.TypeDefinition.ToIqlType();
        }

        public Func<object, object> GetValue => EntityProperty.GetValue;
        public Func<object, object, object> SetValue => EntityProperty.SetValue;
        public PropertyKind Kind => EntityProperty.Kind;
        public string PropertyName => EntityProperty.PropertyName;
        public object UnderlyingObject => EntityProperty;
        public EntityRelationship Relationship => EntityProperty.Relationship;
        public IProperty EntityProperty { get; }

        public PropertyMetadataProvider(IIqlTypeMetadata typeMetadata, PropertyBase entityProperty)
        {
            TypeMetadata = typeMetadata;
            EntityProperty = (IProperty)entityProperty;
        }
    }
}