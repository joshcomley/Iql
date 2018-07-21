using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Data;
using Iql.Data.Lists;
using Iql.Entities.Events;
using Iql.OData.TypeScript.Generator.DataContext;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class EntityTypeToClassGenerator : ClassGenerator
    {
        private readonly ODataTypeDefinition _entityType;

        public EntityTypeToClassGenerator(ODataSchema schema, ODataTypeDefinition entityType, OutputType outputType,  GeneratorSettings settings)
            : base(schema, outputType, settings)
        {
            _entityType = entityType;
            _entityType.OriginalName = _entityType.Name;
            _entityType.Name = NameMapper(_entityType.Name);
        }

        public GeneratedFile Generate()
        {
            File.FileName = _entityType.Name;
            File.Namespace = _entityType.Namespace;
            if (_entityType.GetType() == typeof(EntityTypeDefinition))
            {
                GenerateEntityType(_entityType as EntityTypeDefinition);
            }
            if (_entityType.GetType() == typeof(EnumTypeDefinition))
            {
                GenerateEnumType(_entityType as EnumTypeDefinition);
            }
            File.Contents = Contents();
            return File;
        }

        public void GenerateEntityType(EntityTypeDefinition entityType)
        {
            File.References.AddRange(entityType.FindAllInternalTypeReferences(Schema, Settings));
            var entityTypeName = NameMapper(entityType.Name);
            var baseClass = $"{entityTypeName}Base";
            TryAddReference(new TypeInfo(baseClass));
            File.IsEntity = true;
            Class(
                NameMapper(_entityType.Name),
                File.Namespace,
                "",
                () =>
                {
                    //var propertyChangedType = TypeResolver.TranslateType(typeof(EventEmitter<IPropertyChangeEvent>));
                    //Property("public", "PropertyChanged", propertyChangedType, null, false);
                    foreach (var property in entityType.Properties)
                    {
                        var changedVariable = new PropertyDefinition("changed", true);
                        var changedSetVariable = new PropertyDefinition("changedSet", true);
                        var getterSetter = new GetterSetter();
                        var oldValue = new PropertyDefinition("oldValue", true);
                        var value = new PropertyDefinition("value", true);

                        void SetIsChanged(PropertyDefinition backingField)
                        {
                            AssignProperty(changedVariable,
                                () =>
                                {
                                    var typeName = property.TypeInfo.EdmType.ToLower();
                                    if (OutputType == OutputType.TypeScript && typeName.Contains("edm:datetime"))
                                    {
                                        AppendLine($"({oldValue.Name} != null && {value.Name} != null) ? {oldValue.Name}.getTime() !== {value.Name}.getTime() : {oldValue.Name} !== {value.Name};");
                                    }
                                    else
                                    {
                                        IsNotEqualTo(() => VariableAccessor(value), () => VariableAccessor(oldValue));
                                    }
                                });
                        }

                        getterSetter.BeforeSet = () =>
                        {
                            Let(() => VariableAccessor(changedSetVariable), () => Append("false"));
                            var backingField = new PropertyDefinition(getterSetter.BackingFieldName);
                            Let(() => { VariableAccessor(oldValue); },
                                () => { VariableAccessor(backingField); });
                            Let(() => { VariableAccessor(changedVariable); },
                                () => { Append("false"); });
                            If(
                                () => VariableAccessor(new PropertyDefinition("_propertyChangingSet")),
                                () =>
                                {
                                    SetIsChanged(backingField);
                                    AssignProperty(changedSetVariable, () => { Append("true"); });
                                    If(() => VariableAccessor(changedVariable), () =>
                                    {
                                        PropertyAccessor(new EntitySetPropertyDefinition
                                        {
                                            Name = nameof(IEntity.PropertyChanging)
                                        });
                                        MethodCall(nameof(EventEmitter<object>.Emit), true,
                                            new EntitySetPropertyDefinition
                                            {
                                                Name = "() => " + NewInstanceIdentifier(
                                                           $"{nameof(PropertyChangeEvent<object>)}<{NameMapper(_entityType.Name)}>",
                                                           NameOf(property.Name), "this", "oldValue",
                                                           getterSetter.NewValueName)
                                            });
                                        CloseLine();
                                    });
                                });
                        };
                        getterSetter.AfterSet = () =>
                        {
                            var backingField = new PropertyDefinition(getterSetter.BackingFieldName);
                            If(
                                () => VariableAccessor(new PropertyDefinition("_propertyChangedSet")),
                                () =>
                                {
                                    If(() => Not(() => VariableAccessor(changedSetVariable)),
                                        () =>
                                        {
                                            SetIsChanged(backingField);
                                        });
                                    If(() =>
                                    {
                                        VariableAccessor(changedVariable);
                                    }, () =>
                                    {
                                        PropertyAccessor(new EntitySetPropertyDefinition { Name = nameof(IEntity.PropertyChanged) });
                                        MethodCall(nameof(EventEmitter<object>.Emit), true,
                                            new EntitySetPropertyDefinition
                                            {
                                                Name = "() => " + NewInstanceIdentifier(
                                                           $"{nameof(PropertyChangeEvent<object>)}<{NameMapper(_entityType.Name)}>", NameOf(property.Name), "this", "oldValue", getterSetter.NewValueName)
                                            });
                                        CloseLine();
                                    });
                                });
                        };
                        var navigationProperty = property as NavigationPropertyDefinition;
                        if (navigationProperty != null && navigationProperty.EntityType.IsCollection)
                        {
                            var countPropertyName = $"{property.Name}Count";
                            if (Settings.GenerateCountProperties && entityType.Properties.All(p => p.Name != countPropertyName))
                            {
                                var countProperty = new PropertyDefinition();
                                countProperty.Name = countPropertyName;
                                countProperty.Private = false;
                                countProperty.TypeInfo = TypeResolver.TranslateType(typeof(long?));
                                Property(countProperty, false);
                            }
                            // Add count property
                            //Property();
                            var relationshipEntityTypeName = NameMapper(navigationProperty.EntityType.Type.Name);
                            property.TypeInfo.ResolvedType = $"{nameof(RelatedList<object, object>)}<{entityTypeName},{relationshipEntityTypeName}>";
                            getterSetter.BeforeGet = () =>
                            {
                                var args = new List<string>();
                                args.Add("this");
                                args.Add(NameOf(property.Name));
                                if (OutputType == OutputType.TypeScript)
                                {
                                    args.Add("null");
                                    args.Add(entityTypeName);
                                    args.Add(relationshipEntityTypeName);
                                }
                                AssignProperty(new PropertyDefinition(
                                        getterSetter.BackingFieldName),
                                    GetCoalesce(
                                        $"this.{getterSetter.BackingFieldName}",
                                        NewInstanceIdentifier(
                                            property.TypeInfo.ResolvedType,
                                            args.ToArray()))
                                );
                                AppendLine();
                            };
                        }
                        Property(property, false, getterSetter);
                    }

                    // Implement IEntity
                    //Method(nameof(IEntity.GetODataDataStore), new IVariable[] { }, TypeResolver.TranslateType(typeof(ODataDataStore)),
                    //    () =>
                    //    {
                    //        Throw("Not implemented.");
                    //    }, modifier: Modifier.Override);

                    //const string validateEntityMethodName = nameof(IEntity.ValidateEntity);
                    //if (entityType.Functions.Any())
                    //{
                    //    AppendLine();
                    //    foreach (var method in entityType.Functions)
                    //    {
                    //        var name = method.Name;
                    //        // Ensure we don't get conflicting names
                    //        // if the use already has a method called "ValidateEntity"
                    //        // although not foolproof as they might also have
                    //        // "ValidateEntityCustom"
                    //        if (name == validateEntityMethodName)
                    //        {
                    //            name = validateEntityMethodName + "Custom";
                    //        }
                    //        //Method(
                    //        //    name,
                    //        //    method.Parameters,
                    //        //    new TypeInfo($"{nameof(ODataResult<object>)}<{TypeResolver.ResolveTypeNameFromODataName(method.ReturnType).Name}>"),
                    //        //    () =>
                    //        //    {
                    //        //        var typeScriptReturnType =
                    //        //            TypeResolver.ResolveTypeNameFromODataName(method.ReturnType).Name;
                    //        //        AppendLine("// Call API somehow");
                    //        //        var parameters = new Dictionary<string, string>();
                    //        //        foreach (var parameter in method.Parameters)
                    //        //        {
                    //        //            parameters.Add(parameter.Name, parameter.Name);
                    //        //        }
                    //        //        AsDynamicObject("parameters", parameters);
                    //        //        var dataStoreMethod = method.Type == EntityFunctionDefinitionType.Action
                    //        //            ? nameof(ODataDataStore.PostOnEntityInstance)
                    //        //            : nameof(ODataDataStore.GetOnEntityInstance);
                    //        //        Return($"await this.{nameof(IEntity.GetODataDataStore)}().{dataStoreMethod}<{entityType.Name}, {typeScriptReturnType}>(this, parameters{(OutputType == OutputType.TypeScript ? $", {GetThisType()}, {typeScriptReturnType}" : "")})");
                    //        //    },
                    //        //    async: true,
                    //        //    resolveTypeName: false,
                    //        //    modifier: Modifier.Virtual);
                    //    }
                    //}

                    //Method(nameof(IEntity.OnSaving), new IVariable[] { }, "boolean", () =>
                    //{
                    //    Return("true");
                    //});

                    //Method(nameof(IEntity.OnDeleting), new IVariable[] { }, "boolean", () =>
                    //{
                    //    Return("true");
                    //});
                },
                baseClass,
                new[] { nameof(IEntity) });
        }
    }
}