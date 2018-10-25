using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlGeometryMultiPolygonExpression : IqlMultiPolygonExpression
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

        public IqlGeometryMultiPolygonExpression(IEnumerable<IqlPolygonExpression> points) : base(points, IqlExpressionKind.GeometryMultiPolygon)
        {
        }

        public IqlGeometryMultiPolygonExpression() : base(new IqlPolygonExpression[] { }, IqlExpressionKind.GeometryMultiPolygon)
        {

        }
    }
}