using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class ReportActionsTaken : DbObject
    {
        public int FaultReportId { get; set; }
        public PersonReport PersonReport { get; set; }
        public string Notes { get; set; }
    }
}
