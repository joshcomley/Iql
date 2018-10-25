using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlLengthExpression : IqlExpression
    {
        public IqlLengthExpression(IqlExpression parent = null) : base(IqlExpressionKind.Length, IqlType.Decimal, parent)
        {

        }

        public IqlLengthExpression() : base(IqlExpressionKind.Length)
        {

        }

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