using System.Collections.Generic;

namespace Iql.Data.Configuration.Relationships
{
    public interface IRelationship
    {
        //RelationshipMultiplicity SourceMultiplicity { get; }
        //RelationshipMultiplicity TargetMultiplicity { get; }
        List<IRelationshipConstraint> Constraints { get; }
        RelationshipKind Kind { get; }
        IRelationshipDetail Source { get; }
        IRelationshipDetail Target { get; }
        string ConstraintKey { get; }
        string QualifiedConstraintKey { get; }
    }
}