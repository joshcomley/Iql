namespace Iql.Parsing
{
    public interface IActionParserInstance
    {
        string Parse(IqlExpression expression, EvaluateContext evaluateContext);
    }
}