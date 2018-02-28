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
            Type = IqlExpressionType.RootReference;
        }

        public IqlRootReferenceExpression() : this(null, null)
        {
        }

        public override bool ContainsRootEntity()
        {
            return true;
        }
    }
}