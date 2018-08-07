using System;

namespace Iql
{
    public class IqlRootReferenceExpression : IqlVariableExpression
    {
        public IqlRootReferenceExpression(
            string variableName = null,
            string value = null,
            Type entityType = null) : base(variableName, value, entityType)
        {
            Kind = IqlExpressionKind.RootReference;
        }

        public IqlRootReferenceExpression() : this(null, null)
        {
        }

        public override bool IsOrHas(Func<IqlExpression, bool> matches)
        {
            return true;
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlRootReferenceExpression();
			expression.EntityTypeName = EntityTypeName;
			expression.Value = Value;
			expression.VariableName = VariableName;
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
