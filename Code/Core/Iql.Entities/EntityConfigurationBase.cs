using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities.DisplayFormatting;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Extensions;

namespace Iql.Entities
{
    public abstract class EntityConfigurationBase : MetadataBase, IEntityMetadata
    {
        private IProperty _titleProperty;
        private IProperty _previewProperty;
        private string _titlePropertyName;
        private string _previewPropertyName;
        public IList<IProperty> Properties { get; set; }
        public IList<IRelationship> Relationships { get; set; }

        public object GetVersion(object entity)
        {
            var versionProperty = Properties.FirstOrDefault(p => p.HasHint(KnownHints.Version));
            if (versionProperty != null)
            {
                return versionProperty.GetValue(entity);
            }

            return null;
        }


        /// <summary>
        /// Determines whether this entity type has any fields that aren't key fields
        /// </summary>
        /// <returns></returns>
        public bool HasNonKeyFields()
        {
            for (var i = 0; i < Properties.Count; i++)
            {
                var property = Properties[i];
                if (property.Kind.HasFlag(PropertyKind.Primitive) &&
                    !property.Kind.HasFlag(PropertyKind.Key) &&
                    !property.Kind.HasFlag(PropertyKind.Count))
                {
                    return true;
                }

                if (property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    var constraints = property.Relationship.ThisEnd.Constraints();
                    if (constraints.Any(c => !c.Kind.HasFlag(PropertyKind.Key)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasRelationshipKeys => Key != null && Key.HasRelationshipKeys;

        public IPropertyGroup[] OrderedProperties()
        {
            if (PropertyOrder == null || !PropertyOrder.Any())
            {
                var all = new List<IPropertyGroup>();
                all.AddRange(Properties.Where(p => p.Geographic == null && p.NestedSet == null));
                if (Geographics != null)
                {
                    all.AddRange(Geographics);
                }

                if (NestedSets != null)
                {
                    all.AddRange(NestedSets);
                }

                return all.ToArray();
            }

            var ordered = PropertyOrder.ToList();
            for (var i = 0; i < Properties.Count; i++)
            {
                var property = Properties[i];
                var found = false;
                for (var j = 0; j < PropertyOrder.Count; j++)
                {
                    var propertyGroup = PropertyOrder[j];
                    if (propertyGroup.GetProperties().Contains(property))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    ordered.Add(property);
                }
            }

            return ordered.ToArray();
        }

        public IList<IGeographic> Geographics { get; set; } = new List<IGeographic>();
        public IList<INestedSet> NestedSets { get; set; } = new List<INestedSet>();
        public IDisplayFormatting DisplayFormatting { get; set; }
        public IRuleCollection<IBinaryRule> EntityValidation { get; set; }
        public IEntityKey Key { get; set; }

        public Type Type { get; set; }

        private bool _titlePropertyNameChanged = false;
        private bool _previewPropertyNameChanged = false;
        private string _setFriendlyName;

        public IProperty TitleProperty
        {
            get
            {
                if (_titlePropertyNameChanged)
                {
                    _titlePropertyNameChanged = false;
                    _titleProperty = Properties.SingleOrDefault(p => p.Name == TitlePropertyName);
                }

                return _titleProperty;
            }
        }
        public IProperty PreviewProperty
        {
            get
            {
                if (_previewPropertyNameChanged)
                {
                    _previewPropertyNameChanged = false;
                    _previewProperty = Properties.SingleOrDefault(p => p.Name == PreviewPropertyName);
                }

                return _previewProperty;
            }
        }

        public string TitlePropertyName
        {
            get => _titlePropertyName;
            set
            {
                _titlePropertyName = value;
                _titlePropertyNameChanged = true;
            }
        }

        public string PreviewPropertyName
        {
            get => _previewPropertyName;
            set
            {
                _previewPropertyName = value;
                _previewPropertyNameChanged = true;
            }
        }

        public EntityManageKind ManageKind { get; set; } = EntityManageKind.Full;

        private bool _setFriendlyNameSet = false;
        private string _setName;

        public string SetFriendlyName
        {
            get
            {
                if (!_setFriendlyNameSet && _setFriendlyName == null)
                {
                    _setFriendlyName = IntelliSpace.Parse(SetName);
                }
                return _setFriendlyName;
            }
            set
            {
                if (!SetNameSet)
                {
                    _setName = null;
                }
                _setFriendlyNameSet = true;
                _setFriendlyName = value;
            }
        }

        public bool SetNameSet { get; private set; }

        public string SetName
        {
            get
            {
                if (!SetNameSet && _setName == null)
                {
                    if (_setFriendlyNameSet)
                    {
                        _setName = _setFriendlyName;
                    }
                    else
                    {
                        _setName = ResolveName();
                    }
                }

                return _setName;
            }
            set
            {
                if (!_setFriendlyNameSet)
                {
                    _setFriendlyName = null;
                }
                SetNameSet = true;
                _setName = value;
            }
        }

        protected override string ResolveName()
        {
            return Type?.Name ?? _setName ?? _setFriendlyName;
        }

        public string DefaultSortExpression { get; set; }
        public bool DefaultSortDescending { get; set; }

        public static string DefaultRequiredAutoValidationFailureMessage { get; set; } = "This field is required";
        public static string DefaultRequiredAutoValidationFailureKey { get; set; } = "Auto";
    }
}