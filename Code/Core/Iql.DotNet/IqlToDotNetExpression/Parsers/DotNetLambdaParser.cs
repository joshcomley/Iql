using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetLambdaParser : DotNetActionParserBase<IqlLambdaExpression>
    {
        public override IqlExpression ToQueryString(IqlLambdaExpression action,
            DotNetIqlParserInstance parser)
        {
            var parameters = new List<ParameterExpression>();
            foreach (var parameter in action.Parameters)
            {
                var dotNetOutput = parser.Parse(parameter);
                var parameterExpression = (ParameterExpression) dotNetOutput.Expression;
                parameters.Add(parameterExpression);
            }
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    Expression.Lambda(parser.Parse(action.Body).Expression, parameters)
                );
            return expression;
        }
    }
}