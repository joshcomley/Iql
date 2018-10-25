using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlGeographyMultiPolygonExpression : IqlMultiPolygonExpression
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

        public IqlGeographyMultiPolygonExpression(IEnumerable<IqlPolygonExpression> points, int? srid = null) : base(points, IqlExpressionKind.GeographyMultiPolygon)
        {
            Srid = srid ?? IqlConstants.DefaultGeographicSrid;
        }

        public IqlGeographyMultiPolygonExpression() : base(new IqlPolygonExpression[] { }, IqlExpressionKind.GeographyMultiPolygon)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
        }
    }
}