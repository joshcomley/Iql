using System.Linq;
using IqlSampleApp.Data.Entities;

namespace IqlSampleApp.Data.Contracts
{
    public interface IIqlSampleAppService
    {
        IQueryable<ApplicationUser> Users { get; }
        IQueryable<ApplicationLog> ApplicationLogs { get; }
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
        IQueryable<SiteArea> SiteAreas { get; }
        IQueryable<SiteInspection> SiteInspections { get; }
        IQueryable<UserSetting> UserSettings { get; }
        IQueryable<UserSite> UserSites { get; }
    }
}
