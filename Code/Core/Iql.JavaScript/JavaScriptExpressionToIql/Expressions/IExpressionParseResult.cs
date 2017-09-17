namespace Iql.JavaScript.JavaScriptExpressionToIql.Expressions
{
    public interface IExpressionParseResult<T> : IExpressionParseResultBase
    {
        new T Value { get; set; }
    }
}