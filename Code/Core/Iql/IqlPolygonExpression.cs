using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlPolygonExpression : IqlSridExpression
    {
        public IqlRingExpression OuterRing { get; set; }
        public List<IqlRingExpression> InnerRings { get; set; }

        public IqlPolygonExpression(IqlRingExpression outerRing = null, IEnumerable<IqlRingExpression> innerRings = null, IqlType type = IqlType.GeographyPolygon, int? srid = null) : base(srid, type)
        {
            OuterRing = outerRing;
            InnerRings = innerRings?.ToList();
        }

        public IqlPolygonExpression() : base(null, IqlType.GeographyPolygon)
        {

        }

        public bool Intersects(IqlPointExpression point)
        {
            return IqlPointExpression.IntersectsPolygon(point.Y, point.X, this);
        }

        public static IqlPolygonExpression From(double[][] points, int? srid = null)
        {
            var polygon = new IqlPolygonExpression(null, null, srid != null && srid != 0 ? IqlType.GeographyPolygon : IqlType.GeometryPolygon);
            polygon.OuterRing = IqlRingExpression.From(points, srid);
            return polygon;
        }


        internal override void FlattenInternal(IqlFlattenContext context)
        {
            // #FlattenStart

				context.Flatten(OuterRing);
				if(InnerRings != null)
				{
					for(var i = 0; i < InnerRings.Count; i++)
					{
						context.Flatten(InnerRings[i]);
					}
				}
				context.Flatten(Parent);

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

			OuterRing = (IqlRingExpression)context.Replace(this, nameof(OuterRing), null, OuterRing);
			if(InnerRings != null)
			{
				for(var i = 0; i < InnerRings.Count; i++)
				{
					InnerRings[i] = (IqlRingExpression)context.Replace(this, nameof(InnerRings), i, InnerRings[i]);
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

		public static IqlPolygonExpression Clone(IqlPolygonExpression source)
		{
			// #CloneStart

			var expression = new IqlPolygonExpression();
			expression.OuterRing = (IqlRingExpression)source.OuterRing?.Clone();
			if(source.InnerRings == null)
			{
				expression.InnerRings = null;
			}
			else
			{
				var listCopy = new List<IqlRingExpression>();
				for(var i = 0; i < source.InnerRings.Count; i++)
				{
					listCopy.Add((IqlRingExpression)source.InnerRings[i]?.Clone());
				}
				expression.InnerRings = listCopy;
			}
			expression.Srid = source.Srid;
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
