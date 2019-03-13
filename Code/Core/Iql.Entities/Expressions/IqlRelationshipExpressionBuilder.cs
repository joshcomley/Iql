using System.Collections.Generic;
using Iql.Data.Queryable;

namespace Iql.Entities.Expressions
{
    public class IqlRelationshipExpressionBuilder
    {
        public static IqlExpression BuildEntityQuery(IqlPropertyExpression propertyExpression, CompositeKey compositeKey) {
            var expressions = new List<IqlExpression>();
            for (var i = 0; i<compositeKey.Keys.Length; i++) {
                var key = compositeKey.Keys[i];
                var parentAsReference = propertyExpression.Parent as IqlReferenceExpression;
                var newPropertyExpression = new IqlPropertyExpression(key.Name, parentAsReference);
                expressions.Add(new IqlIsEqualToExpression(newPropertyExpression,
                    new IqlLiteralExpression(key.Value, key.ValueType.Kind)));
            }
            return IqlQueryBuilder.And(expressions);
        }

    }
}