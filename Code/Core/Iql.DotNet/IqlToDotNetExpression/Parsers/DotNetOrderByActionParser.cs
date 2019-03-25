using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetOrderByActionParser : DotNetActionParserBase<IqlOrderByExpression>
    {
        static DotNetOrderByActionParser()
        {
            OrderByMethod = typeof(DotNetOrderByActionParser).GetMethod(nameof(OrderBy), BindingFlags.NonPublic | BindingFlags.Static);
        }

        public static MethodInfo OrderByMethod { get; set; }

        protected static IqlFinalExpression<Expression> OrderBy<TEntity, TKey>(
            Expression<Func<TEntity, TKey>> orderByExpression,
            IqlOrderByExpression action, 
            DotNetIqlParserContext parser)
            where TEntity : class
        {
            return new IqlFinalExpression<Expression>(
                parser.Chain<TEntity>(null, e => e.OrderBy(orderByExpression.Compile(), action.Descending)));
        }

        public override IqlExpression ToQueryStringTyped<TEntity>(IqlOrderByExpression action, DotNetIqlParserContext parser)
        {
            var csUncast = parser.Parse(action.OrderExpression).Expression;
            var lambda = (LambdaExpression) (csUncast as UnaryExpression).Operand;
            var keyType = lambda.ReturnType;
            return (IqlExpression) OrderByMethod.MakeGenericMethod(typeof(TEntity), keyType).Invoke(null, 
                new object[]
                {
                    lambda,
                    action,
                    parser
                });
        }
    }
}