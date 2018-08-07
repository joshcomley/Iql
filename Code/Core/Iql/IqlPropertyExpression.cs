using System.Collections.Generic;

namespace Iql
{
    public class IqlPropertyExpression : IqlReferenceExpression
    {
        public IqlPropertyExpression(
            string propertyName = null,
            IqlReferenceExpression parent = null,
            IqlType propertyType = IqlType.Unknown) : base(
            IqlExpressionKind.Property,
            propertyType,
            parent)
        {
            PropertyName = propertyName;
        }

        public IqlPropertyExpression() : base(IqlExpressionKind.Property, IqlType.Unknown)
        {

        }

        public string PropertyName { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlPropertyExpression();
			expression.PropertyName = PropertyName;
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}

		internal override void FlattenInternal(IList<IqlExpression> expressions)
        {
			// #FlattenStart

			if(expressions.Contains(this))
			{
				return;
			}
			expressions.Add(this);
			Parent?.FlattenInternal(expressions);

			// #FlattenEnd
        }
    }
}
