using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Serialization;
using Iql.Data.Types;
using Iql.Entities;
using Iql.Entities.Permissions;
using Iql.Entities.Services;
using Iql.Extensions;
using Iql.Parsing.Types;

namespace Iql.Data.Evaluation
{
    public class PermissionsEvaluationSession : IEvaluationSessionContainer
    {
        static PermissionsEvaluationSession()
        {
            EvaluateEntityPermissionsRuleCustomAsyncMethod = typeof(PermissionsEvaluationSession)
                .GetMethod(nameof(EvaluateEntityPermissionsRuleCustomAsync),
                    BindingFlags.Instance | BindingFlags.Public);
        }

        public static MethodInfo EvaluateEntityPermissionsRuleCustomAsyncMethod { get; set; }

        class PermissionsEvaluationResult
        {
            public IqlUserPermissionRule Rule { get; }
            public object Entity { get; }
            public string EntityKey { get; set; }
            public object User { get; }
            public string UserKey { get; set; }
            public IqlUserPermission Permission { get; }

            public PermissionsEvaluationResult(IqlUserPermissionRule rule, object entity, string entityKey, object user, string userKey, IqlUserPermission permission)
            {
                Rule = rule;
                Entity = entity;
                EntityKey = entityKey;
                User = user;
                UserKey = userKey;
                Permission = permission;
            }
        }
        private List<PermissionsEvaluationResult> Results { get; } = new List<PermissionsEvaluationResult>();
        public PermissionsEvaluationSession(
            bool enforceLatest = false, 
            EvaluationCacheMode cacheMode = EvaluationCacheMode.All,
            IEvaluationSessionContainer evaluationSession = null)
        {
            Session = evaluationSession?.Session ?? new EvaluationSession(enforceLatest, cacheMode);
            Session.EnforceLatest = enforceLatest;
            Session.CacheMode = cacheMode;
        }

        public IEvaluationSession Session { get; set; }

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
            UserPermissionsCollection permissionsCollection,
            IIqlDataEvaluator evaluationContext,
            object user,
            Type userType,
            object entity = null,
            Type entityType = null,
            IServiceProviderProvider serviceProviderProvider = null,
            ITypeResolver typeResolver = null
        )
        {
            if (permissionsCollection == null || permissionsCollection.Keys == null || permissionsCollection.Keys.Count == 0)
            {
                return IqlUserPermission.Unset;
            }
            typeResolver = typeResolver ?? permissionsManager.EntityConfigurationBuilder;
            serviceProviderProvider = serviceProviderProvider ?? permissionsManager.EntityConfigurationBuilder;
            IqlUserPermission result = IqlUserPermission.Unset;
            for (var i = 0; i < permissionsManager.Container.PermissionRules.Count; i++)
            {
                var rule = permissionsManager.Container.PermissionRules[i];
                if (permissionsCollection.Keys.All(_ => _ != rule.Key))
                {
                    continue;
                }
                var task = (Task<IqlUserPermission>)EvaluateEntityPermissionsRuleCustomAsyncMethod.InvokeGeneric(
                    this,
                    new object[] { rule, user, entity, serviceProviderProvider, evaluationContext, typeResolver },
                    entityType ?? entity?.GetType() ?? typeof(object), userType ?? user.GetType());
                var evaluatedResult = await task;
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
            var userType = typeof(TUser);
            if (user != null && userType == typeof(object) && user.GetType() != typeof(object))
            {
                var task = (Task<IqlUserPermission>)EvaluateEntityPermissionsRuleCustomAsyncMethod.InvokeGeneric(
                    this,
                    new object[] {rule, user, entity, serviceProviderProvider, evaluator, typeResolver}, 
                    entity?.GetType() ?? typeof(object), user.GetType());
                return await task;
            }

            if (!Session.EnforceLatest)
            {
                var entityKey = GetEntityKey(entity, typeResolver, evaluator);
                var userKey = GetEntityKey(user, typeResolver, evaluator);
                var cached = Results.SingleOrDefault(_ => _.EntityKey == entityKey && _.UserKey == userKey && _.Rule == rule);
                if (cached != null)
                {
                    return cached.Permission;
                }
            }
            var isEntityNew = evaluator.EntityStatus(entity) != IqlEntityStatus.Existing;
            var context = new IqlEntityUserPermissionContext<TEntity, TUser>(isEntityNew, user, entity);
            var contextExpectedFullName = $"{nameof(IqlEntityUserPermissionContext<TEntity, TUser>)}<{nameof(TEntity)}, {typeof(TUser).Name}>";
            var iqlExpression = rule.IqlExpression.Clone();
            var contextExpressions = iqlExpression.Flatten()
                .Where(_ => _.Expression.Kind == IqlExpressionKind.Variable ||
                            _.Expression.Kind == IqlExpressionKind.RootReference)
                .ToArray();
            for (var i = 0; i < contextExpressions.Length; i++)
            {
                var exp = contextExpressions[i];
                var variableExpression = exp.Expression as IqlVariableExpression;
                if (variableExpression != null && variableExpression.EntityTypeName ==
                    contextExpectedFullName)
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
                        var evaluatedResult = await Session.EvaluateIqlCustomAsync(
                            flattenedExpression.Expression,
                            serviceProviderProvider,
                            context,
                            evaluator,
                            typeResolver,
                            typeof(IqlEntityUserPermissionContext<TEntity, TUser>),
                            false);
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
            var result = await Session.EvaluateIqlCustomAsync(
                iqlExpression,
                serviceProviderProvider,
                context,
                evaluator,
                typeResolver,
                typeof(IqlEntityUserPermissionContext<TEntity, TUser>));
            var permission = (IqlUserPermission)result.Result;
            var currentEntityKey = GetEntityKey(entity, typeResolver, evaluator);
            var currentUserKey = GetEntityKey(user, typeResolver, evaluator);
            var cachedResult = Results.SingleOrDefault(_ => _.Entity == entity && _.User == user && _.Rule == rule);
            if (cachedResult == null)
            {
                cachedResult = new PermissionsEvaluationResult(
                    rule,
                    entity,
                    currentEntityKey,
                    user,
                    currentUserKey,
                    permission);
                Results.Add(cachedResult);
            }
            cachedResult.EntityKey = currentEntityKey;
            cachedResult.UserKey = currentUserKey;
            return permission;
        }

        private string GetEntityKey<TEntity>(TEntity entity, ITypeResolver typeResolver,
            IIqlDataEvaluator evaluator) where TEntity : class
        {
            IEntityConfigurationBuilder builder = typeResolver as IEntityConfigurationBuilder;
            if (builder == null)
            {
                if (!(typeResolver is EntityConfigurationTypeResolver))
                {
                    return Guid.NewGuid().ToString();
                }
                else
                {
                    builder = (typeResolver as EntityConfigurationTypeResolver).Builder;
                }
            }

            var entityConfiguration = builder.EntityType<TEntity>();
            var all = entityConfiguration.Builder.FlattenObjectGraph(entity, typeof(TEntity));
            var sb = new StringBuilder();
            foreach (var item in all)
            {
                var config = entityConfiguration.Builder.GetEntityByType(item.Key);
                for (var i = 0; i < item.Value.Count; i++)
                {
                    var itemEntity = item.Value[i];
                    var json = JsonDataSerializer.SerializeEntityToJson(
                        itemEntity, 
                        config);
                    sb.Append($"{Md5.Hash(json)}:");
                }
            }
            return Md5.Hash(sb.ToString());
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
            var result = await Session.EvaluateIqlCustomAsync(
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