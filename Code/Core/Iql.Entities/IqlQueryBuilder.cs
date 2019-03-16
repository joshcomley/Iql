using System.Collections.Generic;
using System.Linq;
using Iql.Entities;
using Iql.Entities.Search;
using Iql.Extensions;

namespace Iql.Data.Queryable
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
                        var value = keyProperty.GetValue(id);
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
                        var iqlIsEqualToExpression = new IqlIsEqualToExpression(
                            propertyExpression,
                            new IqlLiteralExpression(keyPart.Value, keyPart.ValueType.Type.ToIqlType()));
                        entityWheres.Add(iqlIsEqualToExpression);
                    }
                }

                var expression = entityWheres.And();
                wheres.Add(expression);
            }

            var query = wheres.Or();
            return query;
        }

        public static IqlExpression BuildSearchQuery(
            this IEntityConfiguration entityConfiguration, 
            string search, 
            PropertySearchKind searchKind = PropertySearchKind.Secondary, 
            bool splitIntoTerms = false)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return null;
            }
            return BuildSearchQueryForProperties(
                search, 
                entityConfiguration.ResolveSearchProperties(searchKind), 
                splitIntoTerms);
        }

        public static IqlExpression BuildSearchQueryForPropertyExpressions(
            string search,
            IEnumerable<IqlPropertyExpression> propertyExpressions, 
            bool splitIntoTerms = false)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return null;
            }
            return BuildSearchQueryForPropertyExpressionsWithTerms(new IqlSearchText(search, splitIntoTerms),
                propertyExpressions
                );
        }

        public static IqlExpression BuildSearchQueryForPropertyPaths(
            string search, 
            IEnumerable<IqlPropertyPath> searchFields,
            bool splitIntoTerms = false)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return null;
            }
            var propertyExpressions = searchFields.Select(_ => _.Expression);
            return BuildSearchQueryForPropertyExpressions(search, propertyExpressions, splitIntoTerms);
        }

        public static IqlExpression BuildSearchQueryForProperties(
            string search, 
            IEnumerable<IProperty> searchFields, 
            bool splitIntoTerms = false)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return null;
            }
            var root = new IqlRootReferenceExpression("root", "");
            return BuildSearchQueryForPropertyExpressions(search,
                searchFields.Select(_ => new IqlPropertyExpression(_.Name, root)),
                splitIntoTerms);
        }

        public static IqlExpression BuildSearchQueryWithTerms(
            this IEntityConfiguration entityConfiguration,
            IqlSearchText terms,
            PropertySearchKind searchKind = PropertySearchKind.Secondary)
        {
            return BuildSearchQueryForPropertiesWithTerms(
                terms,
                entityConfiguration.ResolveSearchProperties(searchKind));
        }

        public static IqlExpression BuildSearchQueryForPropertiesWithTerms(
            IqlSearchText terms,
            IEnumerable<IProperty> searchFields)
        {
            var root = new IqlRootReferenceExpression("root", "");
            return BuildSearchQueryForPropertyExpressionsWithTerms(
                terms,
                searchFields.Select(_ => new IqlPropertyExpression(_.Name, root))
            );
        }

        public static IqlExpression BuildSearchQueryForPropertyPathsWithTerms(
            IqlSearchText terms,
            IEnumerable<IqlPropertyPath> searchFields)
        {
            var propertyExpressions = searchFields.Select(_ => _.Expression);
            return BuildSearchQueryForPropertyExpressionsWithTerms(terms, propertyExpressions);
        }

        public static IqlExpression BuildSearchQueryForPropertyExpressionsWithTerms(
            IqlSearchText terms,
            IEnumerable<IqlPropertyExpression> propertyExpressions)
        {
            var queries = new List<IqlExpression>();
            foreach (var term in terms.Terms)
            {
                var searchFieldsArray = propertyExpressions.ToArray();
                var expressions = new List<IqlExpression>();
                for (var i = 0; i < searchFieldsArray.Length; i++)
                {
                    var property = searchFieldsArray[i].CloneIql();
                    var stringIncludesExpression = new IqlStringIncludesExpression(
                        property,
                        new IqlLiteralExpression(term.Value, IqlType.String));
                    expressions.Add(stringIncludesExpression);
                }

                queries.Add(Or(expressions));
            }

            return Or(queries);
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