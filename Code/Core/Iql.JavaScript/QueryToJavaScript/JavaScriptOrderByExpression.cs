namespace Iql.JavaScript.QueryToJavaScript
{
    public class JavaScriptOrderByExpression : JavaScriptExpression
    {
        public JavaScriptOrderByExpression(
            string rootVariableName,
            string expression,
            bool descending)
            : base(rootVariableName, expression)
        {
            Descending = descending;
        }

        public bool Descending { get; }
    }
}