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
        private UserPermissionsCollection _permissions;
        public IMetadataCollection Metadata { get; set; } = new MetadataCollection();

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
        
        public string Description { get; set; }
        public List<string> Hints { get; set; } = new List<string>();
        public MetadataHint FindHint(string name)
        {
            return HintHelper.FindHint(this, name);
        }

        public bool HasHint(string name)
        {
            return HintHelper.HasHint(this, name);
        }

        public IConfiguration RemoveHint(string name)
        {
            HintHelper.RemoveHint(this, name);
            return this;
        }

        public List<HelpText> HelpTexts { get; set; } = new List<HelpText>();

        public IConfiguration SetHint(string name, string value = null)
        {
            HintHelper.SetHint(this, name, value);
            return this;
        }

        public UserPermissionsCollection Permissions
        {
            get => _permissions = _permissions ?? new UserPermissionsCollection(EntityConfiguration?.Builder);
            set => _permissions = value;
        }
    }
}