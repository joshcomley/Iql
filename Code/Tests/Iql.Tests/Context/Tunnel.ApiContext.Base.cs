using Iql.Queryable.Operations;
using Tunnel.ApiContext.Base;
using Tunnel.App.Data.Entities;
using Iql.OData.Data;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.DataStores;
using System;
using Iql.Queryable;
namespace Tunnel.ApiContext.Base
{
	public class TunnelDataContextBase : DataContext
	{
		public TunnelDataContextBase(IDataStore dataStore) : base(dataStore)
		{
			this.Users = (DbSet<ApplicationUser,string>)this.AsDbSet<ApplicationUser, string>();
			this.Clients = (DbSet<Client,int>)this.AsDbSet<Client, int>();
			this.ClientTypes = (DbSet<ClientType,int>)this.AsDbSet<ClientType, int>();
			this.DocumentCategories = (DbSet<DocumentCategory,int>)this.AsDbSet<DocumentCategory, int>();
			this.SiteDocuments = (DbSet<SiteDocument,int>)this.AsDbSet<SiteDocument, int>();
			this.ReportActionsTaken = (DbSet<ReportActionsTaken,int>)this.AsDbSet<ReportActionsTaken, int>();
			this.ReportCategories = (DbSet<ReportCategory,int>)this.AsDbSet<ReportCategory, int>();
			this.ReportDefaultRecommendations = (DbSet<ReportDefaultRecommendation,int>)this.AsDbSet<ReportDefaultRecommendation, int>();
			this.ReportRecommendations = (DbSet<ReportRecommendation,int>)this.AsDbSet<ReportRecommendation, int>();
			this.ReportTypes = (DbSet<ReportType,int>)this.AsDbSet<ReportType, int>();
			this.Projects = (DbSet<Project,int>)this.AsDbSet<Project, int>();
			this.ReportReceiverEmailAddresses = (DbSet<ReportReceiverEmailAddress,int>)this.AsDbSet<ReportReceiverEmailAddress, int>();
			this.RiskAssessments = (DbSet<RiskAssessment,int>)this.AsDbSet<RiskAssessment, int>();
			this.RiskAssessmentSolutions = (DbSet<RiskAssessmentSolution,int>)this.AsDbSet<RiskAssessmentSolution, int>();
			this.RiskAssessmentAnswers = (DbSet<RiskAssessmentAnswer,int>)this.AsDbSet<RiskAssessmentAnswer, int>();
			this.RiskAssessmentQuestions = (DbSet<RiskAssessmentQuestion,int>)this.AsDbSet<RiskAssessmentQuestion, int>();
			this.People = (DbSet<Person,int>)this.AsDbSet<Person, int>();
			this.PersonInspections = (DbSet<PersonInspection,int>)this.AsDbSet<PersonInspection, int>();
			this.PersonLoadings = (DbSet<PersonLoading,int>)this.AsDbSet<PersonLoading, int>();
			this.PersonTypes = (DbSet<PersonType,int>)this.AsDbSet<PersonType, int>();
			this.PersonTypesMap = (DbSet<PersonTypeMap,CompositeKey>)this.AsDbSet<PersonTypeMap, CompositeKey>();
			this.PersonReports = (DbSet<PersonReport,int>)this.AsDbSet<PersonReport, int>();
			this.Sites = (DbSet<Site,int>)this.AsDbSet<Site, int>();
			this.SiteInspections = (DbSet<SiteInspection,int>)this.AsDbSet<SiteInspection, int>();
			this.UserSites = (DbSet<UserSite,CompositeKey>)this.AsDbSet<UserSite, CompositeKey>();
			this.RegisterConfiguration<ODataConfiguration>(this.ODataConfiguration);
			this.ODataConfiguration.RegisterEntitySet<ApplicationUser>(nameof(Users));
			this.ODataConfiguration.RegisterEntitySet<Client>(nameof(Clients));
			this.ODataConfiguration.RegisterEntitySet<ClientType>(nameof(ClientTypes));
			this.ODataConfiguration.RegisterEntitySet<DocumentCategory>(nameof(DocumentCategories));
			this.ODataConfiguration.RegisterEntitySet<SiteDocument>(nameof(SiteDocuments));
			this.ODataConfiguration.RegisterEntitySet<ReportActionsTaken>(nameof(ReportActionsTaken));
			this.ODataConfiguration.RegisterEntitySet<ReportCategory>(nameof(ReportCategories));
			this.ODataConfiguration.RegisterEntitySet<ReportDefaultRecommendation>(nameof(ReportDefaultRecommendations));
			this.ODataConfiguration.RegisterEntitySet<ReportRecommendation>(nameof(ReportRecommendations));
			this.ODataConfiguration.RegisterEntitySet<ReportType>(nameof(ReportTypes));
			this.ODataConfiguration.RegisterEntitySet<Project>(nameof(Projects));
			this.ODataConfiguration.RegisterEntitySet<ReportReceiverEmailAddress>(nameof(ReportReceiverEmailAddresses));
			this.ODataConfiguration.RegisterEntitySet<RiskAssessment>(nameof(RiskAssessments));
			this.ODataConfiguration.RegisterEntitySet<RiskAssessmentSolution>(nameof(RiskAssessmentSolutions));
			this.ODataConfiguration.RegisterEntitySet<RiskAssessmentAnswer>(nameof(RiskAssessmentAnswers));
			this.ODataConfiguration.RegisterEntitySet<RiskAssessmentQuestion>(nameof(RiskAssessmentQuestions));
			this.ODataConfiguration.RegisterEntitySet<Person>(nameof(People));
			this.ODataConfiguration.RegisterEntitySet<PersonInspection>(nameof(PersonInspections));
			this.ODataConfiguration.RegisterEntitySet<PersonLoading>(nameof(PersonLoadings));
			this.ODataConfiguration.RegisterEntitySet<PersonType>(nameof(PersonTypes));
			this.ODataConfiguration.RegisterEntitySet<PersonTypeMap>(nameof(PersonTypesMap));
			this.ODataConfiguration.RegisterEntitySet<PersonReport>(nameof(PersonReports));
			this.ODataConfiguration.RegisterEntitySet<Site>(nameof(Sites));
			this.ODataConfiguration.RegisterEntitySet<SiteInspection>(nameof(SiteInspections));
			this.ODataConfiguration.RegisterEntitySet<UserSite>(nameof(UserSites));
		}
		
		public ODataConfiguration ODataConfiguration { get; set; } = new ODataConfiguration();
		
		public override void Configure(EntityConfigurationBuilder builder)
		{
			builder.EntityType<ApplicationUser>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.ClientId, true)
				.DefineProperty(p => p.Email, true)
				.DefineProperty(p => p.FullName, false)
				.DefineProperty(p => p.EmailConfirmed, false)
				.DefineProperty(p => p.UserType, false)
				.DefineProperty(p => p.IsLockedOut, false)
				.DefineProperty(p => p.Client, true)
				.DefineCollectionProperty(p => p.ClientsCreated, p => p.ClientsCreatedCount)
				.DefineCollectionProperty(p => p.DocumentCategoriesCreated, p => p.DocumentCategoriesCreatedCount)
				.DefineCollectionProperty(p => p.SiteDocumentsCreated, p => p.SiteDocumentsCreatedCount)
				.DefineCollectionProperty(p => p.FaultActionsTakenCreated, p => p.FaultActionsTakenCreatedCount)
				.DefineCollectionProperty(p => p.FaultCategoriesCreated, p => p.FaultCategoriesCreatedCount)
				.DefineCollectionProperty(p => p.FaultDefaultRecommendationsCreated, p => p.FaultDefaultRecommendationsCreatedCount)
				.DefineCollectionProperty(p => p.FaultRecommendationsCreated, p => p.FaultRecommendationsCreatedCount)
				.DefineCollectionProperty(p => p.FaultTypesCreated, p => p.FaultTypesCreatedCount)
				.DefineCollectionProperty(p => p.ProjectCreated, p => p.ProjectCreatedCount)
				.DefineCollectionProperty(p => p.ReportReceiverEmailAddressesCreated, p => p.ReportReceiverEmailAddressesCreatedCount)
				.DefineCollectionProperty(p => p.RiskAssessmentsCreated, p => p.RiskAssessmentsCreatedCount)
				.DefineCollectionProperty(p => p.RiskAssessmentAnswersCreated, p => p.RiskAssessmentAnswersCreatedCount)
				.DefineCollectionProperty(p => p.RiskAssessmentQuestionsCreated, p => p.RiskAssessmentQuestionsCreatedCount)
				.DefineCollectionProperty(p => p.PeopleCreated, p => p.PeopleCreatedCount)
				.DefineCollectionProperty(p => p.PersonInspectionsCreated, p => p.PersonInspectionsCreatedCount)
				.DefineCollectionProperty(p => p.PersonLoadingsCreated, p => p.PersonLoadingsCreatedCount)
				.DefineCollectionProperty(p => p.PersonTypesCreated, p => p.PersonTypesCreatedCount)
				.DefineCollectionProperty(p => p.FaultReportsCreated, p => p.FaultReportsCreatedCount)
				.DefineCollectionProperty(p => p.SitesCreated, p => p.SitesCreatedCount)
				.DefineCollectionProperty(p => p.SiteInspectionsCreated, p => p.SiteInspectionsCreatedCount)
				.DefineCollectionProperty(p => p.Sites, p => p.SitesCount);
			
			builder.EntityType<ApplicationUser>()
				.HasOne(p => p.Client)
				.WithMany(p => p.Users)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<Client>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.TypeId, false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Name, false)
				.DefineProperty(p => p.Description, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineCollectionProperty(p => p.Users, p => p.UsersCount)
				.DefineProperty(p => p.Type, false)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineCollectionProperty(p => p.People, p => p.PeopleCount)
				.DefineCollectionProperty(p => p.Sites, p => p.SitesCount);
			
			builder.EntityType<Client>()
				.HasOne(p => p.Type)
				.WithMany(p => p.Clients)
				.WithConstraint(p => p.TypeId, p => p.Id);
			
			builder.EntityType<Client>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ClientsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ClientType>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.Name, true)
				.DefineCollectionProperty(p => p.Clients, p => p.ClientsCount);
			
			builder.EntityType<DocumentCategory>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Name, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineCollectionProperty(p => p.Documents, p => p.DocumentsCount);
			
			builder.EntityType<DocumentCategory>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.DocumentCategoriesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<SiteDocument>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.CategoryId, false)
				.DefineProperty(p => p.SiteId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Title, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.Category, false)
				.DefineProperty(p => p.Site, false)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<SiteDocument>()
				.HasOne(p => p.Category)
				.WithMany(p => p.Documents)
				.WithConstraint(p => p.CategoryId, p => p.Id);
			
			builder.EntityType<SiteDocument>()
				.HasOne(p => p.Site)
				.WithMany(p => p.Documents)
				.WithConstraint(p => p.SiteId, p => p.Id);
			
			builder.EntityType<SiteDocument>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.SiteDocumentsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportActionsTaken>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.FaultReportId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Notes, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.PersonReport, false)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<ReportActionsTaken>()
				.HasOne(p => p.PersonReport)
				.WithMany(p => p.ActionsTaken)
				.WithConstraint(p => p.FaultReportId, p => p.Id);
			
			builder.EntityType<ReportActionsTaken>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultActionsTakenCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportCategory>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Name, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineCollectionProperty(p => p.ReportTypes, p => p.ReportTypesCount);
			
			builder.EntityType<ReportCategory>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultCategoriesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportDefaultRecommendation>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Name, true)
				.DefineProperty(p => p.Text, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineCollectionProperty(p => p.Recommendations, p => p.RecommendationsCount);
			
			builder.EntityType<ReportDefaultRecommendation>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultDefaultRecommendationsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportRecommendation>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.ReportId, false)
				.DefineProperty(p => p.RecommendationId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Notes, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.PersonReport, false)
				.DefineProperty(p => p.Recommendation, false)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<ReportRecommendation>()
				.HasOne(p => p.PersonReport)
				.WithMany(p => p.Recommendations)
				.WithConstraint(p => p.ReportId, p => p.Id);
			
			builder.EntityType<ReportRecommendation>()
				.HasOne(p => p.Recommendation)
				.WithMany(p => p.Recommendations)
				.WithConstraint(p => p.RecommendationId, p => p.Id);
			
			builder.EntityType<ReportRecommendation>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultRecommendationsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportType>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CategoryId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.Name, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineProperty(p => p.Category, false)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineCollectionProperty(p => p.FaultReports, p => p.FaultReportsCount);
			
			builder.EntityType<ReportType>()
				.HasOne(p => p.Category)
				.WithMany(p => p.ReportTypes)
				.WithConstraint(p => p.CategoryId, p => p.Id);
			
			builder.EntityType<ReportType>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultTypesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<Project>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.Title, false)
				.DefineProperty(p => p.Description, true)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<Project>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ProjectCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportReceiverEmailAddress>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.SiteId, false)
				.DefineProperty(p => p.EmailAddress, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.Site, false)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<ReportReceiverEmailAddress>()
				.HasOne(p => p.Site)
				.WithMany(p => p.AdditionalSendReportsTo)
				.WithConstraint(p => p.SiteId, p => p.Id);
			
			builder.EntityType<ReportReceiverEmailAddress>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ReportReceiverEmailAddressesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<RiskAssessment>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.SiteInspectionId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.RiskAssessmentSolution, true)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineProperty(p => p.SiteInspection, false);
			
			builder.EntityType<RiskAssessment>()
				.HasOne(p => p.RiskAssessmentSolution)
				.WithOne(p => p.RiskAssessment)
				.WithConstraint(p => p.Id, p => p.RiskAssessmentId);
			
			builder.EntityType<RiskAssessment>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.RiskAssessmentsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<RiskAssessmentSolution>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.RiskAssessmentId, false)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.RiskAssessment, false)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<RiskAssessmentAnswer>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.QuestionId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.SpecificHazard, true)
				.DefineProperty(p => p.PrecautionsToControlHazard, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.Question, false)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<RiskAssessmentAnswer>()
				.HasOne(p => p.Question)
				.WithMany(p => p.Answers)
				.WithConstraint(p => p.QuestionId, p => p.Id);
			
			builder.EntityType<RiskAssessmentAnswer>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.RiskAssessmentAnswersCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<RiskAssessmentQuestion>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Name, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineCollectionProperty(p => p.Answers, p => p.AnswersCount)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<RiskAssessmentQuestion>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.RiskAssessmentQuestionsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<Person>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.TypeId, true)
				.DefineProperty(p => p.LoadingId, true)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Key, true)
				.DefineProperty(p => p.Title, true)
				.DefineProperty(p => p.Description, true)
				.DefineProperty(p => p.Category, false)
				.DefineProperty(p => p.ClientId, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.Client, true)
				.DefineProperty(p => p.Type, true)
				.DefineProperty(p => p.Loading, true)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineCollectionProperty(p => p.Types, p => p.TypesCount)
				.DefineCollectionProperty(p => p.Reports, p => p.ReportsCount);
			
			builder.EntityType<Person>()
				.HasOne(p => p.Client)
				.WithMany(p => p.People)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<Person>()
				.HasOne(p => p.Type)
				.WithMany(p => p.People)
				.WithConstraint(p => p.TypeId, p => p.Id);
			
			builder.EntityType<Person>()
				.HasOne(p => p.Loading)
				.WithMany(p => p.People)
				.WithConstraint(p => p.LoadingId, p => p.Id);
			
			builder.EntityType<Person>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.PeopleCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<PersonInspection>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.SiteInspectionId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.PersonId, false)
				.DefineProperty(p => p.InspectionStatus, false)
				.DefineProperty(p => p.StartTime, false)
				.DefineProperty(p => p.EndTime, false)
				.DefineProperty(p => p.ReasonForFailure, false)
				.DefineProperty(p => p.IsDesignRequired, false)
				.DefineProperty(p => p.DrawingNumber, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.SiteInspection, false)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<PersonInspection>()
				.HasOne(p => p.SiteInspection)
				.WithMany(p => p.PersonInspections)
				.WithConstraint(p => p.SiteInspectionId, p => p.Id);
			
			builder.EntityType<PersonInspection>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.PersonInspectionsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<PersonLoading>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Name, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineCollectionProperty(p => p.People, p => p.PeopleCount)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<PersonLoading>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.PersonLoadingsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<PersonType>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Title, false)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineCollectionProperty(p => p.People, p => p.PeopleCount)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineCollectionProperty(p => p.PeopleMap, p => p.PeopleMapCount);
			
			builder.EntityType<PersonType>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.PersonTypesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<PersonTypeMap>()
				.HasCompositeKey(p => p.PersonId, p => p.TypeId)
				.DefineProperty(p => p.PersonId, false)
				.DefineProperty(p => p.TypeId, false)
				.DefineProperty(p => p.Notes, false)
				.DefineProperty(p => p.Description, false)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Person, false)
				.DefineProperty(p => p.Type, false);
			
			builder.EntityType<PersonTypeMap>()
				.HasOne(p => p.Person)
				.WithMany(p => p.Types)
				.WithConstraint(p => p.PersonId, p => p.Id);
			
			builder.EntityType<PersonTypeMap>()
				.HasOne(p => p.Type)
				.WithMany(p => p.PeopleMap)
				.WithConstraint(p => p.TypeId, p => p.Id);
			
			builder.EntityType<PersonReport>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.PersonId, false)
				.DefineProperty(p => p.TypeId, false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Title, true)
				.DefineProperty(p => p.Status, false)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineCollectionProperty(p => p.ActionsTaken, p => p.ActionsTakenCount)
				.DefineCollectionProperty(p => p.Recommendations, p => p.RecommendationsCount)
				.DefineProperty(p => p.Person, false)
				.DefineProperty(p => p.Type, false)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<PersonReport>()
				.HasOne(p => p.Person)
				.WithMany(p => p.Reports)
				.WithConstraint(p => p.PersonId, p => p.Id);
			
			builder.EntityType<PersonReport>()
				.HasOne(p => p.Type)
				.WithMany(p => p.FaultReports)
				.WithConstraint(p => p.TypeId, p => p.Id);
			
			builder.EntityType<PersonReport>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultReportsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<Site>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.ParentId, true)
				.DefineProperty(p => p.ClientId, true)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Address, true)
				.DefineProperty(p => p.PostCode, true)
				.DefineProperty(p => p.Name, true)
				.DefineProperty(p => p.Left, false)
				.DefineProperty(p => p.Right, false)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineCollectionProperty(p => p.Documents, p => p.DocumentsCount)
				.DefineCollectionProperty(p => p.AdditionalSendReportsTo, p => p.AdditionalSendReportsToCount)
				.DefineProperty(p => p.Parent, true)
				.DefineCollectionProperty(p => p.Children, p => p.ChildrenCount)
				.DefineProperty(p => p.Client, true)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineCollectionProperty(p => p.SiteInspections, p => p.SiteInspectionsCount)
				.DefineCollectionProperty(p => p.Users, p => p.UsersCount);
			
			builder.EntityType<Site>()
				.HasOne(p => p.Parent)
				.WithMany(p => p.Children)
				.WithConstraint(p => p.ParentId, p => p.Id);
			
			builder.EntityType<Site>()
				.HasOne(p => p.Client)
				.WithMany(p => p.Sites)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<Site>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.SitesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<SiteInspection>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.SiteId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.StartTime, false)
				.DefineProperty(p => p.EndTime, false)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineCollectionProperty(p => p.PersonInspections, p => p.PersonInspectionsCount)
				.DefineProperty(p => p.RiskAssessment, true)
				.DefineProperty(p => p.Site, false)
				.DefineProperty(p => p.CreatedByUser, true);

		    builder.EntityType<RiskAssessment>()
		        .HasOne(p => p.SiteInspection)
		        .WithOne(p => p.RiskAssessment)
		        .WithConstraint(p => p.SiteInspectionId, p => p.Id);

		    builder.EntityType<SiteInspection>()
				.HasOne(p => p.RiskAssessment)
				.WithOne(p => p.SiteInspection)
				.WithConstraint(p => p.Id, p => p.SiteInspectionId);
			
			builder.EntityType<SiteInspection>()
				.HasOne(p => p.Site)
				.WithMany(p => p.SiteInspections)
				.WithConstraint(p => p.SiteId, p => p.Id);
			
			builder.EntityType<SiteInspection>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.SiteInspectionsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<UserSite>()
				.HasCompositeKey(p => p.SiteId, p => p.UserId)
				.DefineProperty(p => p.SiteId, false)
				.DefineProperty(p => p.UserId, true)
				.DefineProperty(p => p.User, false)
				.DefineProperty(p => p.Site, false);
			
			builder.EntityType<UserSite>()
				.HasOne(p => p.User)
				.WithMany(p => p.Sites)
				.WithConstraint(p => p.UserId, p => p.Id);
			
			builder.EntityType<UserSite>()
				.HasOne(p => p.Site)
				.WithMany(p => p.Users)
				.WithConstraint(p => p.SiteId, p => p.Id);
		}
		
		
		public DbSet<ApplicationUser, string> Users { get; set; }
		
		public DbSet<Client, int> Clients { get; set; }
		
		public DbSet<ClientType, int> ClientTypes { get; set; }
		
		public DbSet<DocumentCategory, int> DocumentCategories { get; set; }
		
		public DbSet<SiteDocument, int> SiteDocuments { get; set; }
		
		public DbSet<ReportActionsTaken, int> ReportActionsTaken { get; set; }
		
		public DbSet<ReportCategory, int> ReportCategories { get; set; }
		
		public DbSet<ReportDefaultRecommendation, int> ReportDefaultRecommendations { get; set; }
		
		public DbSet<ReportRecommendation, int> ReportRecommendations { get; set; }
		
		public DbSet<ReportType, int> ReportTypes { get; set; }
		
		public DbSet<Project, int> Projects { get; set; }
		
		public DbSet<ReportReceiverEmailAddress, int> ReportReceiverEmailAddresses { get; set; }
		
		public DbSet<RiskAssessment, int> RiskAssessments { get; set; }
		
		public DbSet<RiskAssessmentSolution, int> RiskAssessmentSolutions { get; set; }
		
		public DbSet<RiskAssessmentAnswer, int> RiskAssessmentAnswers { get; set; }
		
		public DbSet<RiskAssessmentQuestion, int> RiskAssessmentQuestions { get; set; }
		
		public DbSet<Person, int> People { get; set; }
		
		public DbSet<PersonInspection, int> PersonInspections { get; set; }
		
		public DbSet<PersonLoading, int> PersonLoadings { get; set; }
		
		public DbSet<PersonType, int> PersonTypes { get; set; }
		
		public DbSet<PersonTypeMap, CompositeKey> PersonTypesMap { get; set; }
		
		public DbSet<PersonReport, int> PersonReports { get; set; }
		
		public DbSet<Site, int> Sites { get; set; }
		
		public DbSet<SiteInspection, int> SiteInspections { get; set; }
		
		public DbSet<UserSite, CompositeKey> UserSites { get; set; }
	}
}

