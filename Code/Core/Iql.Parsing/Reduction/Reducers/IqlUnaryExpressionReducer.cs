using System;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlUnaryExpressionReducer : IqlReducerBase<IqlUnaryExpression>
    {
        public override IqlLiteralExpression Evaluate(IqlUnaryExpression expression, IqlReducer reducer)
        {
            throw new NotImplementedException();
            //expression.Value = new IqlLiteralExpression(
            //    reducer.evaluate(expression.Value), expression.ReturnType);
            //return expression;
        }

        public override IqlExpression ReduceStaticContent(IqlUnaryExpression expression, IqlReducer reducer)
        {
            throw new NotImplementedException();
            //expression.Value = reducer.reduceStaticContent(expression.Value);
            //return expression;
        }
    }
}