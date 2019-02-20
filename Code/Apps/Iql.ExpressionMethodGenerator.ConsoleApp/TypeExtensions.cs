using System;
using System.Linq;

namespace Iql.ExpressionMethodGenerator.ConsoleApp
{
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
    }
}