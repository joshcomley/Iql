using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlLambdaExpression : IqlParameteredLambdaExpression
    {
        private IqlExpression _body;

        public IqlLambdaExpression AddParameter(string name = null)
        {
            Parameters.Add(new IqlRootReferenceExpression(name ?? ""));
            return this;
        }

        public IqlExpression Body
        {
            get => _body;
            set
            {
                _body = value;
                if (_body == null)
                {
                    ReturnType = IqlType.Unknown;
                }
                else
                {
                    ReturnType = _body.ReturnType;
                }
            }
        }

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

        internal override void FlattenInternal(IqlFlattenContext context)
        {
            // #FlattenStart

				context.Flatten(Body);
				if(Parameters != null)
				{
					for(var i = 0; i < Parameters.Count; i++)
					{
						context.Flatten(Parameters[i]);
					}
				}
				context.Flatten(Parent);

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
