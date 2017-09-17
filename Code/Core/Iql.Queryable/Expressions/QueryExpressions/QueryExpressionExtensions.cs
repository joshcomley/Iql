//using Iql.Queryable.JavaScript;

//namespace Iql.Queryable.Expressions.QueryExpressions
//{
//    public static class QueryExpressionExtensions
//    {
//        public static TQueryExpression Flatten<TEntity, TQueryExpression>(this TQueryExpression queryExpression)
//            where TQueryExpression : QueryExpression
//        {
//            switch (queryExpression.Type)
//            {
//                case QueryExpressionType.Or:
//                case QueryExpressionType.And:
//                case QueryExpressionType.Where:
//                    return queryExpression.PerformFlatten<TEntity>(queryExpression);
//            }
//            return queryExpression;
//        }

//        private static WhereQueryExpression<TEntity> PerformFlatten<TEntity>(this QueryExpression queryExpression, QueryExpression context)
//        {
//            switch (queryExpression.Type)
//            {
//                case QueryExpressionType.Or:
//                case QueryExpressionType.And:
//                    return ResolveBinaryToWhere<TEntity>(
//                        queryExpression as BinaryQueryExpression,
//                        context
//                    );
//                case QueryExpressionType.Where:
//                    return queryExpression as WhereQueryExpression<TEntity>;
//            }
//            return null;
//        }

//        private static WhereQueryExpression<TEntity> ResolveBinaryToWhere<TEntity>
//        (
//            BinaryQueryExpression filter,
//            QueryExpression context
//        )
//        {
//            var expression = filter;
//            var expressionResolved = expression.Left.PerformFlatten<TEntity>(context);
//            var otherExpressions = expression.Right;
//            for (var i = 0; i < otherExpressions.Count; i++)
//            {
//                var lastExpression = expressionResolved;
//                var nextExpression = otherExpressions[i].PerformFlatten<TEntity>(context);
//                switch (filter.Type)
//                {
//                    case QueryExpressionType.And:
//                        expressionResolved = new WhereQueryExpression<TEntity>(
//                            entity =>
//                                lastExpression.Expression(entity) && nextExpression.Expression(entity),
//                            new EvaluateContext
//                            {
//                                Context = context,
//                                Evaluate = n => JavaScript.Eval(n)
//                            });
//                        break;
//                    case QueryExpressionType.Or:
//                        expressionResolved = new WhereQueryExpression<TEntity>(
//                            entity =>
//                                lastExpression.Expression(entity) || nextExpression.Expression(entity),
//                            new EvaluateContext
//                            {
//                                Context = context,
//                                Evaluate = n => JavaScript.Eval(n)
//                            });
//                        break;
//                }
//            }
//            return expressionResolved;
//        }
//    }
//}

