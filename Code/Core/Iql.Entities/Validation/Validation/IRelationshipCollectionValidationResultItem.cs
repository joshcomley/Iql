namespace Iql.Entities.Validation.Validation
{
    public interface IRelationshipCollectionValidationResultItem
    {
        IRelationshipValidationResult ValidationResult { get; }
        int Index { get; }

    }
}