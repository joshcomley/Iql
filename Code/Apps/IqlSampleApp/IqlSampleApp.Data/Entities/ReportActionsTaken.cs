using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
{
    public class ReportActionsTaken : DbObject
    {
        public int FaultReportId { get; set; }
        public PersonReport PersonReport { get; set; }
        public string Notes { get; set; }
    }
}
