using Iql.Entities.DisplayFormatting;
using System;
using System.Linq.Expressions;

namespace Iql.Server.Serialization
{
    public class DisplayFormatter : IEntityDisplayTextFormatter
    {
        public string Key { get; }
        public LambdaExpression FormatterExpression { get; set; }
        public IqlExpression FormatterExpressionIql { get; set; }
        public Func<object, string> Format { get; }
    }
}