using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.DisplayFormatting
{
    public interface IDisplayFormatting
    {
        IEntityDisplayTextFormatter Default { get; }
        IEntityDisplayTextFormatter Get(string key);
        IEntityDisplayTextFormatter Set(Expression<Func<object, string>> expression, string key = null);
    }
}