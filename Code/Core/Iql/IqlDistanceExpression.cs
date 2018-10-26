using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlDistanceExpression : IqlBinaryExpression, IGeographicExpression
    {
        public IqlDistanceExpression(IqlReferenceExpression left, IqlReferenceExpression right) : base(IqlExpressionKind.Distance, left, right)
        {
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
			expression.Srid = Srid;
			expression.Left = Left?.Clone();
			expression.Right = Right?.Clone();
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
				Left?.FlattenInternal(expressions, checker);
				Right?.FlattenInternal(expressions, checker);
				Parent?.FlattenInternal(expressions, checker);
			}

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

			Left = context.Replace(this, nameof(Left), null, Left);
			Right = context.Replace(this, nameof(Right), null, Right);
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
