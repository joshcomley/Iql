using Iql.Entities;

namespace Iql.Data.IqlToIql
{
    public static class IqlToIqlExtensions
    {
        public static IqlExpression Process(this IqlExpression expression, IEntityConfiguration entityConfiguration)
        {
            var parser = new IqlToIqlParserInstance(entityConfiguration);
            return parser.Parse(expression).Expression;
        }
    }
}