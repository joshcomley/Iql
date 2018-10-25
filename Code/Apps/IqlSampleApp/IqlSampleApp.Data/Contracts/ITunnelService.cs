using System.Linq;
using Tunnel.App.Data.Entities;

namespace Tunnel.App.Data.Models.Contracts
{
    public interface ITunnelService
    {
        IQueryable<ApplicationUser> Users { get; }
        IQueryable<Client> Clients { get; }
        IQueryable<ClientType> ClientTypes { get; }
        IQueryable<DocumentCategory> DocumentCategories { get; }
        IQueryable<SiteDocument> SiteDocuments { get; }
        IQueryable<ReportActionsTaken> ReportActionsTaken { get; }
        IQueryable<ReportCategory> ReportCategories { get; }
        IQueryable<ReportDefaultRecommendation> ReportDefaultRecommendations { get; }
        IQueryable<ReportRecommendation> ReportRecommendations { get; }
        IQueryable<ReportType> ReportTypes { get; }
        IQueryable<Project> Projects { get; }
        IQueryable<ReportReceiverEmailAddress> ReportReceiverEmailAddresses { get; }
        IQueryable<RiskAssessment> RiskAssessments { get; }
        IQueryable<RiskAssessmentSolution> RiskAssessmentSolutions { get; }
        IQueryable<RiskAssessmentAnswer> RiskAssessmentAnswers { get; }
        IQueryable<RiskAssessmentQuestion> RiskAssessmentQuestions { get; }
        IQueryable<Person> People { get; }
        IQueryable<PersonInspection> PersonInspections { get; }
        IQueryable<PersonLoading> PersonLoadings { get; }
        IQueryable<PersonType> PersonTypes { get; }
        IQueryable<PersonTypeMap> PersonTypesMap { get; }
        IQueryable<PersonReport> PersonReports { get; }
        IQueryable<Site> Sites { get; }
        IQueryable<SiteInspection> SiteInspections { get; }
        IQueryable<UserSite> UserSites { get; }
    }
}
