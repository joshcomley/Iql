using System;
using System.Linq.Expressions;

namespace Iql.Entities
{
    public interface IEntityProperty<T> : IProperty
        where T : class
    {
        new MediaKey<T> MediaKey { get; }
        IEntityProperty<T> IsInferredWith(Expression<Func<T, object>> expression);
        IEntityProperty<T> Configure(Action<IEntityProperty<T>> action);
    }
}