using Iql.Entities.Rules.Relationship;
using System;
using System.Linq.Expressions;

namespace Iql.Server.Serialization
{
    public class RelationshipRule : RuleBase, IRelationshipRule
    {
        public Func<object, LambdaExpression> Run { get; }
    }
}