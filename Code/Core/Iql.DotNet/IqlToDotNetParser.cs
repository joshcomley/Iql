using System;
using System.Linq.Expressions;
using Iql.DotNet.IqlToDotNet;

namespace Iql.DotNet
{
    public static class IqlToDotNetParser
    {
        public static LambdaExpression GetExpression(IqlExpression iql, Type rootEntityType)
        {
            var adapter = new DotNetIqlExpressionAdapter("entity");
            var parser = new DotNetIqlParserInstance(adapter, rootEntityType);
            parser.IsFilter = true;
            var javascriptExpression = parser.Parse(iql
#if TypeScript
            , null
#endif
            );
            throw new NotImplementedException();
            //var javascript = new JavaScriptExpression(adapter.RootVariableName, javascriptExpression);
            //return javascript;
        }
    }
}