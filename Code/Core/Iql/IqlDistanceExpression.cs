using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlDistanceExpression : IqlExpression, IGeographicExpression
    {
        public IqlReferenceExpression Point { get; set; }

        public IqlDistanceExpression(IqlReferenceExpression parent, IqlReferenceExpression point) : base(IqlExpressionKind.Distance, IqlType.Decimal, parent)
        {
            Point = point;
            Srid = IqlConstants.DefaultGeographicSrid;
        }

        public IqlDistanceExpression() : base(IqlExpressionKind.Distance)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlDistanceExpression(null, null);
			expression.Point = (IqlReferenceExpression)Point?.Clone();
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

            // #CloneEnd
        }

        internal override void FlattenInternal(IList<IqlExpression> expressions,
            Func<IqlExpression, FlattenReactionKind> checker = null)
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
				Point?.FlattenInternal(expressions, checker);
				Parent?.FlattenInternal(expressions, checker);
			}

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

			Point = (IqlReferenceExpression)context.Replace(this, nameof(Point), null, Point);
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

            // #ReplaceEnd
        }

        public int Srid { get; set; }
    }
}
