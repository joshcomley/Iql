using System;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Types;
using Iql.Entities;
using Iql.Entities.Services;
using Iql.Entities.SpecialTypes;
using Iql.Parsing;

namespace Iql.Data.IqlToIql
{
    public class IqlToIqlParserContext : AsyncActionParserContext<IqlToIqlIqlData, IqlToIqlExpressionAdapter,
        string, IqlToIqlIqlOutput, IExpressionConverter>, IServiceProviderProvider
    {
        public bool Success { get; set; } = true;
        public IqlServiceProvider ServiceProvider { get; }

        public IqlToIqlParserContext(IEntityConfiguration entityConfiguration, IqlServiceProvider serviceProvider) : base(
            new IqlToIqlExpressionAdapter(entityConfiguration.Builder), entityConfiguration.Type, null, new TypeResolver())
        {
            ServiceProvider = serviceProvider;
        }

        public override async Task<IqlToIqlIqlOutput> ParseExpressionAsync(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            if (expression == null)
            {
                return new IqlToIqlIqlOutput(null);
            }
            var parser = Adapter.Registry.Resolve(IqlExpression.ResolveExpressionType(expression));
            if (parser == null)
            {
                throw new Exception("No parser found for " + expression.GetType().Name);
            }
            var result = await parser.ToQueryStringAsync(expression, this);
            //return result == null ? null : new IqlToIqlIqlOutput(result);
            return new IqlToIqlIqlOutput(result);
        }

        public SpecialTypeDefinition ResolveSpecialTypeMap(Action<SpecialTypeDefinition> action = null)
        {
            var definition =
                Adapter.EntityConfigurationContext.GetEntityByType(CurrentEntityType).SpecialTypeDefinition;
            if(definition  != null && action != null)
            {
                action(definition);
            }

            return definition;
        }
    }
}