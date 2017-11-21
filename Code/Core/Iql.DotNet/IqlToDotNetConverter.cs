using System;
using System.Linq.Expressions;
using Iql.DotNet.IqlToDotNet;
using Iql.Queryable.Expressions;

namespace Iql.DotNet
{
    public class IqlToDotNetConverter : IIqlToNativeConverter
    {
        public Expression<LambdaExpression> Parse<TEntity>(IqlExpression expression) where TEntity : class
        {
            throw new System.NotImplementedException();
        }
    }

    public static class IqlToDotNetParser
    {
        public static LambdaExpression GetExpression(IqlExpression iql)
        {
            var adapter = new DotNetIqlExpressionAdapter("entity");
            var parser = new DotNetIqlParserInstance(adapter);
            parser.IsFilter = true;
            var javascriptExpression = parser.Parse(iql);
            throw new NotImplementedException();
            //var javascript = new JavaScriptExpression(adapter.RootVariableName, javascriptExpression);
            //return javascript;
        }
    }
}