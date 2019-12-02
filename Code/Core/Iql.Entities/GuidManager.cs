using System;
using System.Collections.Generic;

namespace Iql.Entities.PropertyGroups.Files
{
    public class GuidManager
    {
        public static bool GloballyDisabled { get; set; }
        private bool _lookupDelayedInitialized;
        private Dictionary<Guid, MetadataBase> _lookupDelayed;
        public Dictionary<Guid, MetadataBase> _lookup { get { if(!_lookupDelayedInitialized) { _lookupDelayedInitialized = true; _lookupDelayed = new Dictionary<Guid, MetadataBase>(); } return _lookupDelayed; } set { _lookupDelayedInitialized = true; _lookupDelayed = value; } }
        private bool _reverseLookupDelayedInitialized;
        private Dictionary<MetadataBase, Guid> _reverseLookupDelayed;
        public Dictionary<MetadataBase, Guid> _reverseLookup { get { if(!_reverseLookupDelayedInitialized) { _reverseLookupDelayedInitialized = true; _reverseLookupDelayed = new Dictionary<MetadataBase, Guid>(); } return _reverseLookupDelayed; } set { _reverseLookupDelayedInitialized = true; _reverseLookupDelayed = value; } }
        public MetadataBase FindByGuid(Guid guid)
        {
            if (_lookup.ContainsKey(guid))
            {
                return _lookup[guid];
            }
            return null;
        }

        /// <summary>
        /// For internal use only
        /// </summary>
        public void Clear()
        {
            _lookup.Clear();
            _reverseLookup.Clear();
        }

        public void Assert(Guid guid, MetadataBase sender)
        {
            if (guid != Guid.Empty)
            {
                if (_lookup.ContainsKey(guid) && _lookup[guid] != sender)
                {
                    if(!GloballyDisabled)
                    {
                        var friendlyName = sender.FriendlyName;
                        throw new ArgumentException($@"""{_lookup[guid].FriendlyName}"" is already using guid ""{guid}"", which ""{friendlyName}"" is attempting to reuse.");
                    }
                    _lookup[guid] = sender;
                }
                if (!_lookup.ContainsKey(guid))
                {
                    _lookup.Add(guid, sender);
                    _reverseLookup.Add(sender, guid);
                }
            }
            else if (_reverseLookup.ContainsKey(sender))
            {
                var oldGuid = _reverseLookup[sender];
                _reverseLookup.Remove(sender);
                _lookup.Remove(oldGuid);
            }
        }
    }
}