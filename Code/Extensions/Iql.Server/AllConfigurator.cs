using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Brandless.Data.Contracts;
using Brandless.Data.Entities;
using Iql.Data.Contracts;
using Iql.Entities;
using Iql.Entities.InferredValues;
using Iql.Forms;
using Microsoft.AspNetCore.Identity;

namespace Iql.Server
{
    public abstract class AllConfigurator<TService> : IIqlEntitySetConfigurator
    {
        public virtual void Configure(IEntityConfigurationBuilder builder)
        {
            var entityTypes = typeof(TService)
                .GetProperties()
                .Select(p => p.PropertyType.GetGenericArguments()[0]);
            var allMethods = GetType()
                .GetRuntimeMethods()
                .Where(m => m.GetCustomAttribute<ConfigureEntityAttribute>() != null)
                .ToList();
            foreach (var entityType in entityTypes)
            {
                var methodInfos = new List<MethodInfo>();
                foreach (var method in allMethods)
                {
                    var typeArguments = method.GetGenericArguments();
                    if (typeArguments.Length > 0)
                    {
                        var typeArgument = typeArguments[0];
                        var constraints = typeArgument.GetGenericParameterConstraints();
                        if (constraints.Length == 1)
                        {
                            var constraint = constraints[0];
                            var wasGenericType = constraint.IsGenericType;
                            if (constraint.IsGenericType)
                            {
                                constraint = constraint.GetGenericTypeDefinition();
                            }

                            entityType.TryGetBaseType(constraint, type =>
                            {
                                var typeArguments2 = new List<Type>();
                                typeArguments2.Add(entityType);
                                if (wasGenericType)
                                {
                                    typeArguments2.Add(type.Type.GetGenericArguments()[0]);
                                }

                                methodInfos.Add(method.MakeGenericMethod(typeArguments2.ToArray()));
                            });
                        }
                    }
                }

                foreach (var methodInfo in methodInfos)
                {
                    methodInfo.Invoke(this, new object[] { builder });
                }

                var entityConfiguration = builder.GetEntityByType(entityType);
                foreach (var property in entityConfiguration.Properties)
                {
                    if (property.GeographicPoint != null || property.NestedSet != null)
                    {
                        property.SetReadOnly();
                    }
                }

                if (entityConfiguration.PersistenceKeyProperty != null)
                {
                    ConfigurePersistenceKeyBase(entityConfiguration.PersistenceKeyProperty);
                }
            }
        }

        [ConfigureEntity]
        public void ConfigureIHasDescription<T>(IEntityConfigurationBuilder builder)
            where T : class, IHasDescription
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.Description, p => p.SetHint(FormHints.BigText));
            });
        }

        [ConfigureEntity]
        public void ConfigureIdentityUser<T>(IEntityConfigurationBuilder builder)
            where T : IdentityUser
        {
            builder.EntityType<T>().Configure(config =>
            {
                foreach (var prop in new[]
                {
                    config.FindProperty(nameof(IdentityUser.EmailConfirmed)),
                    config.FindProperty(nameof(IdentityUser.PhoneNumberConfirmed)),
                    config.FindProperty(nameof(IdentityUser.TwoFactorEnabled))
                })
                {
                    if (prop != null)
                    {
                        prop.EditKind = IqlPropertyEditKind.Display;
                    }
                }
                config.ConfigureProperty(_ => _.Email, p =>
                {
                    p.Nullable = false;
                    p.HasHint(FormHints.EmailAddress);
                });
                config.ConfigureProperty(_ => _.PhoneNumber, p =>
                {
                    p.HasHint(FormHints.PhoneNumber);
                });
            });
        }

        [ConfigureEntity]
        public void ConfigureIHasPersistenceKey<T>(IEntityConfigurationBuilder builder)
            where T : class, IHasPersistenceKey
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.PersistenceKey, ConfigureAsPersistenceKey);
            });
        }

        [ConfigureEntity]
        public void ConfigureIPersistenceKey<T>(IEntityConfigurationBuilder builder)
            where T : class, IPersistenceKey
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.PersistenceKey, ConfigureAsPersistenceKey);
            });
        }

        private static void ConfigureAsPersistenceKey<T>(IEntityProperty<T> p) where T : class
        {
            p.SetReadOnlyAndHidden();
            p.EntityConfiguration.PersistenceKeyProperty = p;
        }

        private static void ConfigurePersistenceKeyBase(IProperty p)
        {
            typeof(AllConfigurator<TService>)
                .GetMethod(nameof(ConfigurePersistenceKey), BindingFlags.Static | BindingFlags.NonPublic)
                .MakeGenericMethod(p.EntityConfiguration.Type)
                .Invoke(null, new object[] { p });
        }

        private static void ConfigurePersistenceKey<T>(IEntityProperty<T> p) where T : class
        {
            p.EntityConfiguration.PersistenceKeyProperty = p;
            p.IsInferredWith(_ => new IqlNewGuidExpression(), false, InferredValueKind.IfNullOrEmpty);
        }

        [ConfigureEntity]
        public void ConfigureICreatedDate<T>(IEntityConfigurationBuilder builder)
            where T : class, ICreatedDate
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.CreatedDate, p =>
                {
                    p.SetHint(FormHints.CreatedDate);
                    p.SetReadOnly();
                    p.IsInferredWith(_ => new IqlNowExpression(), true, InferredValueKind.IfNullOrEmpty);
                });
                config.DefaultBrowseSortExpression = nameof(ICreatedDate.CreatedDate);
                config.DefaultBrowseSortDescending = true;
            });
        }

        [ConfigureEntity]
        public void ConfigureDbObject<T, TUser>(IEntityConfigurationBuilder builder)
            where T : DbObjectBase<TUser>
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.Id, p => p.SetReadOnly());
                config.ConfigureProperty(p => p.CreatedDate, p => p.SetReadOnly());
                config.ConfigureProperty(p => p.Guid, p => p.SetHidden());
                config.ConfigureProperty(p => p.PersistenceKey, p => p.SetReadOnlyAndHidden());
            });
        }

        [ConfigureEntity]
        public void ConfigureIRevisionable<T>(IEntityConfigurationBuilder builder)
            where T : class, IRevisionable
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.RevisionKey, p => p.SetReadOnlyAndHidden().SetHint(KnownHints.Version));
            });
        }

        [ConfigureEntity]
        public void ConfigureICreatedBy<T, TUser>(IEntityConfigurationBuilder builder)
            where T : class, ICreatedBy<TUser>
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.CreatedByUser, p =>
                {
                    p.SetHint(FormHints.CreatedByUser);
                    p.SetReadOnly();
                });
                config.ConfigureProperty(
                    p => p.CreatedByUserId,
                    p =>
                    {
                        p.SetReadOnly();
                        p.IsInferredWith(_ => new IqlCurrentUserIdExpression());
                    });
            });
        }

        [ConfigureEntity]
        public void ConfigureIDbObject<T, TKey>(IEntityConfigurationBuilder builder)
            where T : class, IDbObject<TKey>
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.Id, p => p.SetReadOnly());
            });
        }

        [ConfigureEntity]
        public void ConfigureIdentityUser<T, TKey>(IEntityConfigurationBuilder builder)
            where T : IdentityUser<TKey>
            where TKey : IEquatable<TKey>
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.Id, p => p.SetReadOnly());
                config.ConfigureProperty(p => p.Email, p => p.SetHint(FormHints.EmailAddress));
            });
        }

        [ConfigureEntity]
        public void ConfigureName<T>(IEntityConfigurationBuilder builder)
            where T : class, IHasName
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.DefinePropertyValidation(_ => _.Name, entity => entity.Name == "" || entity.Name == null, "Please enter a name.");
                config.TitlePropertyName = nameof(IHasName.Name);
                config.ConfigureProperty(p => p.Name, p =>
                {
                    p.SetNullable(false);
                    p.SearchKind = IqlPropertySearchKind.Primary;
                });
            });
        }

        [ConfigureEntity]
        public void ConfigureTitle<T>(IEntityConfigurationBuilder builder)
            where T : class, IHasTitle
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.TitlePropertyName = nameof(IHasTitle.Title);
                config.ConfigureProperty(p => p.Title, p => p.SetNullable(false));
            });
        }

        [ConfigureEntity]
        public void ConfigureHasGuid<T>(IEntityConfigurationBuilder builder)
            where T : class, IHasGuid
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.Guid, p =>
                {
                    p.SetReadOnlyAndHidden();
                    p.IsInferredWith(_ => new IqlNewGuidExpression(), false, InferredValueKind.IfNullOrEmpty);
                });
            });
        }

        [ConfigureEntity]
        public void ConfigurePersistenceKeyObject<T>(IEntityConfigurationBuilder builder)
            where T : class, IHasPersistenceKey
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.PersistenceKey, p => p.SetReadOnlyAndHidden());
            });
        }
    }
}