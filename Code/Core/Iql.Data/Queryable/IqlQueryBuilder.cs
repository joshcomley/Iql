using System.Collections.Generic;
using System.Linq;
using Iql.Extensions;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable
{
    public static class IqlQueryBuilder
    {
        public static IqlExpression BuildSearchKeyQuery(this IEntityConfiguration entityConfiguration, IEnumerable<object> keys)
        {
            var root = new IqlRootReferenceExpression("root", "");
            var wheres = new List<IqlExpression>();
            foreach (var id in keys)
            {
                var entityWheres = new List<IqlExpression>();
                CompositeKey compositeKey;
                if (id is CompositeKey)
                {
                    compositeKey = id as CompositeKey;
                }
                else if(entityConfiguration.Key.Properties.Length == 1 && id.GetType() == entityConfiguration.Key.Properties[0].TypeDefinition.Type)
                {
                    var primaryKey = entityConfiguration.Key.Properties[0];
                    compositeKey = new CompositeKey(1);
                    compositeKey.Keys[0] = new KeyValue(primaryKey.Name, id, primaryKey.TypeDefinition);
                }
                else
                {
                    compositeKey = new CompositeKey(entityConfiguration.Key.Properties.Length);
                    for (var i = 0; i < entityConfiguration.Key.Properties.Length; i++)
                    {
                        var keyProperty = entityConfiguration.Key.Properties[i];
                        var value = keyProperty.PropertyGetter(id);
                        if (value != null)
                        {
                            compositeKey.Keys[i] = new KeyValue(keyProperty.Name, value, keyProperty.TypeDefinition);
                        }
                    }
                }

                for (var i = 0; i < compositeKey.Keys.Length; i++)
                {
                    var keyPart = compositeKey.Keys[i];
                    if (keyPart != null)
                    {
                        var propertyExpression = new IqlPropertyExpression(keyPart.Name, root);
                        var where = new IqlIsEqualToExpression(
                            propertyExpression,
                            new IqlLiteralExpression(keyPart.Value, keyPart.ValueType.Type.ToIqlType()));
                        entityWheres.Add(where);
                    }
                }

                var expression = entityWheres.And();
                wheres.Add(expression);
            }

            var query = wheres.Or();
            return query;
        }

        public static IqlExpression BuildSearchQuery(this IEntityConfiguration entityConfiguration, string search, PropertySearchKind searchKind = PropertySearchKind.Secondary)
        {
            return BuildSearchPropertiesQuery(search, entityConfiguration.ResolveSearchProperties(searchKind));
        }

        public static IqlExpression BuildSearchPropertiesQuery(string search, IEnumerable<IProperty> searchFields)
        {
            var searchFieldsArray = searchFields.ToArray();
            var root = new IqlRootReferenceExpression("root", "");
            var expressions = new List<IqlExpression>();
            for (var i = 0; i < searchFieldsArray.Length; i++)
            {
                var property = new IqlPropertyExpression(searchFieldsArray[i].Name, root);
                var where = new IqlStringIncludesExpression(
                    property,
                    new IqlLiteralExpression(search, IqlType.String));
                expressions.Add(where);
            }
            var query = Or(expressions);
            return query;
        }

        public static IqlExpression And(this IEnumerable<IqlExpression> expressions)
        {
            return expressions.AndOr(true);
        }

        public static IqlExpression Or(this IEnumerable<IqlExpression> expressions)
        {
            return expressions.AndOr(false);
        }

        private static IqlExpression AndOr(this IEnumerable<IqlExpression> expressionsEnumerable, bool isAnd)
        {
            if (expressionsEnumerable == null)
            {
                return null;
            }
            var expressions = expressionsEnumerable.Where(e => e != null).ToArray();
            if (expressions.Length <= 0)
            {
                return null;
            }

            IqlExpression expression = null;
            if (expressions.Length == 1)
            {
                expression = expressions[0];
            }
            else
            {
                var aggregateExpression =
                    isAnd
                        ? (IqlExpression)new IqlAndExpression(expressions[0], expressions[1])
                        : new IqlOrExpression(expressions[0], expressions[1]);
                var i = 1;
                while (i < expressions.Length - 1)
                {
                    aggregateExpression = isAnd
                        ? (IqlExpression)new IqlAndExpression(aggregateExpression, expressions[++i])
                        : new IqlOrExpression(aggregateExpression, expressions[++i]);
                }
                expression = aggregateExpression;
            }
            return expression;
        }
    }
}