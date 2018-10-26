using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator
{
    public class CSharpTypeResolver : TypeResolver, ISchemaTypeResolver
    {
        public CSharpTypeResolver(ODataSchema schema, GeneratorSettings settings) : base(schema, settings)
        {

        }

        public override GeneratorTypeDefinition ResolveTypeNameFromODataNameInternal(ITypeInfo type, bool resolveCollection = false, params string[] hints)
        {
            var def = new GeneratorTypeDefinition();
            if (type == null)
            {
                return def;
            }

            if (type is EntityTypeDefinition)
            {
                def.Name = type.EdmType;
                def.IsCollection = false;
                return def;
            }
            var nullable = type.Nullable ? "?" : "";
            var odataTypeName = type.EdmType;
            var resolvedName = type.ResolvedType;
            if (type.EdmType.StartsWith("Collection("))
            {
                var innnerType = type.EdmType.Substring("Collection(".Length);
                innnerType = innnerType.Substring(0, innnerType.Length - 1);
                var resolved = ResolveTypeNameFromODataNameInternal(
                    new TypeInfo(innnerType));
                resolvedName = $"IEnumerable<{resolved.Name}>";
                def.ElementName = resolved.Name;
                def.IsCollection = true;
            }
            else
            {
                switch (odataTypeName)
                {
                    case "String":
                    case "Edm.String":
                        resolvedName = "string";
                        break;
                    case "GeographyPoint":
                    case "Edm.GeographyPoint":
                        resolvedName = nameof(IqlGeographyPointExpression);
                        break;
                    case "GeographyMultiPoint":
                    case "Edm.GeographyMultiPoint":
                        resolvedName = nameof(IqlGeographyMultiPointExpression);
                        break;
                    case "GeographyPolygon":
                    case "Edm.GeographyPolygon":
                        resolvedName = nameof(IqlGeographyPolygonExpression);
                        break;
                    case "GeographyMultiPolygon":
                    case "Edm.GeographyMultiPolygon":
                        resolvedName = nameof(IqlGeographyMultiPolygonExpression);
                        break;
                    case "GeographyLine":
                    case "Edm.GeographyLine":
                        resolvedName = nameof(IqlGeographyLineExpression);
                        break;
                    case "GeographyMultiLine":
                    case "Edm.GeographyMultiLine":
                        resolvedName = nameof(IqlGeographyMultiLineExpression);
                        break;
                    case "Guid":
                    case "Edm.Guid":
                        resolvedName = $"Guid{nullable}";
                        break;
                    case "Double":
                    case "Edm.Double":
                        resolvedName = $"double{nullable}";
                        break;
                    case "Single":
                    case "Edm.Single":
                        resolvedName = $"float{nullable}";
                        break;
                    case "Decimal":
                    case "Edm.Decimal":
                        resolvedName = $"decimal{nullable}";
                        break;
                    case "Int32":
                    case "Edm.Int32":
                        resolvedName = $"int{nullable}";
                        break;
                    case "Int64":
                    case "Edm.Int64":
                        resolvedName = $"long{nullable}";
                        break;
                    case "Boolean":
                    case "Edm.Boolean":
                        resolvedName = $"bool{nullable}";
                        break;
                    case "DateTimeOffset":
                    case "Edm.DateTimeOffset":
                        resolvedName = $"DateTimeOffset{nullable}";
                        break;
                    default:
                        var typeReference = TryResolveType(type);
                        if (typeReference != null && typeReference.Type != null)
                        {
                            if (typeReference.Type is EnumTypeDefinition)
                            {
                                var enumType = typeReference.Type as EnumTypeDefinition;
                                resolvedName = $"{enumType.AbsoluteName}{nullable}";
                                break;
                            }

                            resolvedName = typeReference.Type.Name;
                            if (typeReference.IsCollection)
                            {
                                if (!resolveCollection)
                                {
                                    resolvedName = $"{typeof(List<object>).SimpleName()}<{resolvedName}>";
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

        public ITypeInfo TranslateType(Type type, params string[] hints)
        {
            var typeInfo = new TypeInfo();
            var unwrapNullable = Nullable.GetUnderlyingType(type);
            if (unwrapNullable != null)
            {
                typeInfo.Nullable = true;
                type = unwrapNullable;
            }
            var name = ResolveName(type);
            typeInfo.EdmType = type.Name;
            if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
            {
                name = "List<" + name.Substring(0, name.Length - "[]".Length) + ">";
            }
            typeInfo.ResolvedType = name;
            return typeInfo;
        }

        public string ResolveName(Type type, params string[] hints)
        {
            if (type == typeof(void))
            {
                return "void";
            }
            string friendlyName = type.SimpleName();
            if (type.IsGenericType)
            {
                friendlyName += $"<{string.Join(",", type.GetGenericArguments().Select(p => ResolveName(p)))}>";
            }
            return friendlyName;
        }
    }


}