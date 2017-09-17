using System;
using System.Linq.Expressions;
using TypeSharp.Extensions;

namespace Iql.DotNet.Visitors
{
    [DoNotConvert]
    internal class ExpressionContainsRootVisitor : ExpressionVisitor
    {
        private readonly Type _rootType;
        private readonly string _rootVariableName;
        private bool _containsRoot;

        public ExpressionContainsRootVisitor(Type rootType, string rootVariableName)
        {
            _rootType = rootType;
            _rootVariableName = rootVariableName;
        }

        public bool ContainsRoot(Expression expression)
        {
            Visit(expression);
            return _containsRoot;
        }

        public override Expression Visit(Expression exp)
        {
            var parameterExpression = exp as ParameterExpression;
            if (parameterExpression != null)
            {
                if (parameterExpression.Type == _rootType && parameterExpression.Name == _rootVariableName)
                {
                    _containsRoot = true;
                    return exp;
                }
            }
            return base.Visit(exp);
        }
    }
}