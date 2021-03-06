﻿using Iql.Entities.DisplayFormatting;
using Iql.Entities.Geography;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using System.Collections.Generic;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Functions;

namespace Iql.Entities
{
    public interface IEntityMetadata : IMetadata, IMethodContainer
    {
        bool AutoTitleProperty { get; set; }
        IList<IGeographicPoint> Geographics { get; set; }
        IList<INestedSet> NestedSets { get; set; }
        IList<IDateRange> DateRanges { get; set; }
        IDisplayFormatting DisplayFormatting { get; }
        IRuleCollection<IBinaryRule> EntityValidation { get; }
        IEntityKey Key { get; }
        string TitlePropertyName { get; set; }
        string PreviewPropertyName { get; set; }
        EntityManageKind ManageKind { get; set; }
        string SetFriendlyName { get; set; }
        string SetName { get; set; }
        bool SetNameSet { get; }
        string DefaultBrowseSortExpression { get; set; }
        string DefaultSearchSortExpression { get; set; }
        bool DefaultBrowseSortDescending { get; set; }
        bool DefaultSearchSortDescending { get; set; }
        IList<DisplayConfiguration> DisplayConfigurations { get; set; }
        IList<IProperty> Properties { get; set; }
        IProperty PersistenceKeyProperty { get; set; }
        IList<IRelationship> Relationships { get; }
        IList<IFile> Files { get; }
        IPropertyGroup[] AllPropertyGroups();
    }
}