using System.Collections.Generic;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
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
            switch (action.Kind)
            {
                case IqlExpressionKind.StringIndexOf:
                    _ignoreCase = true;
                    return "indexOf";
                case IqlExpressionKind.StringIncludes:
                    _ignoreCase = true;
                    return "includes";
                case IqlExpressionKind.StringEndsWith:
                    _ignoreCase = true;
                    return "endsWith";
                case IqlExpressionKind.StringStartsWith:
                    _ignoreCase = true;
                    return "startsWith";
                case IqlExpressionKind.StringConcat:
                    return "concat";
            }
            JavaScriptErrors.OperationNotSupported(action.Kind);
            return null;
        }
    }
}