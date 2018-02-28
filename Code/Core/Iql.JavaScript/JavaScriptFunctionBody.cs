using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript
{
    public class JavaScriptFunctionBody
    {
        public JavaScriptFunctionBody(
            string body,
            string signature,
            string originalCode,
            string cleanedCode)
        {
            ParameterNames = signature.Split(',');
            for (var i = 0; i < ParameterNames.Length; i++)
            {
                ParameterNames[i] = ParameterNames[i].Trim();
            }
            Body = body.Trim();
            Signature = signature.Trim();
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