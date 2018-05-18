using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetRootReferenceParser : DotNetActionParserBase<IqlRootReferenceExpression>
    {
        public override IqlExpression ToQueryString(IqlRootReferenceExpression action,
            DotNetIqlParserInstance parser)
        {
            var lambda = parser.ResolveLambdaExpressionForParameter(action);
            var parameter = parser.GetParameterExpression(lambda, action);
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    parameter
                );
            return expression;
        }
    }
}