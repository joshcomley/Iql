namespace Iql
{
    public enum IqlExpressionKind
    {
        Aggregate = 1,
        Parenthesis = 2,
        And = 3,
        Or = 4,
        IsGreaterThan = 5,
        IsGreaterThanOrEqualTo = 6,
        IsLessThan = 7,
        IsLessThanOrEqualTo = 8,
        Assign = 9,
        IsEqualTo = 10,
        IsNotEqualTo = 11,
        Not = 12,
        Modulo = 13,
        ModuloEquals = 14,
        Add = 15,
        Subtract = 16,
        Multiply = 17,
        Divide = 18,
        AddEquals = 19,
        SubtractEquals = 20,
        MultiplyEquals = 21,
        DivideEquals = 22,
        BitwiseOr = 23,
        Has = 24,
        BitwiseNot = 25,
        Literal = 26,
        UnarySubtract = 27,
        RootReference = 28,
        Variable = 29,
        Property = 30,
        StringIncludes = 31,
        StringIndexOf = 32,
        StringSubString = 33,
        StringToUpperCase = 34,
        StringToLowerCase = 35,
        StringTrim = 36,
        StringEndsWith = 37,
        StringStartsWith = 38,
        StringConcat = 39,
        StringLength = 40,
        ToString = 41,
        Final = 42,
        Now = 43,
        Any = 44,
        All = 45,
        Count = 46,
        TimeSpan = 47,
        Expand = 48,
        DataSetQuery = 49,
        DataSetReference = 50,
        WithKey = 51,
        OrderBy = 52,
        EnumLiteral = 53,
        EnumValue = 54,
        Lambda = 55,
        Condition = 56,
        Invocation = 57,
        GeoPoint = 58,
        GeoMultiPoint = 59,
        GeoLine = 60,
        GeoMultiLine = 61,
        GeoPolygon = 62,
        GeoMultiPolygon = 63,
        Intersects = 64,
        Length = 65,
        Distance = 66,
        GeoRing = 67,
        CurrentUser = 68,
        CurrentUserId = 69,
        CurrentLocation = 70,
        NewGuid = 71
    }
}