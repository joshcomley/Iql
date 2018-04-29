namespace Iql
{
    public enum IqlExpressionKind
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
        Final,
        Now,
        Any,
        All,
        Count,
        TimeSpan
//        Lambda
        ,
        Expand,
        DataSetQuery,
        DataSetReference,
        Skip,
        Take,
        PropertyNavigation,
        CollectionNavigation,
        CollectionPropertyNavigation
    }
}