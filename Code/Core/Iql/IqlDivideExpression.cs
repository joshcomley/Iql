using System.Collections.Generic;

namespace Iql
{
    public class IqlDivideExpression : IqlBinaryExpression
    {
        public IqlDivideExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Divide, left, right)
        {
        }

        public IqlDivideExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlDivideExpression(null, null);
			expression.Left = Left?.Clone();
			expression.Right = Right?.Clone();
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
			Left?.FlattenInternal(expressions);
			Right?.FlattenInternal(expressions);
			Parent?.FlattenInternal(expressions);

			// #FlattenEnd
        }
    }
}
