using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlGeographyLineExpression : IqlLineExpression, IGeographic
    {
        public IqlGeographyLineExpression(IEnumerable<IqlPointExpression> points, int? srid = null) : base(points,
            IqlExpressionKind.GeographyLine)
        {
            Srid = srid ?? IqlConstants.DefaultGeographicSrid;
        }

        public IqlGeographyLineExpression() : base(new IqlPointExpression[] { },
            IqlExpressionKind.GeographyLine)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
        }

        public int Srid { get; set; }

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
    }
}