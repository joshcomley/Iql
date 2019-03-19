using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using System;
using System.Linq;
using Iql.Entities.Extensions;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.Validation;

namespace Iql.Entities
{
    public abstract class PropertyGroupBase<T> : MetadataBase, IPropertyGroup, IConfigurable<T>
        where T : IConfigurable<T>
    {
        protected IEntityConfiguration _entityConfiguration;
        private IRuleCollection<IRelationshipRule> _relationshipFilterRules;
        private IRuleCollection<IBinaryRule> _validationRules;
        private IRuleCollection<IDisplayRule> _displayRules;
        public string Key { get; set; }
        public string GroupKey => this.ResolveGroupKey();

        public abstract PropertyKind Kind { get; set; }
        public abstract IqlPropertyGroupKind GroupKind { get; }

        public virtual IEntityConfiguration EntityConfiguration => _entityConfiguration = _entityConfiguration ?? GetGroupProperties().Where(p => p != null).Select(p => p.EntityConfiguration).FirstOrDefault();
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