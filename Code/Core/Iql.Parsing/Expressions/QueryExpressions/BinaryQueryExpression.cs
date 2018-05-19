using System.Collections.Generic;
using System.Linq;

namespace Iql.Parsing.Expressions.QueryExpressions
{
    public class BinaryQueryExpression : QueryExpression
    {
        public BinaryQueryExpression(
            QueryExpressionKind kind,
#if TypeScript
            EvaluateContext evaluateContext,
#endif
            QueryExpression left,
            QueryExpression[] right) : base(kind
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