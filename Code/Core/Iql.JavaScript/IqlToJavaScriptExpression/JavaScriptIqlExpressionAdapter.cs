using Iql.JavaScript.IqlToJavaScriptExpression.Parsers;
using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScriptExpression
{
    public class JavaScriptIqlExpressionAdapter : IqlExpressionAdapter<JavaScriptIqlData>
    {
        public JavaScriptIqlExpressionAdapter()
        {
            //Registry.Register(typeof(IqlExpression), () => new JavaScriptActionParser());
            Registry.Register(typeof(IqlExpression), () => new JavaScriptStringSourceActionParser());
            Registry.Register(typeof(IqlNotExpression), () => new JavaScriptNotActionParser());
            Registry.Register(typeof(IqlParenthesisExpression), () => new JavaScriptParenthesisParser());
            Registry.Register(typeof(IqlPropertyExpression), () => new JavaScriptPropertyReferenceParser());
            Registry.Register(typeof(IqlRootReferenceExpression), () => new JavaScriptRootReferenceParser());
            Registry.Register(typeof(IqlLiteralExpression), () => new JavaScriptLiteralParser());
            Registry.Register(typeof(IqlStringSubStringExpression),
                () => new JavaScriptStringSubStringActionParser());
            Registry.Register(typeof(IqlParentValueExpression),
                () => new JavaScriptStringSourceValueActionParser());
            Registry.Register(typeof(IqlStringLengthExpression),
                () => new JavaScriptStringLengthParser());
            Registry.Register(typeof(IqlUnaryExpression), () => new JavaScriptUnaryActionParser());
            Registry.Register(typeof(IqlBinaryExpression), () => new JavaScriptBinaryActionParser());
            Registry.Register(typeof(IqlAnyAllExpression), () => new JavaScriptAnyAllActionParser());
        }

        public override JavaScriptIqlData NewData()
        {
            return new JavaScriptIqlData();
        }
    }
}