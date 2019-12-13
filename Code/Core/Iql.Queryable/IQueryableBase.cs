using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Entities;
using Iql.Parsing;
using Iql.Parsing.Expressions.QueryExpressions;

namespace Iql.Queryable
{
    public interface IQueryableBase
    {
        Type MappedFrom { get; set; }
        bool? AllowOffline { get; set; }
        bool? TrackEntities { get; set; }
        bool HasDefaults { get; set; }
        EvaluateContext EvaluateContext { get; }
        Type ItemType { get; }
        List<IQueryOperation> Operations { get; }
        IQueryableBase Copy();
        IQueryableBase New();
        IQueryableBase Skip(int skip);
        IQueryableBase Take(int take);
        IQueryableBase Reverse();
        IQueryableBase Then(IQueryOperation operation);
        Task<IEnumerable> ToListAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<IEnumerable> AllPagesToListAsync(ProgressNotifier progressNotifier = null,
            LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );
        Task<object> SingleAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<object> SingleOrDefaultAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<object> FirstAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<object> FirstOrDefaultAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<object> LastAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
        Task<object> LastOrDefaultAsync(LambdaExpression expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<IQueryableBase> ApplyRelationshipFiltersAsync(IProperty relatedProperty, object entity);

        IQueryableBase Where(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<bool> AllAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<bool> AnyAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<long> CountAsync(LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<bool> AllQueryAsync(IqlLambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<bool> AnyQueryAsync(IqlLambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<long> CountQueryAsync(IqlLambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        IQueryableBase WherePropertyEquals(string propertyName, object value
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        IQueryableBase WhereEquals(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        IQueryableBase WhereQuery(QueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        IQueryableBase OrderByProperty(string propertyName, bool? descending = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        IQueryableBase OrderByDefault(bool? descending = null, IqlDefaultOrderKind? orderKind = null);

//        IQueryableBase ExpandProperty(string propertyName
        //#if TypeScript
        //            , EvaluateContext evaluateContext = null
        //#endif
        //        );

        IqlPropertyExpression PropertyExpression(string propertyName, string rootReferenceName = null);

        Task<IqlDataSetQueryExpression> ToIqlAsync(IExpressionToIqlConverter expressionConverter = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
    }
}