using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlLengthExpression : IqlSridExpression
    {
        public IqlLengthExpression(IqlReferenceExpression parent = null, int? srid = null) : base(srid, IqlType.Decimal, parent, IqlExpressionKind.Length)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
        }

        public IqlLengthExpression() : base(null, IqlType.Decimal, null, IqlExpressionKind.Length)
        {
            Srid = IqlConstants.DefaultGeographicSrid;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlLengthExpression();
			expression.Srid = Srid;
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
    }
}
