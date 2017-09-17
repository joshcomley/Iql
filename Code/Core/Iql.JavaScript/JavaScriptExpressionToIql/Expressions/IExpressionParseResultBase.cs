namespace Iql.JavaScript.JavaScriptExpressionToIql.Expressions
{
    public interface IExpressionParseResultBase
    {
        object Value { get; set; }
        object ResolveFinalResult();
    }
}