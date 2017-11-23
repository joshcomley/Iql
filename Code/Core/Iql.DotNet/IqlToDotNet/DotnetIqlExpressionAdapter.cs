using Iql.DotNet.IqlToDotNet.Parsers;
using Iql.Parsing;

namespace Iql.DotNet.IqlToDotNet
{
    public class DotNetIqlExpressionAdapter : IqlExpressionAdapter<DotNetIqlData>
    {
        public DotNetIqlExpressionAdapter(string rootVariableName)
        {
            RootVariableName = rootVariableName;
            //Registry.Register(typeof(IqlExpression), () => new JavaScriptActionParser());
            //Registry.Register(typeof(IqlExpression), () => new JavaScriptStringSourceActionParser());
            //Registry.Register(typeof(IqlNotExpression), () => new JavaScriptNotActionParser());
            //Registry.Register(typeof(IqlParenthesisExpression), () => new JavaScriptParenthesisParser());
            Registry.Register(typeof(IqlPropertyExpression), () => new DotNetPropertyReferenceParser());
            Registry.Register(typeof(IqlRootReferenceExpression), () => new DotNetRootReferenceParser());
            Registry.Register(typeof(IqlLiteralExpression), () => new DotNetLiteralParser());
            //Registry.Register(typeof(IqlStringSubStringExpression), () => new JavaScriptStringSubStringActionParser());
            //Registry.Register(typeof(IqlParentValueExpression), () => new JavaScriptStringSourceValueActionParser());
            //Registry.Register(typeof(IqlStringLengthExpression), () => new JavaScriptStringLengthParser());
            //Registry.Register(typeof(IqlUnaryExpression), () => new JavaScriptUnaryActionParser());
            Registry.Register(typeof(IqlBinaryExpression), () => new DotNetBinaryActionParser());
        }

        public string RootVariableName { get; set; }

        public override DotNetIqlData NewData()
        {
            return new DotNetIqlData();
        }
    }
}