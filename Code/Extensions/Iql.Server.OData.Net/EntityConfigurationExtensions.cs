//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using Iql.DotNet.Serialization;
//using Iql.Entities;
//using Iql.Entities.Rules;
//using Iql.Entities.Rules.Display;
//using Iql.Entities.Rules.Relationship;
//using Iql.Extensions;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.OData.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;

//namespace Iql.Server.OData.Net
//{
//    public static class EntityConfigurationExtensions
//    {
//        static EntityConfigurationExtensions()
//        {
//            BuildEntityTypeMethod = typeof(EntityConfigurationExtensions).GetMethod(nameof(BuildEntityType),
//                BindingFlags.NonPublic | BindingFlags.Static);
//            BuildEntitySetMethod = typeof(EntityConfigurationExtensions).GetMethod(nameof(BuildEntitySet),
//                BindingFlags.NonPublic | BindingFlags.Static);
//            BuildEntityPropertyMethod = typeof(EntityConfigurationExtensions).GetMethod(nameof(BuildEntityProperty),
//                BindingFlags.NonPublic | BindingFlags.Static);
//            BuildEntityNavigationPropertyMethod = typeof(EntityConfigurationExtensions).GetMethod(nameof(BuildEntityNavigationProperty),
//                BindingFlags.NonPublic | BindingFlags.Static);
//        }

//        private static MethodInfo BuildEntitySetMethod { get; }
//        private static MethodInfo BuildEntityTypeMethod { get; }
//        private static MethodInfo BuildEntityPropertyMethod { get; }
//        private static MethodInfo BuildEntityNavigationPropertyMethod { get; }

//        public static IqlConfiguration ConfigureFromOData<T>(this IqlConfiguration app, ODataModelBuilder model)
//        {
//            app.ConfigureFromOData(typeof(T), model);
//            return app;
//        }

//        public static IqlConfiguration ConfigureFromOData(this IqlConfiguration app, Type serviceType, ODataModelBuilder model)
//        {
//            return app.ConfigureFromOData(null, serviceType, null, model);
//        }

//        public static IqlConfiguration ConfigureFromOData(this IqlConfiguration app, IEntityConfigurationBuilder builder, ODataModelBuilder model)
//        {
//            return app.ConfigureFromOData(null, null, builder, model);
//        }

//        public static IqlConfiguration ConfigureFromOData(this IqlConfiguration app, string key, ODataModelBuilder model)
//        {
//            return app.ConfigureFromOData(key, null, null, model);
//        }

//        private static IqlConfiguration ConfigureFromOData(this IqlConfiguration app, string key, Type type, IEntityConfigurationBuilder builder, ODataModelBuilder model)
//        {
//            app.Bootup.Add(() =>
//            {
//                var provider = app.App.ApplicationServices.GetService<IEntityConfigurationProvider>();
//                if (builder == null)
//                {
//                    builder = string.IsNullOrWhiteSpace(key)
//                        ? provider.Get(type)
//                        : provider.Get(key);
//                }
//                foreach (var set in model.EntitySets)
//                {
//                    BuildEntityTypeMethod.MakeGenericMethod(set.ClrType).Invoke(null, new object[] { builder, model });
//                }
//            });
//            return app;
//        }
//        private static void BuildEntitySet<T>(IEntityConfigurationBuilder builder, ODataModelBuilder model, EntitySetConfiguration set)
//            where T : class
//        {
//            builder.EntityType<T>().SetName = set.Name;
//            builder.EntityType<T>().Name = set.EntityType.Name;
//        }

//        private static void BuildEntityType<T>(EntityConfigurationBuilder builder, ODataModelBuilder model)
//            where T : class
//        {
//            var typeConfiguration = model.EntityType<T>();
//            foreach (EntitySetConfiguration entitySet in model.EntitySets.Where(s => s.EntityType.ClrType == typeof(T)))
//            {
//                BuildEntitySet<T>(builder, model, entitySet);
//                // Define keys
//                //entitySet.EntityType.Keys;
//            }

//            foreach (var property in typeConfiguration.Properties)
//            {
//                var parameter = Expression.Parameter(typeof(T));
//                var expression = Expression.Lambda(Expression.Property(parameter, property.Name), parameter);
//                BuildEntityPropertyMethod.MakeGenericMethod(typeof(T), property.RelatedClrType)
//                    .Invoke(null, new object[] { builder, model, typeConfiguration, property, expression });
//            }

//            foreach (var property in typeConfiguration.NavigationProperties)
//            {
//                var parameter = Expression.Parameter(typeof(T));
//                var expression = Expression.Lambda(Expression.Property(parameter, property.Name), parameter);
//                BuildEntityNavigationPropertyMethod.MakeGenericMethod(typeof(T), property.RelatedClrType, property.Partner?.RelatedClrType ?? typeof(object), property.Partner?.PropertyInfo?.PropertyType ?? typeof(object))
//                    .Invoke(null, new object[] { builder, model, typeConfiguration, property, expression });
//            }
//        }

//        private static void BuildEntityProperty<T, TProperty>(
//            EntityConfigurationBuilder builder, 
//            ODataModelBuilder model, 
//            EntityTypeConfiguration<T> typeConfiguration, 
//            PropertyConfiguration property,
//            Expression<Func<T, TProperty>> expression)
//            where T : class
//        {
//            var entityConfiguration = builder.EntityType<T>();
//            entityConfiguration.DefineProperty(expression, property.PropertyInfo.PropertyType.IsNullable(), property.PropertyInfo.PropertyType.ToIqlType());
//            // Nullable
//            // Type

//            //foreach (EntitySetConfiguration entitySet in builder.EntitySets)
//            //{
//            //    entitySet.EntityType.k
//            //}
//        }

//        private static void BuildEntityNavigationProperty<T, TProperty, TPartner, TPartnerProperty>(
//            EntityConfigurationBuilder builder,
//            ODataModelBuilder model,
//            EntityTypeConfiguration<T> typeConfiguration,
//            NavigationPropertyConfiguration property,
//            Expression<Func<T, TProperty>> expression)
//            where T : class
//        where TPartner : class
//        {
//            var entityConfiguration = builder.EntityType<T>();
//            if (property.DependentProperties?.Any() == true)
//            {
//                foreach (var keyProperty in property.DependentProperties)
//                {
//                    entityConfiguration.DefineProperty(expression, keyProperty.PropertyType.IsNullable(), property.PropertyInfo.PropertyType.ToIqlType());
//                }
//            }

//            if (property.Partner != null)
//            {
//                var parameter = Expression.Parameter(typeof(TPartner));
//                var partnerExpression =
//                    Expression.Lambda(Expression.Property(parameter, property.Partner.PropertyInfo.Name), parameter);
//                builder.EntityType<TPartner>().DefineProperty(
//                    (Expression<Func<TPartner, TProperty>>)partnerExpression,
//                    property.Partner.PropertyInfo.PropertyType.IsNullable(),
//                    property.Partner.PropertyInfo.PropertyType.ToIqlType());
//            }
//            // Nullable
//            // Type

//            //foreach (EntitySetConfiguration entitySet in builder.EntitySets)
//            //{
//            //    entitySet.EntityType.k
//            //}
//        }

//        class EntityConfigurationDocument
//        {
//            public List<IEntityMetadata> EntityTypes { get; set; } = new List<IEntityMetadata>();
//        }

//        public static string ToJson(this EntityConfigurationBuilder entityConfigurationBuilder)
//        {
//            var settings = new JsonSerializerSettings()
//            {
//                ContractResolver = new InterfaceContractResolver()
//            };
//            settings.Converters.Add(new ExpressionJsonConverter());
//            var doc = new EntityConfigurationDocument();
//            doc.EntityTypes.AddRange(entityConfigurationBuilder.EntityTypes());
//            settings.Formatting = Newtonsoft.Json.Formatting.Indented;
//            var serialized = JsonConvert.SerializeObject(doc, settings);
//            return serialized;
//        }

//        public class ExpressionJsonConverter : JsonConverter
//        {
//            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//            {
//                if (value != null)
//                {
//                    writer.WriteValue(IqlXmlSerializer.SerializeToXml((LambdaExpression)value));
//                }
//            }

//            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//            {
//                throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
//            }

//            public override bool CanRead
//            {
//                get { return false; }
//            }

//            public override bool CanConvert(Type objectType)
//            {
//                return typeof(Expression).IsAssignableFrom(objectType);
//            }
//        }

//        class InterfaceContractResolver : DefaultContractResolver
//        {
//            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
//            {
//                if (typeof(IEntityConfiguration).IsAssignableFrom(type))
//                {
//                    return base.CreateProperties(typeof(IEntityMetadata), memberSerialization);
//                }
//                if (typeof(IProperty).IsAssignableFrom(type))
//                {
//                    return base.CreateProperties(typeof(IPropertyMetadata), memberSerialization);
//                }
//                if (typeof(ITypeDefinition).IsAssignableFrom(type))
//                {
//                    return base.CreateProperties(typeof(ITypeConfiguration), memberSerialization);
//                }
//                if (typeof(IDisplayRule).IsAssignableFrom(type))
//                {
//                    return base.CreateProperties(typeof(IDisplayRule), memberSerialization)
//                        .Where(p => p.PropertyName != nameof(IRuleBase<string>.Run))
//                        .ToList();
//                }
//                if (typeof(IRelationshipRule).IsAssignableFrom(type))
//                {
//                    return base.CreateProperties(typeof(IRelationshipRule), memberSerialization)
//                        .Where(p => p.PropertyName != nameof(IRuleBase<string>.Run))
//                        .ToList();
//                }
//                if (typeof(IBinaryRule).IsAssignableFrom(type))
//                {
//                    return base.CreateProperties(typeof(IBinaryRule), memberSerialization)
//                        .Where(p => p.PropertyName != nameof(IRuleBase<string>.Run))
//                        .ToList();
//                }
//                //if (type == typeof(IRuleCollection<IBinaryRule>))
//                //{
//                //    return base.CreateProperties(typeof(IRuleCollection<IBinaryRule>), memberSerialization);
//                //}
//                //if (typeof(IRuleCollection<IDisplayRule>).IsAssignableFrom(type))
//                //{
//                //    int a = 0;
//                //}
//                //if (typeof(IRuleCollection<IRelationshipRule>).IsAssignableFrom(type))
//                //{
//                //    int a = 0;
//                //}

//                //if (type == typeof(IBinaryRule))
//                //{
//                //    int a = 0;
//                //}
//                //IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
//                return base.CreateProperties(type, memberSerialization);
//            }
//        }
//    }
//}
