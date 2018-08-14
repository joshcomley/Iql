using System;
using Iql.Conversion;
using Iql.Data.Types;
using Iql.Entities;
using Iql.Entities.SpecialTypes;
using Iql.Parsing;

namespace Iql.Data.IqlToIql
{
    public class IqlToIqlParserInstance : ActionParserInstance<IqlToIqlIqlData, IqlToIqlExpressionAdapter,
        string, IqlToIqlIqlOutput, IExpressionConverter>
    {
        public IqlToIqlParserInstance(IEntityConfiguration entityConfiguration) : base(
            new IqlToIqlExpressionAdapter(entityConfiguration.Builder), entityConfiguration.Type, null, new TypeResolver())
        { }
        public override IqlToIqlIqlOutput ParseExpression(IqlExpression expression
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

            var result = parser.ToQueryString(expression, this);
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