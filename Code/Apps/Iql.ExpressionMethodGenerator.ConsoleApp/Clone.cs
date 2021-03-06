﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Iql.Extensions;

namespace Iql.ExpressionMethodGenerator.ConsoleApp
{
    public class Clone : MethodGenerator
    {
        public static void Generate()
        {
            var iqlTypes = typeof(IqlExpression).Assembly.GetTypes().Where(t => typeof(IqlExpression).IsAssignableFrom(t) && !t.IsAbstract);
            var sourceName = "source";
            var targetName = "expression";
            foreach (var type in iqlTypes)
            {
                var typeName = type.GetFullName();
                var files = Directory.GetFiles(ResolveIqlDirectory(), $"{type.SimpleName()}.cs");
                var file = files[0];
                var lines = File.ReadAllLines(file).ToList();
                if (!lines.Any(l => l.Contains($"public static {typeName} Clone({typeName} {sourceName})")))
                {
                    files[0].Dump("Adding stub");
                    lines.Insert(lines.Count - 2, $@"
		public static {typeName} Clone({typeName} {sourceName})
		{{
			// #CloneStart
			return null;
			// #CloneEnd
		}}");
                    File.WriteAllLines(file, lines.ToArray());
                }

                lines = File.ReadAllLines(file).ToList();
                var startIndex = lines.IndexOf(lines.Single(l => l.Contains("#CloneStart")));
                file.Dump();
                var endIndex = lines.IndexOf(lines.Single(l => l.Contains("#CloneEnd"))).Dump();
                var linesList = lines.ToList();
                linesList.RemoveRange(startIndex + 1, endIndex - startIndex - 1);
                endIndex = linesList.IndexOf(lines.Single(l => l.Contains("#CloneEnd"))).Dump();
                var hasList = false;
                var hasTryClone = false;
                var propertyAssignments = new StringBuilder();
                foreach (var property in type.GetProperties())
                {
                    var clone = "";
                    var cast = "";
                    var sourceProperty = $"{sourceName}.{property.Name}";
                    var targetProperty = $"{targetName}.{property.Name}";
                    if (typeof(IqlExpression).IsAssignableFrom(property.PropertyType))
                    {
                        clone = $"?.{nameof(IqlExpressionExtensions.Clone)}()";
                        cast = property.PropertyType == typeof(IqlExpression) ? "" : $"({property.PropertyType.Name})";
                    }
                    else if ((typeof(IqlLiteralExpression).IsAssignableFrom(type) ||
                              typeof(IqlUnaryExpression).IsAssignableFrom(type))
                             && property.Name == nameof(IqlLiteralExpression.Value))
                    {
                        hasTryClone = true;
                        clone = $"?.{nameof(IqlExpressionExtensions.TryCloneIql)}()";
                    }
                    if (property.CanWrite)
                    {
                        if (typeof(IEnumerable<IqlExpression>).IsAssignableFrom(property.PropertyType))
                        {
                            hasList = true;
                            propertyAssignments.AppendLine($"if({sourceProperty} == null)");
                            propertyAssignments.AppendLine("{");
                            propertyAssignments.AppendLine($"{targetProperty} = null;".Indent());
                            propertyAssignments.AppendLine("}");
                            propertyAssignments.AppendLine("else");
                            propertyAssignments.AppendLine("{");
                            var listCopyName = "listCopy";
                            var copyType = property.PropertyType;
                            if (copyType.IsArray)
                            {
                                copyType = typeof(List<>).MakeGenericType(copyType.ArrayItemType());
                            }
                            propertyAssignments.AppendLine($"var listCopy = {copyType.New()};".Indent());
                            propertyAssignments.AppendLine($"for(var i = 0; i < {sourceProperty}.{(property.PropertyType.IsArray ? "Length" : "Count")}; i++)".Indent());
                            propertyAssignments.AppendLine("{".Indent());
                            var toArray = "";
                            Type itemType = null;
                            var itemCast = "";
                            if (property.PropertyType.IsArray)
                            {
                                toArray = ".ToArray()";
                                itemType = property.PropertyType.ArrayItemType();
                            }
                            else
                            {
                                var genericArguments = property.PropertyType.GetGenericArguments().ToList();
                                itemType = genericArguments[0];
                            }
                            itemCast = itemType == typeof(IqlExpression) ? "" : $"({itemType.InstantiateName()})";
                            propertyAssignments.AppendLine($"{listCopyName}.Add({itemCast}{sourceProperty}[i]?.Clone());".Indent(2));
                            propertyAssignments.AppendLine("}".Indent());
                            propertyAssignments.AppendLine($"{targetProperty} = {listCopyName}{toArray};".Indent());
                            propertyAssignments.AppendLine("}");
                        }
                        else
                        {
                            propertyAssignments.AppendLine($"{targetProperty} = {cast}{sourceProperty}{clone};");
                        }
                    }
                }

                var implementation = $@"
		var {targetName} = {type.New(true)};
		{propertyAssignments}
		return {targetName};";

                var indent = "			";
                implementation = Regex.Replace(implementation.Trim(), @"^\s*", indent, RegexOptions.Multiline);
                implementation = implementation.ReplaceIndents();
                linesList.Insert(startIndex + 1, implementation);
                linesList.Insert(startIndex + 1, "");
                linesList.Insert(endIndex + 2, "");
                var importNamespaces = new List<string>();
                if (hasList)
                {
                    importNamespaces.Add("System.Collections.Generic");
                }

                if (hasTryClone)
                {
                    importNamespaces.Add("Iql.Extensions");
                }
                foreach (var importNamespace in importNamespaces)
                {
                    var fullImport = $"using {importNamespace};";
                    if (linesList.IndexOf(fullImport) == -1)
                    {
                        var addSpace = false;
                        if (!linesList.Any(l => l.StartsWith("using ")))
                        {
                            addSpace = true;
                        }
                        if (addSpace)
                        {
                            linesList.Insert(0, "");
                        }
                        linesList.Insert(0, fullImport);
                    }
                }
                File.WriteAllLines(file, linesList.ToArray());
            }
        }
    }
}