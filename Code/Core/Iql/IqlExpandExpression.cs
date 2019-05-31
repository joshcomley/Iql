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


		internal override void FlattenInternal(IqlFlattenContext context)
        {
			// #FlattenStart

				context.Flatten(NavigationProperty);
				context.Flatten(Query);
				context.Flatten(Parent);

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

		public static IqlExpandExpression Clone(IqlExpandExpression source)
		{
			// #CloneStart

			var expression = new IqlExpandExpression();
			expression.NavigationProperty = (IqlPropertyExpression)source.NavigationProperty?.Clone();
			expression.Query = (IqlCollectitonQueryExpression)source.Query?.Clone();
			expression.Count = source.Count;
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
