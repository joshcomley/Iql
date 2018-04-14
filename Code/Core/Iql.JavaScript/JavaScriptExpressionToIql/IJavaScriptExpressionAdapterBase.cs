using System;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public interface IJavaScriptExpressionAdapterBase
    {
        IExpressionParserBase
            ResolveParser(JavaScriptExpressionNode expression);
    }
}