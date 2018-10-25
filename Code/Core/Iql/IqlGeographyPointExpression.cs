using System;
using System.Collections.Generic;
using Iql.Extensions;

namespace Iql
{
    public class IqlGeographyPointExpression : IqlPointExpression, IGeographic
    {
        public int Srid { get; set; }

        public IqlGeographyPointExpression(long x, long y, int? srid = null) : base(x, y, IqlExpressionKind.GeographyPoint)
        {
            Srid = srid ?? IqlConstants.DefaultGeographicSrid;
        }

        public IqlGeographyPointExpression() : base(0, 0, IqlExpressionKind.GeographyPoint)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

            throw new NotImplementedException();

            // #CloneEnd
        }

        internal override void FlattenInternal(IList<IqlExpression> expressions, Func<IqlExpression, FlattenReactionKind> checker = null)
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