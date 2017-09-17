using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.DotNet.Extensions;

namespace Iql.DotNet
{
    public class ExpressionParserContext
    {
        public ExpressionParserContext(
            Type rootType,
            string rootVariableName,
            Func<Expression, ExpressionParserContext, IqlExpression> parse)
        {
            RootType = rootType;
            RootVariableName = rootVariableName;
            Parse = parse;
        }

        public Dictionary<IqlVariableExpression, object> VariableValues { get; }
            = new Dictionary<IqlVariableExpression, object>();

        public Type RootType { get; }
        public string RootVariableName { get; }
        public Func<Expression, ExpressionParserContext, IqlExpression> Parse { get; }

        public IqlExpression ToIqlExpression(Expression node)
        {
            return Parse(node, this);
        }

        public bool ContainsRoot(Expression node)
        {
            return node.ContainsRoot(RootType, RootVariableName);
        }
    }
}