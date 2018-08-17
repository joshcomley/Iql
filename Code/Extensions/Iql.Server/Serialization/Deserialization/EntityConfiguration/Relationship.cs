using System.Linq.Expressions;
using Iql.Entities.Relationships;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public class Relationship : RelationshipBase, IRelationship
    {
        public new IRelationshipDetail Source { get; set; }
        IRelationshipDetail IRelationship.Source => Source;

        public new IRelationshipDetail Target { get; set; }
        IRelationshipDetail IRelationship.Target => Target;

        protected override IRelationshipDetail BuildSource(LambdaExpression property)
        {
            return null;
        }

        protected override IRelationshipDetail BuildTarget(LambdaExpression property)
        {
            return null;
        }
    }
}