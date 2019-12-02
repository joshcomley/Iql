using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Iql.Entities.Events;
using Iql.Entities.Extensions;
using Iql.Entities.Permissions;
using Iql.Entities.Rules.Relationship;
using Iql.Events;

namespace Iql.Entities
{
    public abstract class PropertyGroupBase<T> : MetadataBase, IPropertyGroup, IConfigurable<T>
        where T : IConfigurable<T>
    {
        public override IUserPermission ParentPermissions => EntityConfiguration;
        public bool Matches(params string[] names)
        {
            var prop = this as IProperty;
            for (var i = 0; i < names.Length; i++)
            {
                var name = SanitizeName(names[i]);
                if (name == SanitizeName(Name))
                {
                    return true;
                }

                if (prop != null && name == SanitizeName(prop.PropertyName))
                {
                    return true;
                }
            }

            return false;
        }
        private static bool SanitizedNamesDelayedInitialized;
        private static Dictionary<string , string> SanitizedNamesDelayed;

        private static Dictionary<string , string> SanitizedNames { get { if(!SanitizedNamesDelayedInitialized) { SanitizedNamesDelayedInitialized = true; SanitizedNamesDelayed = new Dictionary<string, string>(); } return SanitizedNamesDelayed; } set { SanitizedNamesDelayedInitialized = true; SanitizedNamesDelayed = value; } }
        private static string SanitizeName(string name)
        {
            string val;
            if (SanitizedNames.TryGetValue(name, out val))
            {
                return val;
            }

            val = Regex.Replace(name.ToLower(), "[^A-Za-z]", "").Trim();
            SanitizedNames.Add(name, val);
            return val;
        }

        public bool ForceDecision { get; set; }

        public Task<IqlUserPermission> GetUserPermissionAsync(object entity = null)
        {
            return Task.FromResult(IqlUserPermission.ReadAndUpdate);
        }

        public virtual bool CanWriteSet
        {
            get
            {
                if (CanWriteHasBeenSet)
                {
                    return true;
                }
                if (this is IProperty property)
                {
                    if (property.Relationship != null)
                    {
                        if (property.Relationship.ThisEnd.CanWriteSet)
                        {
                            CanWriteHasBeenSet = true;
                        }
                    }
                }
                return CanWriteHasBeenSet;
            }
        }

        public bool CanWrite
        {
            get
            {
                if (this is IProperty property)
                {
                    if (property.Relationship != null)
                    {
                        return property.Relationship.ThisEnd.CanWrite;
                    }
                }
                if (EntityConfiguration?.Key != null && EntityConfiguration.Key.Properties.Any(_ => _ == this))
                {
                    return EntityConfiguration.Key.CanWrite;
                }
                return CanWriteInternal ?? CanWriteDefaultValue;
            }
            set
            {
                CanWriteHasBeenSet = true;
                if (this is IProperty property)
                {
                    if (property.Relationship != null)
                    {
                        property.Relationship.ThisEnd.CanWrite = value;
                    }
                    else
                    {
                        CanWriteInternal = value;
                    }
                }
                else
                {
                    CanWriteInternal = value;
                }
            }
        }

        public bool PromptBeforeEdit { get; set; } = false;
        public string Placeholder { get; set; }
        public bool Sortable { get; set; } = true;
        public virtual bool IsHiddenFromEdit =>
            EditKind == IqlPropertyEditKind.Hidden;

        public virtual ReadOnlyEditDisplayKind ReadOnlyEditDisplayKind { get; set; } = ReadOnlyEditDisplayKind.Display;
        public virtual ReadOnlyEditDisplayKind ResolvedReadOnlyEditDisplayKind => ReadOnlyEditDisplayKind;

        public virtual bool IsHiddenFromRead =>
            ReadKind == IqlPropertyReadKind.Hidden;

        public abstract IProperty PrimaryProperty { get; }

        public virtual IPropertyGroup SetReadOnlyAndHidden()
        {
            return SetReadOnly().SetHidden();
        }

        public virtual IPropertyGroup SetReadOnly()
        {
            CanWrite = false;
            return this;
        }

        public virtual IPropertyGroup SetEditorReadOnly()
        {
            EditKind = IqlPropertyEditKind.Display;
            ReadKind = IqlPropertyReadKind.Display;
            return this;
        }

        public virtual IPropertyGroup SetHidden()
        {
            EditKind = IqlPropertyEditKind.Hidden;
            ReadKind = IqlPropertyReadKind.Hidden;
            return this;
        }


        private EventEmitter<ValueChangedEvent<IqlPropertyEditKind>> _editKindChanged;
        private EventEmitter<ValueChangedEvent<IqlPropertyReadKind>> _readKindChanged;
        private IqlPropertyEditKind _editKind = IqlPropertyEditKind.Edit;
        private IqlPropertyReadKind _readKind = IqlPropertyReadKind.Display;

        public EventEmitter<ValueChangedEvent<IqlPropertyEditKind>> EditKindChanged => _editKindChanged =
            _editKindChanged ?? new EventEmitter<ValueChangedEvent<IqlPropertyEditKind>>();

        public EventEmitter<ValueChangedEvent<IqlPropertyReadKind>> ReadKindChanged => _readKindChanged =
            _readKindChanged ?? new EventEmitter<ValueChangedEvent<IqlPropertyReadKind>>();

        public virtual IqlPropertyReadKind ReadKind
        {
            get => _readKind;
            set
            {
                var oldValue = _readKind;
                _readKind = value;
                if (oldValue != value && _readKindChanged != null)
                {
                    ReadKindChanged.Emit(() => new ValueChangedEvent<IqlPropertyReadKind>(oldValue, value));
                }
            }
        }

        public virtual IqlPropertyEditKind EditKind
        {
            get => _editKind;
            set
            {
                var oldValue = _editKind;
                _editKind = value;
                if (oldValue != value && _editKindChanged != null)
                {
                    EditKindChanged.Emit(() => new ValueChangedEvent<IqlPropertyEditKind>(oldValue, value));
                }
            }
        }

        public virtual IPropertyGroup PropertyGroup { get; }
        protected IEntityConfiguration _entityConfiguration;
        private IRuleCollection<IRelationshipRule> _relationshipFilterRules;
        private IRuleCollection<IBinaryRule> _validationRules;
        private IRuleCollection<IDisplayRule> _displayRules;
        protected virtual bool CanWriteDefaultValue { get; } = true;
        protected bool? CanWriteInternal { get; set; }
        protected bool CanWriteHasBeenSet { get; set; }
        public string Key { get; set; }
        public string GroupKey => this.ResolveGroupKey();

        public abstract IqlPropertyKind Kind { get; set; }
        public abstract IqlPropertyGroupKind GroupKind { get; }
        public abstract PropertyGroupMetadata[] GetPropertyGroupMetadata();

        public override IEntityConfiguration EntityConfiguration => _entityConfiguration = _entityConfiguration ?? GetGroupProperties().Where(p => p != null).Select(p => p.EntityConfiguration).FirstOrDefault();
        public virtual IRuleCollection<IRelationshipRule> RelationshipFilterRules
        {
            get => _relationshipFilterRules = _relationshipFilterRules ?? NewRelationshipFilterRulesCollection();
            set => _relationshipFilterRules = value;
        }

        protected virtual IRuleCollection<IRelationshipRule> NewRelationshipFilterRulesCollection()
        {
            return null;
        }

        public virtual IRuleCollection<IBinaryRule> ValidationRules
        {
            get => _validationRules = _validationRules ?? NewValidationRulesCollection();
            set => _validationRules = value;
        }

        protected virtual IRuleCollection<IBinaryRule> NewValidationRulesCollection()
        {
            return null;
        }

        public virtual IRuleCollection<IDisplayRule> DisplayRules
        {
            get => _displayRules = _displayRules ?? NewDisplayRulesCollection();
            set => _displayRules = value;
        }

        protected virtual IRuleCollection<IDisplayRule> NewDisplayRulesCollection()
        {
            return null;
        }

        public abstract IPropertyGroup[] GetGroupProperties();

        public virtual bool IsTypeGroup => GroupKind == IqlPropertyGroupKind.NestedSet ||
                                             GroupKind == IqlPropertyGroupKind.GeographicPoint ||
                                             GroupKind == IqlPropertyGroupKind.DateRange ||
                                             GroupKind == IqlPropertyGroupKind.File;

        protected PropertyGroupBase(IEntityConfiguration entityConfiguration, string key)
        {
            _entityConfiguration = entityConfiguration;
            Key = key;
        }

        public T Configure(Action<T> configure)
        {
            configure((T)(object)this);
            return (T)(object)this;
        }
    }
}