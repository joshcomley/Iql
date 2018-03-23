using System;
using System.Collections.Generic;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public abstract class EntityConfigurationBase : IEntityMetadata
    {
        public List<IProperty> Properties { get; set; }
        public IEntityKey Key { get; set; }
        public Type Type { get; set; }
        public string Description { get; set; }
        public string FriendlyName { get; set; }
        public List<string> Hints { get; set; } = new List<string>();
        public string Name { get; set; }
        public string Title { get; set; }
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
            return SetName ?? Name;
        }
    }
}