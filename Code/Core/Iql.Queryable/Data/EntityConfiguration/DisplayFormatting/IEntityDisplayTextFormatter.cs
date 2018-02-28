using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.DisplayFormatting
{
    public interface IEntityDisplayTextFormatter
    {
        LambdaExpression FormatterExpression { get; }
        Func<object, string> Format { get; }
    }
}