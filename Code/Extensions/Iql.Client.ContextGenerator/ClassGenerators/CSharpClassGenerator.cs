using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Iql.Conversion;
using Iql.DotNet;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class CSharpClassGenerator : ClassGeneratorBase, IClassGenerator
    {
        private string _className;

        public CSharpClassGenerator(ODataSchema schema, GeneratorSettings settings) : base(schema, new CSharpTypeResolver(schema, settings), settings)
        {
            Converter = new DotNetExpressionConverter();
        }

        protected override IExpressionConverter Converter { get; }
        public override char Quote => '"';

        private void Namespace(string @namespace, Action action)
        {
            AppendLine($"namespace {Settings.Namespace ?? @namespace}");
            AppendLine("{");
            Indent(action);
            AppendLine("}");
        }

        public override void Class(
            string @class,
            string @namespace,
            string genericParameters,
            Action action,
            string baseClass = null,
            IEnumerable<string> interfaces = null)
        {
            _className = @class;
            var str = "public class " + @class + genericParameters;
            if (!string.IsNullOrWhiteSpace(baseClass))
            {
                str += " : " + baseClass;
            }
            File.BaseClassName = baseClass;
            if (interfaces != null)
            {
                var interfacesArr = interfaces as string[] ?? interfaces.ToArray();
                if (interfacesArr.Any())
                {
                    str += $"{(string.IsNullOrWhiteSpace(baseClass) ? " :" : ",")} " + string.Join(", ", interfacesArr);
                }
            }
            Namespace(@namespace, () =>
            {
                Scope(str, action);
            });
        }

        public void Field(IVariable field, Action instantiate = null)
        {
            AppendLine();
            Append(string.Format($"protected {field.TypeInfo.ResolvedType ?? TypeResolver.ResolveTypeNameFromODataName(field.TypeInfo).Name} {field.Name}"));
            if (instantiate != null)
            {
                Append(" = ");
                instantiate();
            }
            CloseLine();
            AppendLine();
        }

        public override void Property(string privacy, string name, ITypeInfo type,
            Action instantiator,
            bool instantiate,
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

            var set = getterSetter?.Set;
            var afterSet = getterSetter?.AfterSet;
            var beforeSet = getterSetter?.BeforeSet;
            var afterGet = getterSetter?.AfterGet;
            var beforeGet = getterSetter?.BeforeGet;
            var useBackingField = getterSetter != null && new[] { set, beforeSet, afterSet, beforeGet, afterGet }.Any(s => s != null);
            if (useBackingField)
            {
                getterSetter.BackingFieldName = backingName;
                getterSetter.NewValueName = "value";
                UseTempStringBuilder(sb, () =>
                {
                    Field(
                        new EntityFunctionParameterDefinition(backingName, type), instantiator);
                });
            }
            sb.AppendLine();
            sb.Append(GetIndent());
            sb.Append(privacy);
            sb.Append($"{typeName} ");
            sb.Append(name);
            if (useBackingField)
            {
                sb.AppendLine();
                sb.AppendLine(GetIndent() + "{");
                if (beforeGet == null && afterGet == null)
                {
                    sb.AppendLine(GetIndentPlus(1) + $"get => {backingName};");
                }
                else
                {
                    sb.AppendLine(GetIndentPlus(1) + "get");
                    sb.AppendLine(GetIndentPlus(1) + "{");
                    Indent(() =>
                    {
                        Indent(() =>
                        {
                            UseTempStringBuilder(sb, () =>
                            {
                                beforeGet?.Invoke();
                            });
                        });
                    });
                    if (afterGet == null)
                    {
                        sb.AppendLine(GetIndentPlus(2) + $"return {backingName};");
                    }
                    else
                    {
                        Indent(() =>
                        {
                            Indent(() =>
                            {
                                UseTempStringBuilder(sb, () =>
                                {
                                    afterGet?.Invoke();
                                });
                            });
                        });
                    }
                    sb.AppendLine(GetIndentPlus(1) + "}");
                }
                sb.AppendLine(GetIndentPlus(1) + "set");
                sb.AppendLine(GetIndentPlus(1) + "{");
                Indent(() =>
                {
                    Indent(() =>
                    {
                        UseTempStringBuilder(sb, () =>
                        {
                            if (set != null)
                            {
                                set.Invoke();
                            }
                            else
                            {
                                beforeSet?.Invoke();
                            }
                        });
                    });
                });
                if (set == null)
                {
                    sb.AppendLine(GetIndentPlus(2) + $"{backingName} = {getterSetter.NewValueName};");
                }
                Indent(() =>
                {
                    Indent(() =>
                    {
                        UseTempStringBuilder(sb, () =>
                        {
                            if (set == null)
                            {
                                afterSet?.Invoke();
                            }
                        });
                    });
                });
                sb.AppendLine(GetIndentPlus(1) + "}");
                sb.AppendLine(GetIndent() + "}");
            }
            else
            {
                sb.Append(" { get; set; }");
                if (instantiate)
                {
                    UseTempStringBuilder(sb, () =>
                    {
                        if (instantiator != null)
                        {
                            AppendInstantiate(instantiator);
                        }
                        else
                        {
                            AppendInstantiate(type, instantiationParameters);
                        }
                    });
                }
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
                Append(NewInstanceIdentifier(type.ResolvedType, instantiationParameters));
            });
        }

        public override string GetCoalesce(string left, string right)
        {
            return $"{left} ?? {right}";
        }

        public void Method(string name, IEnumerable<IVariable> parameters, ITypeInfo returnType, Action action, string privacy = null, bool async = false, bool resolveTypeName = true, Modifier modifier = Modifier.None)
        {
            var returnTypeResolved = "";
            var modifiers = "";
            if (!InConstructor)
            {
                if (privacy == null && name != "constructor")
                {
                    privacy = "public";
                }
                switch (modifier)
                {
                    case Modifier.None:
                        break;
                    default:
                        modifiers = $"{modifier.ToString().ToLower()} ";
                        break;
                }
                if (!string.IsNullOrWhiteSpace(privacy))
                {
                    privacy = privacy + " ";
                }

                privacy += modifiers;
                if (async)
                {
                    privacy = privacy + "async ";
                }
                TryAddReference(returnType);
                returnTypeResolved = resolveTypeName ? TypeResolver.ResolveTypeNameFromODataName(returnType).Name : returnType.EdmType;
                if (async)
                {
                    var promiseKind = returnTypeResolved;
                    if (string.IsNullOrWhiteSpace(returnTypeResolved))
                    {
                        promiseKind = "any";
                    }
                    returnTypeResolved = $"Task<{promiseKind}>";
                }
            }

            Scope(
                string.Format(
                    "{0}{1}{2}({3}){4}",
                    privacy,
                    $"{(InConstructor ? "" : (string.IsNullOrWhiteSpace(returnTypeResolved) ? "object " : returnTypeResolved + " "))}",
                    name,
                    FormatParameters(parameters),
                    BaseCall != null && BaseCall.Any()
                        ? $" : base({string.Join(", ", BaseCall.Select(c => c.Name))})"
                        : ""
                ),
                action);
        }

        public string TypeOfExpression(ITypeInfo type)
        {
            var name = type.ResolvedType;
            if (string.IsNullOrWhiteSpace(name))
            {
                name = TypeResolver.ResolveTypeNameFromODataName(type).Name;
            }
            return TypeOfExpression(name);
        }

        public string TypeOfExpression(string name)
        {
            return $"typeof({name})";
        }

        private string FormatParameters(IEnumerable<IVariable> parameters)
        {
            if (parameters == null)
            {
                return "";
            }
            var array = parameters as IVariable[] ?? parameters.ToArray();
            return parameters == null || !array.Any()
                ? ""
                : string.Join(",\n" + GetIndentPlusOne(), array.Select(p => p.AsCSharpParameter(Schema, Settings)));
        }

        protected bool InConstructor { get; set; }

        public void Let(string name, string instantaitor = null)
        {
            AppendLine($"var {name}{(string.IsNullOrWhiteSpace(instantaitor) ? "" : $" = {instantaitor}")};");
        }

        public void Let(Action left, Action right = null)
        {
            Append("var ");
            left();
            if (right != null)
            {
                Append(" = ");
                right();
            }
            CloseLine();
            AppendLine();
        }

        public void Throw(string error)
        {
            AppendLine($"throw new Exception(\"{error}\");");
        }

        public void Constructor(IEnumerable<IVariable> parameters, Action action, IEnumerable<IVariable> baseCall)
        {
            InConstructor = true;
            BaseCall = baseCall;
            Method($"public {_className}", parameters, null, action);
            BaseCall = null;
            InConstructor = false;
        }

        public IEnumerable<IVariable> BaseCall { get; set; }

        public void Constructor(ConstructorInfo constructor, Action action, IEnumerable<IVariable> baseCall)
        {
            var name = _className;
            var parameters = new List<IVariable>();
            foreach (var parameter in constructor.GetParameters())
            {
                var p = new EntityFunctionParameterDefinition(parameter.Name,
                    TypeResolver.TranslateType(parameter.ParameterType),
                    parameter.HasDefaultValue,
                    parameter.DefaultValue);
                parameters.Add(p);
            }
            InConstructor = true;
            BaseCall = baseCall;
            Method(name, parameters, null, action, privacy: "public");
            BaseCall = null;
            InConstructor = false;
        }

        public void Class(string @class, string @namespace, ODataTypeDefinition extends, Action action)
        {
            _className = @class;
            AddReference(extends);
            File.BaseClassName = extends.AbsoluteName;
            Namespace(@namespace, () =>
            {
                Scope("public class " + @class + " : " + extends.AbsoluteName, action);
            });
        }

        public void IsNotNull(Action action)
        {
            action();
            Append(" != null");
        }

        public void IsEqualTo(Action left, Action right)
        {
            left();
            Append(" == ");
            right();
        }

        public void IsNotEqualTo(Action left, Action right)
        {
            left();
            Append(" != ");
            right();
        }

        public void ArrayAdd(string arrayName, Action value)
        {
            Append(arrayName);
            Append(".");
            Append("Add(");
            value();
            Append(")");
        }

        public string GetThisType()
        {
            return "this.GetType()";
        }

        public void AsDynamicObject(string name, Dictionary<string, string> keyValues)
        {
            var obj = new StringBuilder();
            Let(name, "new JObject()");
            foreach (var pair in keyValues)
            {
                obj.AppendLine($"{name}[\"{pair.Key}\"] = {pair.Value};");
            }
            AppendLine(obj.ToString());
        }

        public void GenerateEnumType(EnumTypeDefinition entityType)
        {
            Namespace(entityType.Namespace, () =>
            {
                if (entityType.IsFlags)
                {
                    Append("[Flags]");
                    AppendLine();
                }
                Scope(string.Format("public enum {0}", entityType.Name), () =>
                {
                    var count = 0;
                    foreach (var value in entityType.Values)
                    {
                        count++;
                        AppendFormat("{0} = {1}", value.Name, value.Value);
                        if (count != entityType.Values.Count)
                        {
                            Append(",");
                            AppendLine();
                        }
                    }
                });
            });
        }

        public string Cast(string value)
        {
            return $"({value})";
        }

        public string NameOf(string property)
        {
            return $"nameof({property})";
        }
    }
}