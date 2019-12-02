using System;

namespace Iql.Entities
{
    public interface IPropertyMetadataProvider
    {
        ITypeProperty PropertyMetadata { get; }
    }

    public interface ITypeProperty
    {
        bool IsCollection { get; }
        Type ElementType { get; }
        Type Type { get; }
        IIqlTypeMetadata TypeMetadata { get; }
        IqlType ToIqlType();
        Func<object, object> GetValue { get; }
        Func<object, object, object> SetValue { get; }
        IqlPropertyKind Kind { get; }
        string PropertyName { get; }
        object UnderlyingObject { get; }
        //IProperty EntityProperty { get; }
        //EntityRelationship Relationship { get; }
    }
}