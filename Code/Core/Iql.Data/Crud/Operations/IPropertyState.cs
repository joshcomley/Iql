﻿using Iql.Data.Configuration;
using Iql.Data.Tracking.State;

namespace Iql.Data.Crud.Operations
{
    public interface IPropertyState
    {
        IEntityStateBase EntityState { get; }
        bool HasChanged { get; }
        object NewValue { get; set; }
        object OldValue { get; set; }
        IProperty Property { get; }
        void Reset();
        IPropertyState Copy();
        void AbandonChange();
    }
}