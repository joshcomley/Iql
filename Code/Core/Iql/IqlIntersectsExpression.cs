using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlIntersectsExpression : IqlSridExpression
    {
        public IqlReferenceExpression Polygon { get; set; }

        public IqlIntersectsExpression(IqlReferenceExpression parent, IqlReferenceExpression polygon, int? srid = null) : base(srid, IqlType.Boolean, parent, IqlExpressionKind.Intersects)
        {
            Polygon = polygon;
            Srid = IqlConstants.DefaultGeographicSrid;
        }

        public IqlIntersectsExpression() : base(null, IqlType.Boolean, null, IqlExpressionKind.Intersects)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlIntersectsExpression(null, null);
			expression.Polygon = (IqlReferenceExpression)Polygon?.Clone();
			expression.Srid = Srid;
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
				Polygon?.FlattenInternal(expressions, checker);
				Parent?.FlattenInternal(expressions, checker);
			}

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

			Polygon = (IqlReferenceExpression)context.Replace(this, nameof(Polygon), null, Polygon);
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
