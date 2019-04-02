using System;
using System.Collections.Generic;
using Iql.Extensions;

namespace Iql
{
    public class IqlInvocationExpression : IqlParameteredExpression<IqlExpression>
    {
        public string MethodName { get; set; }

        public IqlInvocationExpression(string methodName = null, IqlType? returnType = IqlType.Unknown, IqlExpression parent = null) : base(IqlExpressionKind.Invocation, returnType, parent)
        {
            MethodName = methodName;
        }

        public IqlInvocationExpression() : this(null)
        {

        }

        public IqlInvocationExpression AddLiteralParameter<T>(T value)
        {
            Parameters.Add(new IqlLiteralExpression(value, typeof(T).ToIqlType()));
            return this;
        }

        public IqlInvocationExpression AddRootReferenceParameter(string name = null)
        {
            Parameters.Add(new IqlRootReferenceExpression(name ?? ""));
            return this;
        }

        public IqlInvocationExpression AddParameter(IqlExpression expression)
        {
            Parameters.Add(expression);
            return this;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlInvocationExpression();
			expression.MethodName = MethodName;
			if(Parameters == null)
			{
				expression.Parameters = null;
			}
			else
			{
				var listCopy = new List<IqlExpression>();
				for(var i = 0; i < Parameters.Count; i++)
				{
					listCopy.Add(Parameters[i]?.Clone());
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

			if(Parameters != null)
			{
				for(var i = 0; i < Parameters.Count; i++)
				{
					Parameters[i] = context.Replace(this, nameof(Parameters), i, Parameters[i]);
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
