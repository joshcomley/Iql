using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlConditionExpression : IqlExpression
    {
        public IqlExpression Test { get; set; }
        public IqlExpression IfTrue { get; set; }
        public IqlExpression IfFalse { get; set; }

        // ReSharper disable once UnusedMember.Global
        public IqlConditionExpression() : base(IqlExpressionKind.Condition, IqlType.Unknown, null)
        {
            
        }

        public IqlConditionExpression(
            IqlExpression condition,
            IqlExpression then,
            IqlExpression otherwise = null,
            IqlExpression parent = null) : base(IqlExpressionKind.Condition, IqlType.Unknown, parent)
        {
            Test = condition;
            IfTrue = then;
            IfFalse = otherwise;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlConditionExpression();
			expression.Test = Test?.Clone();
			expression.IfTrue = IfTrue?.Clone();
			expression.IfFalse = IfFalse?.Clone();
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
				Test?.FlattenInternal(expressions, checker);
				IfTrue?.FlattenInternal(expressions, checker);
				IfFalse?.FlattenInternal(expressions, checker);
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }
    }
}
