using System.Collections.Generic;
using System.Diagnostics;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Crud.Operations
{
    [DebuggerDisplay("{Property.Name}")]
    public class PropertyChange
    {
        public PropertyChange(IProperty property, RelationshipMatch relationship)
        {
            Property = property;
            Relationship = relationship;
        }

        public IProperty Property { get; }
        public RelationshipMatch Relationship { get; }
        public bool IsRelationship { get; }
        public List<PropertyChange> ChildChangedProperties { get; } = new List<PropertyChange>();
        public Dictionary<int, List<PropertyChange>> EnumerableChangedProperties { get; } = new Dictionary<int, List<PropertyChange>>();
    }
}