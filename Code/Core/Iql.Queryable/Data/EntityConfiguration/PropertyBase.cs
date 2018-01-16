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
        public IProperty CountRelationship { get; private set; }
        public bool ReadOnly { get; private set; }
        public string Name { get; private set; }
        public Type ElementType { get; set; }
        public Type Type { get; set; }
        public bool IsCollection { get; internal set; }
        public Type DeclaringType { get; internal set; }
        public string ConvertedFromType { get; private set; }
        public abstract Func<object, object> PropertyGetter { get; set; }
        public abstract Func<object, object, object> PropertySetter { get; set; }

        internal void Configure(
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