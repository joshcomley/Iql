using System.Linq.Expressions;
using Iql.DotNet.IqlToDotNet;
using Iql.DotNet.Queryable.Applicators;
using Iql.Parsing;
using Iql.Queryable;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Operations;

namespace Iql.DotNet.Queryable
{
    public class DotNetQueryableAdapter : QueryableAdapter<IDotNetQueryResult>
    {
        public DotNetQueryableAdapter()
        {
            RegisterApplicator(() => new OrderByOperationApplicatorDotNet());
            RegisterApplicator(() => new WithKeyOperationApplicatorDotNet());
            RegisterApplicator(() => new IncludeCountOperationApplicatorDotNet());
            RegisterApplicator(() => new WhereOperationApplicatorDotNet());
            //this.RegisterApplicator(() => new ReverseOperationApplicatorJavaScript());
            //this.RegisterApplicator(() => new WhereOperationApplicatorJavaScript());
            //this.RegisterApplicator(() => new ExpandOperationApplicatorJavaScript());
            //new QueryParser().parseRoot(queryFilter, new QueryExpressionAdapterJavaScript())({})
        }

        public override IDotNetQueryResult ToQueryResult<TEntity>(IQueryable<TEntity> queryable,
            IDotNetQueryResult data)
        {
            return data;
        }

        public override IDotNetQueryResult NewQueryData<TEntity>(IQueryable<TEntity> queryable)
        {
            return new DotNetQuery<TEntity>(Context);
        }

        public static Expression GetExpression(
            IExpressionQueryOperation operation,
            bool isFilter,
            EntityConfigurationBuilder entityConfigurationContext,
            string rootVariableName = null)
        {
            var adapter = new DotNetIqlExpressionAdapter(
                //"___" + 
                rootVariableName ?? "q"
            // + new Date().getTime()
            );
            var parser = new ActionParserInstance<DotNetIqlData, DotNetIqlExpressionAdapter>(
                adapter);
            parser.IsFilter = isFilter;
            var expression = parser.Parse(operation.Expression);
            return null;
        }
    }
}