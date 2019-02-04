using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public interface IPropertyState
    {
        IEntityStateBase EntityState { get; }
        bool HasChanged { get; }
        object LocalValue { get; set; }
        object RemoteValue { get; set; }
        IProperty Property { get; }
        void Reset();
        IPropertyState Copy();
        void AbandonChange();
    }
}