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
using Iql.Entities.Extensions;
using Iql.Entities.Permissions;
using Iql.Entities.Services;
using Iql.Extensions;
using Iql.Parsing.Types;

namespace Iql.Data.Evaluation
{
    public class PermissionsEvaluationSession : IEvaluationSessionContainer
    {
        public PermissionsResultsCachingKind ResultsCachingKind { get; set; } = PermissionsResultsCachingKind.EntityKey;
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
        private List<PermissionsEvaluationResult> Results { get; set; } = new List<PermissionsEvaluationResult>();
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

        public async Task<object> ResolveCurrentUserAsync(IDataContext db, object user)
        {
            var config = db.UsersManager.Definition.EntityConfiguration;
            CompositeKey preKey = null;
            if (user != null)
            {
                if (!Session.EnforceLatest)
                {
                    return user;
                }
                var state = db.GetEntityState(user, config.Type);
                if (state == null)
                {
                    var userDb = DataContextInternal.FindDataContextForEntity(user);
                    if (userDb != null)
                    {
                        state = userDb.GetEntityState(user);
                    }
                }

                if (state != null && !state.IsNew)
                {
                    preKey = config.GetCompositeKey(user);
                }
                else
                {
                    return user;
                }
            }
            else
            {
                var userId = await db.ServiceProvider.Resolve<IqlCurrentUserService>().ResolveCurrentUserIdAsync(db.ServiceProvider);
                preKey = config.GetCompositeKey(userId.Result);
            }

            object resolvedUser;
            var cachedUser = Session.GetCachedEntity(
                config,
                preKey
            );
            if (cachedUser == null || cachedUser.Exists == false)
            {
                resolvedUser = await db.GetDbSetByEntityType(db.UsersManager.Definition.EntityConfiguration.Type).GetWithKeyAsync(preKey);
                var key = db.UsersManager.Definition.EntityConfiguration.GetCompositeKey(resolvedUser);
                Session.SetCachedEntity(
                    config,
                    key,
                    resolvedUser);
            }
            else
            {
                resolvedUser = cachedUser.Entity;
            }
            return resolvedUser;
        }

        public async Task<IqlUserPermission> GetDbUserPermissionAsync(
            IDataContext db,
            IUserPermission permissionsContainer,
            object user = null,
            Type userType = null,
            object entity = null,
            Type entityType = null
        )
        {
            user = await ResolveCurrentUserAsync(db, user);

            if (userType == null || userType == typeof(object))
            {
                userType = user?.GetType();
            }

            if ((userType == null || userType == typeof(object)) && db.EntityConfigurationContext.UsersDefinition != null)
            {
                userType = db.EntityConfigurationContext.UsersDefinition.EntityConfiguration.Type;
            }

            var permission = await GetUserPermissionAsync(
                db.EntityConfigurationContext.PermissionManager,
                permissionsContainer,
                db,
                user,
                userType,
                entity,
                entityType,
                db,
                db.EntityConfigurationContext
            );
            return permission;
        }

        public async Task<IqlUserPermission> GetUserPermissionAsync(
            UserPermissionsManager permissionsManager,
            IUserPermission permissionsContainer,
            IIqlDataEvaluator evaluationContext,
            object user,
            Type userType,
            object entity = null,
            Type entityType = null,
            IServiceProviderProvider serviceProviderProvider = null,
            ITypeResolver typeResolver = null
        )
        {
            var inheritedPermission = IqlUserPermission.Unset;
            if (permissionsContainer != null && permissionsContainer.ParentPermissions != null)
            {
                inheritedPermission = await GetUserPermissionAsync(
                    permissionsManager,
                    permissionsContainer.ParentPermissions,
                    evaluationContext,
                    user,
                    userType,
                    entity,
                    entityType,
                    serviceProviderProvider,
                    typeResolver);
            }
            var permissionsCollection = permissionsContainer?.Permissions;
            if (permissionsCollection == null ||
                permissionsCollection.Keys == null ||
                permissionsCollection.Keys.Count == 0)
            {
                return inheritedPermission;
            }

            if (inheritedPermission.HasFlag(IqlUserPermission.None))
            {
                return inheritedPermission;
            }

            typeResolver = typeResolver ?? permissionsManager.EntityConfigurationBuilder;
            serviceProviderProvider = serviceProviderProvider ?? permissionsManager.EntityConfigurationBuilder;
            IqlUserPermission result = inheritedPermission;
            var allPermissionRules =
                permissionsManager.Container.PermissionRules;
            var permissionRules = new List<IqlUserPermissionRule>();
            for (var i = 0; i < allPermissionRules.Count; i++)
            {
                var rule = allPermissionRules[i];
                if (entityType == null && rule.AcceptsEntity)
                {
                    continue;
                }

                if (permissionsCollection.Keys.All(_ => _ != rule.Key))
                {
                    continue;
                }
                permissionRules.Add(rule);
            }

            var lastUpPrecedentIndex = -1;
            for (var i = permissionRules.Count - 1; i >= 0; i--)
            {
                if (permissionRules[i].Precedence == IqlUserPermissionRulePrecedenceDirection.Up)
                {
                    lastUpPrecedentIndex = i;
                    break;
                }
            }
            var lastDownPrecedentIndex = -1;
            for (var i = permissionRules.Count - 1; i >= 0; i--)
            {
                if (permissionRules[i].Precedence == IqlUserPermissionRulePrecedenceDirection.Down)
                {
                    lastDownPrecedentIndex = i;
                    break;
                }
            }
            for (var i = 0; i < permissionRules.Count; i++)
            {
                var rule = permissionRules[i];
                var hasUpPrecedenceRulesRemaining = lastUpPrecedentIndex > i;
                var hasDownPrecedenceRulesRemaining = lastDownPrecedentIndex > i;
                var task = (Task<IqlUserPermission>)EvaluateEntityPermissionsRuleCustomAsyncMethod.InvokeGeneric(
                    this,
                    new object[] { rule, user, entity, serviceProviderProvider, evaluationContext, typeResolver },
                    entityType ?? entity?.GetType() ?? typeof(object), userType ?? user.GetType());
                var evaluatedResult = await task;
                if (evaluatedResult == IqlUserPermission.None &&
                    rule.Precedence == IqlUserPermissionRulePrecedenceDirection.Down && 
                    !hasUpPrecedenceRulesRemaining)
                {
                    return evaluatedResult;
                }

                if (evaluatedResult == IqlUserPermission.Full &&
                    rule.Precedence == IqlUserPermissionRulePrecedenceDirection.Up && 
                    !hasDownPrecedenceRulesRemaining)
                {
                    return evaluatedResult;
                }
                if (result == IqlUserPermission.Unset && evaluatedResult != IqlUserPermission.Unset)
                {
                    result = evaluatedResult;
                }
                else if (result != IqlUserPermission.Unset &&
                         evaluatedResult != IqlUserPermission.Unset)
                {
                    if (rule.Precedence == IqlUserPermissionRulePrecedenceDirection.Up)
                    {
                        if (result.HasFlag(IqlUserPermission.Read))
                        {
                            evaluatedResult |= IqlUserPermission.Read;
                        }
                        if (result.HasFlag(IqlUserPermission.Update))
                        {
                            evaluatedResult |= IqlUserPermission.Update;
                        }
                        if (result.HasFlag(IqlUserPermission.Delete))
                        {
                            evaluatedResult |= IqlUserPermission.Delete;
                        }
                        if (result.HasFlag(IqlUserPermission.Create))
                        {
                            evaluatedResult |= IqlUserPermission.Create;
                        }
                        if (evaluatedResult == IqlUserPermission.Full && !hasDownPrecedenceRulesRemaining)
                        {
                            return evaluatedResult;
                        }
                    }
                    else
                    {
                        if (!result.HasFlag(IqlUserPermission.Read))
                        {
                            evaluatedResult &= ~IqlUserPermission.Read;
                        }
                        if (!result.HasFlag(IqlUserPermission.Update))
                        {
                            evaluatedResult &= ~IqlUserPermission.Update;
                        }
                        if (!result.HasFlag(IqlUserPermission.Delete))
                        {
                            evaluatedResult &= ~IqlUserPermission.Delete;
                        }
                        if (!result.HasFlag(IqlUserPermission.Create))
                        {
                            evaluatedResult &= ~IqlUserPermission.Create;
                        }
                        if (evaluatedResult == IqlUserPermission.None && !hasUpPrecedenceRulesRemaining)
                        {
                            return evaluatedResult;
                        }
                    }

                    if (evaluatedResult != IqlUserPermission.Unset)
                    {
                        result = evaluatedResult;
                    }
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
                    new object[] { rule, user, entity, serviceProviderProvider, evaluator, typeResolver },
                    entity?.GetType() ?? typeof(object), user.GetType());
                return await task;
            }

            if (!Session.EnforceLatest && ResultsCachingKind != PermissionsResultsCachingKind.None)
            {
                switch (ResultsCachingKind)
                {
                    case PermissionsResultsCachingKind.EntityKey:
                    case PermissionsResultsCachingKind.EntityHash:
                        {
                            var entityKey = GetEntityKey(entity, typeof(TEntity), typeResolver, ResultsCachingKind);
                            var userKey = GetEntityKey(user, typeof(TUser), typeResolver, ResultsCachingKind);
                            var cached = Results.SingleOrDefault(_ =>
                                _.EntityKey == entityKey &&
                                _.UserKey == userKey &&
                                _.Rule == rule);
                            if (cached != null)
                            {
                                return cached.Permission;
                            }
                            break;
                        }
                    case PermissionsResultsCachingKind.Entity:
                        {
                            var cached = Results.SingleOrDefault(_ =>
                                _.Entity == entity &&
                                _.User == user &&
                                _.Rule == rule);
                            if (cached != null)
                            {
                                return cached.Permission;
                            }
                            break;
                        }

                }
            }
            var isEntityNew = evaluator.EntityStatus(entity) != IqlEntityStatus.Existing;
            var context =
                entity == null
                ? new IqlUserPermissionContext<TUser>(user)
                : new IqlEntityUserPermissionContext<TEntity, TUser>(isEntityNew, user, entity);
            var contextExpectedFullName =
                entity == null
                ? $"{nameof(IqlUserPermissionContext<TUser>)}<{typeof(TUser).Name}>"
                : $"{nameof(IqlEntityUserPermissionContext<TEntity, TUser>)}<{nameof(TEntity)}, {typeof(TUser).Name}>";
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
                            entity == null
                                ? typeof(IqlUserPermissionContext<TUser>)
                                : typeof(IqlEntityUserPermissionContext<TEntity, TUser>),
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
                entity == null ? typeof(IqlUserPermissionContext<TUser>) : typeof(IqlEntityUserPermissionContext<TEntity, TUser>));
            var permission = (IqlUserPermission)Convert.ToInt32(result.Result);
            var currentEntityKey = GetEntityKey(entity, typeof(TEntity), typeResolver, ResultsCachingKind);
            var currentUserKey = GetEntityKey(user, typeof(TUser), typeResolver, ResultsCachingKind);
            switch (ResultsCachingKind)
            {
                case PermissionsResultsCachingKind.EntityKey:
                case PermissionsResultsCachingKind.EntityHash:
                    {
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
                        break;
                    }
                case PermissionsResultsCachingKind.Entity:
                    {
                        var cachedResult = Results.SingleOrDefault(_ => _.Entity == entity && _.User == user && _.Rule == rule);
                        if (cachedResult == null)
                        {
                            cachedResult = new PermissionsEvaluationResult(
                                rule,
                                entity,
                                "",
                                user,
                                "",
                                permission);
                            Results.Add(cachedResult);
                        }
                        cachedResult.EntityKey = "";
                        cachedResult.UserKey = "";
                        break;
                    }
            }
            return permission;
        }

        public void ClearEntitiesResultsCache(ITypeResolver typeResolver, Type entityType, params object[] entities)
        {
            if (entities == null || entities.Length == 0)
            {
                Results = Results.Where(_ => _.Entity == null).ToList();
            }
            else
            {
                var keys = entities.Select(_ => GetEntityKey(_, entityType, typeResolver, ResultsCachingKind)).Where(_ => !string.IsNullOrWhiteSpace(_)).ToArray();
                Results = Results.Where(_ => !entities.Contains(_.Entity) && !keys.Contains(_.EntityKey)).ToList();
            }
        }

        public void ClearUsersResultsCache(ITypeResolver typeResolver, Type userType, params object[] users)
        {
            if (users == null || users.Length == 0)
            {
                Results = Results.Where(_ => _.User == null).ToList();
            }
            else
            {
                var keys = users.Select(_ => GetEntityKey(_, userType, typeResolver, ResultsCachingKind)).Where(_ => !string.IsNullOrWhiteSpace(_)).ToArray();
                Results = Results.Where(_ => !users.Contains(_.User) && !keys.Contains(_.UserKey)).ToList();
            }
        }

        public void ClearResultsCache()
        {
            Results = new List<PermissionsEvaluationResult>();
        }

        private static Dictionary<object, string> _tempIds = new Dictionary<object, string>();
        private string GetEntityKey(object entity, Type entityType, ITypeResolver typeResolver, PermissionsResultsCachingKind cachingKind)
        {
            if (entity == null)
            {
                return "";
            }
            IEntityConfigurationBuilder builder = typeResolver as IEntityConfigurationBuilder;
            switch (cachingKind)
            {
                case PermissionsResultsCachingKind.EntityHash:
                    {
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

                        var entityConfiguration = builder.GetEntityByType(entityType);
                        var all = entityConfiguration.Builder.FlattenObjectGraph(entity, entityType);
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
                case PermissionsResultsCachingKind.EntityKey:
                    if (builder != null)
                    {
                        var compositeKey = builder.GetEntityByType(entityType).GetCompositeKey(entity);
                        if (compositeKey.HasDefaultValue())
                        {
                            if (_tempIds.ContainsKey(entity))
                            {
                                return _tempIds[entity];
                            }

                            var key = Guid.NewGuid().ToString();
                            _tempIds.Add(entity, key);
                            return key;
                        }
                        return compositeKey.AsLegacyKeyString(true);
                    }
                    break;
            }

            return "";
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