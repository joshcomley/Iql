using System;
using System.Linq;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Extensions
{
    public static class EntityConfigurationExtensions
    {
        public static IExpressionQueryOperation BuildExpandOperation(
            this IEntityConfiguration entityConfiguration,
            string propertyName)
        {
            var relationship = entityConfiguration
                .Relationships.Single(r =>
                {
                    var thisEnd = r.Source.Configuration == entityConfiguration
                        ? r.Source
                        : r.Target;
                    if (thisEnd.Property.Name == propertyName)
                    {
                        return true;
                    }

                    return false;
                });
            var source = relationship.Source.Configuration == entityConfiguration
                ? relationship.Source
                : relationship.Target;
            var target = relationship.Source.Configuration == entityConfiguration
                ? relationship.Target
                : relationship.Source;
            var property = entityConfiguration.Properties.Single(p => p.Name == source.Property.Name);
            var type = property.Kind == PropertyKind.Count
                ? typeof(ExpandCountOperation<,,>)
                : typeof(ExpandOperation<,,>);
            var propertyExpression = IqlExpression.GetPropertyExpression(propertyName);
            var expandOperationType = type.MakeGenericType(
                entityConfiguration.Type,
                property.ElementType,
                target.Type);
            var expandOperation =
                (IExpressionQueryOperation)Activator.CreateInstance(expandOperationType, new object[] { null });
            expandOperation.Expression = propertyExpression;
            return expandOperation;
        }
    }
}