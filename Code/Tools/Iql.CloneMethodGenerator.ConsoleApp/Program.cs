using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Iql.CloneMethodGenerator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(Path.GetFullPath(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath));
            while (Directory.GetFiles(path).All(f => Path.GetFileName(f) != "Iql.sln"))
            {
                path = Path.GetDirectoryName(path);
            }
            path = Path.Combine(path, "Core\\Iql").Dump();

            var iqlTypes = typeof(IqlExpression).Assembly.GetTypes().Where(t => typeof(IqlExpression).IsAssignableFrom(t) && !t.IsAbstract);
            foreach (var type in iqlTypes)
            {
                var simpleName = type.SimpleName();
                var files = Directory.GetFiles(path, simpleName + ".cs");
                var file = files[0];
                var lines = File.ReadAllLines(file).ToList();
                if (!lines.Any(l => l.Contains("public IqlExpression CloneDeprecated()")))
                {
                    files[0].Dump("Adding stub");
                    lines.Insert(lines.Count - 2, @"
		public IqlExpression CloneDeprecated()
		{
			// #CloneDeprecatedStart
			return null;
			// #CloneDeprecatedEnd
		}");
                    File.WriteAllLines(file, lines.ToArray());
                }

                lines = File.ReadAllLines(file).ToList();
                var startIndex = lines.IndexOf(lines.Single(l => l.Contains("#CloneDeprecatedStart")));
                file.Dump();
                var endIndex = lines.IndexOf(lines.Single(l => l.Contains("#CloneDeprecatedEnd"))).Dump();
                var linesList = lines.ToList();
                linesList.RemoveRange(startIndex + 1, endIndex - startIndex - 1);
                endIndex = linesList.IndexOf(lines.Single(l => l.Contains("#CloneDeprecatedEnd"))).Dump();
                var hasList = false;
                var propertyAssignments = new StringBuilder();
                foreach (var property in type.GetProperties())
                {
                    var clone = "";
                    var cast = "";
                    if (typeof(IqlExpression).IsAssignableFrom(property.PropertyType))
                    {
                        clone = "?.Clone()";
                        cast = property.PropertyType == typeof(IqlExpression) ? "" : $"({property.PropertyType.Name})";
                    }
                    if (property.CanWrite)
                    {
                        if (typeof(IEnumerable<IqlExpression>).IsAssignableFrom(property.PropertyType))
                        {
                            hasList = true;
                            propertyAssignments.AppendLine($"if({property.Name} == null)");
                            propertyAssignments.AppendLine("{");
                            propertyAssignments.AppendLine($"expression.{property.Name} = null;".Indent());
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
                            propertyAssignments.AppendLine($"for(var i = 0; i < {property.Name}.{(property.PropertyType.IsArray ? "Length" : "Count")}; i++)".Indent());
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
                            propertyAssignments.AppendLine($"{listCopyName}.Add({itemCast}{property.Name}[i]?.Clone());".Indent(2));
                            propertyAssignments.AppendLine("}".Indent());
                            propertyAssignments.AppendLine($"expression.{property.Name} = {listCopyName}{toArray};".Indent());
                            propertyAssignments.AppendLine("}");
                        }
                        else
                        {
                            propertyAssignments.AppendLine($"expression.{property.Name} = {cast}{property.Name}{clone};");
                        }
                    }
                }

                var implementation = $@"
		var expression = {type.New(true)};
		{propertyAssignments.ToString()}
		return expression;";

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

        // Define other methods and classes here

    }
    public static class TypeExtensions
    {
        public static string Indent(this string str, int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                str = "#indent#" + str;
            }
            return str;
        }
        public static string ReplaceIndents(this string str)
        {
            return str.Replace("#indent#", "	");
        }
        public static object GetDefaultValue<T>()
        {
            return default(T);
        }
        public static object DefaultValue(this Type type)
        {
            if (type.IsGenericParameter)
            {
                return $"default({type.Name})";
            }
            if (type.IsEnum)
            {
                return 0;
            }
            var m = typeof(TypeExtensions).GetMethod(nameof(GetDefaultValue)).MakeGenericMethod(type);
            try
            {
                return m.Invoke(null, new object[] { });
            }
            catch { }
            return null;
        }
        public static string ToObjectString(this object obj)
        {
            if (obj == null)
            {
                return "null";
            }
            return obj.ToString();
        }
        public static string New(this Type type, bool useDefaults = false)
        {
            var arguments = "";
            if (useDefaults)
            {
                var ctor = type.GetConstructors().First();
                var parameters = ctor.GetParameters().ToList().Where(p => !p.IsOptional);
                arguments = string.Join(", ", parameters.Select(p => p.ParameterType.DefaultValue().ToObjectString()));
            }
            return $"new {type.InstantiateName()}({arguments})";
        }

        public static Type ArrayItemType(this Type type)
        {
            return type.GetInterfaces().Where(i => i.GetGenericArguments().Count() > 0).First().GetGenericArguments()[0];
        }

        public static string InstantiateName(this Type type)
        {
            var simpleName = type.SimpleName();
            var genericParameters = type.GetGenericArguments().ToList();
            var generics = "";
            if (genericParameters.Count > 0)
            {
                generics = "<" + string.Join(", ", genericParameters.Select(g => g.Name)) + ">";
            }
            return $"{simpleName}{generics}";
        }

        public static string SimpleName(this Type type)
        {
            var simpleName = type.Name;
            if (simpleName.Contains("`"))
            {
                simpleName = simpleName.Substring(0, simpleName.LastIndexOf("`"));
            }
            return simpleName;
        }

        public static T Dump<T>(this T obj, string title = null)
        {
            if (!string.IsNullOrEmpty(title))
            {
                Console.WriteLine($"{title}:");
            }
            Console.WriteLine(obj == null ? "null" : obj.ToString());
            return obj;
        }
    }
}
