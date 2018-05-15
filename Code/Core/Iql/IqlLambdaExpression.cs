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

        public override bool IsOrHas(Func<IqlExpression, bool> matches)
        {
            return Body.IsOrHas(matches);
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlLambdaExpression();
			if(Parameters == null)
			{
				expression.Parameters = null;
			}
			else
			{
				var listCopy = new List<IqlRootReferenceExpression>();
				for(var i = 0; i < Parameters.Count; i++)
				{
					listCopy.Add((IqlRootReferenceExpression)Parameters[i]?.Clone());
				}
				expression.Parameters = listCopy;
			}
			expression.Body = Body?.Clone();
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

            // #CloneEnd
        }
    }
}
