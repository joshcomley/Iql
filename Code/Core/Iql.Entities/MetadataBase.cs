using System.Collections.Generic;
using Iql.Entities.Metadata;
using Iql.Extensions;

namespace Iql.Entities
{
    public class MetadataBase : IMetadata
    {
        private string _friendlyName;
        private bool _friendlyNameSet;
        private string _resolvedFriendlyName;
        private string _name;
        private string _title;
        private bool _titleSet;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                _resolvedFriendlyName = null;
            }
        }
        public string Title
        {
            get => _titleSet ? _title : Name;
            set
            {
                _title = value;
                _titleSet = true;
            }
        }
        public string FriendlyName
        {
            get => _friendlyNameSet ? _friendlyName : Title;
            set
            {
                _friendlyName = value;
                _friendlyNameSet = true;
                _resolvedFriendlyName = null;
            }
        }
        public string ResolveFriendlyName()
        {
            return _resolvedFriendlyName ?? (_resolvedFriendlyName =
                       string.IsNullOrWhiteSpace(FriendlyName) || !_friendlyNameSet
                           ? IntelliSpace.Parse(ResolveName())
                           : FriendlyName);
        }

        public virtual string ResolveName()
        {
            return Name ?? Title ?? "Unknown";
        }
        public string GroupPath { get; set; }
        public string Description { get; set; }
        public List<string> Hints { get; set; } = new List<string>();

        public List<string> PropertyOrder { get; set; } = new List<string>();

        public MetadataHint FindHint(string name)
        {
            return HintHelper.FindHint(this, name);
        }

        public bool HasHint(string name)
        {
            return HintHelper.HasHint(this, name);
        }

        public void RemoveHint(string name)
        {
            HintHelper.RemoveHint(this, name);
        }

        public List<HelpText> HelpTexts { get; set; } = new List<HelpText>();

        public void SetHint(string name, string value = null)
        {
            HintHelper.SetHint(this, name, value);
        }
    }
}