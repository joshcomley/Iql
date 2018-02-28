using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.DotNet.DotNetExpressionToIql;

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

        public static string SerializeToXml<TEntity, TOut>(Expression<Func<TEntity, TOut>> expression)
        {
            return SerializeToXml(DotNetExpressionToIqlExpressionParser<TEntity>.Parse(expression));
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