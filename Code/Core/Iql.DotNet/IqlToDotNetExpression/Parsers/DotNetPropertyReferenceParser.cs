using System.Linq.Expressions;
using System.Reflection;
using Iql.Parsing.Extensions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetPropertyReferenceParser : DotNetActionParserBase<IqlPropertyExpression>
    {
        static DotNetPropertyReferenceParser()
        {
            NullPropagateMethod = typeof(IqlParsingObjectExtensions).GetMethod(nameof(IqlParsingObjectExtensions.NullPropagate));
        }

        public static MethodInfo NullPropagateMethod { get; }

        public override IqlExpression ToQueryString(IqlPropertyExpression action,
            DotNetIqlParserContext parser)
        {
            var dotNetOutput = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            );
            var property = dotNetOutput.Expression.Type.GetProperty(action.PropertyName);
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    DotNetExpressionConverter.DisableNullPropagation 
                        ? Expression.PropertyOrField(dotNetOutput.Expression, action.PropertyName)
                        : (Expression)Expression.Call(NullPropagateMethod.MakeGenericMethod(property.PropertyType), dotNetOutput.Expression, Expression.Constant(action.PropertyName))
                );
            return expression;
        }
    }
}