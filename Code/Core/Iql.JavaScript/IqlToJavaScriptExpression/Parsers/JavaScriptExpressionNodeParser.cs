//using System;
//using System.Linq.Expressions;
//using Iql.Net;
//using Iql.Net.Serialization;

//namespace Iql.TypeScript.Parsers
//{
//    public static class JavaScriptExpressionNodeParser
//    {
//        public static JavaScriptExpression GetJavaScript<TEntity>(this Expression<Func<TEntity, bool>> expression)
//        {
//            var iql = ExpressionToIqlExpressionParser<TEntity>.Parse(expression);
//            return JavaScriptIqlParser.GetJavaScript(iql);
//        }

//        public static JavaScriptExpression GetJavaScript(string iql)
//        {
//            var iqlExpression = IqlSerializer.DeserializeFromXml(iql);
//            return JavaScriptIqlParser.GetJavaScript(iqlExpression);
//        }
//    }
//}

