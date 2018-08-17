namespace Iql.Server.Azure
{
    public class AzureConnectionDetails : IAzureConnectionDetails
    {
        public string ConnectionString { get; set; }

        public AzureConnectionDetails(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}