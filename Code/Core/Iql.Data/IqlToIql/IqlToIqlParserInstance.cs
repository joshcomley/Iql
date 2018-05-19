using System;
using Iql.JavaScript.IqlToJavaScriptExpression;
using Iql.Parsing;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Expressions.Conversion;
using Iql.Queryable.Types;

namespace Iql.Queryable.IqlToIql
{
    public class IqlToIqlParserInstance : ActionParserInstance<IqlToIqlIqlData, IqlToIqlExpressionAdapter,
        string, IqlToIqlIqlOutput, IExpressionConverter>
    {
        public IqlToIqlParserInstance(IEntityConfiguration entityConfiguration) : base(
            new IqlToIqlExpressionAdapter(entityConfiguration.Builder), entityConfiguration.Type, null, new TypeResolver()) { }
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
            return new IqlToIqlIqlOutput(parser.ToQueryString(expression, this));
        }
    }
}