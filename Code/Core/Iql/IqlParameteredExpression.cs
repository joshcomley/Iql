using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlParameteredExpression : IqlExpression
    {
        public List<IqlRootReferenceExpression> Parameters { get; set; } = new List<IqlRootReferenceExpression>();
        protected IqlParameteredExpression(IqlExpressionKind kind, IqlType? returnType, IqlExpression parent = null) : base(kind, returnType, parent) { }
    }
}