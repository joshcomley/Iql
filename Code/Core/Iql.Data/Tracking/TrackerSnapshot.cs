using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Crud.Operations;
using Iql.Data.Tracking.State;

namespace Iql.Data.Tracking
{
    public class TrackerSnapshot
    {
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
        public Guid Id { get; set; }
        public Dictionary<IPropertyState, PropertySnapshot> Values { get; set; } = new Dictionary<IPropertyState, PropertySnapshot>();
        public Dictionary<IEntityStateBase, EntitySnapshot> Entities { get; set; } = new Dictionary<IEntityStateBase, EntitySnapshot>();
    }

    public class PropertySnapshot
    {
        public IPropertyState State { get; set; }
        public object PreviousValue { get; set; }
        public object CurrentValue { get; set; }
    }
    public class EntitySnapshot
    {
        public IEntityStateBase State { get; set; }
        public EntityStatus PreviousValue { get; set; }
        public EntityStatus CurrentValue { get; set; }
    }
}