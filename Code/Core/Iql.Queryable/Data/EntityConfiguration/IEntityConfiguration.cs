using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        IProperty FindOrDefineProperty<TProperty>(LambdaExpression expression, Type elementType);
        IProperty FindOrDefinePropertyByName(string name, Type elementType);
        RelationshipMatch FindRelationship(string propertyName);
        List<RelationshipMatch> AllRelationships();
        bool EntityHasKey(object entity, CompositeKey key);
        bool KeysMatch(object left, object right);
        CompositeKey GetCompositeKey(object entity);
    }
}