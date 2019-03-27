using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Conversion;
using Iql.Entities.Permissions;
using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Entities
{
    public class IqlUserPermissionRule
    {
        static IqlUserPermissionRule()
        {
            ConvertEntityIqlToLambdaMethod = typeof(IqlUserPermissionRule).GetMethod(nameof(ConvertEntityIqlToLambda),
                BindingFlags.Static | BindingFlags.NonPublic);
            ConvertIqlToLambdaMethod = typeof(IqlUserPermissionRule).GetMethod(nameof(ConvertIqlToLambda),
                BindingFlags.Static | BindingFlags.NonPublic);
            RunGenericMethod = typeof(IqlUserPermissionRule).GetMethod(nameof(RunGeneric),
                BindingFlags.Instance | BindingFlags.Public);
            RunGenericWithEntityMethod = typeof(IqlUserPermissionRule).GetMethod(nameof(RunGenericWithEntity),
                BindingFlags.Instance | BindingFlags.Public);
        }

        public static MethodInfo RunGenericWithEntityMethod { get; set; }

        public static MethodInfo RunGenericMethod { get; set; }

        public static MethodInfo ConvertIqlToLambdaMethod { get; set; }

        public static MethodInfo ConvertEntityIqlToLambdaMethod { get; set; }

        private string _iqlConvertedExpressionJson;
        private LambdaExpression _iqlConvertedExpression;
        public IEntityConfigurationBuilder EntityConfigurationBuilder { get; set; }
        public string Key { get; set; }
        public IqlLambdaExpression IqlExpression { get; set; }
        public string UserTypeName { get; set; }
        public string EntityTypeName { get; set; }

        public LambdaExpression Expression
        {
            get
            {
                if (IqlExpression == null)
                {
                    return null;
                }

                var json = JsonConvert.SerializeObject(IqlExpression);
                if (_iqlConvertedExpressionJson == null || json != _iqlConvertedExpressionJson)
                {
                    _iqlConvertedExpressionJson = json;
                    if (AcceptsEntity)
                    {
                        _iqlConvertedExpression = (LambdaExpression) ConvertEntityIqlToLambdaMethod.InvokeGeneric(
                            null,
                            new object[] {IqlExpression},
                            EntityType.Type,
                            UserType.Type
                        );
                    }
                    else
                    {
                        _iqlConvertedExpression = (LambdaExpression)ConvertIqlToLambdaMethod.InvokeGeneric(
                            null,
                            new object[] { IqlExpression },
                            UserType.Type
                        );
                    }
                }

                return _iqlConvertedExpression;
            }
        }

        private static LambdaExpression ConvertEntityIqlToLambda<TEntity, TUser>(IqlExpression expression)
            where TEntity : class
            where TUser : class
        {
            return IqlConverter.Instance.ConvertIqlToExpression<IqlEntityUserPermissionContext<TEntity, TUser>>(expression);
        }

        private static LambdaExpression ConvertIqlToLambda<TUser>(IqlExpression expression)
            where TUser : class
        {
            return IqlConverter.Instance.ConvertIqlToExpression<IqlUserPermissionContext<TUser>>(expression);
        }

        public IEntityConfiguration UserType => EntityConfigurationBuilder.GetEntityByTypeName(UserTypeName);
        public IEntityConfiguration EntityType => EntityConfigurationBuilder.GetEntityByTypeName(EntityTypeName);

        public bool AcceptsEntity => EntityTypeName != null;

        public IqlUserPermission Run(object user, object entity = null)
        {
            if (AcceptsEntity)
            {
                if (entity == null)
                {
                    throw new ArgumentException($"Parameter '{nameof(entity)}' must have a value if the permission rule accepts an entity.");
                }
                return (IqlUserPermission)RunGenericWithEntityMethod.InvokeGeneric(this, new object[]{ user, entity }, entity.GetType(), user.GetType());
            }
            return (IqlUserPermission)RunGenericMethod.InvokeGeneric(this, new object[] { user }, user.GetType());
        }

        public IqlUserPermission RunGenericWithEntity<TEntity, TUser>(TUser user, TEntity entity)
            where TEntity : class
            where TUser : class
        {
            var lambda = Expression.Compile();
            var result =
                lambda.DynamicInvoke(new IqlEntityUserPermissionContext<TEntity, TUser>(true, null, user, entity));
            return (IqlUserPermission)result;
        }

        public IqlUserPermission RunGeneric<TUser>(TUser user)
            where TUser : class
        {
            var lambda = Expression.Compile();
            var result =
                lambda.DynamicInvoke(new IqlUserPermissionContext<TUser>(true, null, user));
            return (IqlUserPermission)result;
        }

        public IqlUserPermissionRule(IEntityConfigurationBuilder entityConfigurationBuilder = null, string key = null, IqlLambdaExpression iqlExpression = null, string userTypeName = null, string entityTypeName = null)
        {
            EntityConfigurationBuilder = entityConfigurationBuilder;
            Key = key;
            IqlExpression = iqlExpression;
            UserTypeName = userTypeName;
            EntityTypeName = entityTypeName;
        }

        public IqlUserPermissionRule()
        {
            
        }
    }
}