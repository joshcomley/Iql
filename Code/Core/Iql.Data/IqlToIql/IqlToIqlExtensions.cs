using System.Threading.Tasks;
using Iql.Entities;
using Iql.Entities.Services;

namespace Iql.Data.IqlToIql
{
    public static class IqlToIqlExtensions
    {
        public static async Task<IqlExpression> ProcessAsync(
            this IqlExpression expression, 
            IEntityConfiguration entityConfiguration,
            IServiceProviderProvider serviceProvider = null)
        {
            var parser = new IqlToIqlParserContext(
                entityConfiguration,
                serviceProvider != null ? serviceProvider.ServiceProvider : entityConfiguration.Builder.ServiceProvider);
            var result = await parser.ParseAsync(expression);
            return result.Expression;
        }
    }
}