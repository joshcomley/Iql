using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlMultiPointExpression : IqlSridExpression, IPointsExpression
    {
        public List<IqlPointExpression> Points { get; set; }
        public IqlMultiPointExpression(IEnumerable<IqlPointExpression> points = null, IqlType type = IqlType.GeographyMultiPoint, int? srid = null) : base(srid, type)
        {
            Points = points?.ToList();
        }

        public IqlMultiPointExpression() : base(null, IqlType.GeographyMultiPoint)
        {

        }

        public bool Intersects(IqlPolygonExpression polygon)
        {
            return Points != null && Points.Any(_ => _.Intersects(polygon));
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

		public static IqlMultiPointExpression Clone(IqlMultiPointExpression source)
		{
			// #CloneStart

			var expression = new IqlMultiPointExpression();
			if(source.Points == null)
			{
				expression.Points = null;
			}
			else
			{
				var listCopy = new List<IqlPointExpression>();
				for(var i = 0; i < source.Points.Count; i++)
				{
					listCopy.Add((IqlPointExpression)source.Points[i]?.Clone());
				}
				expression.Points = listCopy;
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
