using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlCollectitonQueryExpression : IqlNavigationExpression
    {
        protected IqlCollectitonQueryExpression(
            string entityTypeName, 
            IqlExpressionKind kind, 
            IqlExpression parent = null) 
            : base(entityTypeName, kind, IqlType.Class, parent) { }
        public List<IqlOrderByExpression> OrderBys { get; set; }
        public bool? IncludeCount { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}