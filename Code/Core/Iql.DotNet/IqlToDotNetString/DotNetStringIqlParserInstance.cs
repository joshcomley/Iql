using Iql.Parsing;

namespace Iql.DotNet.IqlToDotNetString
{
    public class DotNetStringIqlParserInstance : ActionParserInstance<DotNetStringIqlData, DotNetStringIqlExpressionAdapter, string, DotNetStringOutput, DotNetExpressionConverter>
    {
        public DotNetStringIqlParserInstance(DotNetStringIqlExpressionAdapter adapter, DotNetExpressionConverter expressionConverter) : base(adapter, null, expressionConverter)
        {
            RootVariableName = adapter.RootVariableName;
        }

        public string RootVariableName { get; set; }


        public override DotNetStringOutput Parse(IqlExpression expression
#if TypeScript
                , EvaluateContext evaluateContext
#endif
        )
        {
            var code = ParseAsString(expression
#if TypeScript
                , evaluateContext
#endif
            );
            return new DotNetStringOutput(code);
        }
   }
}