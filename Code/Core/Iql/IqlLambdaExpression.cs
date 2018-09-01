using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlLambdaExpression : IqlParameteredLambdaExpression
    {
        public IqlLambdaExpression AddParameter(string name = null)
        {
            Parameters.Add(new IqlRootReferenceExpression(name ?? ""));
            return this;
        }

        public IqlExpression Body { get; set; }

        public IqlLambdaExpression(IqlType? returnType = IqlType.Unknown, IqlExpression body = null, IqlExpression parent = null) : base(IqlExpressionKind.Lambda, returnType, parent)
        {
            Body = body;
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

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			Body = context.Replace(this, nameof(Body), null, Body);
			if(Parameters != null)
			{
				for(var i = 0; i < Parameters.Count; i++)
				{
					Parameters[i] = (IqlRootReferenceExpression)context.Replace(this, nameof(Parameters), i, Parameters[i]);
				}
			}
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

			// #ReplaceEnd
		}
    }
}
