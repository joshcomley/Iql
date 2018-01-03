using System;
using System.Linq.Expressions;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public abstract class PropertyBase : IProperty
    {
        public bool Nullable { get; set; }
        public RelationshipMatch Relationship { get; set; }
        public PropertyKind Kind { get; set; }
        public IProperty CountRelationship { get; }
        public bool ReadOnly { get; }
        public string Name { get; }
        public Type ElementType { get; }
        public Type Type { get; }
        public bool IsCollection { get; }
        public Type DeclaringType { get; }
        public string ConvertedFromType { get; }
        public abstract Func<object, object> PropertyGetter { get; }
        public abstract Func<object, object, object> PropertySetter { get; }

        protected PropertyBase(
            string name, 
            Type elementType, 
            bool isCollection,
            Type declaringType, 
            string convertedFromType, 
            bool readOnly, 
            IProperty countRelationship
            )
        {
            Name = name;
            ElementType = elementType;
            if (TypeExtensions.IsEnumerableType(elementType))
            {
                var a = 0;
            }
            IsCollection = isCollection;
            DeclaringType = declaringType;
            ConvertedFromType = convertedFromType;
            ReadOnly = readOnly;
            CountRelationship = countRelationship;
            Kind = PropertyKind.Primitive;
        }
    }
}