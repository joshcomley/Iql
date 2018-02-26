using System.Linq.Expressions;

namespace Iql.Queryable.Expressions
{
    public interface IIqlToExpressionConverter
    {
        LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression)
            where TEntity : class;
    }
}