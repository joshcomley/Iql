using System;

namespace Iql.Data.Configuration.Events
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