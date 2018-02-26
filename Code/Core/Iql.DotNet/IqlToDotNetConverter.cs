using System.Linq.Expressions;
using Iql.DotNet.IqlToDotNet;
using Iql.Queryable.Expressions;

namespace Iql.DotNet
{
    public class IqlToDotNetConverter : IIqlToExpressionConverter
    {
        public LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression iql) where TEntity : class
        {
            var adapter = new DotNetIqlExpressionAdapter("entity");
            var parser = new DotNetIqlParserInstance(adapter, typeof(TEntity));
            parser.IsFilter = true;
            var dotNetExpression = parser.Parse(iql);
            return dotNetExpression.ToLambda();
        }
    }
}