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
    }

    //public class Iql
    //{
    //    public static ExpressionTypes = new EnumMapper<IqlExpressionType>()
    //    .map(() => IqlExpressionType.Aggregate)
    //        .map(() => IqlExpressionType.Parenthesis)
    //        .map(() => IqlExpressionType.And)
    //        .map(() => IqlExpressionType.Or)
    //        .map(() => IqlExpressionType.GreaterThan)
    //        .map(() => IqlExpressionType.GreaterThanOrEqualTo)
    //        .map(() => IqlExpressionType.LessThan)
    //        .map(() => IqlExpressionType.LessThanOrEqualTo)
    //        .map(() => IqlExpressionType.Assign)
    //        .map(() => IqlExpressionType.EqualsEquals)
    //        .map(() => IqlExpressionType.NotEquals)
    //        .map(() => IqlExpressionType.EqualsEqualsEquals)
    //        .map(() => IqlExpressionType.NotEqualsEquals)
    //        .map(() => IqlExpressionType.Not)
    //        .map(() => IqlExpressionType.Modulo)
    //        .map(() => IqlExpressionType.Subtract)
    //        .map(() => IqlExpressionType.Add)
    //        .map(() => IqlExpressionType.SubtractEquals)
    //        .map(() => IqlExpressionType.AddEquals)
    //        .map(() => IqlExpressionType.BitwiseOr)
    //        .map(() => IqlExpressionType.BitwiseAnd)
    //        .map(() => IqlExpressionType.BitwiseNot)
    //        .map(() => IqlExpressionType.Literal)
    //        .map(() => IqlExpressionType.UnarySubtract)
    //        .map(() => IqlExpressionType.RootReference)
    //        .map(() => IqlExpressionType.Variable)
    //        .map(() => IqlExpressionType.Property)
    //        .map(() => IqlExpressionType.StringIncludes)
    //        .map(() => IqlExpressionType.StringIndexOf)
    //        .map(() => IqlExpressionType.StringSubString)
    //        .map(() => IqlExpressionType.StringToUpperCase)
    //        .map(() => IqlExpressionType.StringToLowerCase)
    //        .map(() => IqlExpressionType.StringTrim)
    //        .map(() => IqlExpressionType.StringEndsWith)
    //        .map(() => IqlExpressionType.StringStartsWith)
    //        .map(() => IqlExpressionType.StringConcat)
    //        .map(() => IqlExpressionType.ToString)
    //        ;
    //}

    // Variables cannot have parents by definition
}