namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataStringSourceValueActionParser : ODataMethodActionParser<IqlParentValueExpression>
    {
        public override IqlExpression[] ResolveMethodArguments(IqlParentValueExpression action)
        {
            return new[] {action.Parent, action.Value};
        }

        public override string ResolveMethodName(IqlParentValueExpression action)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.StringIndexOf:
                    return "indexof";
                case IqlExpressionKind.StringIncludes:
                    return "contains";
                case IqlExpressionKind.StringEndsWith:
                    return "endswith";
                case IqlExpressionKind.StringStartsWith:
                    return "startswith";
                case IqlExpressionKind.StringConcat:
                    return "concat";
                default:
                    ODataErrors.OperationNotSupported(action.Kind);
                    break;
            }
            return null;
        }
    }
}