namespace Iql.Parsing
{
    public interface IActionParserContext
    {
        object ParseAction(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        );
    }
}