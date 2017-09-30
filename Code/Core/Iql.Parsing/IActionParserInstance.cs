namespace Iql.Parsing
{
    public interface IActionParserInstance
    {
        string Parse(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
            );
    }
}