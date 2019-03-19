using System;

namespace Iql.Entities.InferredValues
{
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