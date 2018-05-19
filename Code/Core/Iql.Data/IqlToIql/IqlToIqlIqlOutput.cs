using System;
using Iql.Parsing;

namespace Iql.Data.IqlToIql
{
    public class IqlToIqlIqlOutput : IParserOutput
    {
        public IqlExpression Expression { get; set; }
        public IqlToIqlIqlOutput(IqlExpression expression)
        {
            Expression = expression;
        }
        public string ToCodeString()
        {
            throw new NotImplementedException();
        }
    }
}