using Iql.Extensions;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlLengthExpressionReducer : IqlReducerBase<IqlLengthExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlLengthExpression expression, IqlReducer reducer)
        {
            object value = null;
            IqlType type = IqlType.Unknown;
            if (expression.Parent.Kind == IqlExpressionKind.Literal)
            {
                value = (expression.Parent as IqlLiteralExpression).Value;
            }
#if TypeScript
            else if (reducer.EvaluateContext != null && expression.Parent.Kind == IqlExpressionKind.Variable)
            {
                value = reducer.EvaluateContext.Evaluate((expression.Parent as IqlVariableExpression).VariableName);
            }
#endif
            else
            {
                return null;
            }
            if (value != null)
            {
                type = value.GetType().ToIqlType();
            }

            if (type == IqlType.GeographyLine || type == IqlType.GeographyMultiLine ||
                type == IqlType.GeometryLine || type == IqlType.GeometryMultiLine)
            {
                var line = value as IqlLineExpression;
                return new IqlLiteralExpression(line.Length(), IqlType.Decimal);
            }
            return null;
        }
    }
}