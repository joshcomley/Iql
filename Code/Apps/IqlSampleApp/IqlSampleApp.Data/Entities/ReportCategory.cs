using System.Collections.Generic;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class ReportCategory : DbObject
    {
        public string Name { get; set; }
        public List<ReportType> ReportTypes { get; set; }
    }
}
