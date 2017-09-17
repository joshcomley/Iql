namespace Iql.JavaScript.JavaScriptExpressionToIql.Expressions
{
    public interface IExpressionParserBase
    {
        IExpressionParseResultBase Parse(IExpressionParserInstance instance);
    }
}