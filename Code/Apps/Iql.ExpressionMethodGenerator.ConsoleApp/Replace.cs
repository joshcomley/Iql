using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Iql.ExpressionMethodGenerator.ConsoleApp
{
    public class Replace : MethodGenerator
    {
        public static void Generate()
        {
            var iqlTypes = typeof(IqlExpression).Assembly.GetTypes().Where(t => typeof(IqlExpression).IsAssignableFrom(t) && !t.IsAbstract);
            foreach (var type in iqlTypes)
            {
                var simpleName = type.SimpleName();
                var files = Directory.GetFiles(ResolveIqlDirectory(), simpleName + ".cs");
                var file = files[0];
                var lines = File.ReadAllLines(file).ToList();
                if (!lines.Any(l => l.Contains("internal override IqlExpression ReplaceExpressions(ReplaceContext context)")))
                {
                    files[0].Dump("Adding stub");
                    lines.Insert(lines.Count - 2, @"
		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart
			return null;
			// #ReplaceEnd
		}");
                    File.WriteAllLines(file, lines.ToArray());
                }

                lines = File.ReadAllLines(file).ToList();
                var startIndex = lines.IndexOf(lines.Single(l => l.Contains("#ReplaceStart")));
                file.Dump();
                var endIndex = lines.IndexOf(lines.Single(l => l.Contains("#ReplaceEnd"))).Dump();
                var linesList = lines.ToList();
                linesList.RemoveRange(startIndex + 1, endIndex - startIndex - 1);
                endIndex = linesList.IndexOf(lines.Single(l => l.Contains("#ReplaceEnd"))).Dump();
                var hasList = false;
                var propertyAssignments = new StringBuilder();
                foreach (var property in type.GetProperties())
                {
                    var cast = "";
                    if (typeof(IqlExpression).IsAssignableFrom(property.PropertyType))
                    {
                        cast = property.PropertyType == typeof(IqlExpression) ? "" : $"({property.PropertyType.Name})";
                        propertyAssignments.AppendLine($"{property.Name} = {cast}context.Replace(this, nameof({property.Name}), null, {property.Name});");
                    }
                    if (property.CanWrite)
                    {
                        if (typeof(IEnumerable<IqlExpression>).IsAssignableFrom(property.PropertyType))
                        {
                            hasList = true;
                            propertyAssignments.AppendLine($"if({property.Name} != null)");
                            propertyAssignments.AppendLine("{");
                            var copyType = property.PropertyType;
                            if (copyType.IsArray)
                            {
                                copyType = typeof(List<>).MakeGenericType(copyType.ArrayItemType());
                            }
                            propertyAssignments.AppendLine($"for(var i = 0; i < {property.Name}.{(property.PropertyType.IsArray ? "Length" : "Count")}; i++)".Indent());
                            propertyAssignments.AppendLine("{".Indent());
                            Type itemType = null;
                            var itemCast = "";
                            if (property.PropertyType.IsArray)
                            {
                                itemType = property.PropertyType.ArrayItemType();
                            }
                            else
                            {
                                var genericArguments = property.PropertyType.GetGenericArguments().ToList();
                                itemType = genericArguments[0];
                            }
                            itemCast = itemType == typeof(IqlExpression) ? "" : $"({itemType.InstantiateName()})";
                            propertyAssignments.AppendLine($"{property.Name}[i] = {itemCast}context.Replace(this, nameof({property.Name}), i, {property.Name}[i]);".Indent(2));
                            propertyAssignments.AppendLine("}".Indent());
                            propertyAssignments.AppendLine("}");
                        }
                    }
                }

                var implementation = $@"
		{propertyAssignments}
		var replaced = context.Replacer(context, this);
		if(replaced != this)
		{{
			#indent#return replaced;	
		}}
		return this;";

                var indent = "			";
                implementation = Regex.Replace(implementation.Trim(), @"^\s*", indent, RegexOptions.Multiline);
                implementation = implementation.ReplaceIndents();
                linesList.Insert(startIndex + 1, implementation);
                linesList.Insert(startIndex + 1, "");
                linesList.Insert(endIndex + 2, "");
                if (hasList && linesList.IndexOf("using System.Collections.Generic;") == -1)
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
                    linesList.Insert(0, "using System.Collections.Generic;");
                }
                File.WriteAllLines(file, linesList.ToArray());
            }
        }
    }
}