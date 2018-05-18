using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.DotNet.Extensions;
using Iql.Parsing;

namespace Iql.DotNet.IqlToDotNetExpression
{
    public class DotNetOutput : IParserOutput
    {
        //public ParameterExpression RootEntity { get; }
        public Expression Expression { get; }
        public IEnumerable<ParameterExpression> Parameters { get; }

        public DotNetOutput(Expression expression, IEnumerable<ParameterExpression> parameters)
        {
            //RootEntity = rootEntity;
            Expression = expression;
            Parameters = parameters;
        }

        public LambdaExpression ToLambda()
        {
            if (Expression is LambdaExpression)
            {
                return Expression as LambdaExpression;
            }
            return Expression.Lambda(Expression, Parameters);
        }

        public string ToCodeString()
        {
            return Expression.ToCSharpString();
        }
    }
}