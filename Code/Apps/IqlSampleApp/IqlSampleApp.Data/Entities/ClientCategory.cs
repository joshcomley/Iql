using System.Collections.Generic;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class ClientCategory : IDbObject<int>
    {
        public List<ClientCategoryPivot> Clients { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}