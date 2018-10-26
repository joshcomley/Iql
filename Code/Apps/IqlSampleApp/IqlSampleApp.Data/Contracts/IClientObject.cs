using IqlSampleApp.Data.Entities;

namespace IqlSampleApp.Data.Contracts
{
    public interface IClientObject
    {
        Client Client { get; set; }
        int ClientId { get; set; }
    }
}
