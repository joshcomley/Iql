namespace Iql.Entities
{
    public interface ITypeConfiguration
    {
        bool IsCollection { get; }
        string ConvertedFromType { get; }
        bool Nullable { get; }
        IqlType Kind { get; }
    }
}