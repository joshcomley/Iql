using System.Collections.Generic;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptStringSourceValueActionParser : JavaScriptMethodActionParser<IqlParentValueExpression>
    {
        private bool _ignoreCase;

        public override IqlExpression ResolveMethodCaller(IqlParentValueExpression action)
        {
            if (_ignoreCase)
            {
                return new IqlStringToUpperCaseExpression(action.Parent as IqlReferenceExpression);
            }
            return action.Parent;
        }

        public override IEnumerable<IqlExpression> ResolveMethodArguments(IqlParentValueExpression action)
        {
            if (_ignoreCase)
            {
                return new IqlExpression[] {new IqlStringToUpperCaseExpression(action.Value as IqlReferenceExpression)};
            }
            return new[] {action.Value};
        }

        public override string ResolveMethodName(IqlParentValueExpression action)
        {
            switch (action.Type)
            {
                case IqlExpressionType.StringIndexOf:
                    _ignoreCase = true;
                    return "indexOf";
                case IqlExpressionType.StringIncludes:
                    _ignoreCase = true;
                    return "includes";
                case IqlExpressionType.StringEndsWith:
                    _ignoreCase = true;
                    return "endsWith";
                case IqlExpressionType.StringStartsWith:
                    _ignoreCase = true;
                    return "startsWith";
                case IqlExpressionType.StringConcat:
                    return "concat";
            }
            JavaScriptErrors.OperationNotSupported(action.Type);
            return null;
        }
    }
}