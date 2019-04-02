using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlLineExpression : IqlSridExpression, IPointsExpression
    {
        public List<IqlPointExpression> Points { get; set; }
        public IqlLineExpression(IEnumerable<IqlPointExpression> points = null, IqlType type = IqlType.GeographyLine, int? srid = null) : base(srid, type)
        {
            Srid = srid;
            Points = points?.ToList();
        }


        public IqlLineExpression() : base(null, IqlType.GeographyLine)
        {

        }

        public double Length()
        {
            var current = Points[0];
            double total = 0;
            for (var i = 1; i < Points.Count; i++)
            {
                var point = Points[i];
                total += IqlPointExpression.DistanceBetween(current.Y, current.X, point.Y, point.X);
                current = point;
            }
            return total;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlLineExpression();
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
