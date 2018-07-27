﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Entities;
using Iql.Entities.Relationships;
using Iql.Extensions;
using Microsoft.AspNetCore.OData.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Iql.Server.OData.Net
{
    public static class EntityConfigurationODataExtensions
    {
        static EntityConfigurationODataExtensions()
        {
            BuildEntityTypeMethod = typeof(EntityConfigurationODataExtensions).GetMethod(nameof(BuildEntityType),
                BindingFlags.NonPublic | BindingFlags.Static);
            BuildEnumTypeMethod = typeof(EntityConfigurationODataExtensions).GetMethod(nameof(BuildEnumType),
                BindingFlags.NonPublic | BindingFlags.Static);
            BuildEntitySetMethod = typeof(EntityConfigurationODataExtensions).GetMethod(nameof(BuildEntitySet),
                BindingFlags.NonPublic | BindingFlags.Static);
            BuildEntityPropertyMethod = typeof(EntityConfigurationODataExtensions).GetMethod(nameof(BuildEntityProperty),
                BindingFlags.NonPublic | BindingFlags.Static);
            BuildEntityNavigationPropertyMethod = typeof(EntityConfigurationODataExtensions).GetMethod(nameof(BuildEntityNavigationProperty),
                BindingFlags.NonPublic | BindingFlags.Static);
            BuildEntityNavigationCollectionPropertyMethod = typeof(EntityConfigurationODataExtensions).GetMethod(nameof(BuildEntityNavigationCollectionProperty),
                BindingFlags.NonPublic | BindingFlags.Static);
            BuildEntityKeyMethod = typeof(EntityConfigurationODataExtensions).GetMethod(nameof(BuildEntityKey),
                BindingFlags.NonPublic | BindingFlags.Static);
            BuildOneToManyKeyMethod = typeof(EntityConfigurationODataExtensions).GetMethod(nameof(BuildOneToManyKey),
                BindingFlags.NonPublic | BindingFlags.Static);
        }

        private static MethodInfo BuildEntitySetMethod { get; }
        private static MethodInfo BuildEntityTypeMethod { get; }
        private static MethodInfo BuildEnumTypeMethod { get; }
        private static MethodInfo BuildEntityPropertyMethod { get; }
        private static MethodInfo BuildEntityNavigationPropertyMethod { get; }
        private static MethodInfo BuildEntityNavigationCollectionPropertyMethod { get; }
        private static MethodInfo BuildEntityKeyMethod { get; }
        private static MethodInfo BuildOneToManyKeyMethod { get; }

        public static IqlConfiguration ConfigureFromOData<T>(this IqlConfiguration app, ODataModelBuilder model)
        {
            app.ConfigureFromOData(typeof(T), model);
            return app;
        }

        public static IqlConfiguration ConfigureFromOData(this IqlConfiguration app, Type serviceType, ODataModelBuilder model)
        {
            return app.ConfigureFromOData(null, serviceType, null, model);
        }

        public static IqlConfiguration ConfigureFromOData(this IqlConfiguration app, IEntityConfigurationBuilder builder, ODataModelBuilder model)
        {
            return app.ConfigureFromOData(null, null, builder, model);
        }

        public static IqlConfiguration ConfigureFromOData(this IqlConfiguration app, string key, ODataModelBuilder model)
        {
            return app.ConfigureFromOData(key, null, null, model);
        }

        private static IqlConfiguration ConfigureFromOData(this IqlConfiguration app, string key, Type type, IEntityConfigurationBuilder builder, ODataModelBuilder model)
        {
            app.Bootup.Add(() =>
            {
                var provider = app.App.ApplicationServices.GetService<IEntityConfigurationProvider>();
                if (builder == null)
                {
                    builder = string.IsNullOrWhiteSpace(key)
                        ? provider.Get(type)
                        : provider.Get(key);
                }
                foreach (var set in model.EntitySets)
                {
                    BuildEntityTypeMethod.MakeGenericMethod(set.ClrType).Invoke(null, new object[] { builder, model });
                }
                foreach (var enumType in model.EnumTypes)
                {
                    BuildEnumTypeMethod.MakeGenericMethod(enumType.ClrType).Invoke(null, new object[] { builder, enumType });
                }
            });
            return app;
        }

        private static void BuildEnumType<T>(IEntityConfigurationBuilder builder, EnumTypeConfiguration enumTypeConfiguration)
        {
            var enumType = builder.EnumType<T>();
            enumType.IsFlags = enumTypeConfiguration.IsFlags;
            var names = Enum.GetNames(typeof(T));
            for (int i = 0; i < names.Length; i++)
            {
                enumType.DefineValue(names[i], Convert.ToInt64(Enum.Parse(typeof(T), names[i])));
            }
        }

        private static void BuildEntitySet<T>(IEntityConfigurationBuilder builder, ODataModelBuilder model, EntitySetConfiguration set)
            where T : class
        {
            builder.EntityType<T>().SetName = set.Name;
            builder.EntityType<T>().Name = set.EntityType.Name;
            foreach (var property in set.EntityType.Keys)
            {
                BuildEntityKeyMethod.MakeGenericMethod(typeof(T), property.PropertyInfo.PropertyType).Invoke(null,
                    new object[]
                    {
                        builder, set, property
                    });
            }
        }

        private static void BuildEntityKey<T, TKey>(IEntityConfigurationBuilder builder, EntitySetConfiguration set, PrimitivePropertyConfiguration property)
            where T : class
        {
            var parameter = Expression.Parameter(typeof(T));
            var expression = Expression.Lambda(Expression.Property(parameter, property.Name), parameter);
            builder.EntityType<T>().HasKey((Expression<Func<T, TKey>>)expression);
        }

        private static void BuildEntityType<T>(EntityConfigurationBuilder builder, ODataModelBuilder model)
            where T : class
        {
            var typeConfiguration = model.EntityType<T>();
            foreach (EntitySetConfiguration entitySet in model.EntitySets.Where(s => s.EntityType.ClrType == typeof(T)))
            {
                BuildEntitySet<T>(builder, model, entitySet);
                // Define keys
                //entitySet.EntityType.Keys;
            }

            foreach (var property in typeConfiguration.Properties)
            {
                var parameter = Expression.Parameter(typeof(T));
                var expression = Expression.Lambda(Expression.Property(parameter, property.Name), parameter);
                BuildEntityPropertyMethod.MakeGenericMethod(typeof(T), property.RelatedClrType)
                    .Invoke(null, new object[] { builder, model, typeConfiguration, property, expression });
            }

            foreach (var property in typeConfiguration.NavigationProperties)
            {
                var parameter = Expression.Parameter(typeof(T));
                var expression = Expression.Lambda(Expression.Property(parameter, property.Name), parameter);
                BuildEntityNavigationPropertyMethod.MakeGenericMethod(typeof(T), property.RelatedClrType, property.Partner?.PropertyInfo?.DeclaringType ?? typeof(object), property.Partner?.PropertyInfo?.PropertyType ?? typeof(object))
                    .Invoke(null, new object[] { builder, model, typeConfiguration, property, expression });
            }
        }

        private static void BuildEntityProperty<T, TProperty>(
            EntityConfigurationBuilder builder,
            ODataModelBuilder model,
            EntityTypeConfiguration<T> typeConfiguration,
            PropertyConfiguration property,
            Expression<Func<T, TProperty>> expression)
            where T : class
        {
            var entityConfiguration = builder.EntityType<T>();
            entityConfiguration.DefineProperty(expression, property.PropertyInfo.PropertyType.IsNullable(), property.PropertyInfo.PropertyType.ToIqlType());
            // Nullable
            // Type

            //foreach (EntitySetConfiguration entitySet in builder.EntitySets)
            //{
            //    entitySet.EntityType.k
            //}
        }

        private static void BuildEntityNavigationCollectionProperty<T, TProperty, TPartner, TPartnerProperty>(
            EntityConfigurationBuilder builder,
            ODataModelBuilder model,
            EntityTypeConfiguration<T> typeConfiguration,
            NavigationPropertyConfiguration property,
            Expression<Func<TPartner, IEnumerable<T>>> expression)
            where T : class
            where TPartner : class
        {
            builder.EntityType<TPartner>().DefineCollectionProperty(expression);
            var parameter = Expression.Parameter(typeof(T));
            var relationshipExpression = Expression.Lambda(Expression.Property(parameter, property.Name), parameter);
            var relationship = builder.EntityType<T>()
                .HasOne((Expression<Func<T, TPartner>>)relationshipExpression);
            var relationship2 = relationship
                .WithMany(expression); //.WithConstraint();
            var i = 0;
            var principalProperties = property.PrincipalProperties.ToArray();
            foreach (var constraint in property.DependentProperties)
            {
                var principalProperty = principalProperties[i];
                BuildOneToManyKeyMethod.MakeGenericMethod(
                        typeof(T), typeof(TProperty),
                        typeof(TPartner), typeof(TPartnerProperty),
                        constraint.PropertyType,
                        principalProperty.PropertyType)
                    .Invoke(null, new object[] { builder, relationship2, constraint, principalProperty });
                i++;
            }
            //relationship2.WithConstraint()
        }

        private static void BuildOneToManyKey<T, TProperty, TPartner, TPartnerProperty, TConstraint, TPrincipal>(
            EntityConfigurationBuilder builder,
            OneToManyRelationship<T, TPartner, IEnumerable<T>> relationship,
            PropertyInfo constraint,
            PropertyInfo principal
        )
            where T : class
            where TPartner : class
        {
            var leftKeyParameter = Expression.Parameter(typeof(T));
            var letfKeyExpression = Expression.Lambda(Expression.Property(leftKeyParameter, constraint.Name), leftKeyParameter);
            var rightKeyParameter = Expression.Parameter(typeof(TPartner));
            var rightKeyExpression = Expression.Lambda(Expression.Convert(Expression.Property(rightKeyParameter, principal.Name), typeof(TConstraint)), rightKeyParameter);
            relationship.WithConstraint((Expression<Func<T, TConstraint>>)letfKeyExpression, (Expression<Func<TPartner, TConstraint>>)rightKeyExpression);
        }

        private static void BuildEntityNavigationProperty<T, TProperty, TPartner, TPartnerProperty>(
            EntityConfigurationBuilder builder,
            ODataModelBuilder model,
            EntityTypeConfiguration<T> typeConfiguration,
            NavigationPropertyConfiguration property,
            Expression<Func<T, TProperty>> expression)
            where T : class
            where TPartner : class
        {
            var entityConfiguration = builder.EntityType<T>();
            if (property.DependentProperties?.Any() == true)
            {
                foreach (var keyProperty in property.DependentProperties)
                {
                    entityConfiguration.DefineProperty(expression, keyProperty.PropertyType.IsNullable(),
                        property.PropertyInfo.PropertyType.ToIqlType());
                }
            }

            if (property.Partner != null)
            {
                var parameter = Expression.Parameter(typeof(TPartner));
                if (property.Partner.PropertyInfo.PropertyType.IsEnumerableType())
                {
                    var partnerExpression =
                        Expression.Lambda(
                            Expression.Convert(Expression.Property(parameter, property.Partner.PropertyInfo.Name), typeof(IEnumerable<>).MakeGenericType(typeof(T)))
                            , parameter);
                    BuildEntityNavigationCollectionPropertyMethod.MakeGenericMethod(
                            typeof(T),
                            typeof(TProperty),
                            typeof(TPartner),
                            typeof(TPartnerProperty))
                        .Invoke(null, new object[]
                        {
                            builder,
                            model,
                            typeConfiguration,
                            property,
                            partnerExpression
                        });
                }
                else
                {
                    var partnerExpression =
                        Expression.Lambda(Expression.Property(parameter, property.Partner.PropertyInfo.Name), parameter);
                    builder.EntityType<TPartner>().DefineProperty(
                        (Expression<Func<TPartner, TPartnerProperty>>)partnerExpression,
                        property.Partner.PropertyInfo.PropertyType.IsNullable(),
                        property.Partner.PropertyInfo.PropertyType.ToIqlType());
                }
            }
            // Nullable
            // Type

            //foreach (EntitySetConfiguration entitySet in builder.EntitySets)
            //{
            //    entitySet.EntityType.k
            //}
        }
    }
}