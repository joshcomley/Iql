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
            switch (action.Type)
            {
                case IqlExpressionType.StringToUpperCase:
                    return "toUpperCase";
                case IqlExpressionType.StringToLowerCase:
                    return "toLowerCase";
                case IqlExpressionType.StringTrim:
                    return "trim";
            }
            JavaScriptErrors.OperationNotSupported(action.Type);
            return null;
        }
    }
}