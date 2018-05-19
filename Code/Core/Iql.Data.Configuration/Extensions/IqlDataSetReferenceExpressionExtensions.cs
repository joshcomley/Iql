using System.Linq;

namespace Iql.Data.Configuration.Extensions
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