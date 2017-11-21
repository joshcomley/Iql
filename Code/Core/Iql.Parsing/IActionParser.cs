namespace Iql.Parsing
{
    public interface IActionParser<in TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, in TParserInstance>
        : IActionParserBase
        where TAction : IqlExpression
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData>
        where TParserInstance : ActionParserInstance<TIqlData, TQueryAdapter, TOutput, TParserOutput>
        where TParserOutput : IParserOutput
    {
        IqlExpression ToQueryString(TAction action, TParserInstance parser);
    }
}