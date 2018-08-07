using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlLambdaExpression : IqlParameteredExpression
    {
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
			expression.Body = Body?.Clone();
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
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

            // #CloneEnd
        }

		internal override void FlattenInternal(IList<IqlExpression> expressions)
        {
			// #FlattenStart

			if(expressions.Contains(this))
			{
				return;
			}
			expressions.Add(this);
			Body?.FlattenInternal(expressions);
			if(Parameters != null)
			{
				for(var i = 0; i < Parameters.Count; i++)
				{
					Parameters[i]?.FlattenInternal(expressions);
				}
			}
			Parent?.FlattenInternal(expressions);

			// #FlattenEnd
        }
    }
}
