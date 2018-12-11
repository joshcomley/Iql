using Iql.DotNet.IqlToDotNetExpression.Parsers;
using Iql.Parsing;

namespace Iql.DotNet.IqlToDotNetExpression
{
    public class DotNetIqlExpressionAdapter : IqlExpressionAdapter<DotNetIqlData>
    {
        public DotNetIqlExpressionAdapter(string rootVariableName)
        {
            RootVariableName = rootVariableName;
            Registry.Register(typeof(IqlCollectitonQueryExpression), () => new DotNetDataSetQueryExpressionParser());
            Registry.Register(typeof(IqlIntersectsExpression), () => new DotNetIntersectsParser());
            Registry.Register(typeof(IqlLengthExpression), () => new DotNetLengthParser());
            Registry.Register(typeof(IqlDistanceExpression), () => new DotNetDistanceParser());
            Registry.Register(typeof(IqlInvocationExpression), () => new DotNetInvocationParser());
            Registry.Register(typeof(IqlVariableExpression), () => new DotNetVariableParser());
            Registry.Register(typeof(IqlLambdaExpression), () => new DotNetLambdaParser());
            Registry.Register(typeof(IqlExpandExpression), () => new DotNetExpandExpressionParser());
            Registry.Register(typeof(IqlWithKeyExpression), () => new DotNetWithKeyExpressionParser());
            Registry.Register(typeof(IqlNotExpression), () => new DotNetNotExpressionParser());
            Registry.Register(typeof(IqlStringTrimExpression), () => new DotNetStringTrimExpressionParser());
            Registry.Register(typeof(IqlPropertyExpression), () => new DotNetPropertyReferenceParser());
            Registry.Register(typeof(IqlRootReferenceExpression), () => new DotNetRootReferenceParser());
            Registry.Register(typeof(IqlEnumLiteralExpression), () => new DotNetEnumLiteralParser());
            Registry.Register(typeof(IqlSpecialValueExpression), () => new DotNetSpecialValueParser());
            Registry.Register(typeof(IqlLiteralExpression), () => new DotNetLiteralParser());
            Registry.Register(typeof(IqlStringSubStringExpression), () => new DotNetStringSubStringExpressionParser());
            Registry.Register(typeof(IqlStringLengthExpression), () => new DotNetStringLengthExpressionParser());
            Registry.Register(typeof(IqlBinaryExpression), () => new DotNetBinaryActionParser());
            Registry.Register(typeof(IqlCountExpression), () => new DotNetCountActionParser());
            Registry.Register(typeof(IqlConditionExpression), () => new DotNetConditionActionParser());
            Registry.Register(typeof(IqlAnyAllExpression), () => new DotNetAnyAllActionParser());
        }

        public string RootVariableName { get; set; }

        public override DotNetIqlData NewData()
        {
            return new DotNetIqlData();
        }
    }
}