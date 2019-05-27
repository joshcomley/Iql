using System.Linq;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class StringMethods
    {
        static StringMethods()
        {
            var methods = typeof(string).GetMethods();
            MethodInfo FindMethod(string methodName)
            {
                return methods.Single(m =>
                {
                    if (m.Name != methodName)
                    {
                        return false;

                    }

                    var parameters = m.GetParameters();
                    if (parameters.Length != 1)
                    {
                        return false;
                    }

                    if (parameters[0].ParameterType != typeof(string))
                    {
                        return false;
                    }

                    return true;
                });
            }
            StringIndexOfMethod = FindMethod(nameof(string.IndexOf));
            StringIncludesMethod = FindMethod(nameof(string.Contains));
            StringToUpperMethod = methods.Single(m => m.Name == nameof(string.ToUpper) && m.GetParameters().Length == 0);
        }

        public static MethodInfo StringToUpperMethod { get; set; }
        public static MethodInfo StringIndexOfMethod { get; set; }
        public static MethodInfo StringIncludesMethod { get; set; }

    }
}