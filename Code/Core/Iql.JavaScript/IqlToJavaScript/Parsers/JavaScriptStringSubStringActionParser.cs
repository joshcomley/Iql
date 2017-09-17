using System.Collections.Generic;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptStringSubStringActionParser : JavaScriptMethodActionParser<IqlStringSubStringExpression>
    {
        public override IEnumerable<IqlExpression> ResolveMethodArguments(IqlStringSubStringExpression action)
        {
            return new[]
            {
                action.Value, action.Take
            };
        }

        public override string ResolveMethodName(IqlStringSubStringExpression action)
        {
            return "substring";
        }
    }
}