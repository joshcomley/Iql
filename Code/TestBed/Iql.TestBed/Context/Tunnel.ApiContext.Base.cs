using Iql.Queryable.Data.EntityConfiguration;
using Tunnel.Sets;
using Tunnel.ApiContext.Base;
using Tunnel.App.Data.Entities;
using Iql.OData;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.DataStores;
using System;
using System.Collections.Generic;
using Iql.OData.Methods;
namespace Tunnel.ApiContext.Base
{
	public class TunnelDataContextBase : DataContext
	{
		public TunnelDataContextBase(IDataStore dataStore) : base(dataStore)
		{
			this.Users = this.AsCustomDbSet<ApplicationUser, String, ApplicationUserSet>();
			
			this.Clients = this.AsCustomDbSet<Client, int, ClientSet>();
			
			this.ClientTypes = this.AsCustomDbSet<ClientType, int, ClientTypeSet>();
			
			this.DocumentCategories = this.AsCustomDbSet<DocumentCategory, int, DocumentCategorySet>();
			
			this.SiteDocuments = this.AsCustomDbSet<SiteDocument, int, SiteDocumentSet>();
			
			this.ReportActionsTaken = this.AsCustomDbSet<ReportActionsTaken, int, ReportActionsTakenSet>();
			
			this.ReportCategories = this.AsCustomDbSet<ReportCategory, int, ReportCategorySet>();
			
			this.ReportDefaultRecommendations = this.AsCustomDbSet<ReportDefaultRecommendation, int, ReportDefaultRecommendationSet>();
			
			this.ReportRecommendations = this.AsCustomDbSet<ReportRecommendation, int, ReportRecommendationSet>();
			
			this.ReportTypes = this.AsCustomDbSet<ReportType, int, ReportTypeSet>();
			
			this.Projects = this.AsCustomDbSet<Project, int, ProjectSet>();
			
			this.ReportReceiverEmailAddresses = this.AsCustomDbSet<ReportReceiverEmailAddress, int, ReportReceiverEmailAddressSet>();
			
			this.RiskAssessments = this.AsCustomDbSet<RiskAssessment, int, RiskAssessmentSet>();
			
			this.RiskAssessmentSolutions = this.AsCustomDbSet<RiskAssessmentSolution, int, RiskAssessmentSolutionSet>();
			
			this.RiskAssessmentAnswers = this.AsCustomDbSet<RiskAssessmentAnswer, int, RiskAssessmentAnswerSet>();
			
			this.RiskAssessmentQuestions = this.AsCustomDbSet<RiskAssessmentQuestion, int, RiskAssessmentQuestionSet>();
			
			this.People = this.AsCustomDbSet<Person, int, PersonSet>();
			
			this.PersonInspections = this.AsCustomDbSet<PersonInspection, int, PersonInspectionSet>();
			
			this.PersonLoadings = this.AsCustomDbSet<PersonLoading, int, PersonLoadingSet>();
			
			this.PersonTypes = this.AsCustomDbSet<PersonType, int, PersonTypeSet>();
			
			this.PersonTypesMap = this.AsCustomDbSet<PersonTypeMap, CompositeKey, PersonTypeMapSet>();
			
			this.PersonReports = this.AsCustomDbSet<PersonReport, int, PersonReportSet>();
			
			this.Sites = this.AsCustomDbSet<Site, int, SiteSet>();
			
			this.SiteInspections = this.AsCustomDbSet<SiteInspection, int, SiteInspectionSet>();
			
			this.UserSites = this.AsCustomDbSet<UserSite, CompositeKey, UserSiteSet>();
			
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

		public ODataConfiguration ODataConfiguration { get; set; }		 = new ODataConfiguration();
		
		public override void Configure(EntityConfigurationBuilder builder)
		{
			builder.EntityType<ApplicationUser>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.ClientId, true, )
				.DefineProperty(p => p.Email, true, )
				.DefineProperty(p => p.FullName, false, )
				.DefineProperty(p => p.EmailConfirmed, false, )
				.DefineProperty(p => p.UserType, false, )
				.DefineProperty(p => p.IsLockedOut, false, )
				.DefineProperty(p => p.Client, true, )
				.DefineCollectionProperty(p => p.ClientsCreated, p => p.ClientsCreatedCount, )
				.DefineCollectionProperty(p => p.DocumentCategoriesCreated, p => p.DocumentCategoriesCreatedCount, )
				.DefineCollectionProperty(p => p.SiteDocumentsCreated, p => p.SiteDocumentsCreatedCount, )
				.DefineCollectionProperty(p => p.FaultActionsTakenCreated, p => p.FaultActionsTakenCreatedCount, )
				.DefineCollectionProperty(p => p.FaultCategoriesCreated, p => p.FaultCategoriesCreatedCount, )
				.DefineCollectionProperty(p => p.FaultDefaultRecommendationsCreated, p => p.FaultDefaultRecommendationsCreatedCount, )
				.DefineCollectionProperty(p => p.FaultRecommendationsCreated, p => p.FaultRecommendationsCreatedCount, )
				.DefineCollectionProperty(p => p.FaultTypesCreated, p => p.FaultTypesCreatedCount, )
				.DefineCollectionProperty(p => p.ProjectCreated, p => p.ProjectCreatedCount, )
				.DefineCollectionProperty(p => p.ReportReceiverEmailAddressesCreated, p => p.ReportReceiverEmailAddressesCreatedCount, )
				.DefineCollectionProperty(p => p.RiskAssessmentsCreated, p => p.RiskAssessmentsCreatedCount, )
				.DefineCollectionProperty(p => p.RiskAssessmentAnswersCreated, p => p.RiskAssessmentAnswersCreatedCount, )
				.DefineCollectionProperty(p => p.RiskAssessmentQuestionsCreated, p => p.RiskAssessmentQuestionsCreatedCount, )
				.DefineCollectionProperty(p => p.PeopleCreated, p => p.PeopleCreatedCount, )
				.DefineCollectionProperty(p => p.PersonInspectionsCreated, p => p.PersonInspectionsCreatedCount, )
				.DefineCollectionProperty(p => p.PersonLoadingsCreated, p => p.PersonLoadingsCreatedCount, )
				.DefineCollectionProperty(p => p.PersonTypesCreated, p => p.PersonTypesCreatedCount, )
				.DefineCollectionProperty(p => p.FaultReportsCreated, p => p.FaultReportsCreatedCount, )
				.DefineCollectionProperty(p => p.SitesCreated, p => p.SitesCreatedCount, )
				.DefineCollectionProperty(p => p.SiteInspectionsCreated, p => p.SiteInspectionsCreatedCount, )
				.DefineCollectionProperty(p => p.Sites, p => p.SitesCount, );
			
			builder.EntityType<ApplicationUser>()
				.HasOne(p => p.Client)
				.WithMany(p => p.Users)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<Client>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.TypeId, false, )
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Name, false, )
				.DefineProperty(p => p.Description, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineCollectionProperty(p => p.Users, p => p.UsersCount, )
				.DefineProperty(p => p.Type, false, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefineCollectionProperty(p => p.People, p => p.PeopleCount, )
				.DefineCollectionProperty(p => p.Sites, p => p.SitesCount, );
			
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
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.Name, true, )
				.DefineCollectionProperty(p => p.Clients, p => p.ClientsCount, );
			
			builder.EntityType<DocumentCategory>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Name, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefineCollectionProperty(p => p.Documents, p => p.DocumentsCount, );
			
			builder.EntityType<DocumentCategory>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.DocumentCategoriesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<SiteDocument>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.CategoryId, false, )
				.DefineProperty(p => p.SiteId, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Title, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.Category, false, )
				.DefineProperty(p => p.Site, false, )
				.DefineProperty(p => p.CreatedByUser, true, );
			
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
				.DefineProperty(p => p.FaultReportId, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Notes, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.PersonReport, false, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefinePropertyValidation(p => p.Notes, entity => (entity.Notes == null ? null : entity.Notes.ToUpper()) != null || (entity.Notes == null ? null : entity.Notes.ToUpper()) != ("" == null ? null : "".ToUpper()), "31c7bee7-2978-40ed-8cee-638586691675", "Please enter some actions taken notes")
				.DefinePropertyValidation(p => p.Notes, entity => entity.Notes.Length > 5, "506ee24d-f42c-43b6-9f70-3efa5349c262", "Please enter at least five characters for notes");
			
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
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Name, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefineCollectionProperty(p => p.ReportTypes, p => p.ReportTypesCount, );
			
			builder.EntityType<ReportCategory>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultCategoriesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportDefaultRecommendation>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Name, true, )
				.DefineProperty(p => p.Text, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefineCollectionProperty(p => p.Recommendations, p => p.RecommendationsCount, );
			
			builder.EntityType<ReportDefaultRecommendation>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultDefaultRecommendationsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportRecommendation>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.ReportId, false, )
				.DefineProperty(p => p.RecommendationId, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Notes, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.PersonReport, false, )
				.DefineProperty(p => p.Recommendation, false, )
				.DefineProperty(p => p.CreatedByUser, true, );
			
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
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CategoryId, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.Name, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineProperty(p => p.Category, false, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefineCollectionProperty(p => p.FaultReports, p => p.FaultReportsCount, );
			
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
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.Title, false, )
				.DefineProperty(p => p.Description, true, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.CreatedByUser, true, );
			
			builder.EntityType<Project>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ProjectCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportReceiverEmailAddress>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.SiteId, false, )
				.DefineProperty(p => p.EmailAddress, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.Site, false, )
				.DefineProperty(p => p.CreatedByUser, true, );
			
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
				.DefineProperty(p => p.SiteInspectionId, false, )
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.SiteInspection, false, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefineProperty(p => p.RiskAssessmentSolution, false, );
			
			builder.EntityType<RiskAssessment>()
				.HasOne(p => p.SiteInspection)
				.WithOne(p => p.RiskAssessment)
				.WithConstraint(p => p.SiteInspectionId, p => p.Id);
			
			builder.EntityType<RiskAssessment>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.RiskAssessmentsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<RiskAssessmentSolution>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.RiskAssessmentId, false, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.RiskAssessment, false, )
				.DefineProperty(p => p.CreatedByUser, true, );
			
			builder.EntityType<RiskAssessmentSolution>()
				.HasOne(p => p.RiskAssessment)
				.WithOne(p => p.RiskAssessmentSolution)
				.WithConstraint(p => p.RiskAssessmentId, p => p.Id);
			
			builder.EntityType<RiskAssessmentAnswer>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.QuestionId, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.SpecificHazard, true, )
				.DefineProperty(p => p.PrecautionsToControlHazard, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.Question, false, )
				.DefineProperty(p => p.CreatedByUser, true, );
			
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
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Name, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineCollectionProperty(p => p.Answers, p => p.AnswersCount, )
				.DefineProperty(p => p.CreatedByUser, true, );
			
			builder.EntityType<RiskAssessmentQuestion>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.RiskAssessmentQuestionsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<Person>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.TypeId, true, )
				.DefineProperty(p => p.LoadingId, true, )
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Key, true, )
				.DefineProperty(p => p.Title, true, )
				.DefineProperty(p => p.Description, true, )
				.DefineProperty(p => p.Category, false, )
				.DefineProperty(p => p.ClientId, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.Client, true, )
				.DefineProperty(p => p.Type, true, )
				.DefineProperty(p => p.Loading, true, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefineCollectionProperty(p => p.Types, p => p.TypesCount, )
				.DefineCollectionProperty(p => p.Reports, p => p.ReportsCount, )
				.DefineEntityValidation(entity => (entity.Title == null ? null : entity.Title.ToUpper()) == null || (entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()) && (entity.Description == null ? null : entity.Description.ToUpper()) == null || (entity.Description.Trim() == null ? null : entity.Description.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()), "NoTitleOrDescription", "Please enter either a title or a description")
				.DefineEntityValidation(entity => (entity.Title == null ? null : entity.Title.ToUpper()) == ("Josh" == null ? null : "Josh".ToUpper()) && (entity.Description == null ? null : entity.Description.ToUpper()) != ("Josh" == null ? null : "Josh".ToUpper()), "JoshCheck", "If the name is 'Josh' please match it in the description")
				.DefineDisplayFormatter(entity => entity.Title, "Default")
				.DefineDisplayFormatter(entity => entity.Title + " (" + entity.Id + ")", "Report")
				.DefinePropertyValidation(p => p.Title, entity => (entity.Title == null ? null : entity.Title.ToUpper()) == null || (entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()), "EmptyTitle", "Please enter a person title")
				.DefinePropertyValidation(p => p.Title, entity => !((entity.Title == null ? null : entity.Title.ToUpper()) == null || (entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper())) && entity.Title.Trim().Length > 50, "TitleMaxLength", "Please enter less than fifty characters")
				.DefinePropertyValidation(p => p.Title, entity => !((entity.Title == null ? null : entity.Title.ToUpper()) == null || (entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper())) && entity.Title.Trim().Length < 3, "TitleMinLength", "Please enter at least three characters for the person's title")
				.DefinePropertyValidation(p => p.Description, entity => (entity.Description == null ? null : entity.Description.ToUpper()) == null || (entity.Description.Trim() == null ? null : entity.Description.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()), "EmptyDescription", "Please enter a person description");
			
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
				.DefineProperty(p => p.SiteInspectionId, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.PersonId, false, )
				.DefineProperty(p => p.InspectionStatus, false, )
				.DefineProperty(p => p.StartTime, false, )
				.DefineProperty(p => p.EndTime, false, )
				.DefineProperty(p => p.ReasonForFailure, false, )
				.DefineProperty(p => p.IsDesignRequired, false, )
				.DefineProperty(p => p.DrawingNumber, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.SiteInspection, false, )
				.DefineProperty(p => p.CreatedByUser, true, );
			
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
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Name, true, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineCollectionProperty(p => p.People, p => p.PeopleCount, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefinePropertyValidation(p => p.Name, entity => (entity.Name == null ? null : entity.Name.ToUpper()) != null && (entity.Name == null ? null : entity.Name.ToUpper()) != ("" == null ? null : "".ToUpper()), "3f047143-92b7-47f3-9b22-de4eff331e54", "Please enter a loading name");
			
			builder.EntityType<PersonLoading>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.PersonLoadingsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<PersonType>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Title, false, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineCollectionProperty(p => p.People, p => p.PeopleCount, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefineCollectionProperty(p => p.PeopleMap, p => p.PeopleMapCount, );
			
			builder.EntityType<PersonType>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.PersonTypesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<PersonTypeMap>()
				.HasCompositeKey(p => p.PersonId, p => p.TypeId)
				.DefineProperty(p => p.PersonId, false, )
				.DefineProperty(p => p.TypeId, false, )
				.DefineProperty(p => p.Notes, false, )
				.DefineProperty(p => p.Description, false, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Person, false, )
				.DefineProperty(p => p.Type, false, );
			
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
				.DefineProperty(p => p.PersonId, false, )
				.DefineProperty(p => p.TypeId, false, )
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Title, true, )
				.DefineProperty(p => p.Status, false, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineCollectionProperty(p => p.ActionsTaken, p => p.ActionsTakenCount, )
				.DefineCollectionProperty(p => p.Recommendations, p => p.RecommendationsCount, )
				.DefineProperty(p => p.Person, false, )
				.DefineProperty(p => p.Type, false, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefinePropertyValidation(p => p.Title, entity => (entity.Title == null ? null : entity.Title.ToUpper()) == null || (entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()), "0d0aea27-092c-4aa6-947d-e3e7859d73fa", "Please enter a valid report title")
				.DefinePropertyValidation(p => p.Title, entity => !((entity.Title == null ? null : entity.Title.ToUpper()) == null || (entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper())) && entity.Title.Trim().Length > 5, "92505f1d-496c-4e27-8c50-cd0b469010f3", "Please enter less than five characters");
			
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
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.ParentId, true, )
				.DefineProperty(p => p.ClientId, true, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.Address, true, )
				.DefineProperty(p => p.PostCode, true, )
				.DefineProperty(p => p.Name, true, )
				.DefineProperty(p => p.Left, false, )
				.DefineProperty(p => p.Right, false, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineCollectionProperty(p => p.Documents, p => p.DocumentsCount, )
				.DefineCollectionProperty(p => p.AdditionalSendReportsTo, p => p.AdditionalSendReportsToCount, )
				.DefineProperty(p => p.Parent, true, )
				.DefineCollectionProperty(p => p.Children, p => p.ChildrenCount, )
				.DefineProperty(p => p.Client, true, )
				.DefineProperty(p => p.CreatedByUser, true, )
				.DefineCollectionProperty(p => p.SiteInspections, p => p.SiteInspectionsCount, )
				.DefineCollectionProperty(p => p.Users, p => p.UsersCount, );
			
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
				.DefineProperty(p => p.Id, false, )
				.DefineProperty(p => p.SiteId, false, )
				.DefineProperty(p => p.CreatedByUserId, true, )
				.DefineProperty(p => p.StartTime, false, )
				.DefineProperty(p => p.EndTime, false, )
				.DefineConvertedProperty(p => p.Guid, "Guid", false, )
				.DefineProperty(p => p.CreatedDate, false, )
				.DefineProperty(p => p.Version, false, )
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, )
				.DefineProperty(p => p.RiskAssessment, false, )
				.DefineCollectionProperty(p => p.PersonInspections, p => p.PersonInspectionsCount, )
				.DefineProperty(p => p.Site, false, )
				.DefineProperty(p => p.CreatedByUser, true, );
			
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
				.DefineProperty(p => p.SiteId, false, )
				.DefineProperty(p => p.UserId, true, )
				.DefineProperty(p => p.User, false, )
				.DefineProperty(p => p.Site, false, );
			
			builder.EntityType<UserSite>()
				.HasOne(p => p.User)
				.WithMany(p => p.Sites)
				.WithConstraint(p => p.UserId, p => p.Id);
			
			builder.EntityType<UserSite>()
				.HasOne(p => p.Site)
				.WithMany(p => p.Users)
				.WithConstraint(p => p.SiteId, p => p.Id);
		
		}
		
		
		public ApplicationUserSet Users { get; set; }
		
		public ClientSet Clients { get; set; }
		
		public ClientTypeSet ClientTypes { get; set; }
		
		public DocumentCategorySet DocumentCategories { get; set; }
		
		public SiteDocumentSet SiteDocuments { get; set; }
		
		public ReportActionsTakenSet ReportActionsTaken { get; set; }
		
		public ReportCategorySet ReportCategories { get; set; }
		
		public ReportDefaultRecommendationSet ReportDefaultRecommendations { get; set; }
		
		public ReportRecommendationSet ReportRecommendations { get; set; }
		
		public ReportTypeSet ReportTypes { get; set; }
		
		public ProjectSet Projects { get; set; }
		
		public ReportReceiverEmailAddressSet ReportReceiverEmailAddresses { get; set; }
		
		public RiskAssessmentSet RiskAssessments { get; set; }
		
		public RiskAssessmentSolutionSet RiskAssessmentSolutions { get; set; }
		
		public RiskAssessmentAnswerSet RiskAssessmentAnswers { get; set; }
		
		public RiskAssessmentQuestionSet RiskAssessmentQuestions { get; set; }
		
		public PersonSet People { get; set; }
		
		public PersonInspectionSet PersonInspections { get; set; }
		
		public PersonLoadingSet PersonLoadings { get; set; }
		
		public PersonTypeSet PersonTypes { get; set; }
		
		public PersonTypeMapSet PersonTypesMap { get; set; }
		
		public PersonReportSet PersonReports { get; set; }
		
		public SiteSet Sites { get; set; }
		
		public SiteInspectionSet SiteInspections { get; set; }
		
		public UserSiteSet UserSites { get; set; }
		public virtual ODataDataMethodRequest<string> SendHi(string name)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(name, typeof(string), "name", false));
			return ((ODataDataStore)this.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Global,
				"Tunnel",
				"SendHi",
				null,
				typeof(String));
		}
	
	}
}

