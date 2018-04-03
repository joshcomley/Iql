using System;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlStringIncludesExpressionReducer : IqlParentValueReducerBase<IqlStringIncludesExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlStringIncludesExpression expression, IqlReducer reducer)
        {
            var includes = reducer.EvaluateAs<string>(expression.Parent).IndexOf(
                               reducer.EvaluateAs<string>(expression.Value), StringComparison.Ordinal
                           ) != -1;
            return new IqlLiteralExpression(includes, IqlType.Boolean);
        }
    }
}