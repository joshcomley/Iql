using System;
using System.Collections.Generic;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    /// <summary>
    ///     Choose or add a client
    ///     Choose or add a site
    /// </summary>
    public class SiteInspection : DbObject
    {
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int SiteId { get; set; }
        public Site Site { get; set; }
        public List<PersonInspection> PersonInspections { get; set; }
        //public int? RiskAssessmentId { get; set; }
        //public RiskAssessment RiskAssessment { get; set; }
        public List<RiskAssessment> RiskAssessments { get; set; }

        //public byte[] Signature { get; set; }
    }
}
