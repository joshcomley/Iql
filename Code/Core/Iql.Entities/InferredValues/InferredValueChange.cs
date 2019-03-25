using System;

namespace Iql.Entities.InferredValues
{
    public class InferredValueChange
    {
        public IEntityConfiguration EntityConfiguration => Property.EntityConfiguration;
        public bool HasChanged { get; }
        public bool Success { get; }
        public IProperty Property { get; }
        public object OldEntity { get; }
        public object CurrentEntity { get; }
        public object OldValue { get; }
        public object NewValue { get; }
        public void ApplyChange(object entity = null)
        {
            if (Success)
            {
                Property.SetValue(entity ?? CurrentEntity, NewValue);
            }
        }

        public void UndoChange(object entity = null)
        {
            if (Success)
            {
                Property.SetValue(entity ?? CurrentEntity, OldValue);
            }
        }

        public InferredValueChange(bool hasChanged, bool success, IProperty property, object oldEntity, object currentEntity, object oldValue, object newValue)
        {
            HasChanged = hasChanged;
            Success = success;
            Property = property;
            OldEntity = oldEntity;
            CurrentEntity = currentEntity;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}