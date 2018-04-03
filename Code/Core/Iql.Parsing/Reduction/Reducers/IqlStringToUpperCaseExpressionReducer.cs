namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlStringToUpperCaseExpressionReducer : IqlReducerBase<IqlStringToUpperCaseExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlStringToUpperCaseExpression expression, IqlReducer reducer)
        {
            var value = reducer.EvaluateAs<string>(expression.Parent)?.ToUpper();
            return new IqlLiteralExpression(value, IqlType.String);
        }
    }
}