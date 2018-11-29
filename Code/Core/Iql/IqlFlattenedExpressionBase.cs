namespace Iql
{
    public class IqlFlattenedExpressionBase<T>
        where T : IqlExpression
    {
        public T Expression { get; }
        public IqlFlattenedExpression[] Ancestors { get; }

        public IqlFlattenedExpressionBase(T expression, IqlFlattenedExpression[] ancestors)
        {
            Expression = expression;
            Ancestors = ancestors;
        }
    }
}