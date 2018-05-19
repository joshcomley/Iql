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
            var expression = Expression;
            if (expression is UnaryExpression)
            {
                expression = (expression as UnaryExpression).Operand;
            }
            if (expression is LambdaExpression)
            {
                return expression as LambdaExpression;
            }
            return Expression.Lambda(expression, Parameters);
        }

        public string ToCodeString()
        {
            return Expression.ToCSharpString();
        }
    }
}