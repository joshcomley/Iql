using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Entities.DisplayFormatting
{
    public interface IDisplayFormatting
    {
        IEnumerable<IEntityDisplayTextFormatter> All { get; }
        IEntityDisplayTextFormatter Default { get; }
        IEntityDisplayTextFormatter Get(string key);
        IEntityDisplayTextFormatter Set(Expression<Func<object, string>> expression, string key = null);
    }
}