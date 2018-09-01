using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlStringSubStringExpression : IqlParentValueExpression
    {
        public IqlStringSubStringExpression(IqlReferenceExpression parent, IqlReferenceExpression value,
            IqlReferenceExpression take) :
            base(parent, value, IqlExpressionKind.StringSubString, IqlType.String)
        {
            Take = take;
        }

        public IqlStringSubStringExpression() : this(null, null, null)
        {
        }

        public IqlReferenceExpression Take { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringSubStringExpression(null, null, null);
			expression.Take = (IqlReferenceExpression)Take?.Clone();
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
				Take?.FlattenInternal(expressions, checker);
				Value?.FlattenInternal(expressions, checker);
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			Take = (IqlReferenceExpression)context.Replace(this, nameof(Take), null, Take);
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
