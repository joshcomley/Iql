using Iql.Entities.Relationships;
using System.Linq.Expressions;

namespace Iql.Server.Serialization
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