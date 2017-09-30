using Iql.Extensions;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlVariableExpressionReducer : IqlReducerBase<IqlVariableExpression>
    {
        public override IqlLiteralExpression Evaluate(IqlVariableExpression expression, IqlReducer reducer)
        {
            //if (reducer.EvaluateContext == null)
            //{
            //    throw new Exception("No evaluate context available for reducer.");
            //}
            object value = expression.Value;
            var type = expression.ReturnType;
#if TypeScript
            if (reducer.EvaluateContext != null)
            {
                value = reducer.EvaluateContext.Evaluate(expression.VariableName);
                if (value != null)
                {
                    type = value.GetType().ToIqlType();
                }
            }
#endif
            return new IqlLiteralExpression(value, type);
            //return expression.Value || reducer.evaluate(expression.VariableName);
        }
    }
}