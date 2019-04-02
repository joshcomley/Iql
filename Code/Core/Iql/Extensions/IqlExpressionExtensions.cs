#if TypeScript
using Iql.Serialization;
#endif
using System.Collections.Generic;
using System.Linq;

namespace Iql.Extensions
{
    public static class IqlExpressionExtensions
    {
        public static IqlExpression RootExpression(this IqlExpression expression)
        {
            var parent = expression;
            while (parent.Parent != null)
            {
                parent = parent.Parent;
            }
            return parent;
        }

        public static IqlSimplePropertyPath ToSimplePropertyPath(this IqlExpression expression)
        {
            var parts = new List<string>();
            while (true)
            {
                switch (expression.Kind)
                {
                    case IqlExpressionKind.RootReference:
                        parts.Add((expression as IqlRootReferenceExpression).VariableName);
                        break;
                    case IqlExpressionKind.Property:
                        parts.Add((expression as IqlPropertyExpression).PropertyName);
                        break;
                }

                expression = expression.Parent;
                if (expression == null)
                {
                    break;
                }
            }

            parts.Reverse();

            var root = parts.FirstOrDefault();
            if (parts.Count > 0)
            {
                parts.RemoveAt(0);
            }
            return new IqlSimplePropertyPath(root, parts.ToArray());
        }

        public static TIql CloneIql<TIql>(this TIql iql)
            where TIql : IqlExpression
        {
            return (TIql) iql.Clone();
//#if !TypeScript
//            JsonSerializerSettings jss = new JsonSerializerSettings();
//            jss.TypeNameHandling = TypeNameHandling.All;
//            var json = JsonConvert.SerializeObject(iql, null, jss);
//            var clone = JsonConvert.DeserializeObject<TIql>(json, jss);
//            return clone;
//#else
//            var json = JsonConvert.SerializeObject(iql);
//            return IqlJsonDeserializer.DeserializeJson<TIql>(json);
//#endif
        }

        public static IqlPropertyExpression TryGetPropertyExpression(this IqlExpression expression)
        {
            if (expression is IqlLambdaExpression)
            {
                return (expression as IqlLambdaExpression).Body as IqlPropertyExpression;
            }
            return expression as IqlPropertyExpression;
        }
    }
}