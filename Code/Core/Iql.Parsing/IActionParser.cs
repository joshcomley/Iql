namespace Iql.Parsing
{
    public interface IActionParser<in TAction, TIqlData, TQueryAdapter, TOutput, in TParserInstance>
        : IActionParserBase
        where TAction : IqlExpression
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData>
        where TParserInstance : ActionParserInstance<TIqlData, TQueryAdapter, TOutput>
        where TOutput : IParserOutput
    {
        IqlExpression ToQueryString(TAction action, TParserInstance parser);
    }
}