using Iql.Queryable.Expressions.Conversion;

namespace Iql.Parsing
{
    public interface IActionParser<in TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, in TParserInstance, TConverter>
        : IActionParserBase
        where TAction : IqlExpression
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData>
        where TParserInstance : ActionParserInstance<TIqlData, TQueryAdapter, TOutput, TParserOutput, TConverter>
        where TParserOutput : IParserOutput
        where TConverter : IExpressionConverter
    {
        TConverter Converter { get; set; }
        IqlExpression ToQueryString(TAction action, TParserInstance parser);
    }
}