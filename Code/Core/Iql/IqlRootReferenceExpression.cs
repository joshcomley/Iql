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

        public override bool ContainsRootEntity()
        {
            return true;
        }

		public override IqlExpression Clone()
		{
			// #CloneStart
			return null;
			// #CloneEnd
		}
    }
}
