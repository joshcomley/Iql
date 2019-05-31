using System;
using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlParentValueLambdaExpression : IqlParentValueExpressionBase<IqlLambdaExpression>
    {
        public string RootVariableName { get; set; }

        protected IqlParentValueLambdaExpression(IqlReferenceExpression parent, IqlLambdaExpression value, IqlExpressionKind kind, IqlType returnType) : base(parent, value, kind, returnType)
        {
        }
    }
    public class IqlCountExpression : IqlParentValueLambdaExpression
    {
        public IqlCountExpression(
            string rootVariableName = null,
            IqlReferenceExpression parent = null,
            IqlLambdaExpression expression = null) : base(parent, expression, IqlExpressionKind.Count, IqlType.Integer)
        {
            RootVariableName = rootVariableName;
        }

        public IqlCountExpression()
        : base(null, null, IqlExpressionKind.Count, IqlType.Integer)
        {

        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlCountExpression();
			expression.RootVariableName = RootVariableName;
			expression.Value = (IqlLambdaExpression)Value?.Clone();
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

			Value = (IqlLambdaExpression)context.Replace(this, nameof(Value), null, Value);
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
