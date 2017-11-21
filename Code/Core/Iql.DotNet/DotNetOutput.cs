using System.Linq.Expressions;
using Iql.Parsing;

namespace Iql.DotNet
{
    public class DotNetOutput : IParserOutput
    {
        public Expression Expression { get; }

        public DotNetOutput(Expression expression)
        {
            Expression = expression;
        }

        public string ToCodeString()
        {
            return Expression.ToString();
        }
    }
}