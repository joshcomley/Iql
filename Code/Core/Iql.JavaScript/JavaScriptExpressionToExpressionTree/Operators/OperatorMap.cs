namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree.Operators
{
    public class OperatorMap
    {
        public static EnumMapper<OperatorType> OperatorTypes = new EnumMapper<OperatorType>()
            .Map(() => OperatorType.And, "&&")
            .Map(() => OperatorType.Or, "||")
            .Map(() => OperatorType.Assign, "=")
            .Map(() => OperatorType.EqualsEquals, "==")
            .Map(() => OperatorType.Not, "!")
            .Map(() => OperatorType.NotEquals, "!=")
            .Map(() => OperatorType.EqualsEqualsEquals, "===")
            .Map(() => OperatorType.NotEqualsEquals, "!===")
            .Map(() => OperatorType.GreaterThan, ">")
            .Map(() => OperatorType.GreaterThanOrEqualTo, ">=")
            .Map(() => OperatorType.LessThan, "<")
            .Map(() => OperatorType.LessThanOrEqualTo, "<=")
            .Map(() => OperatorType.LessThanOrEqualTo, "%")
            .Map(() => OperatorType.Subtract, "-")
            .Map(() => OperatorType.Add, "+")
            .Map(() => OperatorType.SubtractOne, "--")
            .Map(() => OperatorType.AddOne, "++")
            .Map(() => OperatorType.SubtractEquals, "-=")
            .Map(() => OperatorType.AddEquals, "+=")
            .Map(() => OperatorType.BitwiseOr, "|")
            .Map(() => OperatorType.BitwiseAnd, "&")
            .Map(() => OperatorType.BitwiseNot, "~");
    }
}