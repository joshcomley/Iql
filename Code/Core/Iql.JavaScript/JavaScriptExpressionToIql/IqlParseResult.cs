namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public class IqlParseResult : IExpressionParseResult<IqlExpression>
    {
        public IqlParseResult(
            IqlExpression value)
        {
            Value = value;
        }

        public IqlExpression Value { get; set; }

        public object ResolveFinalResult()
        {
            return Value;
        }

        object IExpressionParseResultBase.Value
        {
            get => Value;
            set => Value = (IqlExpression) value;
        }
    }
}