using System.Collections.Generic;

namespace Iql.Entities.Relationships
{
    public interface IRelationship
    {
        //RelationshipMultiplicity SourceMultiplicity { get; }
        //RelationshipMultiplicity TargetMultiplicity { get; }
        IList<IRelationshipConstraint> Constraints { get; }
        RelationshipKind Kind { get; }
        IRelationshipDetail Source { get; }
        IRelationshipDetail Target { get; }
        string ConstraintKey { get; }
        string QualifiedConstraintKey { get; }
    }
}