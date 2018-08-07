namespace Iql
{
    public class IqlDataSetReferenceExpression : IqlExpression
    {
        public string Name { get; set; }
        public IqlDataSetReferenceExpression(IqlExpression parent = null)
        : base(IqlExpressionKind.DataSetReference, IqlType.Collection, parent)
        {

        }

        public IqlDataSetReferenceExpression() : this(null)
        {

        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlDataSetReferenceExpression();
			expression.Name = Name;
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
