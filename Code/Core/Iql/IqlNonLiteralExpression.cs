using System;

namespace Iql
{
    public abstract class IqlNonLiteralExpression : IqlReferenceExpression
    {
        protected IqlNonLiteralExpression(IqlExpressionKind kind,
            IqlType returnType)
            : base(kind, returnType)
        {
        }

        public override bool IsOrHas(Func<IqlExpression, bool> matches)
        {
            return Parent.IsOrHas(matches);
        }
    }
}