using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Extensions;

namespace Iql.Entities
{
    public abstract class EntityConfigurationBase : MetadataBase, IEntityMetadata
    {
        private IEntityKey _key;
        private IProperty _titleProperty;
        private IProperty _previewProperty;
        private string _titlePropertyName;
        private string _previewPropertyName;
        public List<IProperty> Properties { get; set; }

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

        public IProperty[] OrderedProperties()
        {
            if (PropertyOrder == null || !PropertyOrder.Any())
            {
                return Properties.ToArray();
            }
            return Properties.OrderBy(t =>
                {
                    var index = PropertyOrder.IndexOf(t.Name);
                    if (index == -1)
                    {
                        return 99999;
                    }
                    return index;
                })
                .ToArray();
        }
        public IEntityKey Key
        {
            get => _key;
            set => _key = value;
        }

        public Type Type { get; set; }

        private bool _titlePropertyNameChanged = false;
        private bool _previewPropertyNameChanged = false;
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
        public string SetFriendlyName { get; set; }
        public string SetName { get; set; }

        public override string ResolveName()
        {
            var name = Name ?? Title ?? Type?.Name ?? ResolveSetName();
            return name ?? "Unknown";
        }

        public string ResolveSetFriendlyName()
        {
            return SetFriendlyName ?? IntelliSpace.Parse(ResolveSetName());
        }

        public string ResolveSetName()
        {
            //var dataContextType = DataContext.FindDataContextTypeForEntityType(Type);
            return SetName ?? Name ?? Type.Name;
        }

        public string DefaultSortExpression { get; set; }
        public bool DefaultSortDescending { get; set; }

        public static string DefaultRequiredAutoValidationFailureMessage { get; set; } = "This field is required";
        public static string DefaultRequiredAutoValidationFailureKey { get; set; } = "Auto";
    }
}