using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlFinalExpression<TValue> : IqlFinalExpressionBase
    {
        public IqlFinalExpression(
            TValue value = default(TValue)) : base(IqlExpressionKind.Final, null)
        {
            Value = value;
        }

        public TValue Value { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlFinalExpression<TValue>();
			expression.Value = Value;
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

        public override object ResolveValue()
        {
            return Value;
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
