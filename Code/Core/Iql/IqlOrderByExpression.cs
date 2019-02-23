using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlOrderByExpression : IqlExpression
    {
        public IqlOrderByExpression(IqlExpression orderExpression = null, IqlExpression parent = null) : base(IqlExpressionKind.OrderBy, IqlType.Collection, parent)
        {
            OrderExpression = orderExpression;
        }

        public IqlOrderByExpression() : this(null)
        {

        }

        public IqlExpression OrderExpression { get; set; }
        public bool Descending { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlOrderByExpression(null);
			expression.OrderExpression = OrderExpression?.Clone();
			expression.Descending = Descending;
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}

		internal override void FlattenInternal(IqlFlattenContext context)
        {
			// #FlattenStart

				context.Flatten(OrderExpression);
				context.Flatten(Parent);

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			OrderExpression = context.Replace(this, nameof(OrderExpression), null, OrderExpression);
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

			// #ReplaceEnd
		}
    }
}
