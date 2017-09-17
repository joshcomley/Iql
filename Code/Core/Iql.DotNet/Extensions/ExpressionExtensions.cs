using System;
using System.Linq.Expressions;
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
    }
}