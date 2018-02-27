using System;
using System.Linq.Expressions;
using Iql.Parsing;

namespace Iql.DotNet.IqlToDotNetString
{
    public class DotNetStringIqlParserInstance : ActionParserInstance<DotNetStringIqlData, DotNetStringIqlExpressionAdapter, string, DotNetStringOutput>
    {
        public DotNetStringIqlParserInstance(DotNetStringIqlExpressionAdapter adapter) : base(adapter, null)
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