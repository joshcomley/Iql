using System;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Types;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.Services;
using Iql.Entities.SpecialTypes;
using Iql.Parsing;
using Iql.Parsing.Types;

namespace Iql.Data.IqlToIql
{
    public class IqlToIqlParserContext : AsyncActionParserContext<IqlToIqlIqlData, IqlToIqlExpressionAdapter,
        string, IqlToIqlIqlOutput, IExpressionConverter>, IServiceProviderProvider
    {
        public bool Success { get; set; } = true;
        public IqlServiceProvider ServiceProvider { get; }
        public bool ResolveSpecialValues { get; }

        public IqlToIqlParserContext(
            IIqlTypeMetadata resolvedType, 
            ITypeResolver resolver, 
            IqlServiceProvider serviceProvider,
            bool resolveSpecialValues = false) : base(
            new IqlToIqlExpressionAdapter(resolver), resolvedType.Type, null, resolver)
        {
            ServiceProvider = serviceProvider;
            ResolveSpecialValues = resolveSpecialValues;
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
                Adapter.TypeResolver.FindTypeByType(CurrentEntityType).EntityConfiguration()?.SpecialTypeDefinition;
            if(definition  != null && action != null)
            {
                action(definition);
            }

            return definition;
        }
    }
}