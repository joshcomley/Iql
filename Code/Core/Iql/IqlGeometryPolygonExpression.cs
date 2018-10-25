using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlGeometryPolygonExpression : IqlPolygonExpression
    {

        public override IqlExpression Clone()
        {
            // #CloneStart

            throw new NotImplementedException();

            // #CloneEnd
        }

        internal override void FlattenInternal(IList<IqlExpression> expressions,
            Func<IqlExpression, FlattenReactionKind> checker = null)
        {
            // #FlattenStart

            throw new NotImplementedException();

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

            throw new NotImplementedException();

            // #ReplaceEnd
        }

        public IqlGeometryPolygonExpression(IEnumerable<IqlPointExpression> points) : base(points, IqlExpressionKind.GeometryPolygon)
        {
        }

        public IqlGeometryPolygonExpression() : base(new IqlPointExpression[] { }, IqlExpressionKind.GeometryPolygon)
        {

        }
    }
}