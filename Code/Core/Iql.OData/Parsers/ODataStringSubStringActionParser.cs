namespace Iql.OData.Parsers
{
    public class ODataStringSubStringActionParser : ODataMethodActionParser<IqlStringSubStringExpression>
    {
        public override IqlExpression[] ResolveMethodArguments(IqlStringSubStringExpression action)
        {
            return new[] {action.Value, action.Take};
        }

        public override string ResolveMethodName(IqlStringSubStringExpression action)
        {
            return "substring";
        }
    }
}