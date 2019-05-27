using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.DotNet.Extensions;
using Iql.Queryable.Extensions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetBinaryActionParser : DotNetActionParserBase<IqlBinaryExpression>
    {
        public static bool IsString(IqlExpression expression)
        {
            var literalExpression = expression as IqlLiteralExpression;
            if (literalExpression != null)
            {
                return literalExpression.ReturnType == IqlType.String;
            }
            var propertyExpression = expression as IqlPropertyExpression;
            return propertyExpression?.ReturnType == IqlType.String;
        }

        public override IqlExpression ToQueryString(
            IqlBinaryExpression action,
            DotNetIqlParserContext parser)
        {
            var isStringComparison =
                (action.Kind == IqlExpressionKind.IsEqualTo || action.Kind == IqlExpressionKind.IsNotEqualTo) &&
                (IsString(action.Left) || IsString(action.Right));
            var left = parser.Parse(action.Left
#if TypeScript
                , null
#endif
            ).Expression;
            var right = parser.Parse(action.Right
#if TypeScript
                , null
#endif
            ).Expression;
            if (isStringComparison && !parser.Data.AlreadyCoalesced.Contains(action))
            {
                left = CoalesceOrUpperCase(left);
                right = CoalesceOrUpperCase(right);
            }

            left = left.GetBody();
            right = right.GetBody();
            MethodInfo method = null;
            // Deal with string concatenation
            var @operator = ResolveOperator(action);
            if (@operator == ExpressionType.Add)
            {
                method = left.Type == typeof(string) || right.Type == typeof(string) ? ConcatMethod : null;
                if (left.Type == typeof(string) && right.Type != typeof(string))
                {
                    right = AsToString(right);
                }
                if (right.Type == typeof(string) && left.Type != typeof(string))
                {
                    left = AsToString(left);
                }
            }
            switch (@operator)
            {
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                    left = parser.ValueOf<float?>(left);
                    right = parser.ValueOf<float?>(right);
                    break;
            }

            var leftIsNullableWrapped = Nullable.GetUnderlyingType(left.Type) != null;
            var rightIsNullableWrapped = Nullable.GetUnderlyingType(right.Type) != null;

            if (leftIsNullableWrapped && !rightIsNullableWrapped)
            {
                right = Expression.Convert(right, left.Type);
            }

            if (rightIsNullableWrapped && !leftIsNullableWrapped)
            {
                left = Expression.Convert(left, right.Type);
            }

            void EnsureCompatibleType(Type from, Type to, Type convertTo = null)
            {
                if (left.Type == to && right.Type == from)
                {
                    right = Expression.Convert(right, convertTo ?? left.Type);
                    if (convertTo != null)
                    {
                        left = Expression.Convert(left, convertTo);
                    }
                }
                else if (right.Type == to && left.Type == from)
                {
                    left = Expression.Convert(left, convertTo ?? right.Type);
                    if (convertTo != null)
                    {
                        right = Expression.Convert(right, convertTo);
                    }
                }
            }
            List<Type> typePrecedence = new List<Type>(new Type[]
            {
                typeof(ushort),
                typeof(uint),
                typeof(ulong),
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(decimal),
                typeof(float),
                typeof(double),
            });

            var leftIndex = typePrecedence.IndexOf(left.Type);
            var rightIndex = typePrecedence.IndexOf(right.Type);
            if (leftIndex != -1 && rightIndex != -1)
            {
                if (rightIndex < leftIndex)
                {
                    right = Expression.Convert(right, left.Type);
                }
                else if (leftIndex < rightIndex)
                {
                    left = Expression.Convert(left, right.Type);
                }
            }
            //EnsureCompatibleType(typeof(uint), typeof(int));
            //EnsureCompatibleType(typeof(ulong), typeof(long));
            //EnsureCompatibleType(typeof(ushort), typeof(short));

            //EnsureCompatibleType(typeof(int), typeof(long));
            //EnsureCompatibleType(typeof(short), typeof(int));
            //EnsureCompatibleType(typeof(short), typeof(long));

            //EnsureCompatibleType(typeof(uint), typeof(float));
            //EnsureCompatibleType(typeof(ulong), typeof(float));
            //EnsureCompatibleType(typeof(ushort), typeof(float));

            //EnsureCompatibleType(typeof(int), typeof(double));
            //EnsureCompatibleType(typeof(short), typeof(double));
            //EnsureCompatibleType(typeof(long), typeof(double));

            //EnsureCompatibleType(typeof(float), typeof(double));
            //EnsureCompatibleType(typeof(decimal), typeof(float));
            //EnsureCompatibleType(typeof(decimal), typeof(double));

            EnsureCompatibleType(typeof(object), typeof(int), typeof(int?));
            EnsureCompatibleType(typeof(object), typeof(long), typeof(int?));
            EnsureCompatibleType(typeof(object), typeof(short), typeof(int?));

            EnsureCompatibleType(typeof(object), typeof(ulong), typeof(ulong?));
            EnsureCompatibleType(typeof(object), typeof(uint), typeof(uint?));
            EnsureCompatibleType(typeof(object), typeof(ushort), typeof(ushort?));

            EnsureCompatibleType(typeof(object), typeof(double), typeof(double?));
            EnsureCompatibleType(typeof(object), typeof(float), typeof(float?));
            EnsureCompatibleType(typeof(object), typeof(decimal), typeof(decimal?));

            if (right is ConstantExpression)
            {
                var constant = right as ConstantExpression;
                if (constant.Value is DateTime)
                {
                    var dateTime = (DateTime)constant.Value;
                    DateTimeOffset dateTimeOffset = dateTime;
                    right = Expression.Constant(dateTimeOffset);
                }
            }

            if (left.Type.IsEnum && !right.Type.IsEnum)
            {
                right = Expression.Convert(right, left.Type);
            }
            if (right.Type.IsEnum && !left.Type.IsEnum)
            {
                left = Expression.Convert(left, right.Type);
            }

            Expression expression = null;
            try
            {
                expression = Expression.MakeBinary(@operator, left, right, false, method);
            }
            catch (InvalidOperationException e)
            {
                switch (@operator)
                {
                    case ExpressionType.Equal:
                        expression = Expression.Constant(false);
                        break;
                    case ExpressionType.NotEqual:
                        expression = Expression.Constant(true);
                        break;
                    default:
                        throw;
                }
            }
            return new IqlFinalExpression<Expression>(
                expression);
        }

        private static readonly Dictionary<Type, MethodInfo> ToStringMethods = new Dictionary<Type, MethodInfo>();
        private static Expression AsToString(Expression expression)
        {
            var toStringMethod = ToStringMethods.Ensure(expression.Type,
                () => expression.Type.GetMethods()
                    .First(m => m.Name == nameof(ToString) && m.GetParameters().Length == 0));
            var canBeNull = !expression.Type.IsValueType || (Nullable.GetUnderlyingType(expression.Type) != null);
            expression = Expression.Call(
                canBeNull && !DotNetExpressionConverter.DisableNullPropagation
                    ? Expression.Coalesce(expression, Expression.Constant(""))
                    : expression,
                toStringMethod);
            return expression;
        }

        private static MethodInfo ConcatMethod { get; } = typeof(string).GetMethod(nameof(string.Concat), new[] { typeof(string), typeof(string) });

        static Expression CoalesceOrUpperCase(Expression expression)
        {
            if (DotNetExpressionConverter.DisableCaseSensitivityHandling)
            {
                return expression;
            }
            if (expression is ConstantExpression)
            {
                var constantExpression = expression as ConstantExpression;
                return Expression.Constant(
                    constantExpression.Value == null
                        ? constantExpression.Value
                        : ((expression as ConstantExpression).Value as string).ToUpper());
            }

            return Expression.Condition(
                Expression.Equal(expression,
                    Expression.Constant(null)),
                expression,
                Expression.Call(expression, nameof(string.ToUpper), new Type[] { })
            );
        }

        public ExpressionType ResolveOperator(IqlBinaryExpression action)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.And:
                    return ExpressionType.AndAlso;
                case IqlExpressionKind.Or:
                    return ExpressionType.OrElse;
                case IqlExpressionKind.IsGreaterThan:
                    return ExpressionType.GreaterThan;
                case IqlExpressionKind.IsGreaterThanOrEqualTo:
                    return ExpressionType.GreaterThanOrEqual;
                case IqlExpressionKind.IsLessThan:
                    return ExpressionType.LessThan;
                case IqlExpressionKind.IsLessThanOrEqualTo:
                    return ExpressionType.LessThanOrEqual;
                case IqlExpressionKind.IsEqualTo:
                    return ExpressionType.Equal;
                case IqlExpressionKind.IsNotEqualTo:
                    return ExpressionType.NotEqual;
                case IqlExpressionKind.Modulo:
                    return ExpressionType.Modulo;
                case IqlExpressionKind.Add:
                    return ExpressionType.Add;
                case IqlExpressionKind.Subtract:
                    return ExpressionType.Subtract;
                case IqlExpressionKind.Multiply:
                    return ExpressionType.Multiply;
                case IqlExpressionKind.Divide:
                    return ExpressionType.Divide;
                case IqlExpressionKind.AddEquals:
                    return ExpressionType.AddAssign;
                case IqlExpressionKind.SubtractEquals:
                    return ExpressionType.SubtractAssign;
                case IqlExpressionKind.MultiplyEquals:
                    return ExpressionType.MultiplyAssign;
                case IqlExpressionKind.DivideEquals:
                    return ExpressionType.DivideAssign;
                case IqlExpressionKind.Has:
                    return ExpressionType.And;
            }
            throw new NotSupportedException(
                $"{nameof(IqlExpressionKind)} of type {action.Kind} is not supported for binary operations");

        }
    }
}