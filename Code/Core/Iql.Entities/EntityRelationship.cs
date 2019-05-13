using System.Collections.Generic;
using Iql.Entities.Relationships;

namespace Iql.Entities
{
    public class EntityRelationship
    {
        public IRelationship Relationship { get; }
        public bool ThisIsTarget { get; }
        public bool ThisIsSource => !ThisIsTarget;
        public IRelationshipDetail ThisEnd { get; }
        public IRelationshipDetail OtherEnd { get; }

        private static readonly Dictionary<IRelationship, EntityRelationship> TargetMappings
            = new Dictionary<IRelationship, EntityRelationship>();
        private static readonly Dictionary<IRelationship, EntityRelationship> SourceMappings
            = new Dictionary<IRelationship, EntityRelationship>();

        public static EntityRelationship Get(IRelationship relationship, bool thisIsTarget)
        {
            var mapping = thisIsTarget ? TargetMappings : SourceMappings;
            if (!mapping.ContainsKey(relationship))
            {
                mapping.Add(relationship, new EntityRelationship(relationship, thisIsTarget));
            }

            return mapping[relationship];
        }

        private EntityRelationship(IRelationship relationship, bool thisIsTarget)
        {
            Relationship = relationship;
            ThisIsTarget = thisIsTarget;
            ThisEnd = thisIsTarget ? relationship.Target : relationship.Source;
            OtherEnd = thisIsTarget ? relationship.Source : relationship.Target;
        }
    }
}