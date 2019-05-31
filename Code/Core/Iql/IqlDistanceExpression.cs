using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlDistanceExpression : IqlBinaryExpression, ISrid
    {
        public IqlDistanceExpression(IqlReferenceExpression left = null, IqlReferenceExpression right = null) : base(IqlExpressionKind.Distance, left, right)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
            ReturnType = IqlType.Decimal;
        }

        public IqlDistanceExpression() : base(IqlExpressionKind.Distance)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
            ReturnType = IqlType.Decimal;
        }


        internal override void FlattenInternal(IqlFlattenContext context)
        {
            // #FlattenStart

				context.Flatten(Left);
				context.Flatten(Right);
				context.Flatten(Parent);

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

			Left = context.Replace(this, nameof(Left), null, Left);
			Right = context.Replace(this, nameof(Right), null, Right);
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

            // #ReplaceEnd
        }

        public int? Srid { get; set; }

		public static IqlDistanceExpression Clone(IqlDistanceExpression source)
		{
			// #CloneStart

			var expression = new IqlDistanceExpression();
			expression.Srid = source.Srid;
			expression.Left = source.Left?.Clone();
			expression.Right = source.Right?.Clone();
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
