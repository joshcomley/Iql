using System.Threading.Tasks;

namespace Iql.Parsing
{
    public interface IAsyncActionParserContext
    {
        Task<object> ParseActionAsync(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        );
    }
}