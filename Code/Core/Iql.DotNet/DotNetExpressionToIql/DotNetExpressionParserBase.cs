using System.Linq.Expressions;

namespace Iql.DotNet.DotNetExpressionToIql
{
    public abstract class DotNetExpressionParserBase<TEntity, TExpression> : IDotNetExpressionParser<TEntity, TExpression>
        where TExpression : Expression
    {
        public bool CanHandle(Expression node)
        {
            return CanHandleNode(node);
        }

        public IqlExpression Parse(Expression expression, DotNetExpressionParserContext context)
        {
            return Parse((TExpression) expression, context);
        }

        public IqlExpression Parse(TExpression node, DotNetExpressionParserContext context)
        {
            return PerformParse(node, context);
        }

        public abstract bool CanHandleNode(Expression node);

        public abstract IqlExpression PerformParse(TExpression node, DotNetExpressionParserContext context);
    }
}