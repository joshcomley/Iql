using System.Collections.Generic;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Crud.Operations
{
    public class PropertyChange
    {
        public PropertyChange(IKeyProperty property)
        {
            Property = property;
        }

        public IKeyProperty Property { get; }
        public List<PropertyChange> ChildChangedProperties { get; } = new List<PropertyChange>();
        public Dictionary<int, List<PropertyChange>> EnumerableChangedProperties { get; } = new Dictionary<int, List<PropertyChange>>();
    }
}