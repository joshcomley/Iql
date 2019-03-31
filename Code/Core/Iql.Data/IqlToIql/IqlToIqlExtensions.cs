using System.Threading.Tasks;
using Iql.Data.Types;
using Iql.Entities;
using Iql.Entities.Services;
using Iql.Parsing.Types;

namespace Iql.Data.IqlToIql
{
    public static class IqlToIqlExtensions
    {
        public static async Task<IqlExpessionResult> ProcessAsync(
            this IqlExpression expression, 
            IIqlTypeMetadata resolvedType,
            ITypeResolver typeResolver,
            IServiceProviderProvider serviceProvider,
            bool resolveSpecialValues = false)
        {
            var parser = new IqlToIqlParserContext(
                resolvedType,
                typeResolver,
                serviceProvider?.ServiceProvider,
                resolveSpecialValues);
            var result = await parser.ParseAsync(expression);
            return new IqlExpessionResult(parser.Success, result.Expression);
        }
    }
}