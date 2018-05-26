namespace Iql.Data.Contracts
{
    public interface ICustomReport
    {
        string Report { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        string Query { get; set; }
        string Iql { get; set; }
        string Fields { get; set; }
    }
}