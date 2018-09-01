using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlExpandExpression : IqlExpression
    {
#if !TypeScript
        public IqlExpandExpression() : this(null)
        {
            
        }
#endif
        public IqlExpandExpression(IqlExpression parent = null) : base(IqlExpressionKind.Expand, IqlType.Collection, parent)
        {
        }

        public IqlPropertyExpression NavigationProperty { get; set; }
        public IqlCollectitonQueryExpression Query { get; set; }
        public bool Count { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlExpandExpression();
			expression.NavigationProperty = (IqlPropertyExpression)NavigationProperty?.Clone();
			expression.Query = (IqlCollectitonQueryExpression)Query?.Clone();
			expression.Count = Count;
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
				NavigationProperty?.FlattenInternal(expressions, checker);
				Query?.FlattenInternal(expressions, checker);
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			NavigationProperty = (IqlPropertyExpression)context.Replace(this, nameof(NavigationProperty), null, NavigationProperty);
			Query = (IqlCollectitonQueryExpression)context.Replace(this, nameof(Query), null, Query);
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
