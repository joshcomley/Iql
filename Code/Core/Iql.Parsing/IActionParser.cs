namespace Iql.Parsing
{
    public interface IActionParser<TAction, TIqlData, TQueryAdapter>
        : IActionParserBase
        where TAction : IqlExpression
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData>
    {
        IqlExpression ToQueryString(TAction action, ActionParserInstance<TIqlData, TQueryAdapter> parser);
    }
}