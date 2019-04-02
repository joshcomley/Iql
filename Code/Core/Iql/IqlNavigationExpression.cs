using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlNavigationExpression : IqlParameteredLambdaExpression
    {
        public string EntityTypeName { get; set; }

        protected IqlNavigationExpression(
            string entityTypeName,
            IqlExpressionKind kind,
            IqlType type,
            IqlExpression parent = null) : base(
            kind,
            IqlType.Class,
            parent)
        {
            EntityTypeName = entityTypeName;
            Parameters.Add(new IqlRootReferenceExpression() { EntityTypeName = entityTypeName });
        }

        public List<IqlExpandExpression> Expands { get; set; }
        public IqlExpression Filter { get; set; }
        public IqlWithKeyExpression WithKey { get; set; }
    }
}