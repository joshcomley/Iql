using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class Property<TProperty> : PropertyBase
    {
        public Property(
            string name, 
            bool isCollection, 
            Type declaringType,
            string convertedFromType = null,
            bool readOnly = false,
            IProperty countRelationship = null) : base(name, typeof(TProperty), isCollection, declaringType, convertedFromType, readOnly, countRelationship)
        {
        }
    }
}