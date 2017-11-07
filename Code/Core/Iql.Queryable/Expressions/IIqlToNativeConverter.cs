using System.Linq.Expressions;

namespace Iql.Queryable.Expressions
{
    public interface IIqlToNativeConverter
    {
        Expression<LambdaExpression> Parse<TEntity>(IqlExpression expression)
            where TEntity : class;
    }
}