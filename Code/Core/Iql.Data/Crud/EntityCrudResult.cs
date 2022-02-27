using System.Collections.Generic;
using System.Linq;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Validation.Validation;

namespace Iql.Data.Crud
{
    public class EntityCrudResult<T, TOperation> : CrudResult<T, TOperation>, 
        IEntityCrudResult, IValidationContainer
        where TOperation : IEntityCrudOperationBase
        where T : class
    {
        public T LocalEntity { get; }
        public CompositeKey KeyBeforeSave { get; }
        public IEntityState<T> EntityState => (IEntityState<T>)Operation.EntityState;
        IEntityStateBase IEntityCrudResult.EntityState => EntityState;
        private bool _entityValidationResultsInitialized;
        private Dictionary<object, IEntityValidationResult> _entityValidationResults;

        public Dictionary<object, IEntityValidationResult> EntityValidationResults
        {
            get
            {
                if (!_entityValidationResultsInitialized)
                {
                    _entityValidationResultsInitialized = true;
                    _entityValidationResults = new Dictionary<object, IEntityValidationResult>();
                }

                return _entityValidationResults;
            }
            set
            {
                _entityValidationResultsInitialized = true;
                _entityValidationResults = value;
            }
        }

        public IDataContext DataContext { get; }
        public IqlOperationKind Kind => Operation.Kind;
        object IEntityCrudResult.LocalEntity => LocalEntity;
        public EntityValidationResult<T> RootEntityValidationResult { get; set; }

        public virtual string ValidationString => ToValidationString();
        public virtual string ToValidationString(bool includeCollectionResults = false)
        {
            return string.Join("\n", ToValidationStrings(includeCollectionResults));
        }
        public string[] ToValidationStrings(bool includeCollectionResults = false)
        {
            var all = new List<string>();

            void AddFailures(string name, List<ValidationError> validationFailures)
            {
                foreach (var failure in validationFailures)
                {
                    var parts = new[] { name, failure.Key, failure.Message }
                        .Where(_ => !string.IsNullOrWhiteSpace(_));
                    all.Add(string.Join(" - ", parts));
                }
            }

            if (EntityValidationResults != null)
            {
                foreach (var entityResult in EntityValidationResults)
                {
                    if (entityResult.Value.ValidationFailures != null &&
                        entityResult.Value.ValidationFailures.Count > 0)
                    {
                        AddFailures(
                            "entity",
                            entityResult.Value.ValidationFailures);
                    }

                    if (entityResult.Value.PropertyValidationResults != null)
                    {
                        foreach (var validationResult in entityResult.Value.PropertyValidationResults)
                        {
                            AddFailures(
                                validationResult.Property.Name,
                                validationResult.ValidationFailures);
                        }
                    }

                    if (entityResult.Value.RelationshipValidationResults != null)
                    {
                        foreach (var validationResult in entityResult.Value.RelationshipValidationResults)
                        {
                            AddFailures(
                                validationResult.Property.Name,
                                validationResult.ValidationFailures);
                        }
                    }

                    if (includeCollectionResults)
                    {
                        if (entityResult.Value.RelationshipCollectionValidationResults != null)
                        {
                            foreach (var validationResult in entityResult.Value
                                         .RelationshipCollectionValidationResults)
                            {
                                if (validationResult.RelationshipValidationResults != null)
                                {
                                    foreach (var resultItem in validationResult.RelationshipValidationResults)
                                    {
                                        AddFailures(
                                            resultItem.ValidationResult.Property.Name,
                                            resultItem.ValidationResult.ValidationFailures);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return all.ToArray();
        }

        IEntityValidationResult IEntityCrudResult.RootEntityValidationResult
        {
            get => RootEntityValidationResult;
            set => RootEntityValidationResult = (EntityValidationResult<T>)value;
        }

        public EntityCrudResult(T localEntity, bool success, TOperation operation) : base(success, operation)
        {
            LocalEntity = localEntity;
            DataContext = operation.DataContext;
            KeyBeforeSave = operation?.KeyBeforeSave;
        }
    }
}