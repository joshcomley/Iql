using System;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public interface IManyToManyRelationship
    {
        Type PivotType { get; }
        IqlPropertyExpression PivotTargetKeyProperty { get; }
        IqlPropertyExpression PivotSourceKeyProperty { get; }
    }
}