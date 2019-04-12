using System;
using Iql.Extensions;
using Iql.Serialization;

namespace Iql.Entities
{
    public class TypeDetail : ITypeDefinition
    {
        private string _typeName;
        private string _elementTypeName;
        public Type Type { get; set; }

        public string TypeName
        {
            get => Type == null ? _typeName : Type.GetFullName();
            set => _typeName = value;
        }

        public bool Nullable { get; set; }
        public Type DeclaringType { get; set; }
        public IqlType Kind { get; set; }

        public object EnsureValueType(object value)
        {
            return IqlJsonDeserializer.EnsureValueType(value, Type, Nullable);
        }

        public string ConvertedFromType { get; set; }
        public Type ElementType { get; set; }

        public string ElementTypeName
        {
            get => ElementType == null ? _elementTypeName : ElementType.GetFullName();
            set => _elementTypeName = value;
        }

        public bool IsCollection { get; set; }

        public TypeDetail(
            Type type = null, 
            bool nullable = false, 
            Type declaringType = null, 
            string convertedFromType = null, 
            Type elementType = null, 
            bool isCollection = false, 
            IqlType kind = IqlType.Unknown)
        {
            Type = type;
            Nullable = nullable;
            DeclaringType = declaringType;
            ConvertedFromType = convertedFromType;
            ElementType = elementType;
            IsCollection = isCollection;
            Kind = kind;
        }
    }
}
