using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.Relationships
{
    public abstract class RelationshipBase : IRelationship
    {
        protected EntityConfigurationBuilder Builder;

        protected RelationshipBase()
        {
            Constraints = new List<IRelationshipConstraint>();
        }

        public void Configure(
            EntityConfigurationBuilder builder,
            Func<IRelationshipDetail> source,
            Func<IRelationshipDetail> target,
            RelationshipKind kind)
        {
            Builder = builder;
            Kind = kind;
            Source = source();
            Target = target();
        }

        public List<IRelationshipConstraint> Constraints { get; private set; }
        public RelationshipKind Kind { get; private set; }
        public IRelationshipDetail Source { get; private set; }
        public IRelationshipDetail Target { get; private set; }
        public string ConstraintKey { get; private set; }
        public string QualifiedConstraintKey { get; private set; }

        protected void UpdateConstraintKey()
        {
            ConstraintKey = string.Join(",", Constraints.Select(c => c.TargetKeyProperty.Name));
            QualifiedConstraintKey = $"{Target.Type.Name}:{ConstraintKey}";
        }
    }
}