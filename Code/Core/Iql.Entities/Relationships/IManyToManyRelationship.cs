using System;

namespace Iql.Data.Configuration.Relationships
{
    public interface IManyToManyRelationship
    {
        Type PivotType { get; }
        IqlPropertyExpression PivotTargetKeyProperty { get; }
        IqlPropertyExpression PivotSourceKeyProperty { get; }
    }
}