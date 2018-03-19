using System;
using System.Collections.Generic;
using System.Reflection;
using Iql.Queryable.Data.EntityConfiguration.Validation;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IProperty
    {
#if !TypeScript
        PropertyInfo PropertyInfo { get; set; }
#endif
        IValidationCollection Validation { get; }
        bool Nullable { get; set; }
        RelationshipMatch Relationship { get; set; }
        PropertyKind Kind { get; set; }
        IProperty CountRelationship { get; }
        bool ReadOnly { get; set; }
        string Name { get; set; }
        string FriendlyName { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        List<object> Helpers { get; set; }
        List<string> Hints { get; set; }
        Type ElementType { get; }
        Type Type { get; }
        bool IsCollection { get; }
        Type DeclaringType { get; }
        string ConvertedFromType { get; }
        Func<object, object> PropertyGetter { get; }
        Func<object, object, object> PropertySetter { get; }
        Dictionary<string, object> CustomInformation { get; }
    }
}