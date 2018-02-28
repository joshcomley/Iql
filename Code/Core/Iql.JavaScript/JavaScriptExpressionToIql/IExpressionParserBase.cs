namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public interface IExpressionParserBase
    {
        IExpressionParseResultBase Parse(IExpressionParserInstance instance);
    }
}