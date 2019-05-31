using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlConditionExpression : IqlExpression
    {
        public IqlExpression Test { get; set; }
        public IqlExpression IfTrue { get; set; }
        public IqlExpression IfFalse { get; set; }

        public IqlConditionExpression(
            IqlExpression condition = null,
            IqlExpression then = null,
            IqlExpression otherwise = null,
            IqlExpression parent = null) : base(IqlExpressionKind.Condition, IqlType.Unknown, parent)
        {
            Test = condition;
            IfTrue = then;
            IfFalse = otherwise;
        }

        public IqlConditionExpression() : this(null)
        {

        }


		internal override void FlattenInternal(IqlFlattenContext context)
        {
			// #FlattenStart

				context.Flatten(Test);
				context.Flatten(IfTrue);
				context.Flatten(IfFalse);
				context.Flatten(Parent);

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			Test = context.Replace(this, nameof(Test), null, Test);
			IfTrue = context.Replace(this, nameof(IfTrue), null, IfTrue);
			IfFalse = context.Replace(this, nameof(IfFalse), null, IfFalse);
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

			// #ReplaceEnd
		}

		public static IqlConditionExpression Clone(IqlConditionExpression source)
		{
			// #CloneStart

			var expression = new IqlConditionExpression();
			expression.Test = source.Test?.Clone();
			expression.IfTrue = source.IfTrue?.Clone();
			expression.IfFalse = source.IfFalse?.Clone();
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
