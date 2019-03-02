using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlIntersectsExpression : IqlSridExpression
    {
        public IqlReferenceExpression Polygon { get; set; }

        public IqlIntersectsExpression(IqlReferenceExpression parent = null, IqlReferenceExpression polygon = null, int? srid = null) : base(srid, IqlType.Boolean, parent, IqlExpressionKind.Intersects)
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

        internal override void FlattenInternal(IqlFlattenContext context)
        {
            // #FlattenStart

				context.Flatten(Polygon);
				context.Flatten(Parent);

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
