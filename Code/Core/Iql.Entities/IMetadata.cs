﻿using System.Collections.Generic;
using Iql.Entities.Metadata;

namespace Iql.Entities
{
    public interface IMetadata
    {
        string GroupPath { get; set; }
        string Description { get; set; }
        string FriendlyName { get; set; }
        List<string> Hints { get; set; }
        string Name { get; set; }
        string Title { get; set; }
        List<HelpText> HelpTexts { get; set; }
    }
}