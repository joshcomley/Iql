using System.Linq.Expressions;
using Iql.Parsing;

namespace Iql.DotNet
{
    public class DotNetOutput : IParserOutput
    {
        public ParameterExpression RootEntity { get; }
        public Expression Expression { get; }

        public DotNetOutput(ParameterExpression rootEntity, Expression expression)
        {
            RootEntity = rootEntity;
            Expression = expression;
        }

        public LambdaExpression ToLambda()
        {
            return Expression.Lambda(Expression, RootEntity);
        }

        public string ToCodeString()
        {
            return Expression.ToString();
        }
    }
}