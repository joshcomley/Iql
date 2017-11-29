using System;

namespace Iql.Queryable.Events
{
    public interface IPropertyChangeEvent
    {
        string PropertyName { get; }
        object Entity { get; }
        Type EntityType { get; }
        object OldValue { get; }
        object NewValue { get; }
    }
}