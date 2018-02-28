using Iql.DotNet.IqlToDotNetExpression.Parsers;
using Iql.Parsing;

namespace Iql.DotNet.IqlToDotNetExpression
{
    public class DotNetIqlExpressionAdapter : IqlExpressionAdapter<DotNetIqlData>
    {
        public DotNetIqlExpressionAdapter(string rootVariableName)
        {
            RootVariableName = rootVariableName;
            Registry.Register(typeof(IqlNotExpression), () => new DotNetNotExpressionParser());
            Registry.Register(typeof(IqlStringTrimExpression), () => new DotNetStringTrimExpressionParser());
            Registry.Register(typeof(IqlPropertyExpression), () => new DotNetPropertyReferenceParser());
            Registry.Register(typeof(IqlRootReferenceExpression), () => new DotNetRootReferenceParser());
            Registry.Register(typeof(IqlLiteralExpression), () => new DotNetLiteralParser());
            Registry.Register(typeof(IqlStringSubStringExpression), () => new DotNetStringSubStringExpressionParser());
            Registry.Register(typeof(IqlStringLengthExpression), () => new DotNetStringLengthExpressionParser());
            Registry.Register(typeof(IqlBinaryExpression), () => new DotNetBinaryActionParser());
        }

        public string RootVariableName { get; set; }

        public override DotNetIqlData NewData()
        {
            return new DotNetIqlData();
        }
    }
}