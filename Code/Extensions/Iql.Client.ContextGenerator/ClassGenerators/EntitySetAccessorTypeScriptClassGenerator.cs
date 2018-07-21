//using System.Collections.Generic;
//using System.Linq;
//using Iql.OData.Data;
//using Iql.OData.TypeScript.Generator.Definitions;
//using Iql.OData.TypeScript.Generator.Extensions;
//using Iql.OData.TypeScript.Generator.Models;
//using Iql.OData.TypeScript.Generator.Parsers;
//using Iql.Parsing;
//using Iql.Queryable;
//using Iql.Queryable.Data;
//using Iql.Queryable.Data.DataStores;
//using Iql.Queryable.Data.EntityConfiguration;
//using Iql.Queryable.Data.EntityConfiguration.Relationships;

//namespace Iql.OData.TypeScript.Generator.ClassGenerators
//{
//    public class EntitySetAccessorTypeScriptClassGenerator : TypeScriptClassGenerator
//    {
//        private readonly string _className;
//        private readonly IEnumerable<EntitySetDefinition> _entitySetDefinitions;
//        private readonly string _genericParameters;
//        private readonly string _namespace;

//        public EntitySetAccessorTypeScriptClassGenerator(ODataSchema schema, string @namespace, string className,
//            string genericParameters, IEnumerable<EntitySetDefinition> entitySetDefinitions) : base(schema)
//        {
//            _entitySetDefinitions = entitySetDefinitions;
//            _namespace = @namespace;
//            _genericParameters = genericParameters;
//            _className = className;
//        }

//        public GeneratedFile Generate()
//        {
//            File.FileName = _namespace;
//            File.Namespace = _namespace;
//            //File.References.Add(new ODataTypeDefinition("DataContext", "app/queryable/data.context", true));
//            //File.References.Add(new ODataTypeDefinition("EntityConfigurationBuilder",
//            //    "app/queryable/entity.configuration", true));
//            //File.References.Add(new ODataTypeDefinition("IQueryResult", "app/queryable/queryable", true));
//            //File.References.Add(new ODataTypeDefinition("IQueryableAdapterBase", "app/queryable/queryable.adapter",
//            //    true));
//            //File.References.Add(new ODataTypeDefinition("IDataStore", "app/queryable/store.adapter", true));
//            var odataConfigurationPropertyName = "ODataConfiguration";
//            var entitySetDefinitions = new List<EntitySetPropertyDefinition>();
//            foreach (var @class in _entitySetDefinitions)
//            {
//                var key = @class.Type.Key.Properties.First();
//                var keyType = TypeResolver.ResolveTypeNameFromODataName(key.TypeInfo, true);
//                var propertyDefinition = new EntitySetPropertyDefinition
//                {
//                    Name = @class.Name,
//                    EntityType = @class.Type.Name,
//                    KeyType = keyType.Name
//                };
//                propertyDefinition.TypeInfo.ResolvedType = $"{nameof(DbSet<object, object>)}<{propertyDefinition.EntityType},{propertyDefinition.KeyType}>";
//                propertyDefinition.Private = false;
//                entitySetDefinitions.Add(propertyDefinition);
//            }
//            Class(
//                _className,
//                _genericParameters,
//                () =>
//                {
//                    var ctorParams = new IVariable[]
//                    {
//                        new EntityFunctionParameterDefinition("dataStore", TypeResolver.TranslateType(typeof(IDataStore))),
//                        new EntityFunctionParameterDefinition("evaluateContext", TypeResolver.TranslateType(typeof(EvaluateContext), "nullable"), true)
//                    };
//                    Constructor(ctorParams, () =>
//                    {
//                        Super(ctorParams);
//                        foreach (var propertyDefinition in entitySetDefinitions)
//                        {
//                            AssignProperty(propertyDefinition, 
//                                $"<{nameof(DbSet<object, object>)}<{propertyDefinition.EntityType},{propertyDefinition.KeyType}>>this.{nameof(global::Iql.Queryable.Data.DataContext.AsDbSet)}({propertyDefinition.EntityType}, {propertyDefinition.KeyType})");
//                        }
//                        AppendLine($"this.{nameof(IDataContext.RegisterConfiguration)}<{nameof(ODataConfiguration)}>(this.{odataConfigurationPropertyName}, {nameof(ODataConfiguration)});");
//                        foreach (var entitySet in _entitySetDefinitions)
//                        {
//                            AppendLine($"this.{odataConfigurationPropertyName}.{nameof(ODataConfiguration.RegisterEntitySet)}<{entitySet.Type.Name}>(`{entitySet.Name}`, {entitySet.Type.Name});");
//                        }
//                    });
//                    Property("public", odataConfigurationPropertyName, TypeResolver.TranslateType(typeof(ODataConfiguration)), $"new {nameof(ODataConfiguration)}", true);
//                    AppendLine();
//                    var builder = new EntityFunctionParameterDefinition("builder", TypeResolver.TranslateType(typeof(EntityConfigurationBuilder)));
//                    Method(nameof(global::Iql.Queryable.Data.DataContext.Configure), new[] {builder}, TypeResolver.TranslateType(typeof(void)), () =>
//                    {
//                        foreach (var entityDefinition in _entitySetDefinitions)
//                        {
//                            VariableAccessor(builder, () =>
//                            {
//                                MethodCall(
//                                    nameof(EntityConfigurationBuilder.DefineEntity),
//                                    new[] {new EntityFunctionParameterDefinition(entityDefinition.Type.Name)}
//                                );
//                            });
//                            AppendLine();
//                            Indent(() =>
//                            {
//                                Dot();
//                                var keyProperty = entityDefinition.Type.Key.Properties.First();
//                                // Currently only support types with single property keys
//                                var keyTypeName = TypeResolver.ResolveTypeNameFromODataName(keyProperty.TypeInfo)
//                                    .Name;
//                                MethodCall(
//                                    nameof(EntityConfiguration<object>.HasKey),
//                                    new[]
//                                    {
//                                        new EntityFunctionParameterDefinition("p => p." + keyProperty.Name),
//                                        new EntityFunctionParameterDefinition(
//                                            keyTypeName)
//                                    }
//                                );
//                                foreach (var property in entityDefinition.Type.Properties)
//                                {
//                                    AppendLine();
//                                    Dot();
//                                    var propertyType =
//                                        TypeResolver.ResolveTypeNameFromODataName(property.TypeInfo, true);
//                                    var propertyEntityType =
//                                        Schema.AllTypes().SingleOrDefault(t => t.Name == propertyType.Name);
//                                    if (propertyEntityType != null)
//                                    {
//                                        File.References.Add(propertyEntityType);
//                                    }
//                                    var parameters = new List<IVariable>(new[]
//                                    {
//                                        new EntityFunctionParameterDefinition("p => p." + property.Name)
//                                    });
//                                    if (propertyType.IsCollection)
//                                    {
//                                        parameters.Add(new EntityFunctionParameterDefinition("p => p." + property.Name + "Count"));
//                                    }
//                                    parameters.Add(new EntityFunctionParameterDefinition(propertyType.Name));
//                                    MethodCall(
//                                        propertyType.IsCollection 
//                                            ? nameof(EntityConfiguration<object>.DefineCollectionProperty)
//                                            : nameof(EntityConfiguration<object>.DefineProperty),
//                                        parameters
//                                    );
//                                }
//                            });
//                            Append(";");
//                            AppendLine();

//                            foreach (var property in entityDefinition.Type.Properties)
//                            {
//                                var navigationProperty = property as NavigationPropertyDefinition;
//                                if (navigationProperty?.Constraint == null ||
//                                    string.IsNullOrWhiteSpace(navigationProperty.Partner))
//                                {
//                                    continue;
//                                }
//                                AppendLine();
//                                VariableAccessor(builder, () =>
//                                {
//                                    MethodCall(
//                                        nameof(EntityConfigurationBuilder.DefineEntity),
//                                        new[]
//                                        {
//                                            new EntityFunctionParameterDefinition(entityDefinition.Type.Name)
//                                        }
//                                    );
//                                    AppendLine();
//                                    Indent(() =>
//                                    {
//                                        var type =
//                                            TypeResolver.ResolveTypeNameFromODataName(
//                                                navigationProperty.TypeInfo);
//                                        var relatedEntityDefinition =
//                                            Schema.EntityTypes.Single(t => t.Name == type.Name);
//                                        var partnerProperty =
//                                            relatedEntityDefinition.Properties.Single(
//                                                p => p.Name == navigationProperty.Partner);
//                                        var partnerPropertyType =
//                                            TypeResolver.ResolveTypeNameFromODataName(partnerProperty
//                                                .TypeInfo);

//                                        Dot();
//                                        MethodCall(nameof(EntityConfiguration<object>.HasOne), new[]
//                                        {
//                                            new EntityFunctionParameterDefinition(
//                                                "p => p." + navigationProperty.Name),
//                                            new EntityFunctionParameterDefinition(type.Name)
//                                        });
//                                        AppendLine();
//                                        Dot();
//                                        MethodCall(
//                                            partnerPropertyType.IsCollection
//                                                ? nameof(OneToRelationshipMap<object, object>.WithMany)
//                                                : nameof(OneToRelationshipMap<object, object>.WithOne),
//                                            new[]
//                                            {
//                                                new EntityFunctionParameterDefinition(
//                                                    "p => p." + navigationProperty.Partner)
//                                            });
//                                        AppendLine();
//                                        Dot();
//                                        MethodCall(nameof(OneToOneRelationship<object, object>.WithConstraint), new[]
//                                        {
//                                            new EntityFunctionParameterDefinition(
//                                                "p => p." + navigationProperty.Constraint.LocalIdProperty),
//                                            new EntityFunctionParameterDefinition(
//                                                "p => p." + navigationProperty.Constraint.RemoteIdProperty)
//                                        });
//                                    });
//                                    Append(";");
//                                    AppendLine();
//                                });
//                            }

//                            if (entityDefinition != _entitySetDefinitions.Last())
//                            {
//                                AppendLine();
//                            }

//                            /*
//                                builder
//                                    .defineEntity(Person)
//                                    .hasOne(School, p => p.school)
//                                    .withMany(s => s.students)
//                                    .withKey(p => p.schoolId, s => s.id);
                                    
//                                builder.defineEntity(Person)
//                                    .hasKey(Number, p => p.id)
//                                    .defineProperty(School, p => p.school)
//                                    .defineProperty(Number, p => p.schoolId)
//                                    .defineProperty(Certificate, p => p.certificate)
//                            */
//                        }
//                    });
//                    AppendLine();
//                    foreach (var entitySetProperty in entitySetDefinitions)
//                    {
//                        Property(entitySetProperty, false);
//                    }
//                }, nameof(DataContext));
//            foreach (var set in _entitySetDefinitions)
//            {
//                File.References.Add(set.Type);
//            }
//            File.Contents = Contents();
//            return File;
//        }
//    }
//}