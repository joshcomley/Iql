using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Crud.Operations
{
    public interface IPropertyState
    {
        IEntityStateBase EntityState { get; }
        bool HasChanged { get; }
        object NewValue { get; set; }
        object OldValue { get; set; }
        IProperty Property { get; }
        void Reset();
    }
}