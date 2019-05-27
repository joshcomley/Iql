using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetCountActionParser : DotNetActionParserBase<IqlCountExpression>
    {
        public override IqlExpression ToQueryString(IqlCountExpression action, DotNetIqlParserContext parser)
        {
            var method = typeof(Enumerable).GetMethods().Single(m =>
                m.Name == nameof(Enumerable.LongCount) && m.GetParameters().Length == 2);
            var entityType = parser.CurrentEntityType;
            if (entityType == null)
            {
                return Zero();
            }
            var parentExpression = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            if (parentExpression.Expression == null)
            {
                return Zero();
            }

            if (parentExpression.Expression is ConstantExpression constantExpression &&
                constantExpression.Value == null)
            {
                return Zero();
            }
            //var expressionType = parentExpression.Expression.Type;
            //var elementType = expressionType;
            //expressionType.TryGetBaseType(typeof(IEnumerable<>), type =>
            //{
            //    elementType = type.Type.GenericTypeArguments[0];
            //});
            var predicate = parser.Parse(action.Value
#if TypeScript
                        , null
#endif
            );
            method = method.MakeGenericMethod(entityType);
            var predicateExpression = predicate.Expression;
            if (predicateExpression is UnaryExpression)
            {
                predicateExpression = (predicateExpression as UnaryExpression).Operand;
            }
            var methodCallExpression =
                Expression.Call(
                    null,
                    method,
                    parentExpression.Expression,
                    predicateExpression);
            var expression =
                new IqlFinalExpression<Expression>(
                    methodCallExpression
                );
            return expression;
        }

        private static IqlExpression Zero()
        {
            return new IqlFinalExpression<Expression>(Expression.Constant(0));
        }

        //private static string[] ResolveAccessors(IqlPropertyExpression propertyExpression)
        //{
        //    var accessors = new List<string>();
        //    var expression = propertyExpression;
        //    accessors.Add(expression.PropertyName);
        //    while (expression.Parent is IqlPropertyExpression)
        //    {
        //        expression = expression.Parent as IqlPropertyExpression;
        //        accessors.Add(expression.PropertyName);
        //    }
        //    accessors.Reverse();
        //    return accessors.ToArray();
        //}
    }
}