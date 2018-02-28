namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public interface IExpressionParseResultBase
    {
        object Value { get; set; }
        object ResolveFinalResult();
    }
}