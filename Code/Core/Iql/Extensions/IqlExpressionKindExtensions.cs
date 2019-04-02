namespace Iql.Extensions
{
    public static class IqlExpressionKindExtensions
    {
        public static IqlType ResolveDefaultReturnType(this IqlExpressionKind kind)
        {
            switch (kind)
            {
                case IqlExpressionKind.IsEqualTo:
                case IqlExpressionKind.IsNotEqualTo:
                case IqlExpressionKind.IsGreaterThan:
                case IqlExpressionKind.IsGreaterThanOrEqualTo:
                case IqlExpressionKind.IsLessThan:
                case IqlExpressionKind.IsLessThanOrEqualTo:
                    return IqlType.Boolean;
            }

            return IqlType.Unknown;
        }
    }
}