using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Entities;
using Iql.Entities.Permissions;
using Iql.Entities.Services;
using Iql.Extensions;
using Iql.Parsing.Types;

namespace Iql.Data.Evaluation
{
    public class PermissionsEvaluationSession
    {
        public PermissionsEvaluationSession()
        {
            this.EvaluationSession = new EvaluationSession();
        }

        public EvaluationSession EvaluationSession { get; set; }

        public Task<IqlUserPermission> EvaluateEntityPermissionsRuleAsync<TEntity, TUser>(
            IqlUserPermissionRule rule,
            TUser user,
            TEntity entity,
            IDataContext dataContext)
            where TEntity : class
            where TUser : class
        {
            return EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                entity,
                dataContext,
                dataContext,
                dataContext.EntityConfigurationContext);
        }

        public async Task<IqlUserPermission> GetUserPermissionAsync(
            UserPermissionsManager permissionsManager,
            IIqlDataEvaluator evaluationContext,
            object user,
            object entity = null,
            IServiceProviderProvider serviceProviderProvider = null,
            ITypeResolver typeResolver = null
        )
        {
            typeResolver = typeResolver ?? permissionsManager.EntityConfigurationBuilder;
            serviceProviderProvider = serviceProviderProvider ?? permissionsManager.EntityConfigurationBuilder;
            IqlUserPermission result = IqlUserPermission.Unset;
            for (var i = 0; i < permissionsManager.Container.PermissionRules.Count; i++)
            {
                var rule = permissionsManager.Container.PermissionRules[i];
                var evaluatedResult = await EvaluateEntityPermissionsRuleCustomAsync(
                    rule,
                    user,
                    entity,
                    serviceProviderProvider, evaluationContext,
                    typeResolver);
                if (evaluatedResult == IqlUserPermission.None)
                {
                    return evaluatedResult;
                }

                if (result == IqlUserPermission.Unset && evaluatedResult != IqlUserPermission.Unset)
                {
                    result = evaluatedResult;
                }
                else if (result != IqlUserPermission.Unset &&
                         evaluatedResult != IqlUserPermission.Unset &&
                         (int)evaluatedResult < (int)result)
                {
                    result = evaluatedResult;
                }
            }
            // TODO: Evaluate the permissions
            return result;
        }

        public async Task<IqlUserPermission> EvaluateEntityPermissionsRuleCustomAsync<TEntity, TUser>(
            IqlUserPermissionRule rule,
            TUser user,
            TEntity entity,
            IServiceProviderProvider serviceProviderProvider,
            IIqlDataEvaluator evaluator,
            ITypeResolver typeResolver)
            where TEntity : class
            where TUser : class
        {
            var isEntityNew = evaluator.EntityStatus(entity) != IqlEntityStatus.Existing;
            var context = new IqlEntityUserPermissionContext<TEntity, TUser>(isEntityNew, user, entity);
            var iqlExpression = rule.IqlExpression.Clone();
            var contextExpessions = iqlExpression.Flatten()
                .Where(_ => _.Expression.Kind == IqlExpressionKind.Variable ||
                            _.Expression.Kind == IqlExpressionKind.RootReference)
                .ToArray();
            for (var i = 0; i < contextExpessions.Length; i++)
            {
                var exp = contextExpessions[i];
                var variableExpression = exp.Expression as IqlVariableExpression;
                if (variableExpression != null && variableExpression.EntityTypeName ==
                    $"{nameof(IqlEntityUserPermissionContext<TEntity, TUser>)}<{nameof(TEntity)}, {typeof(TUser).Name}>")
                {
                    variableExpression.EntityTypeName =
                        context.GetType().GetFullName();
                }
            }

            var topLevelPropertyExpressions = iqlExpression.TopLevelPropertyExpressions();
            for (var i = 0; i < topLevelPropertyExpressions.Length; i++)
            {
                var flattenedExpression = topLevelPropertyExpressions[i];
                var rootExpression = flattenedExpression.Expression.RootExpression();
                if (rootExpression.Kind == IqlExpressionKind.Variable || rootExpression.Kind == IqlExpressionKind.RootReference)
                {
                    var variableExpression = rootExpression as IqlVariableExpression;
                    if (variableExpression.EntityTypeName == context.GetType().GetFullName())
                    {
                        // Construct fake lambda
                        var evaluatedResult = await EvaluationSession.EvaluateIqlCustomAsync(
                            flattenedExpression.Expression,
                            serviceProviderProvider,
                            context,
                            evaluator,
                            typeResolver,
                            typeof(IqlEntityUserPermissionContext<TEntity, TUser>),
                            false,
                            true);
                        if (evaluatedResult.Success)
                        {
                            iqlExpression.ReplaceExpression(
                                flattenedExpression.Expression,
                                new IqlLiteralExpression(evaluatedResult.Result)
                            );
                        }
                    }
                }
            }

            var queries = iqlExpression.Flatten()
                .Where(_ => _.Expression.Kind == IqlExpressionKind.DataSetQuery).Select(_ => _.Expression).ToArray();
            for (var i = 0; i < queries.Length; i++)
            {
                var query = queries[i] as IqlDataSetQueryExpression;
                object evaluatedValue = null;
                switch (query.Key)
                {
                    case nameof(IqlUserPermissionContext<object>.QueryAny):
                        evaluatedValue = await evaluator.QueryAnyAsync(query, typeResolver
#if TypeScript
                            , null
#endif
                        );
                        break;
                    case nameof(IqlUserPermissionContext<object>.QueryAll):
                        evaluatedValue = await evaluator.QueryAllAsync(query, typeResolver
#if TypeScript
                            , null
#endif
                        );
                        break;
                    case nameof(IqlUserPermissionContext<object>.QueryCount):
                        evaluatedValue = await evaluator.QueryCountAsync(query, typeResolver
#if TypeScript
                            , null
#endif
                        );
                        break;
                }
                iqlExpression.ReplaceExpression(query, new IqlLiteralExpression(evaluatedValue));
                //var lambda =
                //    IqlConverter.Instance.ConvertIqlToLambdaExpression(query, typeResolver);
            }
            var result = await EvaluationSession.EvaluateIqlCustomAsync(
                iqlExpression,
                serviceProviderProvider,
                context,
                evaluator,
                typeResolver,
                typeof(IqlEntityUserPermissionContext<TEntity, TUser>));
            var permission = (IqlUserPermission)result.Result;
            return permission;



            //var isEntityNew = evaluator.EntityStatus(entity) != IqlEntityStatus.Existing;
            //var context = new IqlEntityUserPermissionContext<TEntity, TUser>(isEntityNew, user, entity);
            //var result = await rule.IqlExpression.EvaluateIqlCustomAsync(
            //    serviceProviderProvider,
            //    context,
            //    evaluator,
            //    typeResolver,
            //    typeof(IqlEntityUserPermissionContext<TEntity, TUser>));
            //var permission = (IqlUserPermission)result.Result;
            //return permission;
        }

        public Task<IqlUserPermission> EvaluatePermissionsRuleAsync<TEntity, TUser>(IqlUserPermissionRule rule, TUser user, TEntity entity, IDataContext dataContext)
            where TEntity : class
            where TUser : class
        {
            return EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                entity,
                dataContext,
                dataContext,
                dataContext.EntityConfigurationContext);
        }

        public async Task<IqlUserPermission> EvaluatePermissionsRuleCustomAsync<TEntity, TUser>(
            IqlUserPermissionRule rule,
            TUser user,
            IServiceProviderProvider serviceProviderProvider,
            IIqlDataEvaluator evaluator,
            ITypeResolver typeResolver)
            where TEntity : class
            where TUser : class
        {
            var context = new IqlUserPermissionContext<TUser>(user);
            var result = await EvaluationSession.EvaluateIqlCustomAsync(
                rule.IqlExpression,
                serviceProviderProvider,
                context,
                evaluator,
                typeResolver,
                typeof(IqlUserPermissionContext<TUser>));
            var permission = (IqlUserPermission)result.Result;
            return permission;
        }

    }
}