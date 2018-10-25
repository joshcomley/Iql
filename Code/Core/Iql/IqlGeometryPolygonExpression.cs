using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlGeometryPolygonExpression : IqlPolygonExpression
    {
        public IqlGeometryPolygonExpression(IEnumerable<IqlPointExpression> points) : base(points,
            IqlExpressionKind.GeometryPolygon, IqlType.GeometryPolygon)
        {
        }

        public IqlGeometryPolygonExpression() : base(new IqlPointExpression[] { }, IqlExpressionKind.GeometryPolygon, IqlType.GeometryPolygon)
        {
        }

        public static IqlGeometryPolygonExpression From(double[][] points)
        {
            var polygon = new IqlGeometryPolygonExpression(null);
            polygon.Points = new List<IqlPointExpression>();
            foreach (var pair in points)
            {
                if (pair.Length < 2)
                {
                    throw new Exception("Each points pair must have at least two values");
                }
                polygon.Points.Add(new IqlGeometryPointExpression(pair[0], pair[1]));
            }
            return polygon;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlGeometryPolygonExpression(null);
			if(Points == null)
			{
				expression.Points = null;
			}
			else
			{
				var listCopy = new List<IqlPointExpression>();
				for(var i = 0; i < Points.Count; i++)
				{
					listCopy.Add((IqlPointExpression)Points[i]?.Clone());
				}
				expression.Points = listCopy;
			}
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
					Points[i] = (IqlPointExpression)context.Replace(this, nameof(Points), i, Points[i]);
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
    }
}
