using System;
using Iql.Extensions;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlPropertyExpressionReducer : IqlReducerBase<IqlPropertyExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlPropertyExpression expression, IqlReducer reducer)
        {
            // We should never have a property without a parent
            var parent = reducer.Evaluate(expression.Parent);
            object type = parent.Value.GetType();
            var returnType = expression.ReturnType;
            var container = parent.Value;
            if (parent is IqlEnumLiteralExpression)
            {
                type = (parent as IqlEnumLiteralExpression).EnumType();
                returnType = IqlType.Enum;
                container = type;
            }

            var value = container.GetPropertyValueByName(expression.PropertyName);
            if (parent is IqlEnumLiteralExpression)
            {
                var enumExpression = parent as IqlEnumLiteralExpression;
                enumExpression.AddValue((long)value, expression.PropertyName);
                return parent;
            }
            if (reducer.HasAncestorOfType<IqlHasExpression>())
            {
                return new IqlEnumLiteralExpression(value as Type);
            }
            return new IqlLiteralExpression(value, returnType);
        }
    }
}