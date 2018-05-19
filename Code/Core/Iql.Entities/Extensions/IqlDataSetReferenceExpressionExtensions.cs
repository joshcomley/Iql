using System.Linq;

namespace Iql.Entities.Extensions
{
    public static class IqlDataSetReferenceExpressionExtensions
    {
        public static IEntityConfiguration ResolveEntityConfiguration(this IqlDataSetQueryExpression expression)
        {
            return EntityConfigurationBuilder.FindConfigurationForEntityTypeName(
                expression.Parameters.First().EntityTypeName);
        }
    }
}