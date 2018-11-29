using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlDistanceExpression : IqlBinaryExpression, ISrid
    {
        public IqlDistanceExpression(IqlReferenceExpression left, IqlReferenceExpression right) : base(IqlExpressionKind.Distance, left, right)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
            ReturnType = IqlType.Decimal;
        }

        public IqlDistanceExpression() : base(IqlExpressionKind.Distance)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
            ReturnType = IqlType.Decimal;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlDistanceExpression(null, null);
			expression.Srid = Srid;
			expression.Left = Left?.Clone();
			expression.Right = Right?.Clone();
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
    }
}
