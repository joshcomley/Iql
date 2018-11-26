﻿using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.ValueResolvers;

namespace Iql.Entities
{
    public enum PropertyReadKind
    {
        Display = 1,
        Hidden
    }

    public enum PropertyEditKind
    {
        Edit = 1,
        New,
        NewAndPromptForEdit,
        Display,
        Hidden
    }

    public interface IPropertyMetadata : ISimpleProperty
    {
        IRuleCollection<IRelationshipRule> RelationshipFilterRules { get; set; }
        IEnumerable<IRelationship> Relationships { get; }
        ITypeDefinition TypeDefinition { get; set; }
        PropertySearchKind SearchKind { get; set; }
        string PropertyName { get; set; }
        bool Searchable { get; set; }
        bool? Nullable { get; set; }
        LambdaExpression InferredWith { get; set; }
        IqlPropertyPath GetInferredWithPath();
    }
}