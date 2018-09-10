using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Entities.ValueResolvers;

namespace Iql.Entities
{
    public interface IEntityProperty<T> : IProperty
        where T : class
    {
        IValueResolver<T> DefaultValueResolver { get; set; }
        IEntityProperty<T> UseLiteralDefaultValue(object value);
        IEntityProperty<T> UseFunctionDefaultValue(Func<T, Task<object>> resolver);
        IEntityProperty<T> IsInferredWith(Expression<Func<T, object>> expression);
        IEntityProperty<T> Configure(Action<IEntityProperty<T>> action);
    }
}