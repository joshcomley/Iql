using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public abstract class ClassGeneratorBase
    {
        protected abstract IExpressionConverter Converter { get; }
        private int _indent;
        private bool _indentNextAppend;
        private StringBuilder _sb = new StringBuilder();
        private GeneratedFile _generatedFile;
        public GeneratedFile File => _generatedFile = _generatedFile ?? new GeneratedFile();
        public ODataSchema Schema { get; }
        public ISchemaTypeResolver TypeResolver { get; }
        public GeneratorSettings Settings { get; }
        public Func<string, string> NameMapper { get; }
        public abstract char Quote { get; }

        public string Enquote(string text)
        {
            return $"{Quote}{text}{Quote}";
        }

        protected ClassGeneratorBase(
            ODataSchema schema,
            ISchemaTypeResolver typeResolver,
            GeneratorSettings settings)
        {
            Schema = schema;
            TypeResolver = typeResolver;
            Settings = settings;
        }

        public virtual async Task ScopeAsync(string line, Func<Task> action)
        {
            AppendLine(line);
            await ScopeAsync(action);
        }

        public virtual void Scope(string line, Action action)
        {
#pragma warning disable 4014
            ScopeAsync(line, action.AsAsync());
#pragma warning restore 4014
        }

        public virtual async Task ScopeAsync(Func<Task> action)
        {
            AppendLine("{");
            await IndentAsync(action);
            AppendLine();
            Append("}");
            AppendLine();
        }

        public virtual void Scope(Action action)
        {
#pragma warning disable 4014
            ScopeAsync(action.AsAsync());
#pragma warning restore 4014
        }

        public async Task IndentAsync(Func<Task> action)
        {
            _indent++;
            await action();
            _indent--;
        }

        public virtual void Indent(Action action)
        {
#pragma warning disable 4014
            IndentAsync(action.AsAsync());
#pragma warning restore 4014
        }

        public virtual void AppendLineFormat(string line, params string[] args)
        {
            AppendLine(string.Format(line, args));
        }

        public virtual void AppendFormat(string line, params string[] args)
        {
            Append(string.Format(line, args));
        }

        public virtual void AppendLine()
        {
            AppendLineFormat("");
        }

        public virtual void AppendLine(string line)
        {
            _sb.AppendLine(GetIndent() + line);
            _indentNextAppend = true;
        }

        public virtual void Dot()
        {
            Append(".");
        }

        public virtual void Append(string text)
        {
            _sb.Append(GetIndent() + text);
            _indentNextAppend = false;
        }

        public virtual async Task UseTempStringBuilderAsync(StringBuilder temp, Func<Task> action)
        {
            var old = _sb;
            _sb = temp;
            await action();
            _sb = old;
        }

        public virtual void UseTempStringBuilder(StringBuilder temp, Action action)
        {
            UseTempStringBuilderAsync(temp, action.AsAsync());
        }

        public virtual string IndentText(string text, int? amount = null)
        {
            var indent = GetIndent(amount);
            var separator = $"\n{indent}";
            return indent + string.Join(separator, text.Split('\n'));
        }

        public virtual string GetIndent(int? indent = null)
        {
            return new string('	', indent ?? (_indentNextAppend ? _indent : 0));
        }

        public virtual int IndentLevel()
        {
            return _indent;
        }

        public virtual string GetIndentPlusOne()
        {
            return GetIndent(_indent + 1);
        }

        public virtual string GetCurrentIndent()
        {
            return GetIndent(_indent);
        }

        public virtual string GetIndentPlus(int plus)
        {
            return GetIndent(_indent + plus);
        }

        public virtual string ToCode()
        {
            return _sb.ToString();
        }

        public virtual string Contents()
        {
            return _sb.ToString();
        }

        public virtual string GenericParameterArguments(IEnumerable<GenericParameterArgument> arguments)
        {
            var a = new List<string>();
            foreach (var arg in arguments)
            {
                var sb = new StringBuilder();
                sb.Append(arg.Name);
                if (arg.Extends != null && arg.Extends.Any())
                {
                    var e = new List<string>();
                    foreach (var extend in arg.Extends)
                    {
                        e.Add(extend);
                    }
                    sb.AppendFormat(" {0}", string.Join(", ", e));
                }
                a.Add(sb.ToString());
            }
            return a.Any() ? $"<{string.Join(", ", a)}>" : "";
        }

        public virtual void Class(
            string @class,
            string @namespace,
            IEnumerable<GenericParameterArgument> genericParameters,
            Action action,
            string baseClass = null,
            IEnumerable<string> interfaces = null)
        {
            Class(
                @class,
                @namespace,
                GenericParameterArguments(genericParameters),
                action,
                baseClass,
                interfaces);
        }

        public abstract Task ClassAsync(
            string @class,
            string @namespace,
            string genericParameters,
            Func<Task> action,
            string baseClass = null,
            IEnumerable<string> interfaces = null);

        public void Class(
            string @class,
            string @namespace,
            string genericParameters,
            Action action,
            string baseClass = null,
            IEnumerable<string> interfaces = null)
        {
            ClassAsync(@class, @namespace, genericParameters, action.AsAsync(), baseClass, interfaces);
        }

        public virtual void Property(PropertyInfo property)
        {
            Property(
                property.GetMethod.IsPublic ? "public" : "",
                property.Name,
                TypeResolver.TranslateType(property.PropertyType),
                null,
                false
            );
        }

        public virtual void Property(IVariable property, bool instantiate, GetterSetter getterSetter = null)
        {
            Property(
                property.Private ? "private" : "public",
                property.Name,
                property.TypeInfo,
                null,
                instantiate,
                getterSetter);
        }

        public void Property(
            string privacy,
            string name,
            ITypeInfo type,
            Action instantiator,
            bool instantiate,
            GetterSetter getterSetter = null,
            params string[] instantiationParameters)
        {
            PropertyAsync(privacy, name, type, instantiator.AsAsync(), instantiate, getterSetter, instantiationParameters);
        }

        public abstract Task PropertyAsync(
            string privacy,
            string name,
            ITypeInfo type,
            Func<Task> instantiator,
            bool instantiate,
            GetterSetter getterSetter = null,
            params string[] instantiationParameters);

        public virtual void MethodCall(string name, bool fromObject, params IVariable[] parameters)
        {
            if (fromObject)
            {
                AppendAccessorSeparator();
            }
            Append(name);
            Append("(");
            if (parameters != null)
            {
                var paramsArray = parameters.Where(p => p != null).ToArray();
                for (var i = 0; i < paramsArray.Length; i++)
                {
                    Append(paramsArray[i].Name);
                    if (i < paramsArray.Length - 1)
                    {
                        Append(", ");
                    }
                }
            }
            Append(")");
        }

        private void AppendAccessorSeparator()
        {
            Append(".");
        }

        public virtual void ReturnThis()
        {
            Return("this");
        }

        public virtual void Return(string value)
        {
            Append("return " + value + ";");
        }

        public virtual void CloseLine()
        {
            Append(";");
        }

        public virtual void AssignProperty(IVariable property, string value)
        {
            AssignProperty(property, () =>
            {
                Append(value);
            });
        }

        public virtual void AssignProperty(IVariable property, Action value)
        {
            PropertyAccessor(property);
            Append(" = ");
            value();
            CloseLine();
            AppendLine();
        }

        public virtual void And(Action left, Action right)
        {
            Binary(left, right, "&&");
        }

        public virtual void Or(Action left, Action right)
        {
            Binary(left, right, "||");
        }

        public virtual void Not(Action action)
        {
            Append("!(");
            action();
            Append(")");
        }

        private void Binary(Action left, Action right, string @operator)
        {
            Append("(");
            left();
            Append($" {@operator} ");
            right();
            Append(")");
        }

        public virtual void PropertyAccessor(IVariable property)
        {
            Append($"{(property.IsLocal ? "" : "this.")}{property.Name}");
        }

        public async Task VariableAccessorAsync(IVariable property, Func<Task> action = null)
        {
            if (!property.IsLocal)
            {
                Append("this.");
            }
            Append(property.Name);
            if (action != null)
            {
                Append(".");
                await action();
            }
        }

        public virtual void VariableAccessor(IVariable property, Action action = null)
        {
#pragma warning disable 4014
            VariableAccessorAsync(property, action.AsAsync());
#pragma warning restore 4014
        }

        public virtual EntityTypeReference TryAddReference(ITypeInfo type)
        {
            if (type == null)
            {
                return null;
            }
            var typeReference = TypeResolver.TryResolveType(type);
            if (typeReference?.Type != null)
            {
                File.References.Add(typeReference.Type);
            }
            return typeReference;
        }

        public void AddReference(ODataTypeDefinition definition)
        {
            var genericDefinition = definition as ODataGenericTypeDefinition;
            File.References.Add(genericDefinition != null ? genericDefinition.BaseDefinition : definition);
            if (genericDefinition != null)
            {
                foreach (var genericParameter in genericDefinition.GenericParameters)
                {
                    TryAddReference(genericParameter);
                }
            }
        }
        
        public void If(string expression, Action action)
        {
            If(() => Append(expression), 
                action);
        }

        public void If(Action expression, Action action)
        {
            Append("if(");
            expression();
            Append(")");
            AppendLine();
            Scope(action);
        }

        public virtual string GetExpressionString(IqlExpression iql)
        {
            var code = Converter.ConvertIqlToExpressionString(iql);
            return code;
        }

        public virtual string NewInstanceIdentifier(Type type, params string[] args)
        {
            return NewInstanceIdentifier(type.Name, args);
        }

        public virtual string NewInstanceIdentifier(string name, params string[] args)
        {
            return $"new {name}({string.Join(", ", args)})";
        }

        public virtual string String(string value)
        {
            return $"\"{value}\"";
        }

        public virtual void Coalesce(string left, string right)
        {
            Append(GetCoalesce(left, right));
        }

        public abstract string GetCoalesce(string left, string right);

        public void Comment(string comment)
        {
            Append("// " + comment);
        }

        public void CommentLine(string comment)
        {
            AppendLine();
            Comment(comment);
            AppendLine();
        }
    }
}