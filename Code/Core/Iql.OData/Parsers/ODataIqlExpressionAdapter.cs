using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public class ODataIqlExpressionAdapter : IqlExpressionAdapter<ODataIqlData>
    {
        public ODataIqlExpressionAdapter()
        {
            Registry.Register(typeof(IqlExpression), () => new ODataActionParser());
            Registry.Register(typeof(IqlParenthesisExpression), () => new ODataParenthesisParser());
            Registry.Register(typeof(IqlPropertyExpression), () => new ODataPropertyReferenceParser());
            Registry.Register(typeof(IqlRootReferenceExpression), () => new ODataRootReferenceParser());
            Registry.Register(typeof(IqlLiteralExpression), () => new ODataLiteralParser());
            Registry.Register(typeof(IqlStringSubStringExpression), () => new ODataStringSubStringActionParser());
            Registry.Register(typeof(IqlParentValueExpression), () => new ODataStringSourceValueActionParser());
            Registry.Register(typeof(IqlUnaryExpression), () => new ODataUnaryActionParser());
            Registry.Register(typeof(IqlBinaryExpression), () => new ODataBinaryActionParser());
        }

        public override ODataIqlData NewData()
        {
            return new ODataIqlData();
        }
    }
}