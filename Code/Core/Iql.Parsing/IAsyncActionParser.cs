using System.Threading.Tasks;
using Iql.Conversion;

namespace Iql.Parsing
{
    public interface IAsyncActionParser<in TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, in TParserContext, TConverter>
        : IAsyncActionParserBase
        where TAction : IqlExpression
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData, IqlAsyncParserRegistry, IAsyncActionParserBase>
        where TParserContext : AsyncActionParserContext<TIqlData, TQueryAdapter, TOutput, TParserOutput, TConverter>
        where TParserOutput : IParserOutput
        where TConverter : IExpressionConverter
    {
        TConverter Converter { get; set; }
        Task<IqlExpression> ToQueryStringAsync(TAction action, TParserContext parser);
    }
}