using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlEnumValueExpression : IqlLiteralExpressionBase<long>
    {
        public string Name { get; set; }

        public IqlEnumValueExpression(long value, string name) : base(value, IqlType.EnumValue, IqlExpressionKind.EnumValue)
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

			var expression = new IqlEnumValueExpression(0, null);
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

		internal override void FlattenInternal(IList<IqlExpression> expressions, Func<IqlExpression, FlattenReactionKind> checker = null)
        {
			// #FlattenStart

			if(expressions.Contains(this))
			{
				return;
			}
			var reaction = checker == null ? FlattenReactionKind.Continue : checker(this);
			if(reaction == FlattenReactionKind.Ignore)
			{
				return;
			}
			if(reaction != FlattenReactionKind.OnlyChildren)
			{
				expressions.Add(this);
			}
			if(reaction != FlattenReactionKind.IgnoreChildren)
			{
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }
    }
}
