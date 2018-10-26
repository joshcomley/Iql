using System.Collections.Generic;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class PersonReport : DbObject
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public ReportType Type { get; set; }
        public string Title { get; set; }
        public int TypeId { get; set; }

        //public List<byte[]> Photos { get; set; }
        public FaultReportStatus Status { get; set; }

        /// <summary>
        ///     This comes first in the screens
        /// </summary>
        public List<ReportActionsTaken> ActionsTaken { get; set; }

        public List<ReportRecommendation> Recommendations { get; set; }
    }
}
