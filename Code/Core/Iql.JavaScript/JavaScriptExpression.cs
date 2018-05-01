namespace Iql.JavaScript
{
    public class JavaScriptExpression
    {
        public JavaScriptExpression(
            string rootVariableName,
            string expression)
        {
            RootVariableName = rootVariableName;
            Expression = expression;
        }

        public string RootVariableName { get; }
        public string Expression { get; }

        public string AsFunction(bool typeScript = false)
        {
            if (Expression.StartsWith("function("))
            {
                return Expression;
            }
            if (typeScript)
            {
                return $"{RootVariableName} => {Expression}";
            }
            return $"function({RootVariableName}) {{ return {Expression}; }}";
        }
    }
}