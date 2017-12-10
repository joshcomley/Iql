using System.Collections.Generic;
using System.Diagnostics;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Crud.Operations
{
    [DebuggerDisplay("{Property.Name}")]
    public class PropertyChange
    {
        public PropertyChange(IProperty property, object oldValue, object newValue, IEntityStateBase entityState)
        {
            Property = property;
            OldValue = oldValue;
            NewValue = newValue;
            EntityState = entityState;
        }

        public object OldValue { get; set; }
        public object NewValue { get; set; }
        public IEntityStateBase EntityState { get; }

        public IProperty Property { get; }
        public List<PropertyChange> ChildChangedProperties { get; } = new List<PropertyChange>();

        public Dictionary<int, List<PropertyChange>> EnumerableChangedProperties { get; } =
            new Dictionary<int, List<PropertyChange>>();
    }
}