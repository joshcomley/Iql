namespace Iql
{
    public abstract class IqlExpression
    {
        protected IqlExpression(IqlExpressionType type, IqlType? returnType = IqlType.Void, IqlExpression parent = null)
        {
            Type = type;
            ReturnType = returnType ?? IqlType.Boolean;
            Parent = parent;
        }

        public IqlExpressionType Type { get; set; }
        public IqlType ReturnType { get; set; }
        public IqlExpression Parent { get; set; }

        public static bool IsIqlExpression(object obj)
        {
            return obj is IqlExpression;
        }

        public virtual bool ContainsRootEntity()
        {
            return Parent != null && Parent.ContainsRootEntity();
        }

        public static IqlPropertyExpression GetPropertyExpression(string propertyName)
        {
            var rootReferenceExpression = new IqlRootReferenceExpression("entity", "");
            var propertyExpression = new IqlPropertyExpression(propertyName, null, IqlType.Unknown);
            propertyExpression.Parent = rootReferenceExpression;
            return propertyExpression;
        }
    }
}