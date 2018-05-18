using System;
using Iql.JavaScript.JavaScriptExpressionToIql;
using Iql.Parsing;
using Iql.Queryable.Types;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptIqlParserInstance : ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter, string, JavaScriptOutput, JavaScriptExpressionConverter>
    {
        public JavaScriptIqlParserInstance(JavaScriptIqlExpressionAdapter adapter, Type rootEntityType, JavaScriptExpressionConverter expressionConverter) : base(adapter, rootEntityType, expressionConverter, new TypeResolver())
        {
        }

        //public string ToLambda(string code)
        //{
        //    var rootEntityName = RootEntityParameterName();
        //    var lambda = $"function({rootEntityName}) {{ return {code}; }}";
        //    return lambda;
        //}

        public override JavaScriptOutput ParseExpression(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
        {
            return new JavaScriptOutput(ParseAsString(expression
#if TypeScript
            , evaluateContext
#endif
                ), RootEntityParameterName());
        }
    }
}