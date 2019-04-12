using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Entities;
using Iql.Entities.Services;
using Iql.Parsing.Evaluation;
using Iql.Parsing.Types;

namespace Iql.Data.Evaluation
{
    public interface IEvaluationSession : IEvaluationSessionContainer
    {
        GetCachedEntityResult GetCachedEntity(IEntityConfiguration entityConfiguration, object compositeKeyOrEntity);
        void SetCachedEntity(IEntityConfiguration entityConfiguration, CompositeKey compositeKey, object entity);
        Task<IqlObjectEvaluationResult> EvaluateExpressionAsync<T>(
            Expression<Func<T, object>> expression,
            T entity,
            ITypeResolver typeResolver,
            IServiceProviderProvider serviceProviderProvider)
            where T : class;

        Task<IqlPropertyPathEvaluationResult> EvaluateAsync(
            IqlPropertyPath propertyPath,
            object entity,
            IDataContext dataContext,
            bool populate);

        Task<IqlPropertyPathEvaluationResult> EvaluateCustomAsync(
            IqlPropertyPath propertyPath,
            object entity,
            IIqlDataEvaluator dataEvaluator,
            bool populate,
            Dictionary<object, object> replacements = null);

        Task<T> EvaluateAsAsync<T>(IqlPropertyPath propertyPath, object entity, IDataContext dataContext, bool populate);

        Task<IqlObjectEvaluationResult> EvaluateLambdaAsync(
            LambdaExpression expression,
            object entity,
            ITypeResolver typeResolver,
            IServiceProviderProvider serviceProviderProvider,
            Type entityType = null
        );

        Task<IqlObjectEvaluationResult> EvaluateExpressionWithDbAsync<T>(
            Expression<Func<T, object>> expression,
            T entity,
            IDataContext dataContext)
            where T : class;

        Task<IqlObjectEvaluationResult> EvaluateLambdaWithDbAsync(
            LambdaExpression expression,
            object entity,
            IDataContext dataContext,
            Type entityType = null);

        Task<IqlExpressonEvaluationResult> EvaluateIqlAsync(
            IqlExpression expression,
            object entity,
            IDataContext dataContext,
            Type contextType = null,
            ITypeResolver typeResolver = null);

        Task<IqlExpressonEvaluationResult> EvaluateIqlPathAsync(
            IqlExpression expression,
            object context,
            IDataContext dataContext,
            Type contextType,
            ITypeResolver typeResolver = null,
            bool populatePath = false
        );

        Task<IqlExpressonEvaluationResult> EvaluateIqlCustomAsync(
            IqlExpression expression,
            IServiceProviderProvider serviceProviderProvider,
            object context,
            IIqlDataEvaluator dataEvaluator,
            ITypeResolver typeResolver,
            Type contextType = null,
            bool populatePath = false,
            bool enforceLatest = false
        );
    }
}