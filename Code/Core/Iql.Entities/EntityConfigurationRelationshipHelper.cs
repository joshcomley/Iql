namespace Iql.Entities
{
    internal class EntityConfigurationRelationshipHelper
    {
        internal static void TryAssignRelationshipToProperty(IEntityConfiguration entityConfiguration, string propertyName, bool tryAssignOtherEnd = true)
        {
            var propertyDefinition = entityConfiguration.FindProperty(propertyName);
            if (propertyDefinition != null)
            {
                TryAssignRelationshipToPropertyDefinition(entityConfiguration, propertyDefinition, tryAssignOtherEnd);
            }
        }

        internal static void TryAssignRelationshipToPropertyDefinition(IEntityConfiguration entityConfiguration, IProperty definition, bool tryAssignOtherEnd = true)
        {
            var relationship = entityConfiguration.FindRelationshipByName(definition.Name);
            if (relationship != null)
            {
                definition.Kind = IqlPropertyKind.Relationship;
                definition.Relationship = relationship;
                var otherEndConfiguration = entityConfiguration.Builder.GetEntityByType(relationship.OtherEnd.Type);
                foreach (var constraint in relationship.Relationship.Constraints)
                {
                    var constraintProperty = otherEndConfiguration.FindProperty(constraint.SourceKeyProperty.Name);
                    if (constraintProperty != null &&
                        !constraintProperty.Kind.HasFlag(IqlPropertyKind.RelationshipKey) &&
                        !constraintProperty.Kind.HasFlag(IqlPropertyKind.Key))
                    {
                        constraintProperty.Kind = constraintProperty.Kind | IqlPropertyKind.RelationshipKey;
                        constraintProperty.Relationship = otherEndConfiguration.FindRelationshipByName(relationship.OtherEnd.Property.Name);
                    }
                }
                if (tryAssignOtherEnd)
                {
                    TryAssignRelationshipToProperty(
                        otherEndConfiguration, relationship.OtherEnd.Property.Name, false);
                }
            }
            else
            {
                for (var i = 0; i < entityConfiguration.AllRelationships.Length; i++)
                {
                    var relationshipMatch = entityConfiguration.AllRelationships[i];
                    for (var j = 0; j < relationshipMatch.Relationship.Constraints.Count; j++)
                    {
                        var constraint = relationshipMatch.Relationship.Constraints[j];
                        if (constraint.SourceKeyProperty.Name == definition.Name &&
                            !definition.Kind.HasFlag(IqlPropertyKind.RelationshipKey) &&
                            !definition.Kind.HasFlag(IqlPropertyKind.Key))
                        {
                            definition.Kind = definition.Kind | IqlPropertyKind.RelationshipKey;
                            definition.Relationship = relationshipMatch;
                        }
                    }
                }
            }
        }
    }
}