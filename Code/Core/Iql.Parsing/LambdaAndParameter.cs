namespace Iql.Parsing
{
    public class LambdaAndParameter
    {
        public IqlParameteredLambdaExpression LambdaExpression { get; set; }
        public IqlRootReferenceExpression ParameterExpression { get; set; }
        public LambdaAndParameter(IqlParameteredLambdaExpression lambdaExpression, IqlRootReferenceExpression parameterExpression)
        {
            LambdaExpression = lambdaExpression;
            ParameterExpression = parameterExpression;
        }
    }
}