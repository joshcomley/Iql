using System.Collections.Generic;

namespace Iql
{
    public class IqlNowExpression : IqlExpression
    {
        public IqlNowExpression() : base(IqlExpressionKind.Now, IqlType.Date)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlNowExpression();
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
