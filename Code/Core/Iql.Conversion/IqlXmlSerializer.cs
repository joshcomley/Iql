#if !TypeScript
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Conversion;
using Iql.Entities.Permissions;

namespace Iql.DotNet.Serialization
{
    public class IqlXmlSerializer
    {
        private static readonly Type[] IqlTypes;

        static IqlXmlSerializer()
        {
            var allTypes = typeof(IqlExpression).GetTypeInfo().Assembly.GetTypes().ToList();
            allTypes = allTypes.Where(t =>
                    typeof(IqlExpression).GetTypeInfo().IsAssignableFrom(t) &&
                    t != typeof(IqlFinalExpression<>) &&
                    !t.IsAbstract && 
                    !t.IsInterface
                )
                .ToList();
            allTypes.Add(typeof(IqlUserPermission));
            IqlTypes = allTypes.ToArray();
        }

        public static string SerializeToXml<TEntity, TOut>(Expression<Func<TEntity, TOut>> expression)
        {
            return SerializeToXml((LambdaExpression)expression);
        }

        public static string SerializeToXml(LambdaExpression expression)
        {
            var parameter = expression.Parameters.First();
            var parser = Activator.CreateInstance(IqlConverter.Instance.GetType());
            var method = parser.GetType().GetMethod(nameof(IExpressionConverter.ConvertLambdaExpressionToIqlByType));
            var expressionResult = (ExpressionResult<IqlExpression>)method.Invoke(parser, new object[] { expression, parameter.Type });
            return SerializeToXml(expressionResult.Expression);
            //return SerializeToXml(IqlConverter.Instance.ConvertLambdaExpressionToIql<>().Parse(expression));
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
#endif