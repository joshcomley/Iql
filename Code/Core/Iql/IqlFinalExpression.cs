using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlFinalExpression<TValue> : IqlExpression, IFinalExpression
    {
        public IqlFinalExpression(
            TValue value = default(TValue)) : base(IqlExpressionKind.Final, null)
        {
            Value = value;
        }

        public TValue Value { get; set; }

        object IFinalExpression.Value
        {
            get => Value;
            set => Value = (TValue) value;
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

		public static IqlFinalExpression<TValue> Clone(IqlFinalExpression<TValue> source)
		{
			// #CloneStart

			var expression = new IqlFinalExpression<TValue>();
			expression.Value = source.Value;
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
