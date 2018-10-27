using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlPolygonExpression : IqlSridExpression
    {
        public IqlRingExpression OuterRing { get; set; }
        public List<IqlRingExpression> InnerRings { get; set; }

        public IqlPolygonExpression(IqlRingExpression outerRing, IEnumerable<IqlRingExpression> innerRings = null, IqlType type = IqlType.GeographyPolygon, int? srid = null) : base(srid, type)
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

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlPolygonExpression(null);
			expression.OuterRing = (IqlRingExpression)OuterRing?.Clone();
			if(InnerRings == null)
			{
				expression.InnerRings = null;
			}
			else
			{
				var listCopy = new List<IqlRingExpression>();
				for(var i = 0; i < InnerRings.Count; i++)
				{
					listCopy.Add((IqlRingExpression)InnerRings[i]?.Clone());
				}
				expression.InnerRings = listCopy;
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
				OuterRing?.FlattenInternal(expressions, checker);
				if(InnerRings != null)
				{
					for(var i = 0; i < InnerRings.Count; i++)
					{
						InnerRings[i]?.FlattenInternal(expressions, checker);
					}
				}
				Parent?.FlattenInternal(expressions, checker);
			}

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
    }
}
