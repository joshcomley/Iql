using System.Linq.Expressions;

namespace Iql.DotNet.DotNetExpressionToIql
{
    public interface IDotNetExpressionParser
    {
        bool CanHandle(Expression node);
        IqlExpression Parse(Expression expression, DotNetExpressionParserContext context);
    }

    public interface IDotNetExpressionParser<TEntity, in TExpression>
        : IDotNetExpressionParser
        where TExpression : Expression
    {
        IqlExpression Parse(TExpression expression, DotNetExpressionParserContext context);
    }
}