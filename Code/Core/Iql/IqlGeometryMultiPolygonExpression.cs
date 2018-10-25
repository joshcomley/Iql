using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlGeometryMultiPolygonExpression : IqlMultiPolygonExpression
    {

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlGeometryMultiPolygonExpression(null);
			if(Points == null)
			{
				expression.Points = null;
			}
			else
			{
				var listCopy = new List<IqlPolygonExpression>();
				for(var i = 0; i < Points.Count; i++)
				{
					listCopy.Add((IqlPolygonExpression)Points[i]?.Clone());
				}
				expression.Points = listCopy;
			}
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
				if(Points != null)
				{
					for(var i = 0; i < Points.Count; i++)
					{
						Points[i]?.FlattenInternal(expressions, checker);
					}
				}
				Parent?.FlattenInternal(expressions, checker);
			}

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

			if(Points != null)
			{
				for(var i = 0; i < Points.Count; i++)
				{
					Points[i] = (IqlPolygonExpression)context.Replace(this, nameof(Points), i, Points[i]);
				}
			}
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

            // #ReplaceEnd
        }

        public IqlGeometryMultiPolygonExpression(IEnumerable<IqlPolygonExpression> points) : base(points, IqlExpressionKind.GeometryMultiPolygon)
        {
        }

        public IqlGeometryMultiPolygonExpression() : base(new IqlPolygonExpression[] { }, IqlExpressionKind.GeometryMultiPolygon)
        {

        }
    }
}
