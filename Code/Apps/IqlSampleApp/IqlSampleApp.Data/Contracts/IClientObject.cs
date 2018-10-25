using Tunnel.App.Data.Entities;

namespace Tunnel.App.Data.Models.Contracts
{
    public interface IClientObject
    {
        Client Client { get; set; }
        int ClientId { get; set; }
    }
}
