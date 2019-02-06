using System;

namespace Iql.Conversion.State
{
    public partial class TrackingState
    {
        public bool TrackEntities { get; set; }
        public Guid Id { get; set; }
        public Set[] Sets { get; set; }
    }

    public partial class Set
    {
        public string Type { get; set; }
        public EntityState[] EntityStates { get; set; }
    }

    public partial class EntityState
    {
        public CurrentKey CurrentKey { get; set; }
        public object PersistenceKey { get; set; }
        public bool IsNew { get; set; }
        public bool MarkedForDeletion { get; set; }
        public bool MarkedForCascadeDeletion { get; set; }
        public PropertyState[] PropertyStates { get; set; }
    }

    public partial class CurrentKey
    {
        public Key[] Keys { get; set; }
    }

    public partial class Key
    {
        public string Name { get; set; }
        public long Value { get; set; }
    }

    public partial class PropertyState
    {
        public string RemoteValue { get; set; }
        public string LocalValue { get; set; }
        public string Property { get; set; }
    }
}