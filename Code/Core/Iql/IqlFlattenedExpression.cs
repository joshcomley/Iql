namespace Iql
{
    public class IqlFlattenedExpression : IqlFlattenedExpressionBase<IqlExpression>
    {
        public IqlFlattenedExpression(IqlExpression expression, IqlFlattenedExpression[] ancestors) : base(expression, ancestors)
        {
        }
    }
}