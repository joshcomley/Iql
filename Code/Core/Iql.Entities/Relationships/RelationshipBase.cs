using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Iql.Entities.Relationships
{
    public abstract class RelationshipBase : IRelationship
    {
        protected EntityConfigurationBuilder Builder;

        protected RelationshipBase()
        {
            Constraints = new List<IRelationshipConstraint>();
        }

        private LambdaExpression _sourceProperty;
        private LambdaExpression _targetProperty;
        private IRelationshipDetail _source;
        private IRelationshipDetail _target;

        public void Configure(
            EntityConfigurationBuilder builder,
            LambdaExpression sourceProperty,
            LambdaExpression targetProperty,
            RelationshipKind kind)
        {
            Builder = builder;
            Kind = kind;
            _sourceProperty = sourceProperty;
            _targetProperty = targetProperty;
            _source = null;
            _target = null;
        }

        protected abstract IRelationshipDetail BuildSource(LambdaExpression property);
        protected abstract IRelationshipDetail BuildTarget(LambdaExpression property);
        public List<IRelationshipConstraint> Constraints { get; private set; }
        public RelationshipKind Kind { get; private set; }

        public virtual IRelationshipDetail Source => _source = _source ?? BuildSource(_sourceProperty);

        public virtual IRelationshipDetail Target => _target = _target ?? BuildTarget(_targetProperty);

        public string ConstraintKey { get; private set; }
        public string QualifiedConstraintKey { get; private set; }

        protected void UpdateConstraintKey()
        {
            ConstraintKey = string.Join(",", Constraints.Select(c => c.TargetKeyProperty.Name));
            QualifiedConstraintKey = $"{Target.Type.Name}:{ConstraintKey}";
        }
    }
}