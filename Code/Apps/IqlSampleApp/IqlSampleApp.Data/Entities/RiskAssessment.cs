using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
{
    public class RiskAssessment : DbObject
    {
        public int SiteInspectionId { get; set; }
        public SiteInspection SiteInspection { get; set; }
        public RiskAssessmentSolution RiskAssessmentSolution { get; set; }
    }
}
