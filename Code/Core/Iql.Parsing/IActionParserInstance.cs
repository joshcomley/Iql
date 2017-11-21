namespace Iql.Parsing
{
    public interface IActionParserInstance
    {
        object Parse(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
            );
    }
}