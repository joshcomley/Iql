﻿using System.Collections.Generic;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IMetadata
    {
        string Description { get; set; }
        string FriendlyName { get; set; }
        List<string> Hints { get; set; }
        string Name { get; set; }
        string Title { get; set; }
        string ResolveFriendlyName();
        string ResolveName();
        MetadataHint FindHint(string name);
        bool HasHint(string name);
        void SetHint(string name, string value = null);
        void RemoveHint(string name);
    }
}