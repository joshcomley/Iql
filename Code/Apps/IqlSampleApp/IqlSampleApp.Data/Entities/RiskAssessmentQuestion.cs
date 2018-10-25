using System.Collections.Generic;
using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
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
