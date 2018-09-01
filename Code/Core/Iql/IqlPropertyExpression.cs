using System;
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
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

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
