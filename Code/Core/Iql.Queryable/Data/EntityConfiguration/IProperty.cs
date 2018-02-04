using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IProperty
    {
#if !TypeScript
        PropertyInfo PropertyInfo { get; set; }
#endif
        bool Nullable { get; set; }
        RelationshipMatch Relationship { get; set; }
        PropertyKind Kind { get; set; }
        IProperty CountRelationship { get; }
        bool ReadOnly { get; }
        string Name { get; }
        Type ElementType { get; }
        Type Type { get; }
        bool IsCollection { get; }
        Type DeclaringType { get; }
        string ConvertedFromType { get; }
        Func<object, object> PropertyGetter { get; }
        Func<object, object, object> PropertySetter { get; }
    }
}