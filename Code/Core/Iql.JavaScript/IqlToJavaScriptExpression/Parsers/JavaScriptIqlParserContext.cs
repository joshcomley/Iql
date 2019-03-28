using System;
using System.Linq;
using Iql.Data.Types;
using Iql.JavaScript.JavaScriptExpressionToIql;
using Iql.Parsing;
using Iql.Parsing.Types;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptIqlParserContext : ActionParserContext<JavaScriptIqlData, JavaScriptIqlExpressionAdapter, string, JavaScriptOutput, JavaScriptExpressionConverter>
    {
        public JavaScriptIqlParserContext(JavaScriptIqlExpressionAdapter adapter, Type currentEntityType, JavaScriptExpressionConverter expressionConverter, ITypeResolver typeResolver) : base(adapter, currentEntityType, expressionConverter, typeResolver)
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

        public bool AllowTranspilation()
        {
            return Ancestors.Count < 2 || !(Ancestors[0].Kind == IqlExpressionKind.Lambda && Ancestors[1].Kind == IqlExpressionKind.Lambda);
        }
    }
}