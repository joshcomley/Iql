using Iql.DotNet.IqlToDotNet;
using Iql.Parsing;

namespace Iql.DotNet
{
    public class DotNetIqlParserInstance : ActionParserInstance<DotNetIqlData, DotNetIqlExpressionAdapter, DotNetOutput>
    {
        public DotNetIqlParserInstance(DotNetIqlExpressionAdapter adapter) : base(adapter)
        {
        }

        public override DotNetOutput Parse(IqlExpression expression)
        {
            throw new System.NotImplementedException();
        }
    }
}