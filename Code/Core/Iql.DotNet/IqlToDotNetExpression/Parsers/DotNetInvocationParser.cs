using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetInvocationParser : DotNetActionParserBase<IqlInvocationExpression>
    {
        public override IqlExpression ToQueryString(IqlInvocationExpression action,
            DotNetIqlParserInstance parser)
        {
            var dotNetOutput = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            var methodInfo = ResolveMethodInfo(dotNetOutput.Expression, action.MethodName);
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    Expression.Call(
                        dotNetOutput.Expression,
                        methodInfo,
                        ResolveArguments(methodInfo, action, parser))
                );
            return expression;
        }

        private IEnumerable<Expression> ResolveArguments(MethodInfo methodInfo, IqlInvocationExpression action,
            DotNetIqlParserInstance parser)
        {
            var parameterInfos = methodInfo.GetParameters();
            for (var i = 0; i < action.Parameters.Count; i++)
            {
                var parameterInfo = parameterInfos[i];
                var parameter = action.Parameters[i];
                yield return Expression.Convert(parser.ParseExpression(parameter).Expression, parameterInfo.ParameterType);
            }
        }

        private MethodInfo ResolveMethodInfo(Expression expression, string actionMethodName)
        {
            return expression.Type.GetMethod(actionMethodName);
        }
    }
}