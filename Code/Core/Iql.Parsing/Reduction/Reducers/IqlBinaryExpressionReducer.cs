using Iql.Extensions;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlBinaryExpressionReducer : IqlReducerBase<IqlBinaryExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlBinaryExpression expression, IqlReducer reducer)
        {
            var left = reducer.Evaluate(expression.Left);
            var right = reducer.Evaluate(expression.Right);
            switch (expression.Type)
            {
                case IqlExpressionType.BitwiseOr:
                    {
                        if (left is IqlEnumLiteralExpression)
                        {
                            return IqlEnumLiteralExpression.Combine(left as IqlEnumLiteralExpression, right as IqlEnumLiteralExpression);
                        }
                        var l = (long)left.Value;
                        var r = (long)right.Value;
                        return new IqlEnumLiteralExpression(null).AddValue(l | r, "");
                    }
            }
            return null;
        }

        public override IqlExpression ReduceStaticContent(IqlBinaryExpression expression, IqlReducer reducer)
        {
            expression.Left = reducer.ReduceStaticContent(expression.Left);
            expression.Right = reducer.ReduceStaticContent(expression.Right);
            if (expression.Type == IqlExpressionType.And &&
                expression.Left.Type == IqlExpressionType.Property && expression.Right.Type == IqlExpressionType.Literal
                                                                   && expression.Right.ReturnType == IqlType.Integer)
            {
                return new IqlHasExpression(expression.Left, expression.Right);
            }
            if (expression.Left.Type == IqlExpressionType.Literal && expression.Right.Type == IqlExpressionType.Literal)
            {
                var value = reducer.Evaluate(expression);
                var type = value?.GetType();
                return new IqlLiteralExpression(value, type.ToIqlType());
            }
            return expression;
        }
    }
}