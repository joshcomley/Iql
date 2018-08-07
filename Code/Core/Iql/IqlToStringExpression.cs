using System.Collections.Generic;

namespace Iql
{
    public class IqlToStringExpression : IqlReferenceExpression
    {
        public IqlToStringExpression(IqlReferenceExpression parent)
            : base(IqlExpressionKind.ToString, IqlType.String, parent)
        {
        }

        public IqlToStringExpression() : this(null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlToStringExpression(null);
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}

		internal override void FlattenInternal(IList<IqlExpression> expressions)
        {
			// #FlattenStart

			if(expressions.Contains(this))
			{
				return;
			}
			expressions.Add(this);
			Parent?.FlattenInternal(expressions);

			// #FlattenEnd
        }
    }
}
