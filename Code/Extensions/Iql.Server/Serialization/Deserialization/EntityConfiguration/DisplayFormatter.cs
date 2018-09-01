using System;
using System.Linq.Expressions;
using Iql.Entities.DisplayFormatting;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public class DisplayFormatter : IEntityDisplayTextFormatter
    {
        public string Key { get; }
        public LambdaExpression FormatterExpression { get; set; }
        public IqlExpression FormatterExpressionIql { get; set; }
        public Func<object, string> Format { get; }
        public string FormatAndIntercept(object entity, Func<IFormatterContext, IqlExpression, object, string> expression)
        {
            throw new NotImplementedException();
        }

        public string FormatAndInterceptWith(IFormatterContext context)
        {
            throw new NotImplementedException();
        }
    }
}