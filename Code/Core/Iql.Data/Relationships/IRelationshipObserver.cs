﻿using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Entities;
using Iql.Entities.Events;

namespace Iql.Data.Relationships
{
    public class UntrackedEntityAddedEvent
    {
        public object Entity { get; set; }

        public UntrackedEntityAddedEvent(object entity)
        {
            Entity = entity;
        }
    }
    public interface IRelationshipObserver
    {
        EventEmitter<UntrackedEntityAddedEvent> UntrackedEntityAdded { get; }
        bool TrackEntities { get; }
        void RunIfNotIgnored(Action action, IProperty property, object entity);
        void ObserveAll(Dictionary<Type, IList> dictionary);
        //void ObserveListTyped<T>(List<T> list) where T : class;
        void ObserveList(IList list, Type entityType);
        void Observe(object entity, Type entityType);
        void Unobserve(object entity, Type entityType);
        bool IsAssignedToAnyRelationship(object entity, Type entityType);
        void DeleteRelationships(object entity, Type type);
    }
}