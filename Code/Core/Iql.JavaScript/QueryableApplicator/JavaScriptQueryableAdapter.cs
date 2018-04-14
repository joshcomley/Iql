using Iql.JavaScript.IqlToJavaScriptExpression;
using Iql.JavaScript.IqlToJavaScriptExpression.Parsers;
using Iql.JavaScript.QueryableApplicator.Applicators;
using Iql.Queryable;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Operations;

namespace Iql.JavaScript.QueryableApplicator
{
    public class JavaScriptQueryableAdapter : QueryableAdapter<IJavaScriptQueryResult, JavaScriptQueryableAdapter>
    {
        public JavaScriptQueryableAdapter()
        {
            RegisterApplicator(() => new OrderByOperationApplicatorJavaScript());
            RegisterApplicator(() => new ReverseOperationApplicatorJavaScript());
            RegisterApplicator(() => new WhereOperationApplicatorJavaScript());
            RegisterApplicator(() => new IncludeCountOperationApplicatorJavaScript());
            RegisterApplicator(() => new ExpandOperationApplicatorJavaScript());
            RegisterApplicator(() => new WithKeyOperationApplicatorJavaScript());
            //new QueryParser().parseRoot(queryFilter, new QueryExpressionAdapterJavaScript())({})
        }

        // public generateQuery(): JavaScriptQuery<T> {
        //     throw new Error("Not implemented.");
        // }

        public override IJavaScriptQueryResult ToQueryResult<TEntity>(IQueryable<TEntity> queryable,
            IJavaScriptQueryResult data)
        {
            return data;
        }

        public override IJavaScriptQueryResult NewQueryData<TEntity>(IQueryable<TEntity> queryable)
        {
            return new JavaScriptQuery<TEntity>(queryable, Context);
        }

        public static JavaScriptExpression GetExpression(
            IExpressionQueryOperation operation,
            bool isFilter,
            EntityConfigurationBuilder entityConfigurationContext,
            string rootVariableName = null)
        {
            var adapter = new JavaScriptIqlExpressionAdapter();
            var parser = new JavaScriptIqlParserInstance(
                adapter);
            parser.IsFilter = isFilter;
            var javaScriptOutput = parser.Parse(operation.Expression
#if TypeScript
, operation.EvaluateContext
#endif
            );
            var expression = new JavaScriptExpression(parser.GetRootEntityName(null),
                javaScriptOutput.ToCodeString());
            return expression;
        }
    }
}