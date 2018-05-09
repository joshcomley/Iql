using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlNavigationExpression : IqlExpression
    {
        protected IqlNavigationExpression(IqlExpressionKind kind, IqlType type, IqlExpression parent = null) : base(kind, IqlType.Class, parent) { }
        public List<IqlExpandExpression> Expands { get; set; }
        public IqlExpression Filter { get; set; }
        public IqlWithKeyExpression WithKey { get; set; }
    }
}