using System;
using System.Linq.Expressions;

namespace Iql.Data.Configuration.DisplayFormatting
{
    public interface IEntityDisplayTextFormatter
    {
        LambdaExpression FormatterExpression { get; }
        Func<object, string> Format { get; }
    }
}