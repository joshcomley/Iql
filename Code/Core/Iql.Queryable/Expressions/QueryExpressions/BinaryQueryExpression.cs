using System.Collections.Generic;
using System.Linq;
using Iql.Parsing;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class BinaryQueryExpression : QueryExpression
    {
        public BinaryQueryExpression(
            QueryExpressionType type,
#if TypeScript
            EvaluateContext evaluateContext,
#endif
            QueryExpression left,
            QueryExpression[] right) : base(type
#if TypeScript
            , evaluateContext
#endif
                )
        {
            Left = left;
            Right = right.ToList();
        }

        public List<QueryExpression> Right { get; set; }

        public QueryExpression Left { get; set; }

        public void Add(QueryExpression expression)
        {
            Right.Add(expression);
        }

        public List<QueryExpression> All()
        {
            var all = new List<QueryExpression>();
            all.Add(Left);
            all.AddRange(Right);
            return all;
        }

        public QueryExpression At(int index)
        {
            if (index == 0)
            {
                return Left;
            }
            return Right[index--];
        }
    }
}