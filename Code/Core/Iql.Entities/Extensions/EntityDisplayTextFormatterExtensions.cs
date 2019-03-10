using System.Collections.Generic;
using System.Linq;
using Iql.Conversion;
using Iql.Entities.DisplayFormatting;

namespace Iql.Entities.Extensions
{
    public static class EntityDisplayTextFormatterExtensions
    {
        public static IqlPropertyPath[] ResolveUniqueRelationshipPaths(
            this IEntityDisplayTextFormatter displayFormatter, IEntityConfiguration entityConfiguration)
        {
            var uniquePaths = displayFormatter.ResolveUniquePaths(entityConfiguration);
            var result = new List<IqlPropertyPath>();
            foreach (var path in uniquePaths)
            {
                if (string.IsNullOrWhiteSpace(path.RelationshipPathToHere) || result.Any(_ => _.RelationshipPathToHere == path.RelationshipPathToHere))
                {
                    continue;
                }
                result.Add(path.RelationshipPath);
            }

            return result.ToArray();
        }

        public static IqlPropertyPath[] ResolveUniqueTextSearchablePaths(this IEntityDisplayTextFormatter displayFormatter,
            IEntityConfiguration entityConfiguration)
        {
            if (displayFormatter == null)
            {
                return ResolveAutoTextSearchablePaths(entityConfiguration);
            }
            var uniquePaths = displayFormatter.ResolveUniquePaths(entityConfiguration);
            var result = new List<IqlPropertyPath>();
            foreach (var path in uniquePaths)
            {
                var type = path.Property.TypeDefinition.ToIqlType();
                switch (type)
                {
                    case IqlType.String:
                    case IqlType.Integer:
                    case IqlType.Decimal:
                        result.Add(path);
                        break;
                }
            }
            var paths = result.ToArray();
            return paths.Length == 0 ? ResolveAutoTextSearchablePaths(entityConfiguration) : paths;
        }

        private static IqlPropertyPath[] ResolveAutoTextSearchablePaths(IEntityConfiguration entityConfiguration)
        {
            return entityConfiguration.DisplayFormatting.ResolveAutoProperties().Properties
                .Select(_ => IqlPropertyPath.FromProperty(_)).ToArray();
        }

        public static IqlPropertyPath[] ResolveUniquePaths(this IEntityDisplayTextFormatter displayFormatter, IEntityConfiguration entityConfiguration)
        {
            if (displayFormatter != null)
            {
                var expression = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(displayFormatter.FormatterExpression, entityConfiguration.Type);
                var propertyExpressions = expression.Expression.TopLevelPropertyExpressions().ToArray();
                var propertyPaths = new List<IqlPropertyPath>();
                for (var i = 0; i < propertyExpressions.Length; i++)
                {
                    var path = IqlPropertyPath.FromPropertyExpression(entityConfiguration, propertyExpressions[i].Expression as IqlPropertyExpression);
                    propertyPaths.Add(path);
                }

                return propertyPaths.ToArray();
            }
            return new IqlPropertyPath[]{};
        }
    }
}