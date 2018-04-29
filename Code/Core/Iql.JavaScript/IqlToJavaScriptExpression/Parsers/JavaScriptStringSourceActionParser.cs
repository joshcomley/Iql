using System.Collections.Generic;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptStringSourceActionParser : JavaScriptMethodActionParser<IqlExpression>
    {
        public override IEnumerable<IqlExpression> ResolveMethodArguments(IqlExpression action)
        {
            return new IqlExpression[] { };
        }

        public override string ResolveMethodName(IqlExpression action)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.StringToUpperCase:
                    return "toUpperCase";
                case IqlExpressionKind.StringToLowerCase:
                    return "toLowerCase";
                case IqlExpressionKind.StringTrim:
                    return "trim";
            }
            JavaScriptErrors.OperationNotSupported(action.Kind);
            return null;
        }
    }
}