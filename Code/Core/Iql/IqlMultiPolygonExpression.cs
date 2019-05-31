using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlMultiPolygonExpression : IqlSridExpression
    {
        public List<IqlPolygonExpression> Polygons { get; set; }
        public IqlMultiPolygonExpression(IEnumerable<IqlPolygonExpression> points = null, IqlType type = IqlType.GeographyMultiPolygon, int? srid = null) : base(srid, type)
        {
            Polygons = points?.ToList();
        }

        public IqlMultiPolygonExpression() : base(null, IqlType.GeographyMultiPolygon)
        {

        }

        public bool Intersects(IqlPointExpression point)
        {
            return Polygons != null && Polygons.Any(_ => _.Intersects(point));
        }



        internal override void FlattenInternal(IqlFlattenContext context)
        {
            // #FlattenStart

				if(Polygons != null)
				{
					for(var i = 0; i < Polygons.Count; i++)
					{
						context.Flatten(Polygons[i]);
					}
				}
				context.Flatten(Parent);

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

			if(Polygons != null)
			{
				for(var i = 0; i < Polygons.Count; i++)
				{
					Polygons[i] = (IqlPolygonExpression)context.Replace(this, nameof(Polygons), i, Polygons[i]);
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

		public static IqlMultiPolygonExpression Clone(IqlMultiPolygonExpression source)
		{
			// #CloneStart

			var expression = new IqlMultiPolygonExpression();
			if(source.Polygons == null)
			{
				expression.Polygons = null;
			}
			else
			{
				var listCopy = new List<IqlPolygonExpression>();
				for(var i = 0; i < source.Polygons.Count; i++)
				{
					listCopy.Add((IqlPolygonExpression)source.Polygons[i]?.Clone());
				}
				expression.Polygons = listCopy;
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
