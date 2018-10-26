using System.Collections.Generic;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
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
