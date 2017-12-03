using System;
using System.Collections.Generic;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IEntityConfiguration
    {
        List<IRelationship> Relationships { get; }
        List<IProperty> Properties { get; }
        IEntityKey Key { get; }
        Type Type { get; }
        IProperty FindProperty(string name);
        RelationshipMatch FindRelationship(string propertyName);
        List<RelationshipMatch> AllRelationships();
        bool EntityHasKey(object entity, CompositeKey key);
        bool KeysMatch(object left, object right);
        CompositeKey GetCompositeKey(object entity);
    }
}