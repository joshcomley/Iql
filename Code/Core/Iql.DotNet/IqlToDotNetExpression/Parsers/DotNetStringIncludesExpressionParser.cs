using System.Linq;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetStringIncludesExpressionParser : DotNetStringMethodExpressionParser<IqlStringIncludesExpression>
    {
        protected override MethodInfo StringMethod => StringMethods.StringIncludesMethod;
    }
}