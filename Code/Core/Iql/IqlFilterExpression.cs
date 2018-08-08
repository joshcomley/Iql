using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlFilterExpression : IqlParentValueExpression
    {
        public string RootVariableName { get; set; }
        public IqlFilterExpression(
            string rootVariableName,
            IqlReferenceExpression parent,
            IqlExpression expression) : base(parent, expression, IqlExpressionKind.Count, IqlType.Integer)
        {
            RootVariableName = rootVariableName;
        }

        public IqlFilterExpression()
            : base(null, null, IqlExpressionKind.Count, IqlType.Integer)
        {

        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlFilterExpression(null, null, null);
			expression.RootVariableName = RootVariableName;
			expression.Value = Value?.Clone();
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
				Value?.FlattenInternal(expressions, checker);
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }
    }
}
