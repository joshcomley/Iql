using System.Linq.Expressions;
using TypeSharp.Extensions;

namespace Iql.Parsing.Extensions
{
    [DoNotConvert]
    public static class ExpressionExtensions
    {
        public static object GetValue(this Expression e)
        {
            //A little optimization for constant expressions
            return e.NodeType == ExpressionType.Constant
                ? ((ConstantExpression) e).Value
                : Expression.Lambda(e).Compile().DynamicInvoke();
        }
    }
}