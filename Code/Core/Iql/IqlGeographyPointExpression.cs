using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlGeographyPointExpression : IqlPointExpression, IGeographicExpression
    {
        public int Srid { get; set; }

        public IqlGeographyPointExpression(double x, double y, int? srid = null) : base(x, y, IqlExpressionKind.GeographyPoint, IqlType.GeographyPoint)
        {
            Srid = srid ?? IqlConstants.DefaultGeographicSrid;
        }

        public IqlGeographyPointExpression() : base(0, 0, IqlExpressionKind.GeographyPoint, IqlType.GeographyPoint)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlGeographyPointExpression(0, 0);
			expression.Srid = Srid;
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
