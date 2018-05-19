using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetVariableParser : DotNetActionParserBase<IqlVariableExpression>
    {
        public override IqlExpression ToQueryString(IqlVariableExpression action,
            DotNetIqlParserInstance parser)
        {
            var expression = parser.GetParameterExpression(action.VariableName)
                             ?? (Expression) Expression.Constant(action.VariableName);
            var finalExpression =
                new IqlFinalExpression<Expression>(expression);
            return finalExpression;
        }
    }
}