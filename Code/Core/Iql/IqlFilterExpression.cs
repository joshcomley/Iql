using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlFilterExpression : IqlParentValueExpression
    {
        public string RootVariableName { get; set; }
        public IqlFilterExpression(
            string rootVariableName = null,
            IqlReferenceExpression parent = null,
            IqlExpression expression = null) : base(parent, expression, IqlExpressionKind.Count, IqlType.Integer)
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

			var expression = new IqlFilterExpression();
			expression.RootVariableName = RootVariableName;
			expression.Value = Value?.Clone();
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

				context.Flatten(Value);
				context.Flatten(Parent);

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

			Value = context.Replace(this, nameof(Value), null, Value);
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
