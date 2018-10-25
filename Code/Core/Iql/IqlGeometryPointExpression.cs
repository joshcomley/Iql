using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlGeometryPointExpression : IqlPointExpression
    {
        public IqlGeometryPointExpression(long x, long y) : base(x, y, IqlExpressionKind.GeometryPoint)
        {

        }

        public IqlGeometryPointExpression() : base(0, 0, IqlExpressionKind.GeometryPoint)
        {

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