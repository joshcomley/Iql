using Iql.Parsing;
using Iql.Queryable.Types;

namespace Iql.DotNet.IqlToDotNetString
{
    public class DotNetStringIqlParserInstance : ActionParserInstance<DotNetStringIqlData, DotNetStringIqlExpressionAdapter, string, DotNetStringOutput, DotNetExpressionConverter>
    {
        public DotNetStringIqlParserInstance(DotNetStringIqlExpressionAdapter adapter, DotNetExpressionConverter expressionConverter) : base(adapter, null, expressionConverter, new TypeResolver())
        {
            RootVariableName = adapter.RootVariableName;
        }

        public string RootVariableName { get; set; }


        public override DotNetStringOutput ParseExpression(IqlExpression expression
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