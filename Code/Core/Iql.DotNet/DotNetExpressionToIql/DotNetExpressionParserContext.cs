using System;
using System.Collections.Generic;
using System.Linq;
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
        private Dictionary<IqlVariableExpression, object> _variableValues;

        public Dictionary<IqlVariableExpression, object> VariableValues => _variableValues = _variableValues ?? new Dictionary<IqlVariableExpression, object>();

        public Type RootType { get; }
        public string RootVariableName { get; }
        public Func<Expression, DotNetExpressionParserContext, IqlExpression> Parse { get; }

        public IqlExpression ToIqlExpression(Expression node)
        {
            return Parse(node, this);
        }
        private List<Type> _rootVariableTypes;

        public List<Type> RootVariableTypes => _rootVariableTypes = _rootVariableTypes ?? new List<Type>();
        private List<string> _rootVariableNames;
        public List<string> RootVariableNames => _rootVariableNames = _rootVariableNames ?? new List<string>();

        private int _throwawayRootVariableIndex;
        public string GetThrowawayRootVariableName()
        {
            return $"_{_throwawayRootVariableIndex++}";
        }
        public bool ContainsRoot(Expression node)
        {
            for (var i = 0; i < RootVariableTypes.Count; i++)
            {
                var rootType = RootVariableTypes[i];
                var rootName = RootVariableNames[i];
                if (node.ContainsRoot(rootType, rootName))
                {
                    return true;
                }
            }
            return false;
        }
    }
}