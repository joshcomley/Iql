using System;
using System.Threading.Tasks;

namespace Iql.Entities.ValueResolvers
{
    public interface IValueResolver<in TEntity>
    {
        Task<object> ResolveValueAsync(TEntity entity);
    }

    public class LiteralValueResolver<TEntity> : IValueResolver<TEntity>
    {
        public object Value { get; }

        public LiteralValueResolver(object value)
        {
            Value = value;
        }

        public Task<object> ResolveValueAsync(TEntity entity)
        {
            return Task.FromResult(Value);
        }
    }

    public class FunctionValueResolver<TEntity> : IValueResolver<TEntity>
    {
        public Func<TEntity, Task<object>> Resolver { get; }

        public FunctionValueResolver(Func<TEntity, Task<object>> resolver)
        {
            Resolver = resolver;
        }
        public async Task<object> ResolveValueAsync(TEntity entity)
        {
            return await Resolver(entity);
        }
    }

    public class CurrentUserValueResolver<TEntity> : IValueResolver<TEntity>
    {
        public CurrentUserValueResolver()
        {
        }
        public Task<object> ResolveValueAsync(TEntity entity)
        {
            return Task.FromResult<object>(null);
        }
    }
}