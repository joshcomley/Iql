using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Extensions;

namespace Iql
{
    public class IqlRingExpression : IqlSridExpression, IPointsExpression
    {
        public List<IqlPointExpression> Points { get; set; }

        public IqlRingExpression(IEnumerable<IqlPointExpression> points = null, int? srid = null) : base(srid, IqlType.GeographyRing, null, IqlExpressionKind.GeoRing)
        {
            Points = points?.ToList();
        }

        public IqlRingExpression() : base(null, IqlType.GeographyRing, null, IqlExpressionKind.GeoRing)
        {

        }

        public static IqlRingExpression From(double[][] points, int? srid = null)
        {
            var ring = new IqlRingExpression();
            ring.Points = new List<IqlPointExpression>();
            foreach (var pair in points)
            {
                if (pair.Length < 2)
                {
                    throw new Exception("Each points pair must have at least two values");
                }
                ring.Points.Add(new IqlPointExpression(pair[0], pair[1], srid.ResolveTypeFromSrid(IqlExpressionKind.GeoPoint), srid));
            }
            return ring;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlRingExpression();
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

				if(Points != null)
				{
					for(var i = 0; i < Points.Count; i++)
					{
						context.Flatten(Points[i]);
					}
				}
				context.Flatten(Parent);

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
