using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class RiskAssessmentAnswer : DbObject
    {
        public RiskAssessmentQuestion Question { get; set; }
        public int QuestionId { get; set; }
        public string SpecificHazard { get; set; }
        public string PrecautionsToControlHazard { get; set; }
    }
}
