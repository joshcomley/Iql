using Iql.Extensions;
using System.Collections.Generic;
using System;

namespace Iql
{
    public class IqlRootReferenceExpression : IqlVariableExpression
    {
        public IqlRootReferenceExpression(
            string variableName = null,
            string value = null,
            Type entityType = null) : base(variableName, value, entityType)
        {
            Kind = IqlExpressionKind.RootReference;
        }

        public IqlRootReferenceExpression() : this(null, null)
        {
        }

        public override bool IsOrHas(Func<IqlExpression, bool> matches)
        {
            return true;
        }


		internal override void FlattenInternal(IqlFlattenContext context)
        {
			// #FlattenStart

				context.Flatten(Parent);

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

			// #ReplaceEnd
		}

		public static IqlRootReferenceExpression Clone(IqlRootReferenceExpression source)
		{
			// #CloneStart

			var expression = new IqlRootReferenceExpression();
			expression.EntityTypeName = source.EntityTypeName;
			expression.VariableName = source.VariableName;
			expression.Value = source.Value?.TryCloneIql();
			expression.InferredReturnType = source.InferredReturnType;
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
