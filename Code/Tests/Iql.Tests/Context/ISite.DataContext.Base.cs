using Iql.Queryable.Operations;
using Iql.OData.Data;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.DataStores;
using System;
using Iql.Queryable;
public class ISiteDataContextBase : DataContext {
	public ISiteDataContextBase(IDataStore dataStore) : base(dataStore) {
		this.Users = (DbSet<ApplicationUser,string>)this.AsDbSet<ApplicationUser, string>();		
		this.Clients = (DbSet<Client,int>)this.AsDbSet<Client, int>();		
		this.ClientTypes = (DbSet<ClientType,int>)this.AsDbSet<ClientType, int>();		
		this.DocumentCategories = (DbSet<DocumentCategory,int>)this.AsDbSet<DocumentCategory, int>();		
		this.SiteDocuments = (DbSet<SiteDocument,int>)this.AsDbSet<SiteDocument, int>();		
		this.FaultActionsTaken = (DbSet<FaultActionsTaken,int>)this.AsDbSet<FaultActionsTaken, int>();		
		this.FaultCategories = (DbSet<FaultCategory,int>)this.AsDbSet<FaultCategory, int>();		
		this.FaultDefaultRecommendations = (DbSet<FaultDefaultRecommendation,int>)this.AsDbSet<FaultDefaultRecommendation, int>();		
		this.FaultRecommendations = (DbSet<FaultRecommendation,int>)this.AsDbSet<FaultRecommendation, int>();		
		this.FaultReports = (DbSet<FaultReport,int>)this.AsDbSet<FaultReport, int>();		
		this.FaultTypes = (DbSet<FaultType,int>)this.AsDbSet<FaultType, int>();		
		this.Projects = (DbSet<Project,int>)this.AsDbSet<Project, int>();		
		this.ReportReceiverEmailAddresses = (DbSet<ReportReceiverEmailAddress,int>)this.AsDbSet<ReportReceiverEmailAddress, int>();		
		this.RiskAssessments = (DbSet<RiskAssessment,int>)this.AsDbSet<RiskAssessment, int>();		
		this.RiskAssessmentAnswers = (DbSet<RiskAssessmentAnswer,int>)this.AsDbSet<RiskAssessmentAnswer, int>();		
		this.RiskAssessmentQuestions = (DbSet<RiskAssessmentQuestion,int>)this.AsDbSet<RiskAssessmentQuestion, int>();		
		this.Scaffolds = (DbSet<Scaffold,int>)this.AsDbSet<Scaffold, int>();		
		this.ScaffoldInspections = (DbSet<ScaffoldInspection,int>)this.AsDbSet<ScaffoldInspection, int>();		
		this.ScaffoldLoadings = (DbSet<ScaffoldLoading,int>)this.AsDbSet<ScaffoldLoading, int>();		
		this.ScaffoldTypes = (DbSet<ScaffoldType,int>)this.AsDbSet<ScaffoldType, int>();		
		this.Sites = (DbSet<Site,int>)this.AsDbSet<Site, int>();		
		this.SiteInspections = (DbSet<SiteInspection,int>)this.AsDbSet<SiteInspection, int>();		
		this.UserSites = (DbSet<UserSite,CompositeKey>)this.AsDbSet<UserSite, CompositeKey>();		
		this.RegisterConfiguration<ODataConfiguration>(this.ODataConfiguration);
		this.ODataConfiguration.RegisterEntitySet<ApplicationUser>("Users");
		this.ODataConfiguration.RegisterEntitySet<Client>("Clients");
		this.ODataConfiguration.RegisterEntitySet<ClientType>("ClientTypes");
		this.ODataConfiguration.RegisterEntitySet<DocumentCategory>("DocumentCategories");
		this.ODataConfiguration.RegisterEntitySet<SiteDocument>("SiteDocuments");
		this.ODataConfiguration.RegisterEntitySet<FaultActionsTaken>("FaultActionsTaken");
		this.ODataConfiguration.RegisterEntitySet<FaultCategory>("FaultCategories");
		this.ODataConfiguration.RegisterEntitySet<FaultDefaultRecommendation>("FaultDefaultRecommendations");
		this.ODataConfiguration.RegisterEntitySet<FaultRecommendation>("FaultRecommendations");
		this.ODataConfiguration.RegisterEntitySet<FaultReport>("FaultReports");
		this.ODataConfiguration.RegisterEntitySet<FaultType>("FaultTypes");
		this.ODataConfiguration.RegisterEntitySet<Project>("Projects");
		this.ODataConfiguration.RegisterEntitySet<ReportReceiverEmailAddress>("ReportReceiverEmailAddresses");
		this.ODataConfiguration.RegisterEntitySet<RiskAssessment>("RiskAssessments");
		this.ODataConfiguration.RegisterEntitySet<RiskAssessmentAnswer>("RiskAssessmentAnswers");
		this.ODataConfiguration.RegisterEntitySet<RiskAssessmentQuestion>("RiskAssessmentQuestions");
		this.ODataConfiguration.RegisterEntitySet<Scaffold>("Scaffolds");
		this.ODataConfiguration.RegisterEntitySet<ScaffoldInspection>("ScaffoldInspections");
		this.ODataConfiguration.RegisterEntitySet<ScaffoldLoading>("ScaffoldLoadings");
		this.ODataConfiguration.RegisterEntitySet<ScaffoldType>("ScaffoldTypes");
		this.ODataConfiguration.RegisterEntitySet<Site>("Sites");
		this.ODataConfiguration.RegisterEntitySet<SiteInspection>("SiteInspections");
		this.ODataConfiguration.RegisterEntitySet<UserSite>("UserSites");
	}
	public ODataConfiguration ODataConfiguration { get; set; } = new ODataConfiguration();
	
	public override void Configure(EntityConfigurationBuilder builder) {
		builder.DefineEntity<ApplicationUser>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.ClientId)			
			.DefineProperty(p => p.Email)			
			.DefineProperty(p => p.FullName)			
			.DefineProperty(p => p.EmailConfirmed)			
			.DefineProperty(p => p.UserType)			
			.DefineProperty(p => p.IsLockedOut)			
			.DefineProperty(p => p.Client)			
			.DefineCollectionProperty(p => p.ClientsCreated, p => p.ClientsCreatedCount)			
			.DefineCollectionProperty(p => p.DocumentCategoriesCreated, p => p.DocumentCategoriesCreatedCount)			
			.DefineCollectionProperty(p => p.SiteDocumentsCreated, p => p.SiteDocumentsCreatedCount)			
			.DefineCollectionProperty(p => p.FaultActionsTakenCreated, p => p.FaultActionsTakenCreatedCount)			
			.DefineCollectionProperty(p => p.FaultCategoriesCreated, p => p.FaultCategoriesCreatedCount)			
			.DefineCollectionProperty(p => p.FaultDefaultRecommendationsCreated, p => p.FaultDefaultRecommendationsCreatedCount)			
			.DefineCollectionProperty(p => p.FaultRecommendationsCreated, p => p.FaultRecommendationsCreatedCount)			
			.DefineCollectionProperty(p => p.FaultReportsCreated, p => p.FaultReportsCreatedCount)			
			.DefineCollectionProperty(p => p.FaultTypesCreated, p => p.FaultTypesCreatedCount)			
			.DefineCollectionProperty(p => p.ProjectCreated, p => p.ProjectCreatedCount)			
			.DefineCollectionProperty(p => p.ReportReceiverEmailAddressesCreated, p => p.ReportReceiverEmailAddressesCreatedCount)			
			.DefineCollectionProperty(p => p.RiskAssessmentsCreated, p => p.RiskAssessmentsCreatedCount)			
			.DefineCollectionProperty(p => p.RiskAssessmentAnswersCreated, p => p.RiskAssessmentAnswersCreatedCount)			
			.DefineCollectionProperty(p => p.RiskAssessmentQuestionsCreated, p => p.RiskAssessmentQuestionsCreatedCount)			
			.DefineCollectionProperty(p => p.ScaffoldsCreated, p => p.ScaffoldsCreatedCount)			
			.DefineCollectionProperty(p => p.ScaffoldInspectionsCreated, p => p.ScaffoldInspectionsCreatedCount)			
			.DefineCollectionProperty(p => p.ScaffoldLoadingsCreated, p => p.ScaffoldLoadingsCreatedCount)			
			.DefineCollectionProperty(p => p.ScaffoldTypesCreated, p => p.ScaffoldTypesCreatedCount)			
			.DefineCollectionProperty(p => p.SitesCreated, p => p.SitesCreatedCount)			
			.DefineCollectionProperty(p => p.SiteInspectionsCreated, p => p.SiteInspectionsCreatedCount)			
			.DefineCollectionProperty(p => p.Sites, p => p.SitesCount);		
		
		builder.DefineEntity<ApplicationUser>()		
			.HasOne(p => p.Client)			
			.WithMany(p => p.Users)			
			.WithConstraint(p => p.ClientId, p => p.Id);		
		
		builder.DefineEntity<Client>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.TypeId)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Name)			
			.DefineProperty(p => p.Description)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineCollectionProperty(p => p.Users, p => p.UsersCount)			
			.DefineProperty(p => p.Type)			
			.DefineProperty(p => p.CreatedByUser)			
			.DefineCollectionProperty(p => p.Scaffolds, p => p.ScaffoldsCount);		
		
		builder.DefineEntity<Client>()		
			.HasOne(p => p.Type)			
			.WithMany(p => p.Clients)			
			.WithConstraint(p => p.TypeId, p => p.Id);		
		
		builder.DefineEntity<Client>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.ClientsCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<ClientType>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.Name)			
			.DefineCollectionProperty(p => p.Clients, p => p.ClientsCount);		
		
		builder.DefineEntity<DocumentCategory>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Name)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineProperty(p => p.CreatedByUser)			
			.DefineCollectionProperty(p => p.Documents, p => p.DocumentsCount);		
		
		builder.DefineEntity<DocumentCategory>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.DocumentCategoriesCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<SiteDocument>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.CategoryId)			
			.DefineProperty(p => p.SiteId)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Title)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineProperty(p => p.Category)			
			.DefineProperty(p => p.Site)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<SiteDocument>()		
			.HasOne(p => p.Category)			
			.WithMany(p => p.Documents)			
			.WithConstraint(p => p.CategoryId, p => p.Id);		
		
		builder.DefineEntity<SiteDocument>()		
			.HasOne(p => p.Site)			
			.WithMany(p => p.Documents)			
			.WithConstraint(p => p.SiteId, p => p.Id);		
		
		builder.DefineEntity<SiteDocument>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.SiteDocumentsCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<FaultActionsTaken>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.FaultReportId)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Notes)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineProperty(p => p.FaultReport)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<FaultActionsTaken>()		
			.HasOne(p => p.FaultReport)			
			.WithMany(p => p.ActionsTaken)			
			.WithConstraint(p => p.FaultReportId, p => p.Id);		
		
		builder.DefineEntity<FaultActionsTaken>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.FaultActionsTakenCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<FaultCategory>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Name)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineProperty(p => p.CreatedByUser)			
			.DefineCollectionProperty(p => p.FaultTypes, p => p.FaultTypesCount);		
		
		builder.DefineEntity<FaultCategory>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.FaultCategoriesCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<FaultDefaultRecommendation>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Name)			
			.DefineProperty(p => p.Text)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineProperty(p => p.CreatedByUser)			
			.DefineCollectionProperty(p => p.Recommendations, p => p.RecommendationsCount);		
		
		builder.DefineEntity<FaultDefaultRecommendation>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.FaultDefaultRecommendationsCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<FaultRecommendation>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.FaultReportId)			
			.DefineProperty(p => p.RecommendationId)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Notes)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineProperty(p => p.FaultReport)			
			.DefineProperty(p => p.Recommendation)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<FaultRecommendation>()		
			.HasOne(p => p.FaultReport)			
			.WithMany(p => p.Recommendations)			
			.WithConstraint(p => p.FaultReportId, p => p.Id);		
		
		builder.DefineEntity<FaultRecommendation>()		
			.HasOne(p => p.Recommendation)			
			.WithMany(p => p.Recommendations)			
			.WithConstraint(p => p.RecommendationId, p => p.Id);		
		
		builder.DefineEntity<FaultRecommendation>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.FaultRecommendationsCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<FaultReport>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.ScaffoldId)			
			.DefineProperty(p => p.TypeId)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Status)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineCollectionProperty(p => p.ActionsTaken, p => p.ActionsTakenCount)			
			.DefineCollectionProperty(p => p.Recommendations, p => p.RecommendationsCount)			
			.DefineProperty(p => p.Scaffold)			
			.DefineProperty(p => p.Type)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<FaultReport>()		
			.HasOne(p => p.Scaffold)			
			.WithMany(p => p.FaultReports)			
			.WithConstraint(p => p.ScaffoldId, p => p.Id);		
		
		builder.DefineEntity<FaultReport>()		
			.HasOne(p => p.Type)			
			.WithMany(p => p.FaultReports)			
			.WithConstraint(p => p.TypeId, p => p.Id);		
		
		builder.DefineEntity<FaultReport>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.FaultReportsCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<FaultType>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CategoryId)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Name)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineCollectionProperty(p => p.FaultReports, p => p.FaultReportsCount)			
			.DefineProperty(p => p.Category)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<FaultType>()		
			.HasOne(p => p.Category)			
			.WithMany(p => p.FaultTypes)			
			.WithConstraint(p => p.CategoryId, p => p.Id);		
		
		builder.DefineEntity<FaultType>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.FaultTypesCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<Project>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.Title)			
			.DefineProperty(p => p.Description)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<Project>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.ProjectCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<ReportReceiverEmailAddress>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.SiteId)			
			.DefineProperty(p => p.EmailAddress)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineProperty(p => p.Site)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<ReportReceiverEmailAddress>()		
			.HasOne(p => p.Site)			
			.WithMany(p => p.AdditionalSendReportsTo)			
			.WithConstraint(p => p.SiteId, p => p.Id);		
		
		builder.DefineEntity<ReportReceiverEmailAddress>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.ReportReceiverEmailAddressesCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<RiskAssessment>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.SiteInspectionId)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineProperty(p => p.CreatedByUser)			
			.DefineProperty(p => p.SiteInspection);		
		
		builder.DefineEntity<RiskAssessment>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.RiskAssessmentsCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<RiskAssessment>()		
			.HasOne(p => p.SiteInspection)			
			.WithOne(p => p.RiskAssessment)			
			.WithConstraint(p => p.Id, p => p.RiskAssessmentId);		
		
		builder.DefineEntity<RiskAssessmentAnswer>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.QuestionId)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.SpecificHazard)			
			.DefineProperty(p => p.PrecautionsToControlHazard)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineProperty(p => p.Question)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<RiskAssessmentAnswer>()		
			.HasOne(p => p.Question)			
			.WithMany(p => p.Answers)			
			.WithConstraint(p => p.QuestionId, p => p.Id);		
		
		builder.DefineEntity<RiskAssessmentAnswer>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.RiskAssessmentAnswersCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<RiskAssessmentQuestion>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Name)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineCollectionProperty(p => p.Answers, p => p.AnswersCount)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<RiskAssessmentQuestion>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.RiskAssessmentQuestionsCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<Scaffold>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.TypeId)			
			.DefineProperty(p => p.LoadingId)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Key)			
			.DefineProperty(p => p.Title)			
			.DefineProperty(p => p.Description)			
			.DefineProperty(p => p.Category)			
			.DefineProperty(p => p.ClientId)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineCollectionProperty(p => p.FaultReports, p => p.FaultReportsCount)			
			.DefineProperty(p => p.Client)			
			.DefineProperty(p => p.Type)			
			.DefineProperty(p => p.Loading)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<Scaffold>()		
			.HasOne(p => p.Client)			
			.WithMany(p => p.Scaffolds)			
			.WithConstraint(p => p.ClientId, p => p.Id);		
		
		builder.DefineEntity<Scaffold>()		
			.HasOne(p => p.Type)			
			.WithMany(p => p.Scaffolds)			
			.WithConstraint(p => p.TypeId, p => p.Id);		
		
		builder.DefineEntity<Scaffold>()		
			.HasOne(p => p.Loading)			
			.WithMany(p => p.Scaffolds)			
			.WithConstraint(p => p.LoadingId, p => p.Id);		
		
		builder.DefineEntity<Scaffold>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.ScaffoldsCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<ScaffoldInspection>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.SiteInspectionId)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.ScaffoldId)			
			.DefineProperty(p => p.InspectionStatus)			
			.DefineProperty(p => p.StartTime)			
			.DefineProperty(p => p.EndTime)			
			.DefineProperty(p => p.ReasonForFailure)			
			.DefineProperty(p => p.IsDesignRequired)			
			.DefineProperty(p => p.DrawingNumber)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineProperty(p => p.SiteInspection)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<ScaffoldInspection>()		
			.HasOne(p => p.SiteInspection)			
			.WithMany(p => p.ScaffoldInspections)			
			.WithConstraint(p => p.SiteInspectionId, p => p.Id);		
		
		builder.DefineEntity<ScaffoldInspection>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.ScaffoldInspectionsCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<ScaffoldLoading>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Name)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineCollectionProperty(p => p.Scaffolds, p => p.ScaffoldsCount)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<ScaffoldLoading>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.ScaffoldLoadingsCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<ScaffoldType>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Title)			
			.DefineProperty(p => p.Description)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineCollectionProperty(p => p.Scaffolds, p => p.ScaffoldsCount)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<ScaffoldType>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.ScaffoldTypesCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<Site>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.ParentId)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.Address)			
			.DefineProperty(p => p.PostCode)			
			.DefineProperty(p => p.ClientId)			
			.DefineProperty(p => p.Name)			
			.DefineProperty(p => p.Left)			
			.DefineProperty(p => p.Right)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineCollectionProperty(p => p.Documents, p => p.DocumentsCount)			
			.DefineCollectionProperty(p => p.AdditionalSendReportsTo, p => p.AdditionalSendReportsToCount)			
			.DefineProperty(p => p.Parent)			
			.DefineCollectionProperty(p => p.Children, p => p.ChildrenCount)			
			.DefineProperty(p => p.CreatedByUser)			
			.DefineProperty(p => p.Client)			
			.DefineCollectionProperty(p => p.SiteInspections, p => p.SiteInspectionsCount)			
			.DefineCollectionProperty(p => p.Users, p => p.UsersCount);		
		
		builder.DefineEntity<Site>()		
			.HasOne(p => p.Parent)			
			.WithMany(p => p.Children)			
			.WithConstraint(p => p.ParentId, p => p.Id);		
		
		builder.DefineEntity<Site>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.SitesCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<SiteInspection>()		
			.HasKey(p => p.Id)			
			.DefineProperty(p => p.Id)			
			.DefineProperty(p => p.RiskAssessmentId)			
			.DefineProperty(p => p.SiteId)			
			.DefineProperty(p => p.CreatedByUserId)			
			.DefineProperty(p => p.StartTime)			
			.DefineProperty(p => p.EndTime)			
			.DefineConvertedProperty(p => p.Guid, "Guid")			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Version)			
			.DefineConvertedProperty(p => p.PersistenceKey, "Guid")			
			.DefineCollectionProperty(p => p.ScaffoldInspections, p => p.ScaffoldInspectionsCount)			
			.DefineProperty(p => p.RiskAssessment)			
			.DefineProperty(p => p.Site)			
			.DefineProperty(p => p.CreatedByUser);		
		
		builder.DefineEntity<SiteInspection>()		
			.HasOne(p => p.RiskAssessment)			
			.WithOne(p => p.SiteInspection)			
			.WithConstraint(p => p.Id, p => p.SiteInspectionId);		
		
		builder.DefineEntity<SiteInspection>()		
			.HasOne(p => p.Site)			
			.WithMany(p => p.SiteInspections)			
			.WithConstraint(p => p.SiteId, p => p.Id);		
		
		builder.DefineEntity<SiteInspection>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.SiteInspectionsCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<UserSite>()		
			.HasCompositeKey(p => p.SiteId, p => p.UserId)			
			.DefineProperty(p => p.SiteId)			
			.DefineProperty(p => p.UserId)			
			.DefineProperty(p => p.User)			
			.DefineProperty(p => p.Site);		
		
		builder.DefineEntity<UserSite>()		
			.HasOne(p => p.User)			
			.WithMany(p => p.Sites)			
			.WithConstraint(p => p.UserId, p => p.Id);		
		
		builder.DefineEntity<UserSite>()		
			.HasOne(p => p.Site)			
			.WithMany(p => p.Users)			
			.WithConstraint(p => p.SiteId, p => p.Id);		
	}
	
	public DbSet<ApplicationUser, string> Users { get; set; }
	public DbSet<Client, int> Clients { get; set; }
	public DbSet<ClientType, int> ClientTypes { get; set; }
	public DbSet<DocumentCategory, int> DocumentCategories { get; set; }
	public DbSet<SiteDocument, int> SiteDocuments { get; set; }
	public DbSet<FaultActionsTaken, int> FaultActionsTaken { get; set; }
	public DbSet<FaultCategory, int> FaultCategories { get; set; }
	public DbSet<FaultDefaultRecommendation, int> FaultDefaultRecommendations { get; set; }
	public DbSet<FaultRecommendation, int> FaultRecommendations { get; set; }
	public DbSet<FaultReport, int> FaultReports { get; set; }
	public DbSet<FaultType, int> FaultTypes { get; set; }
	public DbSet<Project, int> Projects { get; set; }
	public DbSet<ReportReceiverEmailAddress, int> ReportReceiverEmailAddresses { get; set; }
	public DbSet<RiskAssessment, int> RiskAssessments { get; set; }
	public DbSet<RiskAssessmentAnswer, int> RiskAssessmentAnswers { get; set; }
	public DbSet<RiskAssessmentQuestion, int> RiskAssessmentQuestions { get; set; }
	public DbSet<Scaffold, int> Scaffolds { get; set; }
	public DbSet<ScaffoldInspection, int> ScaffoldInspections { get; set; }
	public DbSet<ScaffoldLoading, int> ScaffoldLoadings { get; set; }
	public DbSet<ScaffoldType, int> ScaffoldTypes { get; set; }
	public DbSet<Site, int> Sites { get; set; }
	public DbSet<SiteInspection, int> SiteInspections { get; set; }
	public DbSet<UserSite, CompositeKey> UserSites { get; set; }
}

