using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Iql.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iql.Serialization
{
    class IqlJsonDeserializerInstance<TIql>
        where TIql : class
    {
        public string Json { get; set; }

        public IqlJsonDeserializerInstance(string json)
        {
            Json = json;
        }

        public TIql Deserialize()
        {
            var clone = JObject.Parse(Json);
            var typed = EnsureType(clone);
#if TypeScript
            if (typeof(TIql) == null)
            {
                return (TIql)typed;
            }
#endif
            return typed is TIql ? (TIql)typed : null;
        }

        public object EnsureType(object clone)
        {
            if (!Equals(true, clone.GetPropertyValueByName(nameof(IqlExpression.IsIqlExpression))) && Equals(null, clone.GetPropertyValueByName(nameof(IqlExpression.Kind))))
            {
                return clone;
            }

            var kind = (IqlExpressionKind)(long)clone.GetPropertyValueByName(nameof(IqlExpression.Kind));
            var expectedType = IqlExpression.ResolveExpressionTypeFromKind(kind);
            if (expectedType == null)
            {
                return clone;
            }

            var actualType = clone.GetType();
            object typedClone = clone;
            if (actualType != expectedType)
            {
                typedClone = Activator.CreateInstance(expectedType);
            }
#if TypeScript
            var properties = clone.GetType().GetRuntimeProperties();
#else
            var properties = expectedType.GetRuntimeProperties();
#endif
            foreach (var property in properties)
            {
                var value = clone.GetPropertyValueByName(property.Name);
                if (!Equals(null, value))
                {
                    var isIqlType = 
                        !(value is JArray) &&
                        (Equals(true, value.GetPropertyValueByName(nameof(IqlExpression.IsIqlExpression))) || !Equals(null, clone.GetPropertyValueByName(nameof(IqlExpression.Kind))));
                    if (Equals(true, isIqlType))
                    {
                        value = EnsureType(value);
                        if (value is IqlLiteralExpression)
                        {
                            var literal = value as IqlLiteralExpression;
                            if (!Equals(null, literal.Value))
                            {
                                literal.Value =
                                    IqlJsonDeserializer.EnsureValueType(literal.Value, literal.ReturnType.ToType(), true);
                            }
                        }
                    }
                    else if ((!(value is JToken) && value is IEnumerable) || (value is JToken && value is JArray))
                    {
                        var sourceList = (IList)value;
#if !TypeScript
                        var type = property.PropertyType;
                        var isArray = false;
                        if (type.IsArray)
                        {
                            isArray = true;
                            type = typeof(List<>).MakeGenericType(type.GetInterfaces().First(_ => _.GenericTypeArguments.Any()).GenericTypeArguments);
                        }
                        var newList = (IList)Activator.CreateInstance(type);
                        value = newList;
#else
                        var newList = sourceList;
#endif
                        for (var i = 0; i < sourceList.Count; i++)
                        {
                            var listItem = sourceList[i];
#if !TypeScript
                            if (listItem is JProperty)
                            {
                                listItem = (listItem as JProperty).Value;
                            }

                            newList.Add(EnsureType(listItem));
#else
                            newList[i] = EnsureType(listItem);
#endif
                        }

#if !TypeScript
                        if (isArray)
                        {
                            var array = newList.ToArray(type.GenericTypeArguments[0]);
                            typedClone.SetPropertyValueByName(property.Name, array);
                            value = array;
                        }
                        else
                        {
                            typedClone.SetPropertyValueByName(property.Name, newList);
                        }
#else
                        typedClone.SetPropertyValueByName(property.Name, newList);
#endif
                    }
                }
                if (property.Name != nameof(IqlExpression.IsIqlExpression))
                {
#if !TypeScript
                    if (!Equals(null, value))
                    {
                        var propertyType = property.PropertyType;
                        var underlyingType = Nullable.GetUnderlyingType(propertyType);
                        if (underlyingType != null)
                        {
                            propertyType = underlyingType;
                        }
                        if (propertyType.IsValueType && value.GetType() != propertyType)
                        {
                            if (propertyType.IsEnum)
                            {
                                var numberType = propertyType.GetFields()[0].FieldType;
                                value = Convert.ChangeType(value, numberType);
                            }
                            else
                            {
                                value = Convert.ChangeType(value, propertyType);
                            }
                        }
                    }
#endif
                    typedClone.SetPropertyValueByName(property.Name, value);
                }
            }
            return typedClone;

        }
    }
}