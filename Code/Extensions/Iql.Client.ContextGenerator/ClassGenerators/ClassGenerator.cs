using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Iql.Data.DataStores;
using Iql.Data.Lists;
using Iql.Entities;
using Iql.OData.Methods;
using Iql.OData.TypeScript.Generator.DataContext;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;
using TypeInfo = Iql.OData.TypeScript.Generator.Definitions.TypeInfo;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class ClassGenerator : IClassGenerator
    {
        private Func<string, string> _nameMapper;
        public OutputType OutputType { get; }
        public GeneratorSettings Settings { get; }

        public Func<string, string> NameMapper
        {
            get => _nameMapper ?? (s => s);
            set => _nameMapper = value;
        }

        public ClassGenerator(ODataSchema schema, OutputType outputType, GeneratorSettings settings)
        {
            OutputType = outputType;
            Settings = settings;
            Generator =
                OutputType == OutputType.TypeScript
                ? (IClassGenerator)new TypeScriptClassGenerator(schema, settings)
                : new CSharpClassGenerator(schema, settings);
        }

        public IClassGenerator Generator { get; set; }
        public void UseTempStringBuilder(StringBuilder temp, Action action)
        {
            Generator.UseTempStringBuilder(temp, action);
        }

        public string GetExpressionString(IqlExpression iql)
        {
            return Generator.GetExpressionString(iql);
        }

        public GeneratedFile File
        {
            get { return Generator.File; }
        }

        public ISchemaTypeResolver TypeResolver
        {
            get { return Generator.TypeResolver; }
        }

        public ODataSchema Schema
        {
            get { return Generator.Schema; }
        }

        public EntityTypeReference TryAddReference(ITypeInfo type)
        {
            return Generator.TryAddReference(type);
        }

        public string Contents()
        {
            return Generator.Contents();
        }

        public string GenericParameterArguments(IEnumerable<GenericParameterArgument> arguments)
        {
            return Generator.GenericParameterArguments(arguments);
        }

        public string Enquote(string text)
        {
            return Generator.Enquote(text);
        }

        public string NameOf(string property)
        {
            return Generator.NameOf(property);
        }

        public void Class(string @class, string @namespace,
            IEnumerable<GenericParameterArgument> genericParameters, Action action, string baseClass = null,
            IEnumerable<string> interfaces = null)
        {
            Generator.Class(@class, @namespace, genericParameters, action, baseClass, interfaces);
        }

        public void Class(string @class, string @namespace,
            string genericParameters, Action action, string baseClass = null,
            IEnumerable<string> interfaces = null)
        {
            Generator.Class(@class, @namespace, genericParameters, action, baseClass, interfaces);
        }

        public void Field(IVariable field, Action instantiate = null)
        {
            Generator.Field(field, instantiate);
        }

        public void Property(PropertyInfo property)
        {
            Generator.Property(property);
        }

        public void Property(IVariable property, bool instantiate, GetterSetter getterSetter = null)
        {
            Generator.Property(property, instantiate, getterSetter);
        }

        public void Property(string privacy, string name, ITypeInfo type, Action instantiator, bool instantiate,
            GetterSetter getterSetter = null,
            params string[] instantiationParameters)
        {
            Generator.Property(privacy, name, type, instantiator, instantiate, getterSetter, instantiationParameters);
        }

        public void Method(string name, IEnumerable<IVariable> parameters, ITypeInfo returnType, Action action, string privacy = null,
            bool async = false, bool resolveTypeName = true, Modifier modifier = Modifier.None)
        {
            Generator.Method(name, parameters, returnType, action, privacy, async, resolveTypeName, modifier);
        }

        public string TypeOfExpression(ITypeInfo type)
        {
            return Generator.TypeOfExpression(type);
        }

        public string TypeOfExpression(string type)
        {
            return Generator.TypeOfExpression(type);
        }

        public void Coalesce(string left, string right)
        {
            Generator.Coalesce(left, right);
        }

        public void Comment(string comment)
        {
            Generator.Comment(comment);
        }

        public void CommentLine(string comment)
        {
            Generator.CommentLine(comment);
        }

        public string GetCoalesce(string left, string right)
        {
            return Generator.GetCoalesce(left, right);
        }

        public void MethodCall(string name, bool fromObject, params IVariable[] parameters)
        {
            Generator.MethodCall(name, fromObject, parameters);
        }

        public void Let(string name, string instantaitor = null)
        {
            Generator.Let(name, instantaitor);
        }

        public void Let(Action left, Action right = null)
        {
            Generator.Let(left, right);
        }

        public void ReturnThis()
        {
            Generator.ReturnThis();
        }

        public void Return(string value)
        {
            Generator.Return(value);
        }

        public void Throw(string error)
        {
            Generator.Throw(error);
        }

        public void Constructor(IEnumerable<IVariable> parameters, Action action, IEnumerable<IVariable> baseCall = null)
        {
            Generator.Constructor(parameters, action, baseCall);
        }

        public void Constructor(ConstructorInfo constructor, Action action, IEnumerable<IVariable> baseCall = null)
        {
            Generator.Constructor(constructor, action, baseCall);
        }

        public void CloseLine()
        {
            Generator.CloseLine();
        }

        public void AssignProperty(IVariable property, string value)
        {
            Generator.AssignProperty(property, value);
        }

        public void AssignProperty(IVariable property, Action value)
        {
            Generator.AssignProperty(property, value);
        }

        public void And(Action left, Action right)
        {
            Generator.And(left, right);
        }

        public void Or(Action left, Action right)
        {
            Generator.Or(left, right);
        }

        public void Not(Action action)
        {
            Generator.Not(action);
        }

        public void PropertyAccessor(IVariable property)
        {
            Generator.PropertyAccessor(property);
        }

        public void VariableAccessor(IVariable property, Action action = null)
        {
            Generator.VariableAccessor(property, action);
        }

        public void Class(string @class, string @namespace, ODataTypeDefinition extends, Action action)
        {
            Generator.Class(@class, @namespace, extends, action);
        }

        public void AddReference(ODataTypeDefinition definition)
        {
            Generator.AddReference(definition);
        }

        public void If(string expression, Action action)
        {
            Generator.If(expression, action);
        }

        public void If(Action expression, Action action)
        {
            Generator.If(expression, action);
        }

        public void IsNotNull(Action value)
        {
            Generator.IsNotNull(value);
        }

        public void IsEqualTo(Action left, Action right)
        {
            Generator.IsEqualTo(left, right);
        }

        public void IsNotEqualTo(Action left, Action right)
        {
            Generator.IsNotEqualTo(left, right);
        }

        public void Scope(string line, Action action)
        {
            Generator.Scope(line, action);
        }

        public void Scope(Action action)
        {
            Generator.Scope(action);
        }

        public void Indent(Action action)
        {
            Generator.Indent(action);
        }

        public void AppendLineFormat(string line, params string[] args)
        {
            Generator.AppendLineFormat(line, args);
        }

        public void AppendLine()
        {
            Generator.AppendLine();
        }

        public void AppendLine(string line)
        {
            Generator.AppendLine(line);
        }

        public void Dot()
        {
            Generator.Dot();
        }

        public void Append(string text)
        {
            Generator.Append(text);
        }

        public void ArrayAdd(string arrayName, Action value)
        {
            Generator.ArrayAdd(arrayName, value);
        }

        public string GetIndent(int? indent = null)
        {
            return Generator.GetIndent(indent);
        }

        public string GetCurrentIndent()
        {
            return Generator.GetCurrentIndent();
        }

        public string GetIndentPlusOne()
        {
            return Generator.GetIndentPlusOne();
        }

        public string ToCode()
        {
            return Generator.ToCode();
        }

        public string GetThisType()
        {
            return Generator.GetThisType();
        }

        public void AsDynamicObject(string name, Dictionary<string, string> keyValues)
        {
            Generator.AsDynamicObject(name, keyValues);
        }

        public void GenerateEnumType(EnumTypeDefinition entityType)
        {
            Generator.GenerateEnumType(entityType);
        }

        public string NewInstanceIdentifier<T>(params string[] args)
        {
            return NewInstanceIdentifier(typeof(T), args);
        }

        public virtual string NewInstanceIdentifier(Type type, params string[] args)
        {
            return Generator.NewInstanceIdentifier(type, args);
        }

        public virtual string NewInstanceIdentifier(string name, params string[] args)
        {
            return Generator.NewInstanceIdentifier(name, args);
        }

        public string String(string value)
        {
            return Generator.String(value);
        }

        public string Cast(string value)
        {
            return Generator.Cast(value);
        }

        public string ResolveKeyType(EntitySetDefinition @class)
        {
            var keyType =
                @class.Type.Key.Properties.Count == 1
                    ? TypeResolver.ResolveTypeNameFromODataName(@class.Type.Key.Properties.First().TypeInfo, true).Name
                    : nameof(CompositeKey);
            return keyType;
        }

        protected void PrintODataMethod(
            EntityFunctionDefinition method, bool isGlobal = false)
        {
            var name = method.Name;
            // Ensure we don't get conflicting names
            // if the use already has a method called "ValidateEntity"
            // although not foolproof as they might also have
            // "ValidateEntityCustom"
            //if (name == validateEntityMethodName)
            //{
            //    name = validateEntityMethodName + "Custom";
            //}
            var returnType = TypeResolver.ResolveTypeNameFromODataName(method.ReturnType);
            //if (returnType.IsCollection == false && returnType.ElementName == null && returnType.Name == null)
            //{
            //    int a = 0;
            //}
            var typeScriptReturnType =
                returnType.Name;
            var typeScriptReturnTypeParameter =
                returnType.Name;
            if (OutputType == OutputType.TypeScript && returnType.IsCollection)
            {
                typeScriptReturnTypeParameter = "Array";
            }

            if (string.IsNullOrWhiteSpace(typeScriptReturnType))
            {
                if (OutputType == OutputType.TypeScript)
                {
                    typeScriptReturnType = "void";
                    typeScriptReturnTypeParameter = "null";
                }
            }

            if (OutputType == OutputType.TypeScript)
            {
                typeScriptReturnTypeParameter = typeScriptReturnTypeParameter.AsTypeScriptTypeParameter();
            }
            const string dataStoreWithDataMethod = nameof(ODataDataStore.MethodWithResponse);
            const string dataStoreWithoutDataMethod = nameof(ODataDataStore.Method);
            var methodParameters = method.Parameters;
            var hasDataResponse = method.ReturnType != null;
            var dataStoreMethod = hasDataResponse
                ? dataStoreWithDataMethod
                : dataStoreWithoutDataMethod;
            var methodReturnType =
                new TypeInfo(
                    hasDataResponse
                        ? $"{nameof(ODataDataMethodRequest<object>)}<{typeScriptReturnType}>"
                        : nameof(ODataMethodRequest));

            methodReturnType.ResolvedType = methodReturnType.EdmType;
            EntityFunctionParameterDefinition keyParam = null;
            if (method.Scope == ODataMethodScope.Entity)
            {
                keyParam = new EntityFunctionParameterDefinition(
                   "bindingParameter",
                   method.EntityType.Type);
                methodParameters.Insert(0,
                    keyParam);
            }

            Method(
                name,
                methodParameters,
                methodReturnType,
                () =>
                {
                    var parametersVariableName = "parameters";
                    //AppendLine("// Call API somehow");
                    var parameters = new Dictionary<string, string>();
                    foreach (var parameter in methodParameters)
                    {
                        parameters.Add(parameter.Name, parameter.Name);
                    }
                    Let(
                        () => { Append(parametersVariableName); },
                        () => { Append(NewInstanceIdentifier(TypeResolver.ResolveName(typeof(List<ODataParameter>)))); });
                    AppendLine();
                    foreach (var parameter in methodParameters)
                    {
                        ArrayAdd(
                            parametersVariableName,
                            () =>
                            {
                                Append(
                                    NewInstanceIdentifier(
                                        typeof(ODataParameter),
                                        parameter.Name,
                                        TypeOfExpression(parameter.TypeInfo),
                                        String(parameter.Name),
                                        parameter == keyParam ? "true" : "false"));
                            });
                        CloseLine();
                        AppendLine();
                    }

                    //AsDynamicObject("parameters", parameters);
                    //method.Type == EntityFunctionDefinitionType.Action
                    if (!string.IsNullOrWhiteSpace(typeScriptReturnType))
                    {
                        typeScriptReturnType = $"<{typeScriptReturnType}>";
                    }
                    var newLine = $"\r\n{GetIndentPlusOne()}";
                    var scope = isGlobal ? "" : $".{nameof(DbSet<object, object>.DataContext)}";
                    var entityType = "null";
                    if (method.EntityType?.Type?.Name != null)
                    {
                        entityType = TypeOfExpression(method.EntityType?.Type);
                    }

                    var responseElementType =
                        hasDataResponse
                            ? returnType.IsCollection
                                ? TypeOfExpression(returnType.ElementName.AsTypeScriptTypeParameter())
                                : TypeOfExpression(returnType.Name.AsTypeScriptTypeParameter())
                            : null;
                    Return(
                        $"({Cast(nameof(ODataDataStore))}this{scope}.{nameof(DataStore)}).{dataStoreMethod}{(hasDataResponse ? typeScriptReturnType : "")}({newLine}parameters,{newLine}{nameof(ODataMethodType)}.{(method.Type == EntityFunctionDefinitionType.Action ? nameof(ODataMethodType.Action) : nameof(ODataMethodType.Function))},{newLine}{nameof(ODataMethodScope)}.{method.Scope},{newLine}{String(method.Namespace)},{newLine}{String(method.Name)},{newLine}{entityType}{(hasDataResponse ? $",{newLine}{responseElementType}" : "")}{(OutputType == OutputType.TypeScript && hasDataResponse ? $",{newLine}{typeScriptReturnTypeParameter}" : "")})");
                },
                async: false,
                resolveTypeName: false,
                modifier: Modifier.Virtual);
        }
    }
}