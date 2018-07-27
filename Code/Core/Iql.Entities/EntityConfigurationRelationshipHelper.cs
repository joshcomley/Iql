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
                definition.Kind = PropertyKind.Relationship;
                definition.Relationship = relationship;
                var otherEndConfiguration = entityConfiguration.Builder.GetEntityByType(relationship.OtherEnd.Type);
                foreach (var constraint in relationship.Relationship.Constraints)
                {
                    var constraintProperty = otherEndConfiguration.FindProperty(constraint.SourceKeyProperty.Name);
                    if (constraintProperty != null &&
                        !constraintProperty.Kind.HasFlag(PropertyKind.RelationshipKey) &&
                        !constraintProperty.Kind.HasFlag(PropertyKind.Key))
                    {
                        constraintProperty.Kind = constraintProperty.Kind | PropertyKind.RelationshipKey;
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
                foreach (var relationshipMatch in entityConfiguration.AllRelationships())
                {
                    foreach (var constraint in relationshipMatch.Relationship.Constraints)
                    {
                        if (constraint.SourceKeyProperty.Name == definition.Name &&
                            !definition.Kind.HasFlag(PropertyKind.RelationshipKey) &&
                            !definition.Kind.HasFlag(PropertyKind.Key))
                        {
                            definition.Kind = definition.Kind | PropertyKind.RelationshipKey;
                            definition.Relationship = relationshipMatch;
                        }
                    }
                }
            }
        }
    }
}