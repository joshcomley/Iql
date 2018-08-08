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

		internal override void FlattenInternal(IList<IqlExpression> expressions, Func<IqlExpression, FlattenReactionKind> checker = null)
        {
			// #FlattenStart

			if(expressions.Contains(this))
			{
				return;
			}
			var reaction = checker == null ? FlattenReactionKind.Continue : checker(this);
			if(reaction == FlattenReactionKind.Ignore)
			{
				return;
			}
			if(reaction != FlattenReactionKind.OnlyChildren)
			{
				expressions.Add(this);
			}
			if(reaction != FlattenReactionKind.IgnoreChildren)
			{
				Body?.FlattenInternal(expressions, checker);
				if(Parameters != null)
				{
					for(var i = 0; i < Parameters.Count; i++)
					{
						Parameters[i]?.FlattenInternal(expressions, checker);
					}
				}
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }
    }
}
