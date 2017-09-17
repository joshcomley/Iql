namespace Iql
{
    public class IqlFinalExpression : IqlExpression
    {
        public IqlFinalExpression(
            string value) : base(IqlExpressionType.Final)
        {
            Value = value;
        }

        public IqlFinalExpression() : this(null)
        {
        }

        public string Value { get; set; }
    }
}