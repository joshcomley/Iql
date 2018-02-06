using System;
using System.Collections.Generic;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public interface IRelationship
    {
        //RelationshipMultiplicity SourceMultiplicity { get; }
        //RelationshipMultiplicity TargetMultiplicity { get; }
        List<IRelationshipConstraint> Constraints { get; }
        RelationshipKind Kind { get; }
        IRelationshipDetail Source { get; }
        IRelationshipDetail Target { get; }
    }
}