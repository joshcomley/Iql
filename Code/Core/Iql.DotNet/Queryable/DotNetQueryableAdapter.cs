using System;
using System.Linq.Expressions;
using Iql.DotNet.IqlToDotNet;
using Iql.DotNet.Queryable.Applicators;
using Iql.Queryable;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Operations;

namespace Iql.DotNet.Queryable
{
    public class DotNetQueryableAdapter : QueryableAdapter<IDotNetQueryResult, DotNetQueryableAdapter>
    {
        public DotNetQueryableAdapter()
        {
            RegisterApplicator(() => new OrderByOperationApplicatorDotNet());
            RegisterApplicator(() => new WithKeyOperationApplicatorDotNet());
            RegisterApplicator(() => new IncludeCountOperationApplicatorDotNet());
            RegisterApplicator(() => new WhereOperationApplicatorDotNet());
            RegisterApplicator(() => new TakeOperationApplicatorDotNet());
            //this.RegisterApplicator(() => new ReverseOperationApplicatorJavaScript());
            //this.RegisterApplicator(() => new WhereOperationApplicatorJavaScript());
            RegisterApplicator(() => new ExpandOperationApplicatorDotNet());
            //new QueryParser().parseRoot(queryFilter, new QueryExpressionAdapterJavaScript())({})
        }

        public override IDotNetQueryResult ToQueryResult<TEntity>(IQueryable<TEntity> queryable,
            IDotNetQueryResult data)
        {
            return data;
        }

        public override IDotNetQueryResult NewQueryData<TEntity>(IQueryable<TEntity> queryable)
        {
            return new DotNetQuery(Context, typeof(TEntity));
        }

        public static LambdaExpression GetExpression(
            IExpressionQueryOperation operation,
            bool isFilter,
            EntityConfigurationBuilder entityConfigurationContext,
            Type rootEntityType,
            string rootVariableName = null)
        {
            var adapter = new DotNetIqlExpressionAdapter(
                //"___" + 
                rootVariableName ?? "q"
            // + new Date().getTime()
            );
            var parser = new DotNetIqlParserInstance(
                adapter,
                rootEntityType);
            parser.IsFilter = isFilter;
            var expression = parser.Parse(operation.Expression);
            var lambda = Expression.Lambda(expression.Expression, parser.RootEntity);
            return lambda;
        }
    }
}