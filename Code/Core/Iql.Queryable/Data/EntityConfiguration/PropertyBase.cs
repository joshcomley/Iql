using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class PropertyBase : IProperty
    {
        public bool Nullable { get; set; }
        public RelationshipMatch Relationship { get; set; }
        public PropertyKind Kind { get; set; }
        public IProperty CountRelationship { get; }
        public bool ReadOnly { get; }
        public string Name { get; }
        public Type Type { get; }
        public bool IsCollection { get; }
        public Type DeclaringType { get; }
        public string ConvertedFromType { get; }

        public PropertyBase(
            string name, 
            Type type, 
            bool isCollection,
            Type declaringType, 
            string convertedFromType, 
            bool readOnly, 
            IProperty countRelationship
            )
        {
            Name = name;
            Type = type;
            IsCollection = isCollection;
            DeclaringType = declaringType;
            ConvertedFromType = convertedFromType;
            ReadOnly = readOnly;
            CountRelationship = countRelationship;
            Kind = PropertyKind.Primitive;
        }
    }
}