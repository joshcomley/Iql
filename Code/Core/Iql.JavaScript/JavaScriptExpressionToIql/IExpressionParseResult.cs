namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public interface IExpressionParseResult<T> : IExpressionParseResultBase
    {
        new T Value { get; set; }
    }
}