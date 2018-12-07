using Iql.Conversion;

namespace Iql.Parsing
{
    public interface IActionParser<in TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, in TParserContext, TConverter>
        : IActionParserBase where TAction : IqlExpression
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData, IqlParserRegistry, IActionParserBase>
        where TParserContext : ActionParserContext<TIqlData, TQueryAdapter, TOutput, TParserOutput, TConverter>
        where TParserOutput : IParserOutput
        where TConverter : IExpressionConverter
    {
        TConverter Converter { get; set; }
        IqlExpression ToQueryString(TAction action, TParserContext parser);
    }
}