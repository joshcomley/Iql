using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetStringIndexOfExpressionParser : DotNetStringMethodExpressionParser<IqlStringIndexOfExpression>
    {
        protected override MethodInfo StringMethod => StringMethods.StringIndexOfMethod;
    }
}