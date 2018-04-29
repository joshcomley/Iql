namespace Iql.Queryable.Data.Validation
{
    public interface IRelationshipCollectionValidationResultItem
    {
        IRelationshipValidationResult ValidationResult { get; }
        int Index { get; }

    }
}