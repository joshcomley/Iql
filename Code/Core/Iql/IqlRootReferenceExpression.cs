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

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlRootReferenceExpression();
			expression.EntityTypeName = EntityTypeName;
			expression.VariableName = VariableName;
			expression.Value = Value;
			expression.InferredReturnType = InferredReturnType;
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
    }
}
