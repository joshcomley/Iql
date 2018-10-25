using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlGeographyPolygonExpression : IqlPolygonExpression
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

        public IqlGeographyPolygonExpression(IEnumerable<IqlPointExpression> points, int? srid = null) : base(points, IqlExpressionKind.GeographyPolygon)
        {
            Srid = srid ?? IqlConstants.DefaultGeographicSrid;
        }

        public IqlGeographyPolygonExpression() : base(new IqlPointExpression[] { }, IqlExpressionKind.GeographyPolygon)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
        }
    }
}