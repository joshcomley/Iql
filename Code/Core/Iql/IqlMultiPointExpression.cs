using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlMultiPointExpression : IqlSridExpression, IPointsExpression
    {
        public List<IqlPointExpression> Points { get; set; }
        public IqlMultiPointExpression(IEnumerable<IqlPointExpression> points, IqlType type = IqlType.GeographyMultiPoint, int? srid = null) : base(srid, type)
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


        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlMultiPointExpression(null);
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
