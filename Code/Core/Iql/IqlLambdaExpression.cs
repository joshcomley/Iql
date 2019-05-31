using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlLambdaExpression : IqlParameteredLambdaExpression
    {
        public static IqlLambdaExpression Create(IqlExpression body, IqlType returnType = IqlType.Boolean, string rootReference = "_")
        {
            var parameters = new List<IqlRootReferenceExpression>();
            parameters.Add(new IqlRootReferenceExpression(rootReference));
            return new IqlLambdaExpression
            {
                Body = body,
                Parameters = parameters,
                ReturnType = returnType
            };
        }

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

		public static IqlLambdaExpression Clone(IqlLambdaExpression source)
		{
			// #CloneStart

			var expression = new IqlLambdaExpression();
			expression.Body = source.Body?.Clone();
			if(source.Parameters == null)
			{
				expression.Parameters = null;
			}
			else
			{
				var listCopy = new List<IqlRootReferenceExpression>();
				for(var i = 0; i < source.Parameters.Count; i++)
				{
					listCopy.Add((IqlRootReferenceExpression)source.Parameters[i]?.Clone());
				}
				expression.Parameters = listCopy;
			}
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
