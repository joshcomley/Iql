using Iql.Parsing;

namespace Iql.DotNet.IqlToDotNetString
{
    public class DotNetStringOutput : IParserOutput
    {
        public string Expression { get; }

        public DotNetStringOutput(string expression)
        {
            Expression = expression;
        }

        public string ToCodeString()
        {
            return Expression;
        }
    }
}