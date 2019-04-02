using System.Diagnostics;

namespace Iql
{
    [DebuggerDisplay("{ExpressionKindString}")]
    public class IqlFlattenedExpression : IqlFlattenedExpressionBase<IqlExpression>
    {
#if DEBUG
        public string ExpressionKindString => Expression?.Kind.ToString() ?? "NULL";
#endif
        public IqlFlattenedExpression(IqlExpression expression, IqlFlattenedExpression[] ancestors) : base(expression, ancestors)
        {
        }
    }
}