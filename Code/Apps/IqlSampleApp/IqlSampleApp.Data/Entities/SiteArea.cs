using System.Collections.Generic;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class SiteArea : DbObject
    {
        public List<Person> People { get; set; }
        public Site Site { get; set; }
        public int SiteId { get; set; }
    }
}