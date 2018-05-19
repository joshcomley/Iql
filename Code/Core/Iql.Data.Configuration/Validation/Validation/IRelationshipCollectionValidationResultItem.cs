namespace Iql.Data.Configuration.Validation.Validation
{
    public interface IRelationshipCollectionValidationResultItem
    {
        IRelationshipValidationResult ValidationResult { get; }
        int Index { get; }

    }
}