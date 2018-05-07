using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.DotNet.DotNetExpressionToIql;

namespace Iql.DotNet.Serialization
{
    public class IqlXmlSerializer
    {
        private static readonly Type[] IqlTypes;

        static IqlXmlSerializer()
        {
            var allTypes = typeof(IqlExpression).GetTypeInfo().Assembly.GetTypes();
            IqlTypes = allTypes.Where(t =>
                    typeof(IqlExpression).GetTypeInfo().IsAssignableFrom(t) &&
                    t != typeof(IqlFinalExpression<>) &&
                    !t.IsAbstract && 
                    !t.IsInterface
                )
                .ToArray();
        }

        public static string SerializeToXml<TEntity, TOut>(Expression<Func<TEntity, TOut>> expression)
        {
            return SerializeToXml(DotNetExpressionToIqlExpressionConverter.Parse(expression));
        }

        public static string SerializeToXml(LambdaExpression expression)
        {
            return SerializeToXml(DotNetExpressionToIqlExpressionConverter.Parse(expression));
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