namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree.Operators
{
    public class OperatorMap
    {
        private static bool OperatorTypesDelayedInitialized;
        private static EnumMapper<OperatorType> OperatorTypesDelayed;
        public static EnumMapper<OperatorType> OperatorTypes { get { if(!OperatorTypesDelayedInitialized) { OperatorTypesDelayedInitialized = true; OperatorTypesDelayed = new EnumMapper<OperatorType>()
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
            .Map(() => OperatorType.Multiply, "*")
            .Map(() => OperatorType.Divide, "/")
            .Map(() => OperatorType.SubtractOne, "--")
            .Map(() => OperatorType.AddOne, "++")
            .Map(() => OperatorType.SubtractEquals, "-=")
            .Map(() => OperatorType.AddEquals, "+=")
            .Map(() => OperatorType.MultiplyEquals, "*")
            .Map(() => OperatorType.DivideEquals, "/")
            .Map(() => OperatorType.BitwiseOr, "|")
            .Map(() => OperatorType.Has, "&")
            .Map(() => OperatorType.BitwiseNot, "~"); } return OperatorTypesDelayed; } set { OperatorTypesDelayedInitialized = true; OperatorTypesDelayed = value; } }
    }
}