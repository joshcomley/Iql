using System;
using System.Linq.Expressions;

namespace Iql.Data.Configuration.Rules.Display
{
    public class DisplayRule<TEntity> : BinaryRule<TEntity>, IDisplayRule
    {
        public DisplayRule(Expression<Func<TEntity, bool>> expression, string key, string message) : base(expression, key, message)
        {
        }

        public DisplayRuleKind Kind { get; set; }
        protected override bool InverseResult => false;
    }
}