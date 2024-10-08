using Iql.Entities.Metadata;
using Iql.Extensions;
using System.Collections.Generic;

namespace Iql.Entities
{
    public abstract class MetadataBase : IConfiguration
    {
        private string _friendlyName;
        private bool _friendlyNameSet;
        private string _name;
        protected bool _nameSet;
        private string _title;
        private bool _titleSet;
        private UserPermissionsCollection? _permissions;
        private bool _metadataInitialized;
        private IMetadataCollection _metadata;
        public IMetadataCollection Metadata { get { if(!_metadataInitialized) { _metadataInitialized = true; _metadata = new MetadataCollection(); } return _metadata; } set { _metadataInitialized = true; _metadata = value; } }

        public virtual string Name
        {
            get
            {
                if (!_nameSet && string.IsNullOrWhiteSpace(_name))
                {
                    if (_titleSet)
                    {
                        _name = _title;
                    }
                    else if (_friendlyNameSet)
                    {
                        _name = _friendlyName;
                    }
                    else
                    {
                        _name = ResolveName();
                    }
                }
                return _name;
            }
            set
            {
                _nameSet = true;
                _name = value;
                Reset();
            }
        }

        private void Reset()
        {
            if (!_nameSet)
            {
                _name = null;
            }
            if (!_friendlyNameSet)
            {
                _friendlyName = null;
            }
            if (!_titleSet)
            {
                _title = null;
            }
        }

        public string Title
        {
            get
            {
                if (!_titleSet && string.IsNullOrWhiteSpace(_title))
                {
                    if (_friendlyNameSet)
                    {
                        _title = _friendlyName;
                    }
                    else if (_nameSet)
                    {
                        _title = _name;
                    }
                    else
                    {
                        _title = IntelliSpace.Parse(ResolveName());
                    }
                }
                return _title;
            }
            set
            {
                _titleSet = true;
                _title = value;
                Reset();
            }
        }

        protected virtual string ResolveName()
        {
            return null;
        }

        public string FriendlyName
        {
            get
            {
                if (!_friendlyNameSet && string.IsNullOrWhiteSpace(_friendlyName))
                {
                    if (_titleSet)
                    {
                        _friendlyName = _title;
                    }
                    else if (_nameSet)
                    {
                        _friendlyName = IntelliSpace.Parse(_name);
                    }
                    else
                    {
                        _friendlyName = IntelliSpace.Parse(ResolveName());
                    }
                }
                return _friendlyName;
            }
            set
            {
                _friendlyName = value;
                _friendlyNameSet = true;
                Reset();
            }
        }

        public virtual IEntityConfiguration EntityConfiguration { get; protected set; }
        public string GroupPath { get; set; }
        public float GroupOrder { get; set; }

        public string Description { get; set; }
        private bool _hintsInitialized;
        private List<string> _hints;
        public List<string>? Hints { get { if(!_hintsInitialized) { _hintsInitialized = true; _hints = new List<string>(); } return _hints; } set { _hintsInitialized = true; _hints = value; } }
        public MetadataHint FindHint(string name, bool? onlySelf = false)
        {
            return HintHelper.FindHint(this, name, onlySelf);
        }

        public bool HasHint(string name, bool? onlySelf = null)
        {
            return HintHelper.HasHint(this, name, onlySelf);
        }

        public IConfiguration RemoveHint(string name, bool? onlySelf = null)
        {
            HintHelper.RemoveHint(this, name, onlySelf);
            return this;
        }
        private bool _helpTextsInitialized;
        private List<HelpText> _helpTexts;

        public List<HelpText> HelpTexts { get { if(!_helpTextsInitialized) { _helpTextsInitialized = true; _helpTexts = new List<HelpText>(); } return _helpTexts; } set { _helpTextsInitialized = true; _helpTexts = value; } }

        public IConfiguration SetHint(string name, string value = null)
        {
            HintHelper.SetHint(this, name, value);
            return this;
        }

        public UserPermissionsCollection Permissions
        {
            get => _permissions = _permissions ?? new UserPermissionsCollection(EntityConfiguration);
            set => _permissions = value;
        }

        public abstract IUserPermission ParentPermissions { get; }
    }
}