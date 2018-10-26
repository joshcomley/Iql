using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class RiskAssessmentSolution : DbObject
    {
        public int RiskAssessmentId { get; set; }
        public RiskAssessment RiskAssessment { get; set; }
    }
}