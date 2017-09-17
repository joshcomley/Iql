using System;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlStringIndexOfExpressionReducer : IqlParentValueReducerBase<IqlStringIndexOfExpression>
    {
        public override IqlLiteralExpression Evaluate(IqlStringIndexOfExpression expression, IqlReducer reducer)
        {
            var indexOf = reducer.EvaluateAs<string>(expression.Parent).IndexOf(
                reducer.EvaluateAs<string>(expression.Value), StringComparison.Ordinal
            );
            return new IqlLiteralExpression(indexOf, IqlType.Integer);
        }
    }
}