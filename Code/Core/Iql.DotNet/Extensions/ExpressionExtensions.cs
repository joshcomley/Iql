using System;
using System.Linq.Expressions;
using Iql.DotNet.Expressions;
using Iql.DotNet.Visitors;

namespace Iql.DotNet.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression GetBody(this Expression expression)
        {
            if (expression is UnaryExpression unaryExpression)
            {
                expression = unaryExpression.Operand;
                if (expression.NodeType == ExpressionType.Constant &&
                    (unaryExpression.NodeType == ExpressionType.Negate || unaryExpression.NodeType == ExpressionType.NegateChecked))
                {
                    expression = Expression.Constant(Expression.Lambda(unaryExpression).Compile().DynamicInvoke());
                }
            }

            if (expression is LambdaExpression lambdaExpression)
            {
                expression = lambdaExpression.Body;
            }

            return expression;
        }
        public static bool ContainsRoot(this Expression member,
            Type rootType, string rootVariableName)
        {
            return new ExpressionContainsRootVisitor(rootType, rootVariableName)
                .ContainsRoot(member);
        }

        public static string ToCSharpString(this Expression expression)
        {
            return ExpressionCSharpStringBuilder.ExpressionToString(expression);
        }
    }
}