using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Types;
using Iql.Parsing;

namespace Iql.OData.IqlToODataExpression
{
    public class ODataIqlParserInstance : ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter, string, ODataOutput, ODataExpressionConverter>
    {
        private List<string> _validRootReferenceVariables = new List<string>();
        public ODataIqlParserInstance(ODataIqlExpressionAdapter adapter, Type currentEntityType, ODataExpressionConverter expressionConverter) : base(adapter, currentEntityType, expressionConverter, new TypeResolver())
        {
        }

        public string EncodeIfNecessary(string value)
        {
            return Nested ? value : Uri.EscapeDataString(value);
        }

        public bool IsValidRootReferenceName(string name)
        {
            return _validRootReferenceVariables.Contains(name);
        }

        public ODataOutput ParseWithValidRootReferenceVariable(IqlExpression expression,
            params string[] rootReferenceVariables)
        {
            var copy = _validRootReferenceVariables.ToList();
            _validRootReferenceVariables.AddRange(rootReferenceVariables);
            var result = Parse(expression);
            _validRootReferenceVariables = copy;
            return result;
        }

        public override ODataOutput ParseExpression(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return new ODataOutput(ParseAsString(expression
#if TypeScript
            , evaluateContext
#endif
                ));
        }
    }
}