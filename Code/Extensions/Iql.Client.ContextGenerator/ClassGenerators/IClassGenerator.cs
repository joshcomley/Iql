using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public interface IClassGenerator
    {
        void UseTempStringBuilder(StringBuilder temp, Action action);
        string GetExpressionString(IqlExpression iql);
        GeneratedFile File { get; }
        ISchemaTypeResolver TypeResolver { get; }
        ODataSchema Schema { get; }
        EntityTypeReference TryAddReference(ITypeInfo type);
        string Contents();
        string GenericParameterArguments(IEnumerable<GenericParameterArgument> arguments);
        string Enquote(string text);

        void Class(
            string @class,
            string @namespace,
            IEnumerable<GenericParameterArgument> genericParameters,
            Action action,
            string baseClass = null,
            IEnumerable<string> interfaces = null);

        void Class(
            string @class,
            string @namespace,
            string genericParameters,
            Action action,
            string baseClass = null,
            IEnumerable<string> interfaces = null);

        void Field(IVariable field, Action instantiate = null);
        void Property(PropertyInfo property);
        void Property(IVariable property, bool instantiate, GetterSetter getterSetter = null);

        void Property(string privacy, string name, ITypeInfo type, 
            Action instantiator, 
            bool instantiate,
            GetterSetter getterSetter = null,
            params string[] instantiationParameters);

        void Method(string name, IEnumerable<IVariable> parameters, ITypeInfo returnType, Action action, string privacy = null, bool async = false, bool resolveTypeName = true, Modifier modifier = Modifier.None);
        string TypeOfExpression(ITypeInfo type);
        string TypeOfExpression(string type);
        void MethodCall(string name, bool fromObject, params IVariable[] parameters);
        void Coalesce(string left, string right);
        void Comment(string comment);
        void CommentLine(string comment);
        string GetCoalesce(string left, string right);
        void Let(string name, string instantaitor = null);
        void Let(Action left, Action right = null);
        void And(Action left, Action right);
        void Or(Action left, Action right);
        void Not(Action action);
        void ReturnThis();
        void Return(string value);
        void Throw(string error);
        void Constructor(IEnumerable<IVariable> parameters, Action action, IEnumerable<IVariable> baseCall);
        void Constructor(ConstructorInfo constructor, Action action, IEnumerable<IVariable> baseCall);
        //void Super(IEnumerable<IVariable> parameters);
        void CloseLine();
        void AssignProperty(IVariable property, string value);
        void AssignProperty(IVariable property, Action value);
        void PropertyAccessor(IVariable property);
        void VariableAccessor(IVariable property, Action action = null);
        void Class(string @class, string @namespace, ODataTypeDefinition extends, Action action);
        void AddReference(ODataTypeDefinition definition);
        void If(string expression, Action action);
        void If(Action expression, Action action);
        void IsNotNull(Action expression);
        void IsEqualTo(Action left, Action right);
        void IsNotEqualTo(Action left, Action right);
        void Scope(string line, Action action);
        void Scope(Action action);
        void Indent(Action action);
        void AppendLineFormat(string line, params string[] args);
        void AppendLine();
        void AppendLine(string line);
        void Dot();
        void Append(string text);
        void ArrayAdd(string arrayName, Action value);
        string GetIndent(int? indent = null);
        string GetCurrentIndent();
        string GetIndentPlusOne();
        string ToCode();
        string GetThisType();
        void AsDynamicObject(string name, Dictionary<string, string> keyValues);
        void GenerateEnumType(EnumTypeDefinition entityType);
        string NewInstanceIdentifier(Type type, params string[] arguments);
        string NewInstanceIdentifier(string name, params string[] arguments);
        string String(string value);
        string Cast(string value);
        string NameOf(string property);
    }
}