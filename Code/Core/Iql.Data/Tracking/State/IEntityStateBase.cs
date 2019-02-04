﻿using System;
using System.Collections.Generic;
using Iql.Conversion;
using Iql.Data.Crud.Operations;
using Iql.Data.Events;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.Relationships;

namespace Iql.Data.Tracking.State
{
    public interface IEntityStateBase : IJsonSerializable
    {
        object Entity { get; }
        IPropertyState[] PropertyStates { get; }
        CompositeKey CurrentKey { get; set; }
        bool MarkedForDeletion { get; set; }
        bool MarkedForCascadeDeletion { get; set; }
        Guid? PersistenceKey { get; set; }
        bool IsNew { get; set; }
        Type EntityType { get; }
        bool MarkedForAnyDeletion { get; }
        List<CascadeDeletion> CascadeDeletedBy { get; }

        bool HasValidKey();
        void Reset();
        CompositeKey KeyBeforeChanges();
        IPropertyState[] GetChangedProperties(IProperty[] properties = null);
        IEntityConfiguration EntityConfiguration { get; }
        EventEmitter<MarkedForDeletionChangeEvent> MarkedForDeletionChanged { get; }
        IPropertyState GetPropertyState(string name);
        void MarkForCascadeDeletion(object from, IRelationship relationship);
        void UnmarkForDeletion();
        void AbandonChanges();
    }
}