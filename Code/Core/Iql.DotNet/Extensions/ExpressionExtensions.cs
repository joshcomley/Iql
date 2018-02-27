using System;
using System.Linq.Expressions;
using Iql.DotNet.Expressions;
using Iql.DotNet.Visitors;

namespace Iql.DotNet.Extensions
{
    public static class ExpressionExtensions
    {
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