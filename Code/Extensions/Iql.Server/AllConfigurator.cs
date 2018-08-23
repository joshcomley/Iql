using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Brandless.Data.Contracts;
using Brandless.Data.Entities;
using Iql.Entities;
using Iql.Entities.NestedSets;
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
                    methodInfo.Invoke(this, new object[] {builder});
                }

                var entityConfiguration = builder.GetEntityByType(entityType);
                foreach (var property in entityConfiguration.Properties)
                {
                    if (property.Geographic != null || property.NestedSet != null)
                    {
                        property.SetReadOnly();
                    }
                }
            }
        }

        [ConfigureEntity]
        public void ConfigureIHasDescription<T>(IEntityConfigurationBuilder builder)
            where T : class, IHasDescription
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.Description, p => p.SetHint(KnownHints.BigText));
            });
        }

        [ConfigureEntity]
        public void ConfigureICreatedDate<T>(IEntityConfigurationBuilder builder)
            where T : class, ICreatedDate
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.ConfigureProperty(p => p.CreatedDate, p => p.SetReadOnly());
                config.DefaultSortExpression = nameof(ICreatedDate.CreatedDate);
                config.DefaultSortDescending = true;
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
                config.ConfigureProperty(p => p.CreatedByUser, p => p.SetReadOnly());
                config.ConfigureProperty(p => p.CreatedByUserId, p => p.SetReadOnly());
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
                config.ConfigureProperty(p => p.CreatedByUser, p => p.SetReadOnly());
                config.ConfigureProperty(p => p.CreatedByUserId, p => p.SetReadOnly());
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
                config.TitlePropertyName = nameof(IHasName.Name);
                config.ConfigureProperty(p => p.Id, p => p.SetReadOnly());
                config.ConfigureProperty(p => p.Email, p => p.SetHint(KnownHints.EmailAddress));
                config.TitlePropertyName = nameof(IdentityUser.UserName);
            });
        }

        [ConfigureEntity]
        public void ConfigureName<T>(IEntityConfigurationBuilder builder)
            where T : class, IHasName
        {
            builder.EntityType<T>().Configure(config =>
            {
                config.TitlePropertyName = nameof(IHasName.Name);
                config.ConfigureProperty(p => p.Name, p =>
                {
                    p.SetNullable(false);
                    p.SearchKind = PropertySearchKind.Primary;
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
                config.ConfigureProperty(p => p.Guid, p => p.SetReadOnlyAndHidden());
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