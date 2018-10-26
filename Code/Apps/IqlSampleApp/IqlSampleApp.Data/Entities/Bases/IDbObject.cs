namespace IqlSampleApp.Data.Entities.Bases
{
    public interface IDbObject<TKey>
	{
		TKey Id { get; set; }
	}
}
