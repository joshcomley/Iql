using System.Linq.Expressions;

namespace Iql.DotNet
{
    public abstract class ExpressionParserBase<TEntity, TExpression> : IExpressionParser<TEntity, TExpression>
        where TExpression : Expression
    {
        public bool CanHandle(Expression node)
        {
            return CanHandleNode(node);
        }

        public IqlExpression Parse(Expression expression, ExpressionParserContext context)
        {
            return Parse((TExpression) expression, context);
        }

        public IqlExpression Parse(TExpression node, ExpressionParserContext context)
        {
            return PerformParse(node, context);
        }

        public abstract bool CanHandleNode(Expression node);

        public abstract IqlExpression PerformParse(TExpression node, ExpressionParserContext context);
    }
}