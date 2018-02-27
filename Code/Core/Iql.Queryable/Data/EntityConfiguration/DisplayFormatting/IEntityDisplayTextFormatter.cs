using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IEntityDisplayTextFormatter
    {
        Expression FormatterExpression { get; }
        Func<object, string> Format { get; }
    }
}