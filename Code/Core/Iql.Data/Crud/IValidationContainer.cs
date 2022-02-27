namespace Iql.Data.Crud
{
    public interface IValidationContainer
    {
        string ValidationString { get; }
        string ToValidationString(bool includeCollectionResults = false);
        string[] ToValidationStrings(bool includeCollectionResults = false);
    }
}