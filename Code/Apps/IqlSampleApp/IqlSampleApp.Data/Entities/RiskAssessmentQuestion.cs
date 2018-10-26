using System.Collections.Generic;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    /// <summary>
    ///     This data is managed in the web app
    ///     See Risk Assessment list (photo on phone)
    /// </summary>
    public class RiskAssessmentQuestion : DbObject
    {
        public string Name { get; set; }
        public List<RiskAssessmentAnswer> Answers { get; set; }
    }
}
