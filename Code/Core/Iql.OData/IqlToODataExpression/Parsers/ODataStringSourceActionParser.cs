namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataStringSourceActionParser : ODataMethodActionParser<IqlExpression>
    {
        public override IqlExpression[] ResolveMethodArguments(IqlExpression action)
        {
            return new[] {action.Parent};
        }

        public override string ResolveMethodName(IqlExpression action)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.StringToUpperCase:
                    return "toupper";
                case IqlExpressionKind.StringToLowerCase:
                    return "tolower";
                case IqlExpressionKind.StringTrim:
                    return "trim";
                default:
                    ODataErrors.OperationNotSupported(action.Kind);
                    break;
            }
            return null;
        }
    }
}