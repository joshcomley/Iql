namespace Iql
{
    public enum IqlExpressionType
    {
        Aggregate,
        Parenthesis,
        And,
        Or,
        IsGreaterThan,
        IsGreaterThanOrEqualTo,
        IsLessThan,
        IsLessThanOrEqualTo,
        Assign,
        IsEqualTo,
        IsNotEqualTo,
        Not,
        Modulo,
        ModuloEquals,
        Add,
        Subtract,
        Multiply,
        Divide,
        AddEquals,
        SubtractEquals,
        MultiplyEquals,
        DivideEquals,
        BitwiseOr,
        Has,
        BitwiseNot,
        Literal,
        UnarySubtract,
        RootReference,
        Variable,
        Property,
        StringIncludes,
        StringIndexOf,
        StringSubString,
        StringToUpperCase,
        StringToLowerCase,
        StringTrim,
        StringEndsWith,
        StringStartsWith,
        StringConcat,
        StringLength,
        ToString,
        Final
    }
}