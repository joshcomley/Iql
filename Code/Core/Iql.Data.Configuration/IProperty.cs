using System;
using System.Collections.Generic;
using System.Reflection;
using Iql.Data.Configuration.Rules;
using Iql.Data.Configuration.Rules.Relationship;

namespace Iql.Data.Configuration
{
    public interface IProperty : IPropertyMetadata
    {
#if !TypeScript
        PropertyInfo PropertyInfo { get; set; }
#endif
        IRuleCollection<IBinaryRule> ValidationRules { get; }
        IRuleCollection<IBinaryRule> DisplayRules { get; }
        IRuleCollection<IRelationshipRule> RelationshipFilterRules { get; }
        RelationshipMatch Relationship { get; set; }
        List<RelationshipMatch> RelationshipSources { get; set; }
        //IProperty CountRelationship { get; }
        ITypeDefinition TypeDefinition { get; set; }
        List<object> Helpers { get; set; }
        Func<object, object> PropertyGetter { get; }
        Func<object, object, object> PropertySetter { get; }
        Dictionary<string, object> CustomInformation { get; }
    }
}