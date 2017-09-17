namespace Iql.OData.Parsers
{
    public class ODataStringSourceActionParser : ODataMethodActionParser<IqlExpression>
    {
        public override IqlExpression[] ResolveMethodArguments(IqlExpression action)
        {
            return new[] {action.Parent};
        }

        public override string ResolveMethodName(IqlExpression action)
        {
            switch (action.Type)
            {
                case IqlExpressionType.StringToUpperCase:
                    return "toupper";
                case IqlExpressionType.StringToLowerCase:
                    return "tolower";
                case IqlExpressionType.StringTrim:
                    return "trim";
                default:
                    ODataErrors.OperationNotSupported(action.Type);
                    break;
            }
            return null;
        }
    }
}