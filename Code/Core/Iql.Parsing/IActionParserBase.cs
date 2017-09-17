namespace Iql.Parsing
{
    public interface IActionParserBase
    {
        IqlExpression ToQueryString(IqlExpression action, IActionParserInstance parser);
    }
}