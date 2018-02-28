using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.DotNet.Extensions;

namespace Iql.DotNet.DotNetExpressionToIql
{
    public class DotNetExpressionParserContext
    {
        public DotNetExpressionParserContext(
            Type rootType,
            string rootVariableName,
            Func<Expression, DotNetExpressionParserContext, IqlExpression> parse)
        {
            RootType = rootType;
            RootVariableName = rootVariableName;
            Parse = parse;
        }

        public Dictionary<IqlVariableExpression, object> VariableValues { get; }
            = new Dictionary<IqlVariableExpression, object>();

        public Type RootType { get; }
        public string RootVariableName { get; }
        public Func<Expression, DotNetExpressionParserContext, IqlExpression> Parse { get; }

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