using System;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public interface IRelationshipDetail
    {
        Type Type { get; }
        IqlPropertyExpression Property { get; set; }
        IEntityConfiguration Configuration { get; set; }
    }
}