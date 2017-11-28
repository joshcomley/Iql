using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.Serialization
{
    public class IqlSerializer
    {
        private static readonly Type[] IqlTypes;

        static IqlSerializer()
        {
            var allTypes = typeof(IqlExpression).GetTypeInfo().Assembly.GetTypes();
            IqlTypes = allTypes.Where(t =>
                    typeof(IqlExpression).GetTypeInfo().IsAssignableFrom(t) &&
                    t != typeof(IqlFinalExpression<>)
                )
                .ToArray();
        }

        public static string SerializeToXml<T>(Expression<Func<T, bool>> expression)
        {
            return SerializeToXml(ExpressionToIqlExpressionParser<T>.Parse(expression));
        }

        public static string SerializeToXml(IqlExpression expression)
        {
            return expression.SerializeToXml(IqlTypes);
        }

        public static IqlExpression DeserializeFromXml(string xml)
        {
            return xml.DeserializeFromXml<IqlExpression>(IqlTypes);
        }
    }
}