using Iql.Conversion;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Events;

namespace Iql.Data.Crud.Operations
{
    public interface IPropertyState : IJsonSerializable
    {
        EventEmitter<ValueChangedEvent<bool>> HasChangedChanged { get; }
        EventEmitter<ValueChangedEvent<object>> RemoteValueChanged { get; }
        EventEmitter<ValueChangedEvent<object>> LocalValueChanged { get; }
        IEntityStateBase EntityState { get; }
        bool HasChanged { get; }
        object LocalValue { get; set; }
        object RemoteValue { get; set; }
        IProperty Property { get; }
        void HardReset();
        void SoftReset();
        IPropertyState Copy();
        void AbandonChange();
    }
}