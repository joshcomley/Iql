using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlGeographyPolygonExpression : IqlPolygonExpression, IGeographicExpression
    {
        public IqlGeographyPolygonExpression(IEnumerable<IqlPointExpression> points, int? srid = null) : base(points,
            IqlExpressionKind.GeographyPolygon, IqlType.GeographyPolygon)
        {
            Srid = srid ?? IqlConstants.DefaultGeographicSrid;
        }

        public IqlGeographyPolygonExpression() : base(new IqlPointExpression[] { }, IqlExpressionKind.GeographyPolygon,
            IqlType.GeographyPolygon)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
        }

        public static IqlGeographyPolygonExpression From(double[][] points, int? srid = null)
        {
            var polygon = new IqlGeographyPolygonExpression(null, srid);
            polygon.Points = new List<IqlPointExpression>();
            foreach (var pair in points)
            {
                if (pair.Length < 2)
                {
                    throw new Exception("Each points pair must have at least two values");
                }
                polygon.Points.Add(new IqlGeographyPointExpression(pair[0], pair[1], srid));
            }
            return polygon;
        }

        public int Srid { get; set; }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlGeographyPolygonExpression(null);
			expression.Srid = Srid;
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
