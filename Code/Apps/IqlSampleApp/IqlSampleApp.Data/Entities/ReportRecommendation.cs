using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
{
    public class ReportRecommendation : DbObject
    {
        public int ReportId { get; set; }
        public PersonReport PersonReport { get; set; }
        public int RecommendationId { get; set; }
        public ReportDefaultRecommendation Recommendation { get; set; }
        public string Notes { get; set; }
    }
}
