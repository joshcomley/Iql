using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Iql.ExpressionMethodGenerator.ConsoleApp
{
    public class Flatten
    {
        public static void Generate()
        {
            var iqlTypes = typeof(IqlExpression).Assembly.GetTypes().Where(t => typeof(IqlExpression).IsAssignableFrom(t) && !t.IsAbstract);
            foreach (var type in iqlTypes)
            {
                var simpleName = type.SimpleName();
                var files = Directory.GetFiles(@"D:\Code\Brandless\Iql\Code\Core\Iql", simpleName + ".cs");
                var file = files[0];
                var lines = File.ReadAllLines(file).ToList();
                if (!lines.Any(l => l.Contains($"#FlattenStart")))
                {
                    files[0].Dump("Adding stub");
                    lines.Insert(lines.Count - 2, $@"
		internal override void FlattenInternal(IqlFlattenContext context)
        {{
			// #FlattenStart
			// #FlattenEnd
        }}");
                    File.WriteAllLines(file, lines.ToArray());
                }

                lines = File.ReadAllLines(file).ToList();
                file.Dump();
                var startIndex = lines.IndexOf(lines.Single(l => l.Contains("#FlattenStart")));
                var endIndex = lines.IndexOf(lines.Single(l => l.Contains("#FlattenEnd"))).Dump();
                var linesList = lines.ToList();
                linesList.RemoveRange(startIndex + 1, endIndex - startIndex - 1);
                endIndex = linesList.IndexOf(lines.Single(l => l.Contains("#FlattenEnd"))).Dump();
                var hasList = false;
                var propertyAssignments = new StringBuilder();
                //		propertyAssignments.AppendLine("if(expressions.Contains(this))");
                //		propertyAssignments.AppendLine("{");
                //		propertyAssignments.AppendLine("return;".Indent());
                //		propertyAssignments.AppendLine("}");
                //		propertyAssignments.AppendLine($"var reaction = checker == null ? {nameof(FlattenReactionKind)}.{nameof(FlattenReactionKind.Continue)} : checker(this);");
                //		propertyAssignments.AppendLine($"if(reaction == {nameof(FlattenReactionKind)}.{nameof(FlattenReactionKind.Ignore)})");
                //		propertyAssignments.AppendLine("{");
                //		propertyAssignments.AppendLine("return;".Indent());
                //		propertyAssignments.AppendLine("}");
                //		propertyAssignments.AppendLine($"if(reaction != {nameof(FlattenReactionKind)}.{nameof(FlattenReactionKind.OnlyChildren)})");
                //		propertyAssignments.AppendLine("{");
                //		propertyAssignments.AppendLine("expressions.Add(this);".Indent());
                //		propertyAssignments.AppendLine("}");
                //		propertyAssignments.AppendLine($"if(reaction != {nameof(FlattenReactionKind)}.{nameof(FlattenReactionKind.IgnoreChildren)})");
                //		propertyAssignments.AppendLine("{");
                foreach (var property in type.GetProperties())
                {
                    var cast = "";
                    if (typeof(IqlExpression).IsAssignableFrom(property.PropertyType))
                    {
                        cast = property.PropertyType == typeof(IqlExpression) ? "" : $"({property.PropertyType.Name})";
                    }
                    if (typeof(IEnumerable<IqlExpression>).IsAssignableFrom(property.PropertyType))
                    {
                        hasList = true;
                        // 
                        propertyAssignments.AppendLine($"if({property.Name} != null)".Indent());
                        propertyAssignments.AppendLine("{".Indent());
                        propertyAssignments.AppendLine($"for(var i = 0; i < {property.Name}.{(property.PropertyType.GetProperty("Count") != null ? "Count" : "Length")}; i++)".Indent(2));
                        propertyAssignments.AppendLine("{".Indent(2));
                        propertyAssignments.AppendLine($"context.Flatten({property.Name}[i]);".Indent(3));
                        propertyAssignments.AppendLine("}".Indent(2));
                        propertyAssignments.AppendLine("}".Indent());
                    }
                    else if (typeof(IqlExpression).IsAssignableFrom(property.PropertyType))
                    {
                        propertyAssignments.AppendLine($"context.Flatten({property.Name});".Indent());
                    }
                }
                //propertyAssignments.AppendLine("}");

                var implementation = $@"{propertyAssignments.ToString()}";

                var indent = "			";
                implementation = Regex.Replace(implementation.Trim(), @"^\s*", indent, RegexOptions.Multiline);
                implementation = implementation.ReplaceIndents();

                linesList.Insert(startIndex + 1, implementation);
                linesList.Insert(startIndex + 1, "");
                linesList.Insert(endIndex + 2, "");

                foreach (var ns in new[] { "System.Collections.Generic", "System" })
                {
                    var usingLine = $"using {ns};";
                    if (linesList.IndexOf(usingLine) == -1)
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
                        linesList.Insert(0, usingLine);
                    }
                }
                File.WriteAllLines(file, linesList.ToArray());
            }
        }
    }
}