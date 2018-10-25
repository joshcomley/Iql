using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
{
    public class RiskAssessmentSolution : DbObject
    {
        public int RiskAssessmentId { get; set; }
        public RiskAssessment RiskAssessment { get; set; }
    }
}