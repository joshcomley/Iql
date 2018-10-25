using System.Collections.Generic;
using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
{
    /// <summary>
    ///     See six options on top of scafftag
    ///     Also: Other
    /// </summary>
    public class PersonLoading : DbObject
    {
        public List<Person> People { get; set; }
        public string Name { get; set; }
    }
}
