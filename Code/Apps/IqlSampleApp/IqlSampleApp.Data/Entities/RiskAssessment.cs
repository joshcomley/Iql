using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class RiskAssessment : DbObject
    {
        public int SiteInspectionId { get; set; }
        public SiteInspection SiteInspection { get; set; }
        public RiskAssessmentSolution RiskAssessmentSolution { get; set; }
    }
}
