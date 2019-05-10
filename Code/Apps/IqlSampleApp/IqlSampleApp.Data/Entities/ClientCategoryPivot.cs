namespace IqlSampleApp.Data.Entities
{
    public class ClientCategoryPivot
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int CategoryId { get; set; }
        public ClientCategory Category { get; set; }
    }
}