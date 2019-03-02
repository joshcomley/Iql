using System;
using System.Linq;

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
            entity = entity ?? CurrentEntity;
            if (Success)
            {
                Property.SetValue(entity, NewValue);
            }
        }

        public void UndoChange(object entity = null)
        {
            entity = entity ?? CurrentEntity;
            if (Success)
            {
                Property.SetValue(entity, OldValue);
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
    public class InferredValueChanges
    {
        public bool Success { get; }
        public IEntityConfiguration EntityConfiguration => SourceProperty.EntityConfiguration;
        public InferredValueChange[] Changes { get; }
        public IProperty SourceProperty { get; }
        public InferredValueChanges(bool success, object oldEntity, object currentEntity, IProperty sourceProperty, InferredValueChange[] changes)
        {
            Success = success;
            OldEntity = oldEntity;
            CurrentEntity = currentEntity;
            SourceProperty = sourceProperty;
            Changes = changes ?? new InferredValueChange[] { };
        }

        public void ApplyChanges(object entity = null)
        {
            entity = entity ?? CurrentEntity;
            if (Success)
            {
                foreach (var change in Changes)
                {
                    change.ApplyChange(entity);
                }
            }
        }

        public void UndoChanges(object entity = null)
        {
            entity = entity ?? CurrentEntity;
            if (Success)
            {
                foreach (var change in Changes)
                {
                    change.UndoChange(entity);
                }
            }
        }

        public object CurrentEntity { get; set; }
        public object OldEntity { get; set; }
    }

    public class InferredValuesResult
    {
        public InferredValueChanges[] Results { get; }
        public object CurrentEntity { get;  }
        public object OldEntity { get;  }
        public bool Success => Results == null ? true : Results.All(_ => _.Success);
        public void ApplyChanges(object entity = null)
        {
            entity = entity ?? CurrentEntity;
                foreach (var change in Results)
                {
                    change.ApplyChanges(entity);
                }
        }

        public void UndoChanges(object entity = null)
        {
            entity = entity ?? CurrentEntity;
            foreach (var change in Results)
            {
                change.UndoChanges(entity);
            }
        }

        public InferredValuesResult(object oldEntity, object currentEntity, InferredValueChanges[] results)
        {
            Results = results;
            CurrentEntity = currentEntity;
            OldEntity = oldEntity;
        }
    }

    public class InferredValueContext<T> : IInferredValueContext
        where T : class
    {
        public Type EntityType => typeof(T) ?? OldEntityState?.GetType() ?? CurrentEntityState?.GetType();

        public T OldEntityState { get; set; }

        object IInferredValueContext.OldEntityState
        {
            get => OldEntityState; set => OldEntityState = (T)value;
        }
        public T CurrentEntityState { get; set; }
        object IInferredValueContext.CurrentEntityState
        {
            get => CurrentEntityState; set => CurrentEntityState = (T)value;
        }

        public InferredValueContext(T oldEntityState, T currentEntityState)
        {
            OldEntityState = oldEntityState;
            CurrentEntityState = currentEntityState;
        }
    }
}