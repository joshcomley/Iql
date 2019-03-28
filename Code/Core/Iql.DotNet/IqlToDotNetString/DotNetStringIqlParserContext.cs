using Iql.Data.Types;
using Iql.Parsing;
using Iql.Parsing.Types;

namespace Iql.DotNet.IqlToDotNetString
{
    public class DotNetStringIqlParserContext : ActionParserContext<DotNetStringIqlData, DotNetStringIqlExpressionAdapter, string, DotNetStringOutput, DotNetExpressionConverter>
    {
        public DotNetStringIqlParserContext(DotNetStringIqlExpressionAdapter adapter, DotNetExpressionConverter expressionConverter, ITypeResolver typeResolver) : base(
            adapter, 
            null, 
            expressionConverter, 
            typeResolver)
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