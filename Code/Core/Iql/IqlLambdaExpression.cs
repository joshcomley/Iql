using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlLambdaExpression : IqlExpression
    {
        public List<IqlRootReferenceExpression> Parameters { get; set; }
        public IqlExpression Body { get; set; }

        public IqlLambdaExpression(IqlType? returnType = IqlType.Unknown, IqlExpression parent = null) : base(IqlExpressionKind.Lambda, returnType, parent)
        {
        }

        public IqlLambdaExpression() : base(IqlExpressionKind.Lambda, IqlType.Unknown)
        {
            
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

            throw new NotImplementedException();

            // #CloneEnd
        }
    }
}