namespace Iql.Data.Configuration.Validation.Validation
{
    public interface IPropertyValidationResult : IValidationResult
    {
        IProperty Property { get; set; }
    }
}