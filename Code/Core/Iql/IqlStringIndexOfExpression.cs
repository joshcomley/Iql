using System.Collections.Generic;

namespace Iql
{
    public class IqlStringIndexOfExpression : IqlParentValueExpression
    {
        public IqlStringIndexOfExpression(IqlReferenceExpression parent, IqlReferenceExpression value)
            : base(parent, value, IqlExpressionKind.StringIndexOf, IqlType.Integer)
        {
        }

        public IqlStringIndexOfExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringIndexOfExpression(null, null);
			expression.Value = Value?.Clone();
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
			Value?.FlattenInternal(expressions);
			Parent?.FlattenInternal(expressions);

			// #FlattenEnd
        }
    }
}
