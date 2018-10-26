using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class ClientType : IDbObject<int>
    {
        public List<Client> Clients { get; set; }
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
    }
}
