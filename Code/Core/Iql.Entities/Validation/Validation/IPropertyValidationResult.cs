namespace Iql.Entities.Validation.Validation
{
    public interface IPropertyValidationResult : IValidationResult
    {
        IProperty Property { get; set; }
    }
}