using System;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlStringIncludesExpressionReducer : IqlParentValueReducerBase<IqlStringIncludesExpression>
    {
        public override IqlLiteralExpression Evaluate(IqlStringIncludesExpression expression, IqlReducer reducer)
        {
            var includes = reducer.EvaluateAs<string>(expression.Parent).IndexOf(
                               reducer.EvaluateAs<string>(expression.Value), StringComparison.Ordinal
                           ) != -1;
            return new IqlLiteralExpression(includes, IqlType.Boolean);
        }
    }
}