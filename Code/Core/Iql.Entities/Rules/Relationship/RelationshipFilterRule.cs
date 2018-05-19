using System;
using System.Linq.Expressions;

namespace Iql.Entities.Rules.Relationship
{
    public class RelationshipFilterRule<TEntity, TRelationship> : Rule<RelationshipFilterContext<TEntity>, Expression<Func<TRelationship, bool>>>, IRelationshipRule
    {
        public RelationshipFilterRule(Expression<Func<RelationshipFilterContext<TEntity>, Expression<Func<TRelationship, bool>>>> expression, string key, string message) : base(expression, key, message)
        {
        }

        public override Func<RelationshipFilterContext<TEntity>, Expression<Func<TRelationship, bool>>> Run => _ => TypedInvoker(_);

        Func<object, LambdaExpression> IRuleBase<LambdaExpression>.Run => (Func<object, LambdaExpression>) Run;
    }
}