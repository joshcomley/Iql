using System;

namespace Iql.Entities.Rules.Relationship
{
    public interface IRelationshipFilterContext
    {
        Type EntityType { get; }
        object Owner { get; set; }
    }
}