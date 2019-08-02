using Iql.Extensions;

namespace Iql.Parsing.Reduction.Reducers
{
    public abstract class IqlBinaryExpressionReducerBase<T> : IqlReducerBase<T>
        where T : IqlBinaryExpression
    {
        public override void Traverse(T expression, IqlTraverser reducer)
        {
            reducer.Traverse(expression.Left);
            reducer.Traverse(expression.Right);
            base.Traverse(expression, reducer);
        }

        public override IIqlLiteralExpression Evaluate(T expression, IqlReducer reducer)
        {
            var left = reducer.Evaluate(expression.Left);
            var right = reducer.Evaluate(expression.Right);
            switch (expression.Kind)
            {
                case IqlExpressionKind.BitwiseOr:
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

        public override IqlExpression ReduceStaticContent(T expression, IqlReducer reducer)
        {
            expression.Left = reducer.ReduceStaticContent(expression.Left);
            expression.Right = reducer.ReduceStaticContent(expression.Right);
            if (expression.Kind == IqlExpressionKind.And &&
                expression.Left.Kind == IqlExpressionKind.Property && expression.Right.Kind == IqlExpressionKind.Literal
                && expression.Right.ReturnType == IqlType.Integer)
            {
                return new IqlHasExpression(expression.Left, expression.Right);
            }
            if (expression.Left.Kind == IqlExpressionKind.Literal && expression.Right.Kind == IqlExpressionKind.Literal)
            {
                var value = reducer.Evaluate(expression);
                var type = value?.GetType();
                return new IqlLiteralExpression(value, type.ToIqlType());
            }
            return expression;
        }
    }
}