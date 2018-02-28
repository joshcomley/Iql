using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Validation
{
    public interface IValidation
    {
        string Key { get; }
        string Message { get; }
        Expression<Func<object, bool>> ValidationExpression { get; }
        Func<object, bool> Validate { get; }
    }
}