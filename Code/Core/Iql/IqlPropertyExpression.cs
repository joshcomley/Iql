using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlPropertyExpression : IqlReferenceExpression
    {
        public IqlPropertyExpression(
            string propertyName = null,
            IqlReferenceExpression parent = null,
            IqlType propertyType = IqlType.Unknown) : base(
            IqlExpressionKind.Property,
            propertyType,
            parent)
        {
            PropertyName = propertyName;
        }

        public IqlPropertyExpression() : base(IqlExpressionKind.Property, IqlType.Unknown)
        {

        }

        public string PropertyName { get; set; }


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

		public static IqlPropertyExpression Clone(IqlPropertyExpression source)
		{
			// #CloneStart

			var expression = new IqlPropertyExpression();
			expression.PropertyName = source.PropertyName;
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
