using Brandless.ObjectSerializer;
using Iql.Data.Context;
using Iql.Data.DataStores;
using Iql.Data.Lists;
using Iql.Entities;
using Iql.Entities.Geography;
using Iql.Entities.Metadata;
using Iql.Entities.NestedSets;
using Iql.Entities.Relationships;
using Iql.Entities.Rules.Display;
using Iql.Extensions;
using Iql.OData.TypeScript.Generator.DataContext;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;
using Iql.Parsing;
using Iql.Server.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Entities.Functions;
using Iql.Entities.InferredValues;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.SpecialTypes;
using Iql.Server.Serialization.Deserialization.EntityConfiguration;
using TypeSharp;
using TypeSharp.Conversion;
using TypeSharp.Extensions;
using EnumExtensions = Iql.OData.TypeScript.Generator.Extensions.EnumExtensions;
using GeneratedFile = Iql.OData.TypeScript.Generator.Models.GeneratedFile;
using IEntityConfiguration = Iql.Entities.IEntityConfiguration;
using IPropertyCollection = Iql.Entities.IPropertyCollection;
using IPropertyGroup = Iql.Entities.IPropertyGroup;
using IqlMethodParameter = Iql.Entities.Functions.IqlMethodParameter;
using IRelationshipDetail = Iql.Entities.Relationships.IRelationshipDetail;
using PropertyCollection = Iql.Entities.PropertyCollection;
using RelationshipDetail = Iql.Server.Serialization.Deserialization.EntityConfiguration.RelationshipDetail;
using TypeInfo = Iql.OData.TypeScript.Generator.Definitions.TypeInfo;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class DataContextGenerator : ClassGenerator
    {
        private CSharpObjectSerializer CSharpObjectSerializer { get; }
        private readonly string _className;
        private readonly string _dbSetsPath;
        private readonly IEnumerable<EntitySetDefinition> _entitySetDefinitions;
        private readonly string _genericParameters;

        public DataContextGenerator(ODataSchema schema,
            string @namespace,
            string fileName,
            string className,
            string dbSetsPath,
            string genericParameters,
            IEnumerable<EntitySetDefinition> entitySetDefinitions,
            OutputKind outputKind,
            GeneratorSettings settings) : base(fileName, @namespace, schema, outputKind, settings)
        {
            _entitySetDefinitions = entitySetDefinitions;
            _genericParameters = genericParameters;
            _className = className.AsSafeClassName();
            _dbSetsPath = dbSetsPath;
            CSharpObjectSerializer = new CSharpObjectSerializer();
            CSharpObjectSerializer.Converters.Add(new TypeDetailTypeConverter());
        }

        private class MyDataContext : Data.Context.DataContext
        {
            public static string InitializePropertiesName = nameof(MyDataContext.InitializeProperties);
        }

        public async Task<GeneratedFile> GenerateAsync()
        {
            //File.References.Add(new ODataTypeDefinition("DataContext", "app/queryable/data.context", true));
            //File.References.Add(new ODataTypeDefinition("EntityConfigurationBuilder",
            //    "app/queryable/entity.configuration", true));
            //File.References.Add(new ODataTypeDefinition("IQueryResult", "app/queryable/queryable", true));
            //File.References.Add(new ODataTypeDefinition("IQueryableAdapterBase", "app/queryable/queryable.adapter",
            //    true));
            //File.References.Add(new ODataTypeDefinition("IDataStore", "app/queryable/store.adapter", true));
            var odataConfigurationPropertyName = "ODataConfiguration";
            var entitySetDefinitions = new List<EntitySetPropertyDefinition>();
            foreach (var @class in _entitySetDefinitions)
            {
                var keyType = ResolveKeyType(@class);
                var propertyDefinition = new EntitySetPropertyDefinition
                {
                    EntitySet = @class,
                    Name = @class.Name,
                    EntityType = NameMapper(@class.Type.Name),
                    KeyType = keyType
                };
                propertyDefinition.TypeInfo.ResolvedType = $"{nameof(DbSet<object, object>)}<{propertyDefinition.EntityType}, {propertyDefinition.KeyType}>";
                propertyDefinition.Private = false;
                entitySetDefinitions.Add(propertyDefinition);
            }
            await ClassAsync(
                _className,
                Namespace,
                _genericParameters,
                async () =>
                {
                    var ctorParams = new IVariable[]
                    {
                        new EntityFunctionParameterDefinition("dataStore", TypeResolver.TranslateType(typeof(IDataStore))),
                    }.ToList();
                    if (OutputKind == OutputKind.TypeScript)
                    {
                        ctorParams.Add(new EntityFunctionParameterDefinition("evaluateContext", TypeResolver.TranslateType(typeof(EvaluateContext), "nullable"), true));
                    }
                    Constructor(ctorParams, () =>
                    {
                    },
                        ctorParams);
                    Method(MyDataContext.InitializePropertiesName, null, new EntityTypeDefinition
                    {
                        Name = "void"
                    },
                        () =>
                        {
                            if (Settings.GenerateEntitySets)
                            {
                                foreach (var propertyDefinition in entitySetDefinitions)
                                {
                                    var asDbSetParameters =
                                        $"{propertyDefinition.EntityType}, {propertyDefinition.KeyType.AsTypeScriptTypeParameter()}, {propertyDefinition.EntitySet.GetDbSetName(NameMapper)}";
                                    AssignProperty(propertyDefinition,
                                        $"this.{nameof(Data.Context.DataContext.AsCustomDbSet)}{(OutputKind == OutputKind.CSharp ? $"<{asDbSetParameters}>" : "")}({(OutputKind == OutputKind.TypeScript ? asDbSetParameters : "")})");
                                    AppendLine();
                                }

                                if (Settings.ConfigureOData)
                                {
                                    foreach (var entitySet in _entitySetDefinitions)
                                    {
                                        AppendLine(
                                            $"this.{odataConfigurationPropertyName}.{nameof(ODataConfiguration.RegisterEntitySet)}<{NameMapper(entitySet.Type.Name)}>({NameOf(entitySet.Name)}{(OutputKind == OutputKind.TypeScript ? $", {entitySet.Type.Name}" : "")});");
                                    }
                                }
                            }

                            if (Settings.ConfigureOData)
                            {
                                AppendLine(
                                    $"this.{nameof(IDataContext.RegisterConfiguration)}<{nameof(ODataConfiguration)}>(this.{odataConfigurationPropertyName}{(OutputKind == OutputKind.TypeScript ? $", {nameof(ODataConfiguration)}" : "")});");
                            }
                        },
                        "protected",
                        modifier: Modifier.Override);

                    if (Settings.ConfigureOData)
                    {
                        var odataConfigurationBackingFieldName = AsBackingFieldName(odataConfigurationPropertyName);
                        AppendLine($@"        private {nameof(ODataConfiguration)} {odataConfigurationBackingFieldName};
        public {nameof(ODataConfiguration)} {odataConfigurationPropertyName} => {odataConfigurationBackingFieldName} = {odataConfigurationBackingFieldName} ?? new {nameof(ODataConfiguration)}({nameof(IDataContext.EntityConfigurationContext)});");
                        //Property("public", odataConfigurationPropertyName, TypeResolver.TranslateType(typeof(ODataConfiguration)), null, true);
                    }
                    AppendLine();
                    var builder = new EntityFunctionParameterDefinition("builder", TypeResolver.TranslateType(typeof(EntityConfigurationBuilder)));
                    builder.IsLocal = true;
                    await MethodAsync(nameof(Data.Context.DataContext.Configure), new[] { builder }, TypeResolver.TranslateType(typeof(void)), async () =>
                        {
                            await ConfigureMetadataAsync(Schema.EntityConfigurationDocument, null, "builder", false);
                            if (Schema.EntityConfigurationDocument.PermissionRules?.Any() == true)
                            {
                                foreach (var rule in Schema.EntityConfigurationDocument.PermissionRules)
                                {
                                    var iql = rule.IqlExpression;
                                    AppendLine();
                                    VariableAccessor(builder);
                                    AppendLine();
                                    Dot();
                                    AppendLine($"{nameof(IUserPermissionContainer.PermissionRules)}.Add({CSharpObjectSerializer.Serialize(rule).Initialiser});");
                                }
                            }
                            foreach (var entityDefinition in _entitySetDefinitions)
                            {
                                var entityTypeName = entityDefinition.Type.Name;
                                var entityConfiguration =
                                    Schema.EntityConfigurations.ContainsKey(entityTypeName)
                                    ? Schema.EntityConfigurations[entityTypeName]
                                    : null;
                                var defineEntityParameters =
                                    OutputKind == OutputKind.TypeScript ?
                                new[]
                                {
                                new EntityFunctionParameterDefinition(
                                    entityTypeName,
                                    new TypeInfo())
                                } : null;
                                var defineEntityName = nameof(EntityConfigurationBuilder.EntityType);
                                if (OutputKind == OutputKind.CSharp)
                                {
                                    defineEntityName += $"<{NameMapper(entityTypeName)}>";
                                }

                                VariableAccessor(builder, () =>
                                {
                                    MethodCall(
                                        defineEntityName,
                                        false,
                                        defineEntityParameters
                                    );
                                });
                                AppendLine();
                                await IndentAsync(async () =>
                                {
                                    Dot();
                                    var hasKeyParameters = entityDefinition.Type.Key.Properties.Select(keyProperty =>
                                            new EntityFunctionParameterDefinition("p => p." + keyProperty.Name,
                                                keyProperty.TypeInfo))
                                        .Cast<IVariable>()
                                        .ToList();
                                    var keyIqlType = IqlType.Unknown;
                                    var compositeKey = hasKeyParameters.Count > 1;
                                    if (!compositeKey)
                                    {
                                        if (OutputKind == OutputKind.TypeScript)
                                        {
                                            keyIqlType = hasKeyParameters.First().TypeInfo.ResolveIqlType();
                                            hasKeyParameters.Add(new PropertyDefinition($"{nameof(IqlType)}.{keyIqlType}"));
                                            var keyName = TypeResolver.ResolveTypeNameFromODataName(hasKeyParameters.First().TypeInfo).Name;
                                            hasKeyParameters.Add(new EntityFunctionParameterDefinition(
                                                keyName.AsTypeScriptTypeParameter()));
                                        }
                                        else
                                        {
                                            hasKeyParameters.Add(new PropertyDefinition($"{nameof(IqlType)}.{keyIqlType}"));
                                            hasKeyParameters.Add(new PropertyDefinition($"{entityConfiguration.Key.CanWrite.ToString().ToLower()}"));
                                        }
                                    }
                                    else
                                    {
                                        hasKeyParameters.Insert(0, new PropertyDefinition($"{entityConfiguration.Key.CanWrite.ToString().ToLower()}"));
                                    }
                                  // Currently only support types with single property keys
                                  MethodCall(
                                          compositeKey ? nameof(EntityConfiguration<object>.HasCompositeKey) : nameof(EntityConfiguration<object>.HasKey),
                                          false,
                                          hasKeyParameters.ToArray()
                                      );
                                    foreach (var property in entityDefinition.Type.Properties)
                                    {
                                        AppendLine();
                                        Dot();
                                        var propertyType =
                                            TypeResolver.ResolveTypeNameFromODataName(property.TypeInfo, true, "nonull");
                                        var propertyEntityType =
                                            Schema.AllTypes().SingleOrDefault(t => t.Name == propertyType.Name);
                                        if (propertyEntityType != null)
                                        {
                                            File.References.Add(propertyEntityType);
                                        }
                                        var parameters = new List<IVariable>(new[]
                                        {
                                        new EntityFunctionParameterDefinition("p => p." + property.Name)
                                        });
                                        var isConverted = false;
                                        if (property.TypeInfo.EdmType == "Edm.Guid")
                                        {
                                            isConverted = true;
                                            parameters.Add(new EntityFunctionParameterDefinition(String("Guid")));
                                        }
                                        if (propertyType.IsCollection)
                                        {
                                            if (Settings.GenerateCountProperties)
                                            {
                                                parameters.Add(
                                                    new EntityFunctionParameterDefinition(
                                                        "p => p." + property.Name + "Count"));
                                            }
                                            else
                                            {
                                                parameters.Add(
                                                    new EntityFunctionParameterDefinition(
                                                        "null"));
                                            }
                                        }
                                        else
                                        {
                                            parameters.Add(new EntityFunctionParameterDefinition(property.TypeInfo.Nullable ? "true" : "false"));
                                        }

                                        if (!propertyType.IsCollection)
                                        {
                                            var iqlType = property.TypeInfo.ResolveIqlType();
                                            parameters.Add(new PropertyDefinition($"{nameof(IqlType)}.{iqlType}"));
                                        }

                                        var name = propertyType.IsCollection
                                                ? nameof(EntityConfiguration<object>.DefineCollectionProperty)
                                                : (isConverted
                                                    ? nameof(EntityConfiguration<object>.DefineConvertedProperty)
                                                    : nameof(EntityConfiguration<object>.DefineProperty))
                                            ;
                                      //if (propertyType.IsCollection && OutputType == OutputType.CSharp)
                                      //{
                                      //    name += $"<{propertyEntityType.Name}>";
                                      //}
                                      MethodCall(
                                            name,
                                            false,
                                            parameters.ToArray()
                                        );
                                        if (entityConfiguration != null)
                                        {
                                            IMetadata propertyMetadata =
                                                entityConfiguration.Properties.SingleOrDefault(p => p.Name == property.Name);
                                            await ConfigureMetadataAsync(propertyMetadata, parameters.First(), "p", true, entityConfiguration);
                                        }
                                    }
                                    if (entityConfiguration != null)
                                    {
                                        if (entityConfiguration.EntityValidation != null)
                                        {
                                            foreach (var validation in entityConfiguration.EntityValidation.All)
                                            {
                                                var expression = validation.GetPropertyValueByNameAs<IqlExpression>(nameof(RuleBase.ExpressionIql));
                                                AppendLine();
                                                Dot();
                                                MethodCall(nameof(EntityConfiguration<object>.DefineEntityValidation),
                                                    false,
                                                    new[]
                                                    {
                                                      new EntityFunctionParameterDefinition(
                                                          GetExpressionString(expression)),
                                                      new EntityFunctionParameterDefinition(
                                                          String(validation.Message)),
                                                      new EntityFunctionParameterDefinition(
                                                          String(validation.Key)),
                                                    });
                                            }
                                        }
                                        if (entityConfiguration.DisplayFormatting != null)
                                        {
                                            foreach (var formatter in entityConfiguration.DisplayFormatting.All)
                                            {
                                                var expression = formatter.GetPropertyValueByNameAs<IqlExpression>(nameof(DisplayFormatter.FormatterExpressionIql));
                                                AppendLine();
                                                Dot();
                                                MethodCall(nameof(EntityConfiguration<object>.DefineDisplayFormatter),
                                                    false,
                                                    new[]
                                                    {
                                                  new EntityFunctionParameterDefinition(
                                                      GetExpressionString(expression)),
                                                  new EntityFunctionParameterDefinition(
                                                      String(formatter.Key))
                                                    });
                                            }
                                        }
                                        if (entityConfiguration.Properties != null)
                                        {
                                            foreach (var property in entityConfiguration.Properties)
                                            {
                                                foreach (var validation in property.ValidationRules.All)
                                                {
                                                    var expression = validation.GetPropertyValueByNameAs<IqlExpression>(nameof(RuleBase.ExpressionIql));
                                                    var propertyExpression = new IqlPropertyExpression(property.Name, null, IqlType.Unknown);
                                                    var iqlRootReference = new IqlRootReferenceExpression("e", "");
                                                    propertyExpression.Parent = iqlRootReference;
                                                    AppendLine();
                                                    Dot();
                                                    MethodCall(
                                                        nameof(EntityConfiguration<object>.DefinePropertyValidation),
                                                        false,
                                                        new[]
                                                        {
                                                          new EntityFunctionParameterDefinition(
                                                              "p => p." + propertyExpression.PropertyName),
                                                          new EntityFunctionParameterDefinition(
                                                              GetExpressionString(expression)),
                                                          new EntityFunctionParameterDefinition(
                                                              String(validation.Message)),
                                                          new EntityFunctionParameterDefinition(
                                                              String(validation.Key)),
                                                        });
                                                }

                                                if (property.RelationshipFilterRules != null)
                                                {
                                                    foreach (var relationshipFilterRule in property.RelationshipFilterRules.All)
                                                    {
                                                        var expression =
                                                            relationshipFilterRule.GetPropertyValueByNameAs<IqlExpression>(
                                                                "ExpressionIql");
                                                        var propertyExpression = new IqlPropertyExpression(property.Name, null, IqlType.Unknown);
                                                        var iqlRootReference = new IqlRootReferenceExpression("e", "");
                                                        propertyExpression.Parent = iqlRootReference;
                                                        AppendLine();
                                                        Dot();
                                                        MethodCall(nameof(EntityConfiguration<object>.DefineRelationshipFilterRule),
                                                            false,
                                                            new[]
                                                            {
                                                      new EntityFunctionParameterDefinition(
                                                          "p => p." + propertyExpression.PropertyName),
                                                      new EntityFunctionParameterDefinition(
                                                          GetExpressionString(expression)),
                                                      new EntityFunctionParameterDefinition(
                                                          String(relationshipFilterRule.Key)),
                                                      new EntityFunctionParameterDefinition(
                                                          String(relationshipFilterRule.Message))
                                                            });
                                                    }
                                                }

                                                if (property.DisplayRules != null)
                                                {
                                                    foreach (var displayRule in property.DisplayRules.All)
                                                    {
                                                        var expression =
                                                            displayRule.GetPropertyValueByNameAs<IqlExpression>("ExpressionIql");
                                                        var propertyExpression = new IqlPropertyExpression(property.Name, null, IqlType.Unknown);
                                                        var iqlRootReference = new IqlRootReferenceExpression("e", "");
                                                        propertyExpression.Parent = iqlRootReference;
                                                        AppendLine();
                                                        Dot();
                                                        MethodCall(nameof(EntityConfiguration<object>.DefinePropertyDisplayRule),
                                                            false,
                                                            new[]
                                                            {
                                                      new EntityFunctionParameterDefinition(
                                                          "p => p." + propertyExpression.PropertyName),
                                                      new EntityFunctionParameterDefinition(
                                                          GetExpressionString(expression)),
                                                      new EntityFunctionParameterDefinition(
                                                          String(displayRule.Key)),
                                                      new EntityFunctionParameterDefinition(
                                                          String(displayRule.Message)),
                                                      new EntityFunctionParameterDefinition(
                                                          $"{nameof(DisplayRuleKind)}.{displayRule.Kind}"),
                                                      new EntityFunctionParameterDefinition(
                                                          $"{nameof(DisplayRuleAppliesToKind)}.{displayRule.AppliesToKind}"),
                                                            });
                                                    }
                                                }
                                            }
                                        }
                                    }
                                });
                                Append(";");
                                AppendLine();

                                foreach (var property in entityDefinition.Type.Properties)
                                {
                                    var navigationProperty = property as NavigationPropertyDefinition;
                                    if (navigationProperty?.Constraint == null ||
                                        string.IsNullOrWhiteSpace(navigationProperty.Partner))
                                    {
                                        continue;
                                    }
                                    AppendLine();
                                    VariableAccessor(builder, () =>
                                    {
                                        MethodCall(
                                            defineEntityName,
                                            false,
                                            defineEntityParameters
                                        );
                                        AppendLine();
                                        var type =
                                            TypeResolver.ResolveTypeNameFromODataName(
                                                navigationProperty.TypeInfo);
                                        var propertyLambda = new EntityFunctionParameterDefinition(
                                            "p => p." + navigationProperty.Name);
                                        Indent(() =>
                                        {
                                            var relatedEntityDefinition =
                                                Schema.EntityTypes.Single(t => t.Name == type.OriginalName);
                                            var partnerProperty =
                                                relatedEntityDefinition.Properties.Single(
                                                    p => p.Name == navigationProperty.Partner);
                                            var partnerPropertyType =
                                                TypeResolver.ResolveTypeNameFromODataName(partnerProperty
                                                    .TypeInfo);

                                            Dot();
                                            var hasOneParameters = new[]
                                            {
                                            propertyLambda
                                            }.ToList();
                                            if (OutputKind == OutputKind.TypeScript)
                                            {
                                                hasOneParameters.Add(new EntityFunctionParameterDefinition(type.Name.AsTypeScriptTypeParameter()));
                                            }
                                            MethodCall(nameof(EntityConfiguration<object>.HasOne),
                                                false,
                                                hasOneParameters.ToArray());
                                            AppendLine();
                                            Dot();
                                            MethodCall(
                                                partnerPropertyType.IsCollection
                                                    ? nameof(OneToRelationshipMap<object, object>.WithMany)
                                                    : nameof(OneToRelationshipMap<object, object>.WithOne),
                                                false,
                                                new[]
                                                {
                                                new EntityFunctionParameterDefinition(
                                                    "p => p." + navigationProperty.Partner)
                                                });
                                            AppendLine();
                                            Dot();
                                            MethodCall(nameof(OneToOneRelationship<object, object>.WithConstraint),
                                                false,
                                                new[]
                                            {
                                            new EntityFunctionParameterDefinition(
                                                "p => p." + navigationProperty.Constraint.LocalIdProperty),
                                            new EntityFunctionParameterDefinition(
                                                "p => p." + navigationProperty.Constraint.RemoteIdProperty)
                                            });
                                        });
                                        Append(";");
                                        AppendLine();

                                      //if (entityConfiguration != null)
                                      //{
                                      //    var parameters = new List<IVariable>();
                                      //    parameters.Add(propertyLambda);
                                      //    var typeParameter = GetAndAddTypeScriptTypeParameter(type, parameters);
                                      //    IMetadata propertyMetadata =
                                      //        entityConfiguration.PropertyConfigurations?.ContainsKey(property.Name) ==
                                      //        true
                                      //            ? entityConfiguration.PropertyConfigurations[property.Name].Metadata
                                      //            : null;
                                      //    if (propertyMetadata != null)
                                      //    {
                                      //        AppendLine();
                                      //        VariableAccessor(builder, () =>
                                      //        {
                                      //            MethodCall(
                                      //                defineEntityName,
                                      //                false,
                                      //                defineEntityParameters
                                      //            );
                                      //            AppendLine();
                                      //            ConfigreMetadata(propertyMetadata, typeParameter, parameters.First());
                                      //        });
                                      //    }
                                      //}
                                  });
                                }

                                await VariableAccessorAsync(builder, async () =>
                                {
                                    MethodCall(
                                        defineEntityName,
                                        false,
                                        defineEntityParameters
                                    );
                                    AppendLine();
                                    await ConfigureMetadataAsync(entityConfiguration);
                                    Append(";");
                                    AppendLine();

                                  //if (entityConfiguration != null)
                                  //{
                                  //    var parameters = new List<IVariable>();
                                  //    parameters.Add(propertyLambda);
                                  //    var typeParameter = GetAndAddTypeScriptTypeParameter(type, parameters);
                                  //    IMetadata propertyMetadata =
                                  //        entityConfiguration.PropertyConfigurations?.ContainsKey(property.Name) ==
                                  //        true
                                  //            ? entityConfiguration.PropertyConfigurations[property.Name].Metadata
                                  //            : null;
                                  //    if (propertyMetadata != null)
                                  //    {
                                  //        AppendLine();
                                  //        VariableAccessor(builder, () =>
                                  //        {
                                  //            MethodCall(
                                  //                defineEntityName,
                                  //                false,
                                  //                defineEntityParameters
                                  //            );
                                  //            AppendLine();
                                  //            ConfigreMetadata(propertyMetadata, typeParameter, parameters.First());
                                  //        });
                                  //    }
                                  //}
                              });

                                if (entityDefinition.Functions?.Any() == true && entityConfiguration.Methods?.Any() == true)
                                {
                                    // TODO: Define methods
                                }

                                //foreach (var property in entityConfiguration.Properties)
                                //{
                                //    if (property.PermissionRules?.Any() == true)
                                //    {
                                //        foreach (var rule in property.PermissionRules)
                                //        {
                                //            var iql = rule.IqlExpression;
                                //            AppendLine();
                                //            VariableAccessor(builder, () =>
                                //            {
                                //                MethodCall(
                                //                    defineEntityName,
                                //                    false,
                                //                    defineEntityParameters
                                //                );
                                //            });
                                //            AppendLine();
                                //            Dot();
                                //            AppendLine($"{nameof(IUserPermission.PermissionRules)}.Add({CSharpObjectSerializer.Serialize(rule).Initialiser});");
                                //        }
                                //    }
                                //}

                                if (entityDefinition != _entitySetDefinitions.Last())
                                {
                                    AppendLine();
                                }

                                /*
                                    builder
                                        .defineEntity(Person)
                                        .hasOne(School, p => p.school)
                                        .withMany(s => s.students)
                                        .withKey(p => p.schoolId, s => s.id);

                                    builder.defineEntity(Person)
                                        .hasKey(Number, p => p.id)
                                        .defineProperty(School, p => p.school)
                                        .defineProperty(Number, p => p.schoolId)
                                        .defineProperty(Certificate, p => p.certificate)
                                */
                                //if (entityDefinition.Functions?.Any())
                                //{
                                //    new IqlMethod(
                                //        "MyMethod",
                                //        new IqlMethodParameter[] { },
                                //        (context, args) => { },
                                //        null,
                                //        "ns",
                                //        false);
                                //}
                            }
                            Append(await ConfigureRelationshipsAsync(builder));
                            Append(await ConfigurePropertyOrdersAsync(builder));
                            Append(ConfigureSpecialTypes(builder));
                        },
                      modifier: Modifier.Override);
                    AppendLine();
                    if (Settings.GenerateEntitySets)
                    {
                        foreach (var propertyDefinition in entitySetDefinitions)
                        {
                            propertyDefinition.TypeInfo = new TypeInfo(propertyDefinition.EntitySet.GetDbSetName(NameMapper));
                            propertyDefinition.TypeInfo.ResolvedType = propertyDefinition.TypeInfo.EdmType;
                            Property(propertyDefinition, false);
                        }
                    }

                    foreach (var method in Schema.Functions)
                    {
                        PrintODataMethod(method, true);
                    }
                }, nameof(DataContext));
            foreach (var set in _entitySetDefinitions)
            {
                File.References.Add(set.Type);
                File.References.Add(new ODataTypeDefinition(set.GetDbSetName(NameMapper), $"./{_dbSetsPath}", true));
            }
            File.Contents = Contents();
            return File;
        }

        private string AsBackingFieldName(string name)
        {
            return $"_{name.Substring(0, 1).ToLower()}{name.Substring(1)}";
        }

        private string ConfigureSpecialTypes(EntityFunctionParameterDefinition builder)
        {
            var sb = new StringBuilder();
            var cr = Schema.EntityConfigurationDocument.CustomReportsDefinition as CustomReportsDefinition;
            if (cr != null)
            {
                sb.AppendLine($@"{builder.Name}.{nameof(IEntityConfigurationBuilder.CustomReportsDefinition)} = {nameof(IEntityConfigurationBuilder.CustomReportsDefinition)}.{nameof(CustomReportsDefinition.Define)}({builder.Name}.{nameof(IEntityConfigurationBuilder.EntityType)}<{cr.EntityConfiguration.Name}>(),
                _ => _.{cr.IdProperty.PropertyName},
                _ => _.{cr.UserIdProperty.PropertyName},
                _ => _.{cr.NameProperty.PropertyName},
                _ => _.{cr.EntityTypeProperty.PropertyName},
                _ => _.{cr.IqlProperty.PropertyName},
                _ => _.{cr.FieldsProperty.PropertyName},
                _ => _.{cr.SortProperty.PropertyName},
                _ => _.{cr.SortDescendingProperty.PropertyName},
                _ => _.{cr.SearchProperty.PropertyName}
                );");
            }
            var us = Schema.EntityConfigurationDocument.UserSettingsDefinition as UserSettingsDefinition;
            if (us != null)
            {
                sb.AppendLine($@"{builder.Name}.{nameof(IEntityConfigurationBuilder.UserSettingsDefinition)} = {nameof(IEntityConfigurationBuilder.UserSettingsDefinition)}.{nameof(UserSettingsDefinition.Define)}({builder.Name}.{nameof(IEntityConfigurationBuilder.EntityType)}<{us.EntityConfiguration.Name}>(),
                _ => _.{us.IdProperty.PropertyName},
                _ => _.{us.UserIdProperty.PropertyName},
                _ => _.{us.Key1Property.PropertyName},
                _ => _.{us.Key2Property.PropertyName},
                _ => _.{us.Key3Property.PropertyName},
                _ => _.{us.Key4Property.PropertyName},
                _ => _.{us.ValueProperty.PropertyName}
                );");
            }
            var u = Schema.EntityConfigurationDocument.UsersDefinition as UsersDefinition;
            if (u != null)
            {
                sb.AppendLine($@"{builder.Name}.{nameof(IEntityConfigurationBuilder.UsersDefinition)} = {nameof(IEntityConfigurationBuilder.UsersDefinition)}.{nameof(UsersDefinition.Define)}({builder.Name}.{nameof(IEntityConfigurationBuilder.EntityType)}<{u.EntityConfiguration.Name}>(),
                _ => _.{u.IdProperty.PropertyName},
                _ => _.{u.NameProperty.PropertyName}
                );");
            }
            return sb.ToString();
        }

        private IVariable GetAndAddTypeScriptTypeParameter(GeneratorTypeDefinition propertyType, List<IVariable> parameters)
        {
            IVariable typeParameter = null;
            if (OutputKind == OutputKind.TypeScript)
            {
                var genericParameter = propertyType.Name.AsTypeScriptTypeParameter();
                if (genericParameter == "Array")
                {
                    if (propertyType.Name.StartsWith("Array<"))
                    {
                        genericParameter = propertyType.Name.Substring("Array<".Length);
                        genericParameter = genericParameter.Substring(0, genericParameter.Length - 1);
                    }
                    else
                    {
                        genericParameter = propertyType.Name;
                    }
                }

                var enumType = Schema.EnumTypes.SingleOrDefault(t => t.EdmType == propertyType.Name);
                if (enumType != null)
                {
                    genericParameter =
                        $"new Enum<{enumType.Name}>({enumType.Name})";
                }

                typeParameter = new EntityFunctionParameterDefinition(genericParameter);
            }

            parameters.Add(typeParameter);
            return typeParameter;
        }

        private const string DefaultLambdaKey = "p";

        private async Task<string> ConfigureMetadataAsync(
            IMetadata metadata,
            IVariable propertyParameter = null,
            string lambdaKey = null,
            bool appendConfigure = true,
            IEntityMetadata sourceEntityConfiguration = null
            )
        {
            if (lambdaKey == null)
            {
                lambdaKey = DefaultLambdaKey;
            }
            if (metadata != null)
            {
                if (appendConfigure)
                {
                    AppendLine();
                    Dot();
                }
                var configureParameters = new List<IVariable>();
                if (propertyParameter != null)
                {
                    configureParameters.Add(propertyParameter);
                }
                var sb = new StringBuilder();
                var isProperty = metadata is IPropertyMetadata || metadata is IPropertyGroup;
                var metadataType = typeof(IEntityMetadata);
                var metadataSolidType = typeof(Server.Serialization.Deserialization.EntityConfiguration.EntityConfiguration);
                if (isProperty)
                {
                    if (metadata is IPropertyCollection)
                    {
                        metadataType = typeof(IPropertyCollection);
                        metadataSolidType = typeof(PropertyCollection);
                    }
                    else if (metadata is IPropertyPath)
                    {
                        metadataType = typeof(IPropertyPath);
                        metadataSolidType = typeof(PropertyPath);
                    }
                    else if (metadata is IPropertyMetadata)
                    {
                        metadataType = typeof(IPropertyMetadata);
                        metadataSolidType = typeof(Property);
                    }
                    else if (metadata is IRelationshipDetail)
                    {
                        metadataType = typeof(IRelationshipDetailMetadata);
                        metadataSolidType = typeof(RelationshipDetail);
                    }
                    else if (metadata is IFile)
                    {
                        metadataType = typeof(IFile);
                        metadataSolidType = typeof(File);
                    }
                    else
                    {
                        metadataType = typeof(IPropertyGroup);
                        metadataSolidType = typeof(PropertyCollection);
                    }
                }
                else
                {
                    if (metadata is IFilePreview)
                    {
                        metadataType = typeof(IFilePreview);
                        metadataSolidType = typeof(FilePreview);
                    }
                    else if (metadata is IEntityConfigurationContainer)
                    {
                        metadataType = typeof(IMetadata);
                        metadataSolidType = typeof(EntityConfigurationDocument);
                    }
                }
                var metadataProperties = metadataType.GetPublicProperties().ToArray();
                var propertyMetadata = metadata as IPropertyMetadata;
                var entityMetadata = metadata as IEntityMetadata;
                foreach (var metadataProperty in metadataProperties)
                {
                    if (metadata.Name == "Documents" && metadataProperty.Name == "EditKind")
                    {
                        int a = 0;
                    }
                    if (metadataProperty.Name == "Methods")
                    {
                        int a = 0;
                    }
                    var dealtWith = false;
                    string assign = null;
                    var assignIsAssign = true;
                    try
                    {
                        // This is a horrible hack to fix a null reference exception I can't figure out
                        // But sometimes, on the first attempt to get the "SearchKind" property, I
                        // get a null reference exception from somewhere. Repeating the GetValue fixes this......
                        var value1 = metadataProperty.GetValue(metadata);
                    }
                    catch { }
                    var value = metadataProperty.GetValue(metadata);
                    var nullAllowed =
                        metadata is IInferredValueConfiguration &&
                        metadataProperty.Name == nameof(IInferredValueConfiguration.InferredWithIql);
                    if (value == null && !nullAllowed)
                    {
                        continue;
                    }

                    // Default value is "true", so we don't need to do anything that will bloat the code
                    if (metadata is IEntityMetadata && metadataProperty.Name == nameof(IEntityConfiguration.ManageKind) &&
                        Equals(EntityManageKind.Full, value))
                    {
                        continue;
                    }

                    if (metadata is IRelationshipDetailMetadata &&
                        new[]
                        {
                            nameof(IRelationshipDetailMetadata.Property),
                            nameof(IRelationshipDetailMetadata.Kind),
                        }.Contains(metadataProperty.Name))
                    {
                        continue;
                    }

                    void PrintMapping<TMapping, T>(
                        Func<T, string> entityPropertyName,
                        string suffix = "")
                    where TMapping : IMapping<T>
                    {
                        var mappings = (IEnumerable<IMapping<T>>)value;
                        if (mappings != null)
                        {
                            foreach (var mapping in mappings)
                            {
                                sb.AppendLine($@"{lambdaKey}.{metadataProperty.Name}.{nameof(IList.Add)}(
new {typeof(TMapping).Name}({lambdaKey}) {{
    {nameof(IMapping<object>.Property)} = {lambdaKey}.{nameof(IRelationshipDetail.OtherSide)}.{nameof(IRelationshipDetail.EntityConfiguration)}.{nameof(IEntityConfiguration.FindProperty)}({String(entityPropertyName(mapping.Property))}){suffix},
    {nameof(IMapping<object>.Expression)} = {CSharpObjectSerializer.Serialize(mapping.Expression).Initialiser},
    {nameof(IMapping<object>.UseForFiltering)} = {mapping.UseForFiltering.ToString().ToLower()}
}});");
                            }
                        }
                    }

                    if (metadataProperty.Name == nameof(IMetadata.Permissions))
                    {
                        if (metadata.Permissions != null &&
                            metadata.Permissions.Keys != null &&
                            metadata.Permissions.Keys.Any())
                        {
                            sb.Append($"{lambdaKey}");
                            await IndentAsync(async () =>
                            {
                                sb.Append($".{nameof(IUserPermission.Permissions)}");
                                foreach (var permissionKey in metadata.Permissions.Keys)
                                {
                                    sb.Append($".{nameof(UserPermissionsCollection.UseRule)}(\"{permissionKey}\")");
                                }
                                sb.Append(";");
                            });
                        }
                        dealtWith = true;
                    }

                    if (metadata is IProperty property1 &&
                        metadataProperty.Name == nameof(IPropertyMetadata.SearchKind))
                    {
                        if (property1.AutoSearchKind)
                        {
                            dealtWith = true;
                        }
                    }
                    var useAutoResolve = true;
                    if (metadata is IPropertyGroup propertyGroup &&
                        metadataProperty.Name == nameof(IPropertyGroup.CanWrite))
                    {
                        useAutoResolve = false;
                        if (!propertyGroup.CanWriteSet)
                        {
                            dealtWith = true;
                        }
                    }
                    if (metadata is IEntityConfiguration ec &&
                        metadataProperty.Name == nameof(IEntityConfiguration.TitlePropertyName))
                    {
                        if (ec.AutoTitleProperty)
                        {
                            dealtWith = true;
                        }
                    }

                    if (!dealtWith)
                    {
                        if (metadata is IRelationshipDetailMetadata &&
                            metadataProperty.Name == nameof(IRelationshipDetail.ValueMappings))
                        {
                            PrintMapping<ValueMapping, IProperty>(_ => _.Name);
                            dealtWith = true;
                        }
                        else if (metadata is IRelationshipDetailMetadata &&
                            metadataProperty.Name == nameof(IRelationshipDetail.RelationshipMappings))
                        {
                            PrintMapping<RelationshipMapping, IRelationshipDetail>(_ => _.Property.Name, $".{nameof(IProperty.Relationship)}.{nameof(EntityRelationship.ThisEnd)}");
                            dealtWith = true;
                        }
                        else if (!isProperty && metadataProperty.Name == nameof(IEntityMetadata.DisplayConfigurations))
                        {
                            dealtWith = true;
                            // We will do this at the end when we're sure all relationships and other
                            // property groups are configured
                        }
                        else if (!isProperty && metadataProperty.Name == nameof(IEntityMetadata.Geographics))
                        {
                            dealtWith = true;
                            if (entityMetadata.Geographics?.Any() == true)
                            {
                                foreach (var geographic in entityMetadata.Geographics)
                                {
                                    sb.AppendLine();
                                    sb.Append($"{lambdaKey}.{nameof(EntityConfiguration<object>.HasGeographic)}(");
                                    sb.Append($"{lambdaKey}_g => {lambdaKey}_g.{geographic.LongitudeProperty.Name}, {lambdaKey}_g => {lambdaKey}_g.{geographic.LatitudeProperty.Name}");
                                    sb.Append($@", {String(geographic.Key)}");
                                    sb.Append($@", {await ConfigureMetadataAsync(geographic, null, "geo", false)}");
                                    sb.Append(");");
                                }
                            }
                        }
                        else if (!isProperty && metadataProperty.Name == nameof(IEntityMetadata.DateRanges))
                        {
                            dealtWith = true;
                            if (entityMetadata.DateRanges?.Any() == true)
                            {
                                foreach (var dateRange in entityMetadata.DateRanges)
                                {
                                    sb.AppendLine();
                                    sb.Append($"{lambdaKey}.{nameof(EntityConfiguration<object>.HasDateRange)}(");
                                    var parameters = new[]
                                    {
                                        dateRange.StartDateProperty,
                                        dateRange.EndDateProperty
                                    };
                                    var parameterStrings =
                                        parameters.Select(p => p == null ? "null" : $"{lambdaKey}_ns => {lambdaKey}_ns.{p.Name}")
                                            .ToList();
                                    parameterStrings.Add(String(dateRange.Key));
                                    parameterStrings.Add(await ConfigureMetadataAsync(dateRange, null, $"{lambdaKey}_ns", false));
                                    sb.Append(string.Join(", ", parameterStrings));
                                    sb.Append(");");
                                }
                            }
                        }
                        else if (!isProperty && metadataProperty.Name == nameof(IEntityMetadata.Files))
                        {
                            dealtWith = true;
                            if (entityMetadata.Files?.Any() == true)
                            {
                                foreach (var file in entityMetadata.Files)
                                {
                                    sb.AppendLine();
                                    sb.Append($"{lambdaKey}.{nameof(EntityConfiguration<object>.HasFile)}(");
                                    var parameters = new[]
                                    {
                                    file.UrlProperty
                                };
                                    var subLambda = $"{lambdaKey}_f";
                                    var parameterStrings =
                                        parameters.Select(p => p == null ? "null" : $"{subLambda} => {subLambda}.{p.Name}")
                                            .ToList();
                                    var config = await ConfigureMetadataAsync(file, null, subLambda, false);
                                    if (string.IsNullOrWhiteSpace(config))
                                    {
                                        config = $"{subLambda} => {{}}";
                                    }
                                    var allParams = new[]
                                    {
                                    parameterStrings[0],
                                    config
                                };
                                    sb.Append(string.Join(", ", allParams));
                                    sb.Append(");");
                                }
                            }
                        }
                        else if (!isProperty && metadataProperty.Name == nameof(IEntityMetadata.NestedSets))
                        {
                            dealtWith = true;
                            if (entityMetadata.NestedSets?.Any() == true)
                            {
                                foreach (var nestedSet in entityMetadata.NestedSets)
                                {
                                    sb.AppendLine();
                                    sb.Append($"{lambdaKey}.{nameof(EntityConfiguration<object>.HasNestedSet)}(");
                                    var parameters = new[]
                                    {
                                    nestedSet.LeftProperty,
                                    nestedSet.RightProperty,
                                    nestedSet.LeftOfProperty,
                                    nestedSet.RightOfProperty,
                                    nestedSet.KeyProperty,
                                    nestedSet.LevelProperty,
                                    nestedSet.ParentIdProperty,
                                    nestedSet.ParentProperty,
                                    nestedSet.IdProperty
                                };
                                    var parameterStrings =
                                        parameters.Select(p => p == null ? "null" : $"{lambdaKey}_ns => {lambdaKey}_ns.{p.Name}")
                                            .ToList();
                                    parameterStrings.Add(String(nestedSet.SetKey));
                                    parameterStrings.Add(String(nestedSet.Key));
                                    parameterStrings.Add(await ConfigureMetadataAsync(nestedSet, null, $"{lambdaKey}_ns", false));
                                    sb.Append(string.Join(", ", parameterStrings));
                                    sb.Append(");");
                                }
                            }
                        }
                        else if (metadataProperty.CanWrite && metadataProperty.PropertyType == typeof(string))
                        {
                            if (!useAutoResolve || !IsDefaultValue(metadataProperty, value, metadataSolidType))
                            {
                                assign = String(value as string);
                            }
                            dealtWith = true;
                        }
                        else if (metadataProperty.CanWrite && metadataProperty.PropertyType == typeof(bool))
                        {
                            if (!useAutoResolve || !IsDefaultValue(metadataProperty, value, metadataSolidType))
                            {
                                assign = value.ToString().ToLower();
                            }
                            dealtWith = true;
                        }
                        else if (metadataProperty.CanWrite && metadataProperty.PropertyType == typeof(bool?))
                        {
                            if (Equals(value, true))
                            {
                                assign = "true";
                            }
                            else if (Equals(value, false))
                            {
                                assign = "false";
                            }
                            dealtWith = true;
                        }
                        else if (metadataProperty.CanWrite && metadataProperty.PropertyType.IsEnumOrNullableEnum())
                        {
                            if (EnumExtensions.IsValidEnumValue(value))
                            {
                                if (!useAutoResolve || !IsDefaultValue(metadataProperty, value, metadataSolidType))
                                {
                                    assign = value.ToEnumCodeString();
                                }
                            }
                            dealtWith = true;
                        }
                        else if (metadataProperty.Name == nameof(IPropertyMetadata.InferredValueConfigurations))
                        {
                            var output = CSharpObjectSerializer.Serialize(value);
                            sb.Append($"{lambdaKey}.{metadataProperty.Name} = {output.Initialiser};");
                            dealtWith = true;
                        }
                        else if (typeof(IFile).IsAssignableFrom(metadataProperty.DeclaringType) && metadataProperty.Name == nameof(IFile.Previews))
                        {
                            var filePreviews = value as IList<IFilePreview>;
                            if (filePreviews != null && filePreviews.Any())
                            {
                                var sbFilePreviews = new StringBuilder();
                                foreach (var filePreview in filePreviews)
                                {
                                    sbFilePreviews.Append($"{lambdaKey}.{nameof(File<object>.AddPreview)}(fp => fp.{filePreview.UrlProperty.PropertyName}, {(filePreview.MaxWidth == null ? "null" : filePreview.MaxWidth.Value.ToString())}, {(filePreview.MaxHeight == null ? "null" : filePreview.MaxHeight.Value.ToString())}, {String(filePreview.Key)}, {await ConfigureMetadataAsync(filePreview, null, "fp", false)});");
                                }

                                assign = sbFilePreviews.ToString();
                            }

                            assignIsAssign = false;
                            dealtWith = true;
                        }
                        else if (!isProperty && entityMetadata != null && metadataProperty.Name == nameof(IEntityMetadata.PersistenceKeyProperty))
                        {
                            if (entityMetadata.PersistenceKeyProperty != null)
                            {
                                sb.Append(
                                    $"{lambdaKey}.{nameof(IEntityMetadata.PersistenceKeyProperty)} = {lambdaKey}.{nameof(IEntityConfiguration.FindProperty)}(\"{entityMetadata.PersistenceKeyProperty.PropertyName}\");");
                            }
                            dealtWith = true;
                        }
                        else if (metadataProperty.CanWrite && metadataProperty.PropertyType.IsEnumerableType())
                        {
                            var enumerable = value as IEnumerable;
                            if (enumerable != null && enumerable.Cast<object>().Any())
                            {
                                var elementType = metadataProperty.PropertyType.GetGenericArguments()[0];
                                if (elementType.IsClass && elementType != typeof(string))
                                {
                                    var serialized = CSharpObjectSerializer.Serialize(enumerable);
                                    // HACK ALERT
                                    if (OutputKind == OutputKind.CSharp)
                                    {
                                        assign = serialized.Initialiser;
                                    }
                                    else
                                    {
                                        var typeScript = await ConvertToTypeScriptAsync(serialized.Class);
                                        var trimmed = typeScript.Substring(0, typeScript.LastIndexOf("return instance;"));
                                        var init = "let instance = ";
                                        trimmed = trimmed.Substring(trimmed.IndexOf(init) + init.Length);
                                        assign = trimmed;
                                    }
                                }
                                else
                                {
                                    if (elementType == typeof(string))
                                    {
                                        var arr = enumerable.Cast<string>().ToArray();
                                        if (arr.Any())
                                        {
                                            switch (OutputKind)
                                            {
                                                case OutputKind.CSharp:
                                                    assign =
                                                        $"new List<string>(new [] {{ {string.Join(", ", arr.Select(String))} }})";
                                                    break;
                                                case OutputKind.TypeScript:
                                                    assign =
                                                        $"[{string.Join(", ", arr.Select(String))}]";
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                            dealtWith = true;
                        }
                        else if (typeof(IMediaKey).IsAssignableFrom(metadataProperty.PropertyType))
                        {
                            // Special case
                            dealtWith = true;
                            var mediaKey = metadata.GetPropertyValueByNameAs<IMediaKey>(nameof(IFile.MediaKey));
                            if (mediaKey?.Groups?.Any() == true)
                            {
                                sb.AppendLine($"{lambdaKey}.{nameof(IFile.MediaKey)}");
                                foreach (var group in mediaKey.Groups)
                                {
                                    if (group.Parts != null && group.Parts.Any())
                                    {
                                        sb.AppendLine($".{nameof(MediaKey<object>.AddGroup)}({lambdaKey}_g => {lambdaKey}_g");
                                        foreach (var part in group.Parts)
                                        {
                                            sb.AppendLine(
                                                part.IsPropertyPath
                                                    ? $".{nameof(MediaKeyGroup<object>.AddPropertyPath)}({lambdaKey}_g_ => {lambdaKey}_g_.{part.Key.Replace("/", ".")})"
                                                    : $@".{nameof(MediaKeyGroup<object>.AddString)}(""{part.Key}"")");
                                        }

                                        sb.Append(")");
                                    }
                                }

                                sb.Append(";");
                            }
                        }
                        else if (metadataProperty.Name == nameof(IMetadata.Metadata))
                        {
                            dealtWith = true;
                            if (metadata.Metadata != null && metadata.Metadata.All.Any())
                            {
                                sb.AppendLine();
                                sb.Append($"{lambdaKey}.{nameof(IMetadata.Metadata)}.");
                                var items = new List<string>();
                                foreach (var item in metadata.Metadata.All)
                                {
                                    var os = new CSharpObjectSerializer();
                                    var serialized = os.Serialize(item.Value);
                                    items.Add($"{nameof(IMetadataCollection.Set)}({String(item.Key)}, {serialized.Initialiser})");
                                }
                                sb.Append($"{string.Join(".", items)};");
                            }
                        }
                        else if (metadata is IFileUrlBase &&
                                 typeof(IProperty).IsAssignableFrom(metadataProperty.PropertyType))
                        {
                            dealtWith = true;
                            var property = (IProperty)metadataProperty.GetValue(metadata);
                            if (property != null)
                            {
                                sb.AppendLine($"{lambdaKey}.{metadataProperty.Name} = {DefaultLambdaKey}.{nameof(EntityConfiguration<object>.FindProperty)}({String(property.Name)});");
                            }
                        }
                        else if (metadataProperty.CanWrite && (metadataProperty.PropertyType.IsClass || metadataProperty.PropertyType.IsInterface) && metadataProperty.PropertyType != typeof(string))
                        {
                            var skip = false;
                            if (!isProperty)
                            {
                                switch (metadataProperty.Name)
                                {
                                    case nameof(IEntityConfiguration.EntityValidation):
                                    case nameof(IEntityConfiguration.DisplayFormatting):
                                        skip = true;
                                        break;
                                }
                            }
                            else
                            {
                                switch (metadataProperty.Name)
                                {
                                    case nameof(IPropertyMetadata.TypeDefinition):
                                    case nameof(IPropertyMetadata.ValidationRules):
                                    case nameof(IPropertyMetadata.DisplayRules):
                                    case nameof(IPropertyMetadata.RelationshipFilterRules):
                                        skip = true;
                                        break;
                                }
                            }
                            dealtWith = true;
                            if (!skip && value != null)
                            {
                                var serialized = CSharpObjectSerializer.Serialize(value);
                                // HACK ALERT
                                if (OutputKind == OutputKind.CSharp)
                                {
                                    assign = serialized.Initialiser;
                                }
                                else
                                {
                                    var typeScript = await ConvertToTypeScriptAsync(serialized.Class);
                                    var trimmed = typeScript.Substring(0, typeScript.LastIndexOf("return "));
                                    var init = "let instance = ";
                                    trimmed = trimmed.Substring(trimmed.IndexOf(init) + init.Length);
                                    assign = trimmed;
                                }
                            }
                        }
                    }

                    if (assign != null)
                    {
                        sb.Append("\r\n");
                        sb.Append(GetIndentPlusOne());
                        if (assignIsAssign)
                        {
                            sb.Append($"{lambdaKey}.{metadataProperty.Name} = ");
                        }
                        sb.Append($"{assign};");
                    }
                    else if (!Equals(value, false) && !dealtWith && metadataProperty.CanWrite)
                    {
                        sb.Append("\r\n");
                        sb.Append(GetIndentPlusOne());
                        sb.Append($"// {lambdaKey}.{metadataProperty.Name} = ???;");
                    }
                }

                var body = sb.ToString();
                if (string.IsNullOrWhiteSpace(body.Trim()))
                {
                    return null;
                }
                var finalSb = new StringBuilder();
                finalSb.Append($"{lambdaKey} => {{");
                finalSb.Append(body);
                finalSb.Append("\r\n");
                finalSb.Append(GetCurrentIndent());
                finalSb.Append("}");
                //if (OutputType == OutputType.TypeScript && typeParameter != null)
                //{
                //    configureParameters.Add(typeParameter);
                //}
                if (appendConfigure)
                {
                    configureParameters.Add(
                        new PropertyDefinition(finalSb.ToString()));
                    MethodCall(
                                      isProperty
                                          ? nameof(EntityConfiguration<object>.ConfigureProperty)
                                          : nameof(EntityConfiguration<object>.Configure),
                                      false,
                                      configureParameters.ToArray()
                                  );
                }

                return finalSb.ToString();
                /*
                .ConfigureProperty(p => p.PhoneNumber, metadata =>{
                    metadata.Description = "";
                    return metadata;
                })
                                               */
            }

            return "";
        }

        private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();
        private bool IsDefaultValue(MemberInfo metadataProperty, object value, Type metadataSolidType)
        {
            if (!_instances.ContainsKey(metadataSolidType))
            {
                object instance = null;
                if (metadataSolidType == typeof(PropertyCollection))
                {
                    instance = new PropertyCollection(null);
                }
                else if (metadataSolidType == typeof(PropertyPath))
                {
                    instance = new PropertyPath(null, null);
                }
                else if (typeof(IEntityConfiguration).IsAssignableFrom(metadataSolidType))
                {
                    instance = new Server.Serialization.Deserialization.EntityConfiguration.EntityConfiguration(null);
                }
                else
                {
                    instance = Activator.CreateInstance(metadataSolidType);
                }
                _instances.Add(metadataSolidType, instance);
            }
            return Equals(_instances[metadataSolidType].GetPropertyValue(metadataProperty.Name),
                value);
        }

        private async Task<string> ConfigurePropertyOrdersAsync(IVariable builder)
        {
            var sb = new StringBuilder();
            foreach (var config in Schema.EntityConfigurations)
            {
                var entityMetadata = config.Value;
                foreach (var displayConfig in entityMetadata.DisplayConfigurations)
                {
                    await ConfigureDisplaySettingAsync(builder, displayConfig, sb, config, nameof(EntityConfiguration<object>.SetDisplay), entityMetadata);
                }
            }
            return sb.ToString();
        }

        private async Task ConfigureDisplaySettingAsync(
            IVariable builder,
            DisplayConfiguration displayConfiguration,
            StringBuilder sb,
            KeyValuePair<string, IEntityConfiguration> config,
            string displaySettingMethodName,
            IEntityMetadata entityMetadata)
        {
            if (displayConfiguration != null && displayConfiguration.Properties?.Any() == true)
            {
                var serializedPropertyGroups = new List<string>();
                foreach (var property in displayConfiguration.Properties)
                {
                    serializedPropertyGroups.Add(await SerializePropertyGroupsAsync(property, entityMetadata, 0));
                }
                var properties = string.Join(",\n", serializedPropertyGroups);
                var configurator = $@"(ec, displayConfiguration) => {{
                    displayConfiguration.{nameof(DisplayConfiguration.SetProperties)}(
                        ec,
                        {properties});
            }}";
                sb.AppendLine(
                    $"{GetEntityTypeConfiguration(builder, config.Key)}.{displaySettingMethodName}({String(displayConfiguration.Key)}, {nameof(DisplayConfigurationKind)}.{displayConfiguration.Kind}, {configurator});");
            }
        }

        private async Task<string> ConfigureRelationshipsAsync(IVariable builder)
        {
            var lambdaKey = "rel";
            var result = new StringBuilder();
            foreach (var config in Schema.EntityConfigurations)
            {
                var sb = new StringBuilder();
                foreach (var relationship in config.Value.Relationships)
                {
                    var source = relationship.Source as RelationshipDetail;
                    var target = relationship.Target as RelationshipDetail;
                    foreach (var detail in new[] { source, target })
                    {
                        var entityConfig = Schema.EntityConfigurations.Single(ec =>
                            ec.Value.Properties.Contains(detail.Property)).Value;

                        if (entityConfig != config.Value)
                        {
                            continue;
                        }

                        // TODO: Get the relationship and set lamdba etc.
                        var nestedLambdaKey = $"{lambdaKey}_p";
                        var method =
                                detail.Property.TypeDefinition.IsCollection
                                    ? nameof(EntityConfiguration<object>.FindCollectionRelationship)
                                    : nameof(EntityConfiguration<object>.FindRelationship)
                            ;
                        if (method == "FindCollectionRelationship" && detail.Property.Name == "Documents")
                        {
                            int a = 0;
                        }
                        var configured = await ConfigureMetadataAsync(detail, null, $"{lambdaKey}_cnf", false, entityConfig);
                        if (!string.IsNullOrWhiteSpace(configured))
                        {
                            sb.AppendLine();
                            sb.Append($"{lambdaKey}.{method}({nestedLambdaKey} => {nestedLambdaKey}.{detail.Property.Name})");
                            sb.Append($@".{nameof(IRelationshipDetail.Configure)}({configured});");
                        }
                    }
                }

                var relationshipConfigurations = sb.ToString();
                if (!string.IsNullOrWhiteSpace(relationshipConfigurations))
                {
                    result.AppendLine(
                        RunWithinEntityTypeConfiguration(builder, config.Key, lambdaKey, () => relationshipConfigurations)
                        );
                }
            }

            return result.ToString();
        }

        private string RunWithinEntityTypeConfiguration(IVariable builder, string entityTypeName,
            string lambdaKey,
            Func<string> content)
        {
            return $@"{GetEntityTypeConfiguration(builder, entityTypeName)}.{nameof(EntityConfiguration<object>.Configure)}({lambdaKey} => {{
{content()}
}});";
        }

        private string GetEntityTypeConfiguration(IVariable builder, string entityTypeName)
        {
            return $@"{builder.Name}.{nameof(EntityConfigurationBuilder.EntityType)}<{NameMapper(entityTypeName)}>()";
        }
        private async Task<string> SerializePropertyGroupsAsync(IPropertyGroup propertyGroup, IEntityMetadata entityMetadata, int index)
        {
            var groupSb = new StringBuilder();

            string Lambda(int i, bool inline = true)
            {
                var indexCh = i == 0 ? "" : i.ToString();
                var inlineStr = $"_{indexCh}.";
                return $"_{indexCh} => {(inline ? inlineStr : "")}";
            }
            groupSb.Append(Lambda(index));
            if (propertyGroup is IGeographicPoint)
            {
                groupSb.Append(
                    $"{nameof(IEntityMetadata.Geographics)}[{entityMetadata.Geographics.IndexOf(propertyGroup as IGeographicPoint)}]");
            }
            else if (propertyGroup is INestedSet)
            {
                groupSb.Append(
                    $"{nameof(IEntityMetadata.NestedSets)}[{entityMetadata.NestedSets.IndexOf(propertyGroup as INestedSet)}]");
            }
            else if (propertyGroup is IRelationshipDetailMetadata)
            {
                var rel = propertyGroup as IRelationshipDetailMetadata;
                groupSb.Append($"{(rel.IsCollection ? nameof(EntityConfiguration<object>.FindCollectionRelationship) : nameof(EntityConfiguration<object>.FindRelationship))}({Lambda(++index)}{rel.Property.Name})");
            }
            else if (propertyGroup is IPropertyPath)
            {
                var propertyPath = propertyGroup as IPropertyPath;
                var lambda = $"{Lambda(++index)}";
                groupSb.Append($"{nameof(EntityConfiguration<object>.PropertyPath)}({lambda}{propertyPath.Path.Replace("/", ".")})");
                groupSb.Append($@".{nameof(PropertyGroupBase<IPropertyPath>.Configure)}({await ConfigureMetadataAsync(propertyPath, null, $"coll{++index}", false, entityMetadata)})");
            }
            else if (propertyGroup is IPropertyCollection)
            {
                var coll = propertyGroup as IPropertyCollection;
                var list = new List<string>();
                foreach (var subGroup in coll.Properties)
                {
                    list.Add(await SerializePropertyGroupsAsync(subGroup, entityMetadata, ++index));
                }

                groupSb.Append($"{nameof(EntityConfiguration<object>.PropertyCollection)}({string.Join(",\n", list)})");
                var collectionMetadata = await ConfigureMetadataAsync(coll, null, $"coll{++index}", false, entityMetadata);
                if (!string.IsNullOrWhiteSpace(collectionMetadata))
                {
                    groupSb.Append($@".{nameof(PropertyGroupBase<IPropertyCollection>.Configure)}({collectionMetadata})");
                }
            }
            else
            {
                groupSb.Append($"{nameof(EntityConfiguration<object>.FindPropertyByExpression)}({Lambda(++index)}{propertyGroup.Name})");
            }

            return groupSb.ToString();
        }

        private async Task<string> ConvertToTypeScriptAsync(string serialized)
        {
            var outputSelector = new OutputSelector("", "SerializedObject", "GetData", OutputSelectorMode.Inner);
            var settings = new DefaultConversionSettings
            {
                Compile = false,
                GenerateImports = true,
                WriteToDisk = false,
                ResolveCircularDependencies = false,
                OutputClassFunctionsDeclared = false,
                OutputClassInterfacesImplemented = false,
                OutputClassNameStaticProperty = false,
                OutputClassPropertiesDeclared = false,
                OutputJsonClassConversion = false,
                PrintOutput = false,
                PrintOutputFiles = false,
                WrapGettersAndSetters = false,
                OutputSelector = outputSelector
            };
            await settings.MetadataReferences.AddReferenceAsync<HelpText>();
            await settings.MetadataReferences.AddReferenceAsync<ValidationRuleCollection>();
            var task = CSharpToTypescriptConverter.ConvertToTypeScriptAsync(
                serialized,
                settings);
            task.Wait();
            var typescript = task.Result;
            return outputSelector.Extracted;
        }
    }
}