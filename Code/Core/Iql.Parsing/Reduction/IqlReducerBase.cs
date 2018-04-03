using System;

namespace Iql.Parsing.Reduction
{
    public class IqlReducerBase<TIqlExpression> : IIqlReducer<TIqlExpression>
        where TIqlExpression : IqlExpression
    {
        public virtual IIqlLiteralExpression Evaluate(TIqlExpression expression, IqlReducer reducer)
        {
            throw new Exception("Method not implemented.");
        }

        public virtual IqlExpression ReduceStaticContent(TIqlExpression expression, IqlReducer reducer)
        {
            if (expression.Parent != null)
            {
                expression.Parent = (IqlExpression)reducer.ReduceStaticContent(expression.Parent);
            }
            return expression;
        }

        IIqlLiteralExpression IIqlReducerBase.Evaluate(IqlExpression expression, IqlReducer reducer)
        {
            return Evaluate((TIqlExpression)expression, reducer);
        }

        IqlExpression IIqlReducerBase.ReduceStaticContent(IqlExpression expression, IqlReducer reducer)
        {
            return ReduceStaticContent((TIqlExpression)expression, reducer);
        }
    }
}