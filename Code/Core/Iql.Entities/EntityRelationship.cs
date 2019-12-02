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
        private static bool TargetMappingsDelayedInitialized;
        private static Dictionary<IRelationship, EntityRelationship> TargetMappingsDelayed;

        private static Dictionary<IRelationship, EntityRelationship> TargetMappings { get { if(!TargetMappingsDelayedInitialized) { TargetMappingsDelayedInitialized = true; TargetMappingsDelayed = new Dictionary<IRelationship, EntityRelationship>(); } return TargetMappingsDelayed; } set { TargetMappingsDelayedInitialized = true; TargetMappingsDelayed = value; } }
        private static bool SourceMappingsDelayedInitialized;
        private static Dictionary<IRelationship, EntityRelationship> SourceMappingsDelayed;
        private static Dictionary<IRelationship, EntityRelationship> SourceMappings { get { if(!SourceMappingsDelayedInitialized) { SourceMappingsDelayedInitialized = true; SourceMappingsDelayed = new Dictionary<IRelationship, EntityRelationship>(); } return SourceMappingsDelayed; } set { SourceMappingsDelayedInitialized = true; SourceMappingsDelayed = value; } }

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