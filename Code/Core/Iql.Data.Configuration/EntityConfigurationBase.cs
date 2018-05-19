using System;
using System.Collections.Generic;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public abstract class EntityConfigurationBase : IEntityMetadata
    {
        private IEntityKey _key;
        public List<IProperty> Properties { get; set; }

        public IEntityKey Key
        {
            get => _key;
            set => _key = value;
        }

        public Type Type { get; set; }
        public string Description { get; set; }
        public string FriendlyName { get; set; }
        public List<string> Hints { get; set; } = new List<string>();
        public string Name { get; set; }
        public string Title { get; set; }
        public string GroupPath { get; set; }
        public EntityManageKind ManageKind { get; set; } = EntityManageKind.Full;
        public string SetFriendlyName { get; set; }
        public string SetName { get; set; }

        public string ResolveFriendlyName()
        {
            return FriendlyName ?? IntelliSpace.Parse(ResolveName());
        }

        public string ResolveName()
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

        public void SetHint(string name, string value = null)
        {
            HintHelper.SetHint(this, name, value);
        }

        public static string DefaultRequiredAutoValidationFailureMessage { get; set; } = "This field is required";
        public static string DefaultRequiredAutoValidationFailureKey { get; set; } = "Auto";
    }
}