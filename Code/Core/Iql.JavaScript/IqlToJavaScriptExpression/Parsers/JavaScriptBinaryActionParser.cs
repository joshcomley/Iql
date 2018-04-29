using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Expressions;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptBinaryActionParser : JavaScriptActionParserBase<IqlBinaryExpression>
    {
        public static bool IsString(
            IqlExpression expression,
            IEntityConfigurationBuilder builder)
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
            if (action.Kind == IqlExpressionKind.IsEqualTo || action.Kind == IqlExpressionKind.IsNotEqualTo)
            {
                isStringComparison =
                    IsString(action.Left, parser.Adapter.EntityConfigurationBuilder) ||
                    IsString(action.Right, parser.Adapter.EntityConfigurationBuilder);
                // We don't need to coalesce to upper case or null if one side is empty
                if (isStringComparison)
                {
                    var expressions = new[] { action.Left, action.Right };
                    for (var i = 0; i < expressions.Length; i++)
                    {
                        var expression = expressions[i];
                        if (expression.Kind == IqlExpressionKind.Literal)
                        {
                            var literal = expression as IqlLiteralExpression;
                            if (
                                Equals(literal.Value, null) || 
                                Equals(literal.Value, "")
                                )
                            {
                                isStringComparison = false;
                                break;
                            }
                        }
                    }
                }
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
            if (left.Kind == IqlExpressionKind.Literal && (left as IqlLiteralExpression).Value != null)
            {
                return new IqlStringToUpperCaseExpression(left as IqlReferenceExpression);
            }
            var checkExpression = new IqlIsEqualToExpression(left, new IqlFinalExpression<string>("null"));
            parser.Data.AlreadyCoalesced.Add(checkExpression);
            var finalExpression =
                left.Kind == IqlExpressionKind.Literal && (left as IqlLiteralExpression).Value == null
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
            switch (action.Kind)
            {
                case IqlExpressionKind.And:
                    return "&&";
                case IqlExpressionKind.Or:
                    return "||";
                case IqlExpressionKind.IsGreaterThan:
                    return ">";
                case IqlExpressionKind.IsGreaterThanOrEqualTo:
                    return ">=";
                case IqlExpressionKind.IsLessThan:
                    return "<";
                case IqlExpressionKind.IsLessThanOrEqualTo:
                    return "<=";
                case IqlExpressionKind.IsEqualTo:
                    return "==";
                case IqlExpressionKind.IsNotEqualTo:
                    return "!=";
                case IqlExpressionKind.Modulo:
                    return "%";
                case IqlExpressionKind.Add:
                    return "+";
                case IqlExpressionKind.Subtract:
                    return "-";
                case IqlExpressionKind.Multiply:
                    return "*";
                case IqlExpressionKind.Divide:
                    return "/";
                case IqlExpressionKind.AddEquals:
                    return "+=";
                case IqlExpressionKind.SubtractEquals:
                    return "-=";
                case IqlExpressionKind.MultiplyEquals:
                    return "*=";
                case IqlExpressionKind.DivideEquals:
                    return "/=";
                case IqlExpressionKind.Has:
                    return "&";
            }
            JavaScriptErrors.OperationNotSupported(action.Kind);
            return null;
        }
    }
}