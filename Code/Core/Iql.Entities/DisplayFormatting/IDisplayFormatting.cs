using System;
using System.Linq.Expressions;

namespace Iql.Entities.DisplayFormatting
{
    public interface IDisplayFormatting
    {
        IEntityDisplayTextFormatter Default { get; }
        IEntityDisplayTextFormatter Get(string key);
        IEntityDisplayTextFormatter Set(Expression<Func<object, string>> expression, string key = null);
    }
}