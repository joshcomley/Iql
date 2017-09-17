using System;

namespace Iql.Parsing.Reduction
{
    public class IqlReducerBase<TIqlExpression> : IIqlReducer<TIqlExpression>
        where TIqlExpression : IqlExpression
    {
        public virtual IqlLiteralExpression Evaluate(TIqlExpression expression, IqlReducer reducer)
        {
            throw new Exception("Method not implemented.");
        }

        public virtual IqlExpression ReduceStaticContent(TIqlExpression expression, IqlReducer reducer)
        {
            if (expression.Parent != null)
            {
                expression.Parent = reducer.ReduceStaticContent(expression.Parent);
            }
            return expression;
        }

        IqlLiteralExpression IIqlReducerBase.Evaluate(IqlExpression expression, IqlReducer reducer)
        {
            return Evaluate(expression as TIqlExpression, reducer);
        }

        IqlExpression IIqlReducerBase.ReduceStaticContent(IqlExpression expression, IqlReducer reducer)
        {
            return ReduceStaticContent(expression as TIqlExpression, reducer);
        }
    }
}