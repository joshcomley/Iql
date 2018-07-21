using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator
{
    public class TypeScriptTypeResolver : TypeResolver, ISchemaTypeResolver
    {
        private readonly ODataSchema _schema;

        public TypeScriptTypeResolver(ODataSchema schema, GeneratorSettings settings) : base(schema, settings)
        {
            _schema = schema;
        }

        public override GeneratorTypeDefinition ResolveTypeNameFromODataNameInternal(
            ITypeInfo type,
            bool resolveCollection = false,
            params string[] hints)
        {
            var def = new GeneratorTypeDefinition();
            if (type == null)
            {
                return def;
            }
            var allHints = hints.ToList();
            if (type.Nullable)
            {
                allHints.Add("nullable");
            }
            hints = allHints.ToArray();
            var odataTypeName = type.EdmType;
            var resolvedName = type.ResolvedType;
            if (type.EdmType.StartsWith("Collection("))
            {
                var innnerType = type.EdmType.Substring("Collection(".Length);
                innnerType = innnerType.Substring(0, innnerType.Length - 1);
                var resolved = ResolveTypeNameFromODataNameInternal(
                    new TypeInfo(innnerType));
                resolvedName = $"Array<{resolved.Name}>";
                def.IsCollection = true;
                def.ElementName = resolved.Name;
            }
            else
            {
                switch (odataTypeName)
                {
                    case "String":
                    case "Edm.String":
                    case "Guid":
                    case "Edm.Guid":
                        resolvedName = Format("string", type, hints);
                        break;
                    case "Int32":
                    case "Edm.Int32":
                    case "Int64":
                    case "Edm.Int64":
                    case "Double":
                    case "Edm.Double":
                    case "Single":
                    case "Edm.Single":
                    case "Decimal":
                    case "Edm.Decimal":
                        resolvedName = Format("number", type, hints);
                        break;
                    case "Boolean":
                    case "Edm.Boolean":
                        resolvedName = Format("boolean", type, hints);
                        break;
                    case "DateTimeOffset":
                    case "Edm.DateTimeOffset":
                        resolvedName = "Date";
                        break;
                    default:
                        var typeReference = TryResolveType(type);
                        if (typeReference != null && typeReference.Type != null)
                        {
                            //if (typeReference.Type is EnumTypeDefinition)
                            //{
                            //    resolvedName = Format("number", type, hints);
                            //    break;
                            //}
                            resolvedName = typeReference.Type.Name;
                            if (typeReference.IsCollection)
                            {
                                def.ElementName = resolvedName;
                                if (!resolveCollection)
                                {
                                    resolvedName = "Array<" + resolvedName + ">";
                                }
                                def.IsCollection = true;
                            }
                        }
                        break;
                }
            }
            def.Name = resolvedName;
            return def;
        }

        private string Format(string s, ITypeInfo typeInfo, params string[] hints)
        {
            switch (s)
            {
                case "Boolean":
                case "Number":
                case "String":
                    return hints != null && hints.Any(h => h == "raw") ? s.ToLower() : s;
                case "Void":
                    return "void";
            }
            if (hints.All(h => h != "nonull") && (typeInfo?.Nullable == true || hints.Any(h => h == "nullable")))
            {
                return $"{s} | null";
            }
            return s;
        }

        public ITypeInfo TranslateType(Type type, params string[] hints)
        {
            var typeInfo = new TypeInfo();
            typeInfo.EdmType = type.Name;
            typeInfo.Nullable =
                type.IsClass
                || type == typeof(String)
                || Nullable.GetUnderlyingType(type) != null;
            if (type.SimpleName() == "Func")
            {
                typeInfo.EdmType = "() => " + TranslateType(type.GetGenericArguments()[0]).ResolvedType;
                typeInfo.ResolvedType = typeInfo.EdmType;
                return typeInfo;
            }
            var name = ResolveName(type);
            if (typeof(IEnumerable).IsAssignableFrom(type) && !typeof(string).IsAssignableFrom(type))
            {
                name = "Array<" + name.Substring(0, name.Length - "[]".Length) + ">";
            }
            typeInfo.ResolvedType = Format(name, typeInfo, hints);
            typeInfo.ConstructorType = Format(name, typeInfo, hints.Concat(new[] { "nonull" }).ToArray());
            return typeInfo;
        }

        public string ResolveName(Type type, params string[] hints)
        {
            var unwrapped = Nullable.GetUnderlyingType(type);
            var nullable = false;
            if (unwrapped != null)
            {
                nullable = true;
                type = unwrapped;
            }
            var resolved = ResolveTypeName(type, hints);
            if (nullable)
            {
                resolved = resolved + " | null";
            }
            return resolved;
        }

        private string ResolveTypeName(Type type, string[] hints)
        {
            var typName = ResolveTypeNameInternal(type, hints, out var isPrimitiveType);
            if (isPrimitiveType && hints.Any(h => h == "use-wrapper"))
            {
                typName = string.Format("{0}{0}", typName.Substring(0, 1).ToUpper(), typName.Substring(1));
            }

            return typName;
        }

        private string ResolveTypeNameInternal(Type type, string[] hints, out bool isPrimitiveType)
        {
            isPrimitiveType = false;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                return $"Array<{ResolveTypeNameInternal(type.GetGenericArguments()[0], hints, out var _)}>";
            }
            if (type.IsAny(typeof(string)))
            {
                isPrimitiveType = true;
                return Format("string", null, hints);
            }

            if (type.IsAny(
                typeof(sbyte),
                typeof(byte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(float),
                typeof(double),
                typeof(decimal)))
            {
                isPrimitiveType = true;
                return Format("number", null, hints);
            }

            if (type.IsAny(typeof(DateTime)))
            {
                return "Date";
            }

            if (type.IsAny(typeof(bool)))
            {
                isPrimitiveType = true;
                return Format("boolean", null, hints);
            }

            return ResolveGenericName(type);
        }

        public string ResolveGenericName(Type type)
        {
            string friendlyName = type.SimpleName();
            if (type.IsGenericType)
            {
                friendlyName += $"<{string.Join(",", type.GetGenericArguments().Select(p => ResolveTypeName(p, new string[] { })))}>";
            }
            return friendlyName;
        }
    }
}