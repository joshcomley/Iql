using System.Collections.Generic;

namespace Iql
{
    public class IqlStringTrimExpression : IqlReferenceExpression
    {
        public IqlStringTrimExpression(IqlReferenceExpression parent) : base(IqlExpressionKind.StringTrim,
            IqlType.String, parent)
        {
        }

        public IqlStringTrimExpression() : this(null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringTrimExpression(null);
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
