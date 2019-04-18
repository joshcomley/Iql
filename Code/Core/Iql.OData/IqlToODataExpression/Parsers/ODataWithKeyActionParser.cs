using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities;
using Iql.Entities.Extensions;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataWithKeyActionParser : ODataActionParserBase<IqlWithKeyExpression>
    {
        public override IqlExpression ToQueryString(IqlWithKeyExpression action, ODataIqlParserContext parser)
        {
            var compositeKey = new CompositeKey(action.KeyEqualToExpressions.Count);
            var type = parser.TypeResolver.FindTypeByType(parser.CurrentEntityType);
            var mappedType = parser.TypeResolver.GetTypeMap(type);
            type = mappedType ?? type;
            for (var i = 0; i < action.KeyEqualToExpressions.Count; i++)
            {
                var keyPart = action.KeyEqualToExpressions[i];
                var propertyExpressionIsLeft = keyPart.Left is IqlPropertyExpression;
                var left = propertyExpressionIsLeft
                    ? keyPart.Left as IqlPropertyExpression
                    : keyPart.Right as IqlPropertyExpression;
                var right = propertyExpressionIsLeft
                    ? keyPart.Right
                    : keyPart.Left;
                //var property = parser.Parse(left).ToCodeString();
                var property = type.FindProperty(left.PropertyName);
                var entityProperty = property?.EntityProperty();
                var propertyType = entityProperty?.TypeDefinition;
                compositeKey.Keys[i] = new KeyValue(
                    left.PropertyName,
                    parser.Parse(right).ToCodeString(),
                    propertyType
                );
            }

            return new IqlFinalExpression<string>(FormatKey(compositeKey, type));
        }

        public static string FormatKey(CompositeKey key, IIqlTypeMetadata type = null)
        {
            string keyString;
            if (key.Keys.Length == 1)
            {
                var keyValue = key.Keys.Single();
                keyString = GetKeyValue(keyValue, type?.FindProperty(keyValue.Name));
            }
            else
            {
                var keys = key.Keys.Select(k => k.Name + "=" + GetKeyValue(k, type?.FindProperty(k.Name)));
                keyString = string.Join(",", keys);
            }
            return keyString;
        }

        private static string GetKeyValue(KeyValue key, ITypeProperty foundProperty)
        {
            var entityProperty = foundProperty?.EntityProperty();
            if (entityProperty != null && 
                (entityProperty.TypeDefinition.Kind == IqlType.Guid || entityProperty.TypeDefinition.ConvertedFromType == "Guid"))
            {
                return key.Value.ToString();
            }
            if (foundProperty != null)
            {
                if (foundProperty.Type == typeof(string))
                {
                    return EnsureQuoted(key);
                }
            }
            else if (key.ValueType != null && key.ValueType.ConvertedFromType != "Guid")
            {
                if ((key.ValueType.Type == typeof(string) && key.ValueType.Kind != IqlType.Guid) ||
                    key.ValueType.Kind == IqlType.String)
                {
                    return EnsureQuoted(key);
                }
            }
            return key.Value.ToString();
        }

        private static string EnsureQuoted(KeyValue key)
        {
            var str = key.Value == null ? "" : key.Value.ToString();
            // Prevent double wrapping
            while (str.StartsWith("'"))
            {
                str = str.Substring(1);
            }

            while (str.EndsWith("'"))
            {
                str = str.Substring(0, str.Length - 1);
            }

            return $"\'{str}\'";
        }
    }
}