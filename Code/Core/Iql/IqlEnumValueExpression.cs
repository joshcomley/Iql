using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlEnumValueExpression : IqlLiteralExpressionBase<long>
    {
        public string Name { get; set; }

        public IqlEnumValueExpression(long value = 0, string name = null) : base(value, IqlType.EnumValue, IqlExpressionKind.EnumValue)
        {
            Kind = IqlExpressionKind.EnumValue;
            Name = name;
            Value = value;
        }

        public IqlEnumValueExpression() : this(0, null)
        {
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

		public static IqlEnumValueExpression Clone(IqlEnumValueExpression source)
		{
			// #CloneStart

			var expression = new IqlEnumValueExpression();
			expression.Name = source.Name;
			expression.Value = source.Value;
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
