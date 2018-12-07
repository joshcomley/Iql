using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetLambdaParser : DotNetActionParserBase<IqlLambdaExpression>
    {
        public override IqlExpression ToQueryString(IqlLambdaExpression action,
            DotNetIqlParserContext parser)
        {
            var parameters = new List<ParameterExpression>();
            foreach (var parameter in action.Parameters)
            {
                var dotNetOutput = parser.Parse(parameter);
                var parameterExpression = (ParameterExpression) dotNetOutput.Expression;
                parameters.Add(parameterExpression);
            }

            var expession = Expression.Lambda(parser.Parse(action.Body).Expression, parameters);
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    parser.Ancestors.Any() ? (Expression) Expression.Quote(expession) : expession
                );
            return expression;
        }
    }
}