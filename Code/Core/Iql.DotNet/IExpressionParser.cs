using System.Linq.Expressions;

namespace Iql.DotNet
{
    public interface IExpressionParser
    {
        bool CanHandle(Expression node);
        IqlExpression Parse(Expression expression, ExpressionParserContext context);
    }

    public interface IExpressionParser<TEntity, in TExpression>
        : IExpressionParser
        where TExpression : Expression
    {
        IqlExpression Parse(TExpression expression, ExpressionParserContext context);
    }
}