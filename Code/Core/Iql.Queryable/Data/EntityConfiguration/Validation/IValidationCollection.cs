using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IValidationCollection
    {
        IEnumerable<IValidation> All { get; }
        IValidation Get(string key);
        IValidation Add(Expression<Func<object, bool>> expression, string key, string message);
        void Remove(string key);
    }
}