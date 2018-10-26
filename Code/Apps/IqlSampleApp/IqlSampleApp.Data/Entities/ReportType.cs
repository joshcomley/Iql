using System;
using System.Collections.Generic;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class ReportType : DbObject
    {
        public Guid PersistenceKey { get; set; }
        public ReportCategory Category { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<PersonReport> FaultReports { get; set; }
    }
}
