namespace Iql.Queryable.Data.Validation
{
    public class RelationshipCollectionValidationResultItem<T> : IRelationshipCollectionValidationResultItem
    {
        public RelationshipValidationResult<T> ValidationResult { get; set; }
        public int Index { get; set; }
        IRelationshipValidationResult IRelationshipCollectionValidationResultItem.ValidationResult => ValidationResult;

        public RelationshipCollectionValidationResultItem(RelationshipValidationResult<T> validationResult, int index)
        {
            ValidationResult = validationResult;
            Index = index;
        }
    }
}