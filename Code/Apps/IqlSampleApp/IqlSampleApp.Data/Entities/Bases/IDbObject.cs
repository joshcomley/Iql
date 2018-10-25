namespace Brandless.Data.Entities
{
    public interface IDbObject<TKey>
	{
		TKey Id { get; set; }
	}
}
