using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlStringSubStringExpression : IqlParentValueExpression
    {
        public IqlStringSubStringExpression(
            IqlReferenceExpression parent = null, 
            IqlReferenceExpression value = null,
            IqlReferenceExpression take = null) :
            base(parent, value, IqlExpressionKind.StringSubString, IqlType.String)
        {
            Take = take;
        }

        public IqlStringSubStringExpression() : this(null, null, null)
        {
        }

        public IqlReferenceExpression Take { get; set; }


		internal override void FlattenInternal(IqlFlattenContext context)
        {
			// #FlattenStart

				context.Flatten(Take);
				context.Flatten(Value);
				context.Flatten(Parent);

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			Take = (IqlReferenceExpression)context.Replace(this, nameof(Take), null, Take);
			Value = context.Replace(this, nameof(Value), null, Value);
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

			// #ReplaceEnd
		}

		public static IqlStringSubStringExpression Clone(IqlStringSubStringExpression source)
		{
			// #CloneStart

			var expression = new IqlStringSubStringExpression();
			expression.Take = (IqlReferenceExpression)source.Take?.Clone();
			expression.Value = source.Value?.Clone();
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
