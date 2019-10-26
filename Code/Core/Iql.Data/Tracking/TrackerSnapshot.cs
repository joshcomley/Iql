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
        public IEntityStateBase[] EntitiesPendingInsert { get; set; } = new IEntityStateBase[] { };
        public IEntityStateBase[] EntitiesPendingDelete { get; set; } = new IEntityStateBase[] { };
    }

    public class PropertySnapshot
    {
        public object PreviousValue { get; set; }
        public object CurrentValue { get; set; }
    }
}