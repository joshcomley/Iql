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
using System.Text;
using TypeSharp;
using TypeSharp.Conversion;
using TypeSharp.Extensions;
using EnumExtensions = Iql.OData.TypeScript.Generator.Extensions.EnumExtensions;
using IPropertyCollection = Iql.Entities.IPropertyCollection;
using IPropertyGroup = Iql.Entities.IPropertyGroup;
using PropertyCollection = Iql.Entities.PropertyCollection;
using RelationshipDetail = Iql.Server.Serialization.RelationshipDetail;
using TypeInfo = Iql.OData.TypeScript.Generator.Definitions.TypeInfo;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class DataContextGenerator : ClassGenerator
    {
        private List<RelationshipDetail> RelationshipDetailsDealtWith { get; } = new List<RelationshipDetail>();
        private CSharpObjectSerializer CSharpObjectSerializer { get; }
        private readonly string _className;
        private readonly string _dbSetsPath;
        private readonly IEnumerable<EntitySetDefinition> _entitySetDefinitions;
        private readonly string _genericParameters;
        private readonly string _namespace;

        public DataContextGenerator(ODataSchema schema,
            string @namespace,
            string className,
            string dbSetsPath,
            string genericParameters,
            IEnumerable<EntitySetDefinition> entitySetDefinitions,
            OutputType outputType,
            GeneratorSettings settings) : base(schema, outputType, settings)
        {
            _entitySetDefinitions = entitySetDefinitions;
            _namespace = @namespace;
            _genericParameters = genericParameters;
            _className = className;
            _dbSetsPath = dbSetsPath;
            CSharpObjectSerializer = new CSharpObjectSerializer();
        }

        public GeneratedFile Generate()
        {
            File.FileName = _namespace;
            File.Namespace = _namespace;
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
            Class(
                _className,
                _namespace,
                _genericParameters,
                () =>
                {
                    var ctorParams = new IVariable[]
                    {
                        new EntityFunctionParameterDefinition("dataStore", TypeResolver.TranslateType(typeof(IDataStore))),
                    }.ToList();
                    if (OutputType == OutputType.TypeScript)
                    {
                        ctorParams.Add(new EntityFunctionParameterDefinition("evaluateContext", TypeResolver.TranslateType(typeof(EvaluateContext), "nullable"), true));
                    }
                    Constructor(ctorParams, () =>
                    {
                        if (Settings.GenerateEntitySets)
                        {
                            foreach (var propertyDefinition in entitySetDefinitions)
                            {
                                var asDbSetParameters = $"{propertyDefinition.EntityType}, {propertyDefinition.KeyType.AsTypeScriptTypeParameter()}, {propertyDefinition.EntitySet.GetDbSetName(NameMapper)}";
                                AssignProperty(propertyDefinition,
                                    $"this.{nameof(Data.Context.DataContext.AsCustomDbSet)}{(OutputType == OutputType.CSharp ? $"<{asDbSetParameters}>" : "")}({(OutputType == OutputType.TypeScript ? asDbSetParameters : "")})");
                                AppendLine();
                            }

                            if (Settings.ConfigureOData)
                            {
                                foreach (var entitySet in _entitySetDefinitions)
                                {
                                    AppendLine($"this.{odataConfigurationPropertyName}.{nameof(ODataConfiguration.RegisterEntitySet)}<{NameMapper(entitySet.Type.Name)}>({NameOf(entitySet.Name)}{(OutputType == OutputType.TypeScript ? $", {entitySet.Type.Name}" : "")});");
                                }
                            }
                        }
                        if (Settings.ConfigureOData)
                        {
                            AppendLine($"this.{nameof(IDataContext.RegisterConfiguration)}<{nameof(ODataConfiguration)}>(this.{odataConfigurationPropertyName}{(OutputType == OutputType.TypeScript ? $", {nameof(ODataConfiguration)}" : "")});");
                        }
                    },
                        ctorParams);
                    if (Settings.ConfigureOData)
                    {
                        Property("public", odataConfigurationPropertyName, TypeResolver.TranslateType(typeof(ODataConfiguration)), null, true);
                    }
                    AppendLine();
                    var builder = new EntityFunctionParameterDefinition("builder", TypeResolver.TranslateType(typeof(EntityConfigurationBuilder)));
                    builder.IsLocal = true;
                    Method(nameof(Data.Context.DataContext.Configure), new[] { builder }, TypeResolver.TranslateType(typeof(void)), () =>
                      {
                          foreach (var entityDefinition in _entitySetDefinitions)
                          {
                              var entityTypeName = entityDefinition.Type.Name;
                              var entityConfiguration =
                                  Schema.EntityConfigurations.ContainsKey(entityTypeName)
                                  ? Schema.EntityConfigurations[entityTypeName]
                                  : null;
                              var defineEntityParameters =
                                  OutputType == OutputType.TypeScript ?
                              new[]
                              {
                                new EntityFunctionParameterDefinition(entityTypeName,
                                    new TypeInfo())
                              } : null;
                              var defineEntityName = nameof(EntityConfigurationBuilder.EntityType);
                              if (OutputType == OutputType.CSharp)
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
                              Indent(() =>
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
                                      if (OutputType == OutputType.TypeScript)
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
                                      }
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

                                      var typeParameter = GetAndAddTypeScriptTypeParameter(propertyType, parameters);
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
                                          ConfigreMetadata(propertyMetadata, parameters.First());
                                      }
                                  }
                                  if (entityConfiguration != null)
                                  {
                                      if (entityConfiguration.EntityValidation != null)
                                      {
                                          foreach (var validation in entityConfiguration.EntityValidation.All)
                                          {
                                              var expression = validation.GetPropertyValueByNameAs<IqlExpression>("ExpressionIql");
                                              AppendLine();
                                              Dot();
                                              MethodCall(nameof(EntityConfiguration<object>.DefineEntityValidation),
                                                  false,
                                                  new[]
                                                  {
                                                  new EntityFunctionParameterDefinition(
                                                      GetExpressionString(expression)),
                                                  new EntityFunctionParameterDefinition(
                                                      String(validation.Key)),
                                                  new EntityFunctionParameterDefinition(
                                                      String(validation.Message)),
                                                  });
                                          }
                                      }
                                      if (entityConfiguration.DisplayFormatting != null)
                                      {
                                          foreach (var formatter in entityConfiguration.DisplayFormatting.All)
                                          {
                                              var expression = formatter.GetPropertyValueByNameAs<IqlExpression>("FormatterExpressionIql");
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
                                                  var expression = validation.GetPropertyValueByNameAs<IqlExpression>("ExpressionIql");
                                                  var propertyExpression = new IqlPropertyExpression(property.Name, null, IqlType.Unknown);
                                                  var iqlRootReference = new IqlRootReferenceExpression("e", "");
                                                  propertyExpression.Parent = iqlRootReference;
                                                  AppendLine();
                                                  Dot();
                                                  MethodCall(nameof(EntityConfiguration<object>.DefinePropertyValidation),
                                                      false,
                                                      new[]
                                                      {
                                                      new EntityFunctionParameterDefinition(
                                                          "p => p." + propertyExpression.PropertyName),
                                                      new EntityFunctionParameterDefinition(
                                                          GetExpressionString(expression)),
                                                      new EntityFunctionParameterDefinition(
                                                          String(validation.Key)),
                                                      new EntityFunctionParameterDefinition(
                                                          String(validation.Message)),
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
                                          if (OutputType == OutputType.TypeScript)
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

                              VariableAccessor(builder, () =>
                              {
                                  MethodCall(
                                      defineEntityName,
                                      false,
                                      defineEntityParameters
                                  );
                                  AppendLine();
                                  ConfigreMetadata(entityConfiguration);
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
                          }
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

        private IVariable GetAndAddTypeScriptTypeParameter(GeneratorTypeDefinition propertyType, List<IVariable> parameters)
        {
            IVariable typeParameter = null;
            if (OutputType == OutputType.TypeScript)
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

        private string ConfigreMetadata(IMetadata metadata,
            IVariable propertyParameter = null,
            string lambdaKey = "p",
            bool appendConfigure = true
            )
        {
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
                sb.Append($"{lambdaKey} => {{");
                var isProperty = metadata is IPropertyMetadata || metadata is IPropertyGroup;
                var metadataType = typeof(IEntityMetadata);
                var metadataSolidType = typeof(Server.Serialization.EntityConfiguration);
                if (isProperty)
                {
                    if (metadata is IPropertyCollection)
                    {
                        metadataType = typeof(IPropertyCollection);
                    }
                    else if (metadata is IPropertyMetadata)
                    {
                        metadataType = typeof(IPropertyMetadata);
                    }
                    else
                    {
                        metadataType = typeof(IPropertyGroup);
                    }
                    metadataSolidType = metadata is IPropertyMetadata ? typeof(Property) : typeof(PropertyCollection);
                }
                var metadataProperties = metadataType.GetPublicProperties().ToArray();
                var propertyGroupMetadata = metadata as IPropertyGroup;
                var propertyMetadata = metadata as IPropertyMetadata;
                var entityMetadata = metadata as IEntityMetadata;
                foreach (var metadataProperty in metadataProperties)
                {
                    var dealtWith = false;
                    string assign = null;
                    try
                    {
                        // This is a horrible hack to fix a null reference exception I can't figure out
                        // But sometimes, on the first attempt to get the "SearchKind" property, I
                        // get a null reference exception from somewhere. Repeating the GetValue fixes this......
                        var value1 = metadataProperty.GetValue(metadata);
                    }
                    catch { }
                    var value = metadataProperty.GetValue(metadata);
                    if (value == null)
                    {
                        continue;
                    }

                    // Default value is "true", so we don't need to do anything that will bloat the code
                    if (metadata is IEntityMetadata && metadataProperty.Name == nameof(IEntityConfiguration.ManageKind) &&
                        Equals(EntityManageKind.Full, value))
                    {
                        continue;
                    }

                    if (!isProperty && metadataProperty.Name == nameof(IEntityMetadata.PropertyOrder))
                    {
                        dealtWith = true;
                        if (entityMetadata.PropertyOrder?.Any() == true)
                        {
                            // This needs to be recursive, with an incrementing lambda key
                            sb.AppendLine();
                            sb.Append($"{lambdaKey}.{nameof(EntityConfiguration<object>.SetPropertyOrder)}(");
                            var propertyGroups = new List<string>();
                            foreach (var propertyGroup in entityMetadata.PropertyOrder)
                            {
                                var groupSb = SerializePropertyGroups(propertyGroup, entityMetadata, 0);
                                propertyGroups.Add(groupSb);
                            }

                            sb.Append(string.Join(",\n", propertyGroups));
                            sb.Append(");");
                        }
                    }
                    if (!isProperty && metadataProperty.Name == nameof(IEntityMetadata.Geographics))
                    {
                        dealtWith = true;
                        if (entityMetadata.Geographics?.Any() == true)
                        {
                            foreach (var geographic in entityMetadata.Geographics)
                            {
                                sb.AppendLine();
                                sb.Append($"{lambdaKey}.{nameof(EntityConfiguration<object>.HasGeographic)}(");
                                sb.Append($"{lambdaKey}_g => {lambdaKey}_g.{geographic.LongitudeProperty.Name}, {lambdaKey}_g => {lambdaKey}_g.{geographic.LatitudeProperty.Name}");
                                sb.Append($@", {(geographic.Key == null ? "null" : $@"""{geographic.Key}""")}");
                                sb.Append($@", {ConfigreMetadata(geographic, null, "geo", false)}");
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
                                parameterStrings.Add(ConfigreMetadata(nestedSet, null, $"{lambdaKey}_ns", false));
                                sb.Append(string.Join(", ", parameterStrings));
                                sb.Append(");");
                            }
                        }
                    }
                    else if (metadataProperty.CanWrite && metadataProperty.PropertyType == typeof(string))
                    {
                        var str = value as string;
                        if (str != null)
                        {
                            assign = String(str);
                        }

                        dealtWith = true;
                    }
                    else if (metadataProperty.CanWrite && metadataProperty.PropertyType == typeof(bool))
                    {
                        if (!Equals(value, metadataProperty.GetValue(Activator.CreateInstance(metadataSolidType))))
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
                    else if (metadataProperty.CanWrite && metadataProperty.PropertyType.IsEnum)
                    {
                        if (EnumExtensions.IsValidEnumValue(value))
                        {
                            assign = value.ToEnumCodeString();
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
                                if (OutputType == OutputType.CSharp)
                                {
                                    assign = serialized.Initialiser;
                                }
                                else
                                {
                                    var typeScript = ConvertToTypeScript(serialized.Class);
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
                                        switch (OutputType)
                                        {
                                            case OutputType.CSharp:
                                                assign =
                                                    $"new List<string>(new [] {{ {string.Join(", ", arr.Select(String))} }})";
                                                break;
                                            case OutputType.TypeScript:
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
                    else if (isProperty && metadataProperty.Name == nameof(IPropertyMetadata.MediaKey))
                    {
                        // Special case
                        dealtWith = true;
                        if (propertyMetadata.MediaKey?.Groups?.Any() == true)
                        {
                            sb.AppendLine($"{lambdaKey}.MediaKey");
                            foreach (var group in propertyMetadata.MediaKey.Groups)
                            {
                                if (group.Parts != null && group.Parts.Any())
                                {
                                    sb.AppendLine($".AddGroup({lambdaKey}_g => {lambdaKey}_g");
                                    foreach (var part in group.Parts)
                                    {
                                        sb.AppendLine(
                                            part.IsPropertyPath
                                                ? $".AddPropertyPath({lambdaKey}_g_ => {lambdaKey}_g_.{part.Key.Replace("/", ".")})"
                                                : $@".AddString(""{part.Key}"")");
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
                            var os = new CSharpObjectSerializer();
                            foreach (var item in metadata.Metadata.All)
                            {
                                var serialized = os.Serialize(item.Value);
                                items.Add($"{nameof(IMetadataCollection.Set)}({String(item.Key)}, {serialized.Initialiser})");
                            }
                            sb.Append($"{string.Join(".", items)};");
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
                        if (!skip)
                        {
                            var serialized = CSharpObjectSerializer.Serialize(value);
                            // HACK ALERT
                            if (OutputType == OutputType.CSharp)
                            {
                                assign = serialized.Initialiser;
                            }
                            else
                            {
                                var typeScript = ConvertToTypeScript(serialized.Class);
                                var trimmed = typeScript.Substring(0, typeScript.LastIndexOf("return "));
                                var init = "let instance = ";
                                trimmed = trimmed.Substring(trimmed.IndexOf(init) + init.Length);
                                assign = trimmed;
                            }
                        }
                    }

                    if (assign != null)
                    {
                        sb.Append("\r\n");
                        sb.Append(GetIndentPlusOne());
                        sb.Append($"{lambdaKey}.{metadataProperty.Name} = {assign};");
                    }
                    else if (!Equals(value, false) && !dealtWith && metadataProperty.CanWrite)
                    {
                        sb.Append("\r\n");
                        sb.Append(GetIndentPlusOne());
                        sb.Append($"// {lambdaKey}.{metadataProperty.Name} = ???;");
                    }
                }
                // Relationships
                if (metadata is IEntityMetadata)
                {
                    foreach (var relationship in entityMetadata.Relationships)
                    {
                        var source = relationship.Source as RelationshipDetail;
                        var target = relationship.Target as RelationshipDetail;
                        foreach (var detail in new[] { source, target })
                        {
                            var entityConfig = Schema.EntityConfigurations.Single(ec =>
                                ec.Value.Properties.Contains(detail.Property)).Value;
                            if(entityConfig != metadata)
                            {
                                continue;
                            }
                            if (detail.AllowInlineEditing || detail.InferredWithIql != null)
                            {
                                // TODO: Get the relationship and set lamdba etc.
                                sb.AppendLine();
                                var nestedLambdaKey = $"{lambdaKey}_rel";
                                var method = detail == source
                                    ? nameof(EntityConfiguration<object>.FindRelationship)
                                    : nameof(EntityConfiguration<object>.FindCollectionRelationship);
                                sb.Append($"{lambdaKey}.{method}({nestedLambdaKey} => {nestedLambdaKey}.{detail.Property.Name})");
                                var relationshipConfiguration = new StringBuilder();
                                if (detail.AllowInlineEditing)
                                {
                                    relationshipConfiguration.AppendLine(
                                        $"{nestedLambdaKey}.{nameof(IRelationshipDetail.AllowInlineEditing)} = true;");
                                }
                                if (detail.InferredWithIql != null)
                                {
                                    var path = IqlPropertyPath.FromPropertyExpression(entityConfig as IEntityConfiguration,
                                        (detail.InferredWithIql as IqlLambdaExpression).Body as IqlPropertyExpression);
                                    relationshipConfiguration.AppendLine(
                                        $"{nestedLambdaKey}.{nameof(RelationshipDetail<object, object>.IsInferredWith)}({nestedLambdaKey}_inf => {nestedLambdaKey}_inf.{path.PathToHere.Replace("/", ".")});");
                                }
                                sb.AppendLine(
                                        $@".{nameof(RelationshipDetail<object, object>.Configure)}({nestedLambdaKey} => {{
{relationshipConfiguration}
}});");
                            }
                        }
                    }
                }
                sb.Append("\r\n");
                sb.Append(GetCurrentIndent());
                sb.Append("}");
                //if (OutputType == OutputType.TypeScript && typeParameter != null)
                //{
                //    configureParameters.Add(typeParameter);
                //}
                if (appendConfigure)
                {
                    configureParameters.Add(
                        new PropertyDefinition(sb.ToString()));
                    MethodCall(
                                      isProperty
                                          ? nameof(EntityConfiguration<object>.ConfigureProperty)
                                          : nameof(EntityConfiguration<object>.Configure),
                                      false,
                                      configureParameters.ToArray()
                                  );
                }

                return sb.ToString();
                /*
                .ConfigureProperty(p => p.PhoneNumber, metadata =>{
                    metadata.Description = "";
                    return metadata;
                })
                                               */
            }

            return "";
        }

        private string SerializePropertyGroups(IPropertyGroup propertyGroup, IEntityMetadata entityMetadata, int index)
        {
            var groupSb = new StringBuilder();

            string Lambda(int i, bool inline = true)
            {
                var indexCh = i == 0 ? "" : i.ToString();
                var inlineStr = $"_{indexCh}.";
                return $"_{indexCh} => {(inline ? inlineStr : "")}";
            }
            groupSb.Append(Lambda(index));
            if (propertyGroup is IGeographic)
            {
                groupSb.Append(
                    $"{nameof(IEntityMetadata.Geographics)}[{entityMetadata.Geographics.IndexOf(propertyGroup as IGeographic)}]");
            }
            else if (propertyGroup is INestedSet)
            {
                groupSb.Append(
                    $"{nameof(IEntityMetadata.NestedSets)}[{entityMetadata.NestedSets.IndexOf(propertyGroup as INestedSet)}]");
            }
            else if (propertyGroup is IPropertyCollection)
            {
                var coll = propertyGroup as IPropertyCollection;
                var list = new List<string>();
                foreach (var subGroup in coll.Properties)
                {
                    list.Add(SerializePropertyGroups(subGroup, entityMetadata, ++index));
                }

                groupSb.Append($"{nameof(EntityConfiguration<object>.PropertyCollection)}({string.Join(",\n", list)})");
                groupSb.Append($@".{nameof(PropertyGroupBase<IPropertyCollection>.Configure)}({ConfigreMetadata(coll, null, $"coll{++index}", false)})");
            }
            else
            {
                groupSb.Append($"{nameof(EntityConfiguration<object>.FindPropertyByExpression)}({Lambda(++index)}{propertyGroup.Name})");
            }

            return groupSb.ToString();
        }

        private string ConvertToTypeScript(string serialized)
        {
            var outputSelector = new OutputSelector("", "SerializedObject", "GetData", OutputSelectorMode.Inner);
            var settings = new DefaultConversionSettings
            {
                Compile = false,
                GenerateImports = true,
                WriteToDisk = false,
                ResolveCircularDependencies = true,
                OutputClassFunctionsDeclared = false,
                OutputClassInterfacesImplemented = false,
                OutputClassNameStaticProperty = false,
                OutputClassPropertiesDeclared = false,
                OutputJsonClassConversion = false,
                PrintOutput = false,
                PrintOutputFiles = false,
                OutputSelector = outputSelector
            };
            settings.MetadataReferences.AddReference<HelpText>();
            settings.MetadataReferences.AddReference<ValidationRuleCollection>();
            var task = CSharpToTypescriptConverter.ConvertToTypeScript(
                serialized,
                settings);
            task.Wait();
            var typescript = task.Result;
            return outputSelector.Extracted;
        }
    }
}