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
    }
}