using System;
using Iql.Data;
using Iql.Data.Events;
using Iql.Entities.Events;
using Iql.OData.TypeScript.Generator.DataContext;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;
using GeneratedFile = Iql.OData.TypeScript.Generator.Models.GeneratedFile;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class EntityBaseClassGenerator : ClassGenerator
    {
        public EntityBaseClassGenerator(
            string fileName,
            string @namespace,
            ODataSchema schema, 
            OutputKind outputKind, 
            GeneratorSettings settings) : base(
            fileName,
            @namespace,
            schema, 
            outputKind, 
            settings)
        {
        }

        public void Generate(GeneratedFile generatedFile)
        {
            NameMapper = x => x;
            Class(
                generatedFile.BaseClassName,
                generatedFile.Namespace,
                (string)null,
                () =>
            {
                DefineEventProperty<EventEmitter<IPropertyChangeEvent>>(nameof(IEntity.PropertyChanging));
                DefineEventProperty<EventEmitter<IPropertyChangeEvent>>(nameof(IEntity.PropertyChanged));
                DefineEventProperty<EventEmitter<ExistsChangeEvent>>("ExistsChanged");

                var booleanType = TypeResolver.TranslateType(typeof(bool), "raw");
                //Method(nameof(IEntity.OnSaving), null, booleanType, () =>
                //{
                //    Return("true");
                //}, modifier: Modifier.Virtual);
                //Method(nameof(IEntity.OnDeleting), null, booleanType, () =>
                //{
                //    Return("true");
                //}, modifier: Modifier.Virtual);
                var stringType = TypeResolver.TranslateType(typeof(string));
                Property("public static", "ClassName", stringType, () =>
                {
                    Append(
$"\"{generatedFile.BaseClassName}\"");
                }, true);

                //Method("ClassName", null, TypeResolver.TranslateType(typeof(string)), () =>
                //{
                //    Return(String($"{generatedFile.FileName}"));
                //}, modifier: Modifier.Static);
                //Method(nameof(IEntity.GetODataDataStore), null, TypeResolver.TranslateType(typeof(ODataDataStore)), () =>
                //{
                //    Throw("Not implemented.");
                //}, modifier: Modifier.Virtual);
                //Method(nameof(IEntity.ValidateEntity), null, TypeResolver.TranslateType(typeof(EntityValidationResult), "nullable"), () =>
                //{
                //    Return($"{NewInstanceIdentifier<EntityValidationResult>(GetThisType())}");
                //}, modifier: Modifier.Virtual);
            },
                interfaces: new[] { nameof(IEntity) });
        }

        private ITypeInfo DefineEventProperty<TEventType>(string eventName)
        {
            var eventType = typeof(TEventType);
            var propertyChangedType = TypeResolver.TranslateType(eventType);
            var propertyChangingGetterSetter = new GetterSetter();
            var propertyChangingSetPropertyDefinition =
                new EntityFunctionParameterDefinition($"_{eventName.FirstCharToLower()}Set", TypeResolver.TranslateType(typeof(bool)));
            Field(propertyChangingSetPropertyDefinition);
            var valueVariable = new PropertyDefinition("value");
            valueVariable.IsLocal = true;
            propertyChangingGetterSetter.AfterSet = () =>
            {
                AssignProperty(propertyChangingSetPropertyDefinition,
                    () => { IsNotNull(() => { VariableAccessor(valueVariable); }); });
                AppendLine();
            };
            Property("public", eventName, propertyChangedType, null, false, propertyChangingGetterSetter);
            return propertyChangedType;
        }
    }
}