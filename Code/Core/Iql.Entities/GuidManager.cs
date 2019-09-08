using System;
using System.Collections.Generic;

namespace Iql.Entities.PropertyGroups.Files
{
    public class GuidManager
    {
        public static bool GloballyDisabled { get; set; }
        public Dictionary<Guid, MetadataBase> _lookup = new Dictionary<Guid, MetadataBase>();
        public Dictionary<MetadataBase, Guid> _reverseLookup = new Dictionary<MetadataBase, Guid>();
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