using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class Property<TProperty> : PropertyBase
    {
        public Property(string name, bool isCollection, Type declaringType) : base(name, typeof(TProperty), isCollection, declaringType)
        {
        }
    }
}