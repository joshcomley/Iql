using System.Threading.Tasks;
using Iql.Entities;

namespace Iql.Data.IqlToIql
{
    public static class IqlToIqlExtensions
    {
        public static async Task<IqlExpression> ProcessAsync(this IqlExpression expression, IEntityConfiguration entityConfiguration)
        {
            var parser = new IqlToIqlParserContext(entityConfiguration);
            var result = await parser.Parse(expression);
            return result.Expression;

        }
    }
}