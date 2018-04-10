using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Tracking.State;

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
        IPropertyState Copy();
    }
}