using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Brandless.Data.Entities;

namespace Tunnel.App.Data.Entities
{
    public class ClientType : IDbObject<int>
    {
        public List<Client> Clients { get; set; }
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
    }
}
