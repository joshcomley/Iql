namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlReducerRegistry : IqlReducerRegistryBase
    {
        public IqlReducerRegistry()
        {
            Register(typeof(IqlCollectionQueryExpression), () => new IqlCollectitonQueryExpressionReducer());
            Register(typeof(IqlLambdaExpression), () => new IqlLambdaExpressionReducer());
            Register(typeof(IqlParentValueExpression), () => new IqlParentValueExpressionReducer());
            Register(typeof(IqlToStringExpression), () => new IqlToStringExpressionReducer());
            Register(typeof(IqlStringIncludesExpression), () => new IqlStringIncludesExpressionReducer());
            Register(typeof(IqlStringIndexOfExpression), () => new IqlStringIndexOfExpressionReducer());
            Register(typeof(IqlStringSubStringExpression), () => new IqlStringSubStringExpressionReducer());
            Register(typeof(IqlStringStartsWithExpression), () => new IqlStringStartsWithExpressionReducer());
            Register(typeof(IqlStringEndsWithExpression), () => new IqlStringEndsWithExpressionReducer());
            Register(typeof(IqlStringConcatExpression), () => new IqlStringConcatExpressionReducer());
            Register(typeof(IqlStringTrimExpression), () => new IqlStringTrimExpressionReducer());
            Register(typeof(IqlStringToLowerCaseExpression), () => new IqlStringToLowerCaseExpressionReducer());
            Register(typeof(IqlStringToUpperCaseExpression), () => new IqlStringToUpperCaseExpressionReducer());
            Register(typeof(IqlPropertyExpression), () => new IqlPropertyExpressionReducer());
            Register(typeof(IqlLengthExpression), () => new IqlLengthExpressionReducer());
            Register(typeof(IqlVariableExpression), () => new IqlVariableExpressionReducer());
            Register(typeof(IqlLiteralExpression), () => new IqlLiteralExpressionReducer());
            Register(typeof(IqlAddExpression), () => new IqlAddExpressionReducer());
            Register(typeof(IqlUnarySubtractExpression), () => new IqlUnarySubtractExpressionReducer());
            Register(typeof(IqlUnaryExpression), () => new IqlUnaryExpressionReducer());
            Register(typeof(IqlBinaryExpression), () => new IqlBinaryExpressionReducer());
            Register(typeof(IqlIsNotEqualToExpression), () => new IqlIsNotEqualToExpressionReducer());
            Register(typeof(IqlAggregateExpression), () => new IqlAggregateExpressionReducer());
            Register(typeof(IqlParenthesisExpression), () => new IqlParenthesisExpressionReducer());
        }
    }
}