using System;
using System.Linq.Expressions;

namespace Iql.Entities.DisplayFormatting
{
    public interface IEntityDisplayTextFormatter
    {
        string Key { get; }
        LambdaExpression FormatterExpression { get; }
        Func<object, string> Format { get; }
    }
}