using System.Threading.Tasks;

namespace Iql.Parsing
{
    public interface IAsyncActionParserBase
    {
        Task<IqlExpression> ToQueryStringAsync(IqlExpression action, IAsyncActionParserContext parser);
    }
}