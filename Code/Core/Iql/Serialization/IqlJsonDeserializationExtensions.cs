using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Serialization
{
    public static class IqlJsonDeserializationExtensions
    {
        public static bool ClaimsToBeIql(this object obj)
        {
            return obj.ClaimedIqlKind() != null;
        }
        public static IqlExpressionKind? ClaimedIqlKind(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (Equals(obj.GetPropertyValueByName(nameof(IqlExpression.IsIqlExpression)), true))
            {
                return obj.GetPropertyValueByNameAs<IqlExpressionKind>(nameof(IqlExpression.Kind));
            }
            return null;
        }

        public static IqlExpression EnsureIsIql(this IqlExpression expression)
        {
            return expression.Clone();
        }
    }
}