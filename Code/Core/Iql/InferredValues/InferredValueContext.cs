﻿using System;

namespace Iql.Entities.InferredValues
{
    public class InferredValueContext<T> : IInferredValueContext
        where T : class
    {
        public Type EntityType => typeof(T) ?? PreviousEntityState?.GetType() ?? CurrentEntityState?.GetType();

        public T PreviousEntityState { get; set; }

        object IInferredValueContext.PreviousEntityState
        {
            get => PreviousEntityState; set => PreviousEntityState = (T)value;
        }
        public T CurrentEntityState { get; set; }
        object IInferredValueContext.CurrentEntityState
        {
            get => CurrentEntityState; set => CurrentEntityState = (T)value;
        }

        public InferredValueContext(T previousEntityState, T currentEntityState)
        {
            PreviousEntityState = previousEntityState;
            CurrentEntityState = currentEntityState;
        }
    }
}