namespace Iql.OData.Parsers
{
    public class ODataStringSourceValueActionParser : ODataMethodActionParser<IqlParentValueExpression>
    {
        public override IqlExpression[] ResolveMethodArguments(IqlParentValueExpression action)
        {
            return new[] {action.Parent, action.Value};
        }

        public override string ResolveMethodName(IqlParentValueExpression action)
        {
            switch (action.Type)
            {
                case IqlExpressionType.StringIndexOf:
                    return "indexof";
                case IqlExpressionType.StringIncludes:
                    return "contains";
                case IqlExpressionType.StringEndsWith:
                    return "endswith";
                case IqlExpressionType.StringStartsWith:
                    return "startswith";
                case IqlExpressionType.StringConcat:
                    return "concat";
                default:
                    ODataErrors.OperationNotSupported(action.Type);
                    break;
            }
            return null;
        }
    }
}