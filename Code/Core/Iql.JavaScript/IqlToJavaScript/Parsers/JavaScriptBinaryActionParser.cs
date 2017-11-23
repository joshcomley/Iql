namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptBinaryActionParser : JavaScriptActionParserBase<IqlBinaryExpression>
    {
        public static bool IsString(IqlExpression expression)
        {
            var literalExpression = expression as IqlLiteralExpression;
            if (literalExpression != null)
            {
                return literalExpression.ReturnType == IqlType.String;
            }
            var propertyExpression = expression as IqlPropertyExpression;
            return propertyExpression?.ReturnType == IqlType.String;
        }

        public override IqlExpression ToQueryString(
            IqlBinaryExpression action,
            JavaScriptIqlParserInstance parser)
        {
            var spacer = new IqlFinalExpression<string>(" ");
            var isStringComparison = false;
            if (action.Type == IqlExpressionType.IsEqualTo || action.Type == IqlExpressionType.IsNotEqualTo)
            {
                isStringComparison =
                    IsString(action.Left) ||
                    IsString(action.Right);
            }
            var left = action.Left;
            var right = action.Right;
            if (isStringComparison && !parser.Data.AlreadyCoalesced.Contains(action))
            {
                left = CoalesceOrUpperCase(left, parser);
                right = CoalesceOrUpperCase(right, parser);
            }
            return new IqlParenthesisExpression(
                new IqlAggregateExpression(
                    left,
                    spacer,
                    new IqlFinalExpression<string>(ResolveOperator(action)),
                    spacer,
                    right
                )
            );
        }

        private IqlExpression CoalesceOrUpperCase(IqlExpression left,
            JavaScriptIqlParserInstance parser)
        {
            var checkExpression = new IqlIsEqualToExpression(left, new IqlFinalExpression<string>("null"));
            parser.Data.AlreadyCoalesced.Add(checkExpression);
            var finalExpression =
                left.Type == IqlExpressionType.Literal && (left as IqlLiteralExpression).Value == null
                    ? left
                    : new IqlStringToUpperCaseExpression(left as IqlReferenceExpression);
            return
                new IqlParenthesisExpression(
                    new IqlAggregateExpression(
                        checkExpression,
                        new IqlFinalExpression<string>(" ? "),
                        new IqlFinalExpression<string>("null"),
                        new IqlFinalExpression<string>(" : "),
                        finalExpression
                    )
                );
        }

        public string ResolveOperator(IqlBinaryExpression action)
        {
            switch (action.Type)
            {
                case IqlExpressionType.And:
                    return "&&";
                case IqlExpressionType.Or:
                    return "||";
                case IqlExpressionType.IsGreaterThan:
                    return ">";
                case IqlExpressionType.IsGreaterThanOrEqualTo:
                    return ">=";
                case IqlExpressionType.IsLessThan:
                    return "<";
                case IqlExpressionType.IsLessThanOrEqualTo:
                    return "<=";
                case IqlExpressionType.IsEqualTo:
                    return "==";
                case IqlExpressionType.IsNotEqualTo:
                    return "!=";
                case IqlExpressionType.Modulo:
                    return "%";
                case IqlExpressionType.Add:
                    return "+";
                case IqlExpressionType.Subtract:
                    return "-";
                case IqlExpressionType.Multiply:
                    return "*";
                case IqlExpressionType.Divide:
                    return "/";
                case IqlExpressionType.AddEquals:
                    return "+=";
                case IqlExpressionType.SubtractEquals:
                    return "-=";
                case IqlExpressionType.MultiplyEquals:
                    return "*=";
                case IqlExpressionType.DivideEquals:
                    return "/=";
                case IqlExpressionType.BitwiseAnd:
                    return "&";
            }
            JavaScriptErrors.OperationNotSupported(action.Type);
            return null;
        }
    }
}