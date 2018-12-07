using Iql.Extensions;

namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringConditionExpressionParser : DotNetStringActionParserBase<IqlConditionExpression>
    {
        public override IqlExpression ToQueryString(IqlConditionExpression action,
            DotNetStringIqlParserInstance parser)
        {
            if (action.IfTrue.Kind == IqlExpressionKind.Lambda)
            {
                var lambda = action.IfTrue as IqlLambdaExpression;
                var cast = $"Expression<Func<{lambda.Parameters[0].EntityTypeName},{lambda.Body.ReturnType.ToType().GetFullName()}>>";
                return new IqlAggregateExpression(
                    new IqlFinalExpression<string>("("),
                    action.Test,
                    new IqlFinalExpression<string>("?"),
                    new IqlFinalExpression<string>("("),
                    new IqlFinalExpression<string>(cast),
                    new IqlFinalExpression<string>(")"),
                    new IqlFinalExpression<string>("("),
                    action.IfTrue,
                    new IqlFinalExpression<string>(")"),
                    new IqlFinalExpression<string>(":"),
                    action.IfFalse,
                    new IqlFinalExpression<string>(")")
                );
            }
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>("("),
                action.Test,
                new IqlFinalExpression<string>("?"),
                action.IfTrue,
                new IqlFinalExpression<string>(":"),
                action.IfFalse,
                new IqlFinalExpression<string>(")")
            );
        }
    }
}