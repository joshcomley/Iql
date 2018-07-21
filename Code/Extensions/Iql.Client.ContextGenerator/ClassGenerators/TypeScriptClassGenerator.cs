using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Iql.Conversion;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class TypeScriptClassGenerator : ClassGeneratorBase, IClassGenerator
    {
        public TypeScriptClassGenerator(ODataSchema schema, GeneratorSettings settings) 
            : base(schema, new TypeScriptTypeResolver(schema, settings), settings)
        {
            // Converter = new JavaScriptExpressionConverter();
        }

        protected override IExpressionConverter Converter { get; }
        public override char Quote => '`';

        public override void Class(
            string @class,
            string @namespace,
            string genericParameters,
            Action action,
            string baseClass = null,
            IEnumerable<string> interfaces = null)
        {
            var str = "export class " + @class + genericParameters;
            if (!string.IsNullOrWhiteSpace(baseClass))
            {
                str += " extends " + baseClass;
            }
            File.BaseClassName = baseClass;
            if (interfaces != null)
            {
                var interfacesArr = interfaces as string[] ?? interfaces.ToArray();
                if (interfacesArr.Any())
                {
                    str += " implements " + string.Join(", ", interfacesArr);
                }
            }
            Scope(str, action);
        }

        public void Field(IVariable field, Action instantiate = null)
        {
            AppendLine();
            Append($"protected {field.Name}: {field.TypeInfo.ResolvedType ?? TypeResolver.ResolveTypeNameFromODataName(field.TypeInfo).Name}");
            if (instantiate != null)
            {
                Append(" = ");
                instantiate();
            }
            CloseLine();
            AppendLine();
        }

        public override void Property(string privacy, string name, ITypeInfo type, 
            Action instantiator, bool instantiate,
            GetterSetter getterSetter = null,
            params string[] instantiationParameters)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(privacy))
            {
                privacy = privacy + " ";
            }
            TryAddReference(type);
            var typeName = string.IsNullOrWhiteSpace(type.ResolvedType)
                ? TypeResolver.ResolveTypeNameFromODataName(type).Name
                : type.ResolvedType;
            var backingName = $"_{name}";
            backingName = backingName.Substring(0, 2).ToLower() + backingName.Substring(2);

            void AppendInstantiate()
            {
                if (instantiate)
                {
                    UseTempStringBuilder(sb, () =>
                    {
                        if (instantiator != null)
                        {
                            this.AppendInstantiate(instantiator);
                        }
                        else
                        {
                            this.AppendInstantiate(type, instantiationParameters);
                        }
                    });
                }
            }

            var afterSet = getterSetter?.AfterSet;
            var beforeSet = getterSetter?.BeforeSet;
            var afterGet = getterSetter?.AfterGet;
            var beforeGet = getterSetter?.BeforeGet;
            var useBackingField = getterSetter != null && new[] { beforeSet, afterSet, beforeGet, afterGet }.Any(s => s != null);
            if (useBackingField)
            {
                getterSetter.NewValueName = "value";
                getterSetter.BackingFieldName = $"{backingName}";
                sb.AppendFormat("private {0}: {1}", backingName, typeName);
                AppendInstantiate();
                sb.Append(";");
            }
            sb.AppendLine();
            if (useBackingField)
            {
                sb.AppendLine();
                sb.AppendLine(GetIndentPlus(0) + $"get {name}(): {typeName} {{");

                Indent(() =>
                {
                    UseTempStringBuilder(sb, () =>
                    {
                        beforeGet?.Invoke();
                    });
                });
                if (afterGet == null)
                {
                    sb.AppendLine(GetIndentPlus(1) + $"return this.{getterSetter.BackingFieldName};");
                }
                else
                {
                    Indent(() =>
                    {
                        UseTempStringBuilder(sb, () =>
                        {
                            afterSet?.Invoke();
                        });
                    });
                }

                sb.AppendLine(GetIndentPlus(0) + "}");
                sb.AppendLine(GetIndentPlus(0) + $"set {name}({getterSetter.NewValueName}: {typeName}) {{");
                Indent(() =>
                {
                    UseTempStringBuilder(sb, () =>
                    {
                        beforeSet?.Invoke();
                    });
                });
                sb.AppendLine(GetIndentPlus(1) + $"this.{backingName} = {getterSetter.NewValueName};");
                Indent(() =>
                {
                    UseTempStringBuilder(sb, () =>
                    {
                        afterSet?.Invoke();
                    });
                });
                sb.AppendLine(GetIndentPlus(0) + "}");
            }
            else
            {
                sb.Append(GetIndent());
                sb.Append(privacy);
                sb.Append($"{name}: ");
                sb.Append($"{typeName} ");
                AppendInstantiate();
                sb.Append(";");
            }
            AppendLine(sb.ToString());
        }

        void AppendInstantiate(Action instantiator)
        {
            Append(" = ");
            instantiator();
            CloseLine();
        }

        void AppendInstantiate(ITypeInfo type, params string[] instantiationParameters)
        {
            AppendInstantiate(() =>
            {
                Append(NewInstanceIdentifier(type.ConstructorType, instantiationParameters));
            });
        }

        public override string GetExpressionString(IqlExpression iql)
        {
            // var code = (Converter as JavaScriptExpressionConverter).ConvertIqlToTypeScriptExpressionString(iql);
            throw new NotImplementedException();
        }

        public void Method(string name, IEnumerable<IVariable> parameters, ITypeInfo returnType, Action action, string privacy = null, bool async = false, bool resolveTypeName = true, Modifier modifier = Modifier.None)
        {
            if (privacy == null && name != "constructor")
            {
                privacy = "public";
            }
            if (!string.IsNullOrWhiteSpace(privacy))
            {
                privacy = privacy + " ";
            }
            switch (modifier)
            {
                case Modifier.Static:
                    privacy = privacy + "static ";
                    break;
            }
            if (async)
            {
                privacy = privacy + "async ";
            }
            TryAddReference(returnType);
            var returnTypeResolved = returnType?.ResolvedType ?? (resolveTypeName ? TypeResolver.ResolveTypeNameFromODataName(returnType).Name : returnType.ResolvedType);
            if (async)
            {
                var promiseKind = returnTypeResolved;
                if (string.IsNullOrWhiteSpace(returnTypeResolved))
                {
                    promiseKind = "any";
                }
                returnTypeResolved = $"Promise<{promiseKind}>";
            }
            Scope(string.Format("{3}{0}({1}){2}",
                    name,
                    parameters == null
                        ? ""
                        : string.Join(",\n" + GetIndentPlusOne(), parameters.Select(p => p.AsTypeScriptParameter(Schema, Settings))),
                    !string.IsNullOrWhiteSpace(returnTypeResolved)
                        ? string.Format(": {0}", returnTypeResolved)
                        : "",
                    privacy),
                action);
        }

        public string TypeOfExpression(ITypeInfo type)
        {
            var name = type.ResolvedType;
            if (string.IsNullOrWhiteSpace(name))
            {
                var resolveTypeNameFromODataName = TypeResolver.ResolveTypeNameFromODataName(type);
                if (resolveTypeNameFromODataName.Name == null)
                {
                    resolveTypeNameFromODataName = TypeResolver.ResolveTypeNameFromODataName(type, true);
                }
                name = resolveTypeNameFromODataName.Name.AsTypeScriptTypeParameter();
            }
            return TypeOfExpression(name);
        }

        public string TypeOfExpression(string name)
        {
            return name;
        }

        public void Constructor(IEnumerable<IVariable> parameters, Action action, IEnumerable<IVariable> baseCall = null)
        {
            Method("constructor", parameters, null, () =>
            {
                if (baseCall != null)
                {
                    Super(baseCall);
                }
                action();
            });
        }

        public void Constructor(ConstructorInfo constructor, Action action, IEnumerable<IVariable> baseCall = null)
        {
            var name = "constructor";
            var parameters = new List<IVariable>();
            foreach (var parameter in constructor.GetParameters())
            {
                var p = new EntityFunctionParameterDefinition(parameter.Name,
                    TypeResolver.TranslateType(parameter.ParameterType));
                parameters.Add(p);
            }
            Method(name, parameters, null, () =>
            {
                if (baseCall != null)
                {
                    Super(baseCall);
                }
                action();
            });
        }

        public void Super(IEnumerable<IVariable> parameters)
        {
            MethodCall("super", false, parameters.ToArray());
            CloseLine();
            AppendLine();
        }

        public void Class(
            string @class,
            string @namespace,
            ODataTypeDefinition extends,
            Action action)
        {
            AddReference(extends);
            Scope("export class " + @class + " extends " + extends.AbsoluteName, action);
        }

        public void IsNotNull(Action expression)
        {
            expression();
            Append(" != null");
        }

        public void IsEqualTo(Action left, Action right)
        {
            left();
            Append(" === ");
            right();
        }

        public void IsNotEqualTo(Action left, Action right)
        {
            left();
            Append(" !== ");
            right();
        }

        public void ArrayAdd(string arrayName, Action value)
        {
            Append(arrayName);
            Append(".");
            Append("push(");
            value();
            Append(")");
        }

        public string GetThisType()
        {
            //this.constructor as Type<{entityType.Name}>
            return "Object.getPrototypeOf(this)";
        }

        public void AsDynamicObject(string name, Dictionary<string, string> keyValues)
        {
            var parameters = new List<string>();
            foreach (var parameter in keyValues)
            {
                parameters.Add($"{parameter.Key}: {parameter.Value}");
            }
            var objInstantiation = $@"{{
            {GetIndentPlusOne()}{string.Join($",\r\n{GetIndentPlusOne()}", parameters)}
            {GetIndent()}}}";
            Let(name, objInstantiation);
        }

        public void Let(string name, string instantaitor = null)
        {
            AppendLine($"let {name}{(string.IsNullOrWhiteSpace(instantaitor) ? "" : $" = {instantaitor}")};");
        }

        public void Let(Action left, Action right = null)
        {
            AppendLine();
            Append("let ");
            left();
            if (right != null)
            {
                Append(" = ");
                right();
            }
            CloseLine();
        }

        public void GenerateEnumType(EnumTypeDefinition entityType)
        {
            Scope(string.Format("export enum {0}", entityType.Name), () =>
            {
                var count = 0;
                foreach (var value in entityType.Values)
                {
                    count++;
                    AppendLineFormat("{0} = {1}{2}", value.Name, value.Value,
                        count == entityType.Values.Count ? "" : ",");
                }
            });
            if (entityType.IsFlags)
            {
                AppendLineFormat($"Enum.SetHasFlags({entityType.Name});");
            }
        }
        public void Throw(string error)
        {
            AppendLine($"throw new Error(`{error}`);");
        }

        public override string NewInstanceIdentifier(Type type, params string[] args)
        {
            return $"new {type.Name}({string.Join(", ", args)})";
        }

        public override string String(string value)
        {
            return $"`{value}`";
        }

        public override string GetCoalesce(string left, string right)
        {
            return $"{left} || {right}";
        }

        public string Cast(string value)
        {
            return $"<{value}>";
        }

        public string NameOf(string property)
        {
            return Enquote(property);
        }
    }
}
