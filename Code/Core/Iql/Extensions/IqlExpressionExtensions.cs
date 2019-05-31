#if TypeScript
using Iql.Serialization;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Extensions;
using Iql.Serialization;

namespace Iql
{
    public static class IqlExpressionExtensions
    {
        public static T Clone<T>(this T expression)
            where T : IqlExpression
        {
            //return (T)typeof(T).GetMethod(nameof(IqlExpression.CloneDeprecated)).Invoke(expression, null);
            var type = expression.GetType();
            if (!typeof(IqlExpression).IsAssignableFrom(type))
            {
                type = IqlExpression.ResolveExpressionTypeFromKind(expression.ClaimedIqlKind().Value);
            }
            var methodInfo = type.GetMethod(nameof(IqlIsEqualToExpression.Clone), BindingFlags.Static | BindingFlags.Public);
            return (T)methodInfo.Invoke(null, new object[] {expression});
        }

        public static object TryCloneIql(this object potentialExpression)
        {
            if (potentialExpression != null)
            {
                var isIqlType = potentialExpression is IqlExpression;
                if (isIqlType || Equals(true,
                        potentialExpression.GetPropertyValueByName(nameof(IqlExpression.IsIqlExpression))))
                {
                    var iql = (IqlExpression)potentialExpression;
                    if (!isIqlType)
                    {
                        iql = iql.EnsureIsIql();
                    }
                    return iql.Clone();
                }
            }
            return potentialExpression;
        }

        public static IqlExpression RootExpression(this IqlExpression expression)
        {
            var parent = expression;
            while (parent.Parent != null)
            {
                parent = parent.Parent;
            }
            return parent;
        }

        public static ResolvedRuntimeValue TryResolveRuntimeValue(this IqlExpression expression)
        {
            var parent = expression;
            var path = new List<IqlPropertyExpression>();
            while (parent != null)
            {
                if (parent.Kind == IqlExpressionKind.Literal)
                {
                    var literal = (IqlLiteralExpression)parent;
                    var value = literal.Value;
                    var resolved = path.Count < 1;
                    if (value != null)
                    {
                        for (var i = path.Count - 1; i >= 0; i--)
                        {
                            value = value.GetPropertyValueByName(path[i].PropertyName);
                            if (value == null)
                            {
                                break;
                            }

                            if (i == 0)
                            {
                                resolved = true;
                            }
                        }
                    }
                    if (value != null && resolved)
                    {
                        return new ResolvedRuntimeValue(true, value);
                    }
                    break;
                }
                else if (parent.Kind == IqlExpressionKind.Property)
                {
                    path.Add((IqlPropertyExpression)parent);
                }
                parent = parent.Parent;
            }
            return new ResolvedRuntimeValue(false, null);
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
            return (TIql)iql.TryCloneIql();
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

    public class ResolvedRuntimeValue
    {
        public bool Success { get; }
        public object Value { get; }

        public ResolvedRuntimeValue(bool success, object value)
        {
            Success = success;
            Value = value;
        }
    }
}