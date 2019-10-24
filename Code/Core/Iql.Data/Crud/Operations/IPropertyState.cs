using System;
using Iql.Conversion;
using Iql.Conversion.State;
using Iql.Data.Events;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Events;

namespace Iql.Data.Crud.Operations
{
    public interface IPropertyState : IJsonSerializable, IStateful, IDisposable
    {
        Guid Guid { get; }
        //IEntityPropertyEvent
        EventEmitter<ValueChangedEvent<bool>> HasChangedChanged { get; }
        EventEmitter<ValueChangedEvent<bool>> HasChangedSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<object>> RemoteValueChanged { get; }
        EventEmitter<ValueChangedEvent<object>> LocalValueChanged { get; }
        IEntityStateBase EntityState { get; }
        bool HasChanged { get; }
        bool HasChangedSinceSnapshot { get; }
        bool LocalValueSet { get; }
        object LocalValue { get; set; }
        object RemoteValue { get; set; }
        object SnapshotValue { get; }
        void AddSnapshot();
        void ClearSnapshotValue();
        string Data { get; set; }
        IProperty Property { get; }
        void HardReset();
        void SoftReset();
        IPropertyState Copy();
        void Restore(SerializedPropertyState state);
    }
}