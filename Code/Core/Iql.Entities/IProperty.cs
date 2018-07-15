using System;
using System.Collections.Generic;
using System.Reflection;

namespace Iql.Entities
{
    public interface IProperty : IPropertyMetadata, IConfiguration
    {
        bool IsTitleProperty { get; }
        bool IsPreviewProperty { get; }
        bool IsSubTitleProperty { get; }
#if !TypeScript
        PropertyInfo PropertyInfo { get; set; }
#endif
        RelationshipMatch Relationship { get; set; }
        List<RelationshipMatch> RelationshipSources { get; set; }
        //IProperty CountRelationship { get; }
        List<object> Helpers { get; set; }
        Func<object, object> GetValue { get; }
        Func<object, object, object> SetValue { get; }
        Dictionary<string, object> CustomInformation { get; }
        IEntityConfiguration EntityConfiguration { get; }
        IProperty SetReadOnlyAndHidden(bool readOnlyAndHidden = true);
        IProperty SetReadOnly(bool readOnly = true);
        IProperty SetHidden(bool hidden = true);
        IProperty SetNullable(bool nullable = true);
        bool HasMediaKey { get; }
    }
}