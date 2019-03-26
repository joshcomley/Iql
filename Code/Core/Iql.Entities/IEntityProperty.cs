using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Entities.InferredValues;
using Iql.Entities.Permissions;
using Iql.Entities.ValueResolvers;

namespace Iql.Entities
{
    public interface IEntityProperty<T> : IProperty
        where T : class
    {
        IValueResolver<T> DefaultValueResolver { get; set; }
        IEntityProperty<T> UseLiteralDefaultValue(object value);
        IEntityProperty<T> UseFunctionDefaultValue(Func<T, Task<object>> resolver);
        IEntityProperty<T> IsInferredWith(Expression<Func<InferredValueContext<T>, object>> expression, bool onlyIfNew = false, InferredValueKind kind = InferredValueKind.Always, bool canOverride = false, params string[] onPropertyChanges);
        IEntityProperty<T> IsConditionallyInferredWith(Expression<Func<InferredValueContext<T>, object>> expression, Expression<Func<InferredValueContext<T>, bool>> condition);
        IEntityProperty<T> Configure(Action<IEntityProperty<T>> action);
        IEntityProperty<T> DefineUserPermission<TUser>(Expression<Func<IqlEntityUserPermissionContext<T, TUser>, IqlUserPermission>> action) where TUser : class;
    }
}