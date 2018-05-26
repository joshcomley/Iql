using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Extensions;

namespace Iql.Entities
{
    public abstract class EntityConfigurationBase : MetadataBase, IEntityMetadata
    {
        private IEntityKey _key;
        public List<IProperty> Properties { get; set; }

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

        public static string DefaultRequiredAutoValidationFailureMessage { get; set; } = "This field is required";
        public static string DefaultRequiredAutoValidationFailureKey { get; set; } = "Auto";
    }
}