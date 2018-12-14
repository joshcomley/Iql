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

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlEnumValueExpression();
			expression.Name = Name;
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
