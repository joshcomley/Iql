using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Entities.Relationships
{
    public interface IRelationshipDetailMetadata : ISimpleProperty
    {
        List<ValueMapping> ValueMappings { get; set; }
        List<RelationshipMapping> RelationshipMappings { get; set; }
        RelationshipSide RelationshipSide { get; }
        Type Type { get; }
        bool IsCollection { get; }
        IProperty Property { get; set; }
        IProperty CountProperty { get; }
    }
}