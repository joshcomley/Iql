using Iql.JavaScript.JavaScriptExpressionToExpressionTree;

namespace Iql.JavaScript
{
    public class JavaScriptFunctionBody
    {
        public JavaScriptFunctionBody(
            string[] parameterNames,
            string body,
            string signature,
            string originalCode,
            string cleanedCode)
        {
            ParameterNames = parameterNames;
            Body = body;
            Signature = signature;
            OriginalCode = originalCode;
            CleanedCode = cleanedCode;
        }

        public string[] ParameterNames { get; }
        public string Body { get; }
        public string Signature { get; }
        public string OriginalCode { get; }
        public string CleanedCode { get; }

        public JavaScriptExpressionNode ToExpressionTree()
        {
            var jsp = new JavaScriptExpressionStringToExpressionTreeParser(CleanedCode);
            var expressionTree = jsp.Parse();
            return expressionTree;
        }
    }
}