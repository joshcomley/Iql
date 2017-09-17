namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    internal class OperatorMapping
    {
        public OperatorMapping(string javascript, OperatorType _operator)
        {
            Javascript = javascript;
            Operator = _operator;
        }

        public string Javascript { get; set; }
        public OperatorType Operator { get; set; }
    }
}