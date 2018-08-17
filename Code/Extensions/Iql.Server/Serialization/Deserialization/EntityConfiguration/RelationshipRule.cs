using System;
using System.Linq.Expressions;
using Iql.Entities.Rules.Relationship;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public class RelationshipRule : RuleBase, IRelationshipRule
    {
        public Func<object, LambdaExpression> Run { get; }
    }
}