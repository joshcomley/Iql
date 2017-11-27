using System;
using System.Collections.Generic;
using System.Text;

namespace Iql.Tests.Context
{
    public class InMemoryDataBase
    {
        public IList<ClientType> ClientTypes { get; set; } = new List<ClientType>();
        public IList<Client> Clients { get; set; } = new List<Client>();
    }
}
