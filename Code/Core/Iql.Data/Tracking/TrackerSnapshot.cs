using System;
using System.Collections.Generic;
using Iql.Data.Crud.Operations;

namespace Iql.Data.Tracking
{
    internal class TrackerSnapshot
    {
        public Guid Id { get; set; }
        public Dictionary<IPropertyState, PropertySnapshot> Values { get; set; } = new Dictionary<IPropertyState, PropertySnapshot>();
    }

    internal class PropertySnapshot
    {
        public object PreviousValue { get; set; }
        public object CurrentValue { get; set; }
    }
}