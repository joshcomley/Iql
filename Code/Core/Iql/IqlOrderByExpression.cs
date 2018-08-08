using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlOrderByExpression : IqlExpression
    {
        public IqlOrderByExpression(IqlExpression orderExpression, IqlExpression parent = null) : base(IqlExpressionKind.OrderBy, IqlType.Collection, parent)
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

		internal override void FlattenInternal(IList<IqlExpression> expressions, Func<IqlExpression, FlattenReactionKind> checker = null)
        {
			// #FlattenStart

			if(expressions.Contains(this))
			{
				return;
			}
			var reaction = checker == null ? FlattenReactionKind.Continue : checker(this);
			if(reaction == FlattenReactionKind.Ignore)
			{
				return;
			}
			if(reaction != FlattenReactionKind.OnlyChildren)
			{
				expressions.Add(this);
			}
			if(reaction != FlattenReactionKind.IgnoreChildren)
			{
				OrderExpression?.FlattenInternal(expressions, checker);
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }
    }
}
