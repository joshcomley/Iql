using System;

namespace Iql.Entities.Relationships
{
    public interface IManyToManyRelationship
    {
        Type PivotType { get; }
        IqlPropertyExpression PivotTargetKeyProperty { get; }
        IqlPropertyExpression PivotSourceKeyProperty { get; }
    }
}