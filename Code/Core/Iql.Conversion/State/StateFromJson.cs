using System;

namespace Iql.Conversion.State
{
    public partial class SerializedTrackingState
    {
        public SerializedSet[] Sets { get; set; }
    }

    public partial class SerializedSet
    {
        public string Type { get; set; }
        public SerializedEntityState[] EntityStates { get; set; }
    }

    public partial class SerializedEntityState
    {
        public SerializedCompositeKey CurrentKey { get; set; }
        public object PersistenceKey { get; set; }
        public bool IsNew { get; set; }
        public bool MarkedForDeletion { get; set; }
        public bool MarkedForCascadeDeletion { get; set; }
        public SerializedPropertyState[] PropertyStates { get; set; }
    }

    public partial class SerializedCompositeKey
    {
        public SerializedKeyPart[] Keys { get; set; }
    }

    public partial class SerializedKeyPart
    {
        public string Name { get; set; }
        public long Value { get; set; }
    }

    public partial class SerializedPropertyState
    {
        public object RemoteValue { get; set; }
        public object LocalValue { get; set; }
        public string Property { get; set; }
        public string Data { get; set; }
        public string Guid{ get; set; }
    }
}