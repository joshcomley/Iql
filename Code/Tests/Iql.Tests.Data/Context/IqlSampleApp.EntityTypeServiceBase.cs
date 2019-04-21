using Iql.Entities;
using IqlSampleApp.Sets;
using IqlSampleApp.ApiContext.Base;
using IqlSampleApp.Data.Entities;
using System;
namespace IqlSampleApp.ApiContext.Base
{
    public class IqlSampleAppEntityTypeServiceBase: EntityTypeService
    {
        public IqlSampleAppEntityTypeServiceBase(IEntityConfigurationBuilder builder) : base(builder)
        {}
        public Type TypeOf_ApplicationUser => typeof(ApplicationUser);
        private EntityConfiguration<ApplicationUser>_configurationFor_ApplicationUser = null;
        public EntityConfiguration<ApplicationUser>ConfigurationFor_ApplicationUser => _configurationFor_ApplicationUser = _configurationFor_ApplicationUser ?? Builder.EntityType<ApplicationUser>();
        private IProperty _ApplicationUser_IsLockedOut = null;
        public IProperty ApplicationUser_IsLockedOut
        {
            get
            {
                return _ApplicationUser_IsLockedOut = _ApplicationUser_IsLockedOut ?? FindPropertyByName<ApplicationUser>("IsLockedOut");
            }
        }
        private IProperty _ApplicationUser_ClientId = null;
        public IProperty ApplicationUser_ClientId
        {
            get
            {
                return _ApplicationUser_ClientId = _ApplicationUser_ClientId ?? FindPropertyByName<ApplicationUser>("ClientId");
            }
        }
        private IProperty _ApplicationUser_Id = null;
        public IProperty ApplicationUser_Id
        {
            get
            {
                return _ApplicationUser_Id = _ApplicationUser_Id ?? FindPropertyByName<ApplicationUser>("Id");
            }
        }
        private IProperty _ApplicationUser_Email = null;
        public IProperty ApplicationUser_Email
        {
            get
            {
                return _ApplicationUser_Email = _ApplicationUser_Email ?? FindPropertyByName<ApplicationUser>("Email");
            }
        }
        private IProperty _ApplicationUser_Permissions = null;
        public IProperty ApplicationUser_Permissions
        {
            get
            {
                return _ApplicationUser_Permissions = _ApplicationUser_Permissions ?? FindPropertyByName<ApplicationUser>("Permissions");
            }
        }
        private IProperty _ApplicationUser_UserType = null;
        public IProperty ApplicationUser_UserType
        {
            get
            {
                return _ApplicationUser_UserType = _ApplicationUser_UserType ?? FindPropertyByName<ApplicationUser>("UserType");
            }
        }
        private IProperty _ApplicationUser_FullName = null;
        public IProperty ApplicationUser_FullName
        {
            get
            {
                return _ApplicationUser_FullName = _ApplicationUser_FullName ?? FindPropertyByName<ApplicationUser>("FullName");
            }
        }
        private IProperty _ApplicationUser_CreatedByUserId = null;
        public IProperty ApplicationUser_CreatedByUserId
        {
            get
            {
                return _ApplicationUser_CreatedByUserId = _ApplicationUser_CreatedByUserId ?? FindPropertyByName<ApplicationUser>("CreatedByUserId");
            }
        }
        private IProperty _ApplicationUser_UserName = null;
        public IProperty ApplicationUser_UserName
        {
            get
            {
                return _ApplicationUser_UserName = _ApplicationUser_UserName ?? FindPropertyByName<ApplicationUser>("UserName");
            }
        }
        private IProperty _ApplicationUser_EmailConfirmed = null;
        public IProperty ApplicationUser_EmailConfirmed
        {
            get
            {
                return _ApplicationUser_EmailConfirmed = _ApplicationUser_EmailConfirmed ?? FindPropertyByName<ApplicationUser>("EmailConfirmed");
            }
        }
        private IProperty _ApplicationUser_PhoneNumber = null;
        public IProperty ApplicationUser_PhoneNumber
        {
            get
            {
                return _ApplicationUser_PhoneNumber = _ApplicationUser_PhoneNumber ?? FindPropertyByName<ApplicationUser>("PhoneNumber");
            }
        }
        private IProperty _ApplicationUser_PhoneNumberConfirmed = null;
        public IProperty ApplicationUser_PhoneNumberConfirmed
        {
            get
            {
                return _ApplicationUser_PhoneNumberConfirmed = _ApplicationUser_PhoneNumberConfirmed ?? FindPropertyByName<ApplicationUser>("PhoneNumberConfirmed");
            }
        }
        private IProperty _ApplicationUser_TwoFactorEnabled = null;
        public IProperty ApplicationUser_TwoFactorEnabled
        {
            get
            {
                return _ApplicationUser_TwoFactorEnabled = _ApplicationUser_TwoFactorEnabled ?? FindPropertyByName<ApplicationUser>("TwoFactorEnabled");
            }
        }
        private IProperty _ApplicationUser_LockoutEnd = null;
        public IProperty ApplicationUser_LockoutEnd
        {
            get
            {
                return _ApplicationUser_LockoutEnd = _ApplicationUser_LockoutEnd ?? FindPropertyByName<ApplicationUser>("LockoutEnd");
            }
        }
        private IProperty _ApplicationUser_LockoutEnabled = null;
        public IProperty ApplicationUser_LockoutEnabled
        {
            get
            {
                return _ApplicationUser_LockoutEnabled = _ApplicationUser_LockoutEnabled ?? FindPropertyByName<ApplicationUser>("LockoutEnabled");
            }
        }
        private IProperty _ApplicationUser_Client = null;
        public IProperty ApplicationUser_Client
        {
            get
            {
                return _ApplicationUser_Client = _ApplicationUser_Client ?? FindPropertyByName<ApplicationUser>("Client");
            }
        }
        private IProperty _ApplicationUser_CreatedByUser = null;
        public IProperty ApplicationUser_CreatedByUser
        {
            get
            {
                return _ApplicationUser_CreatedByUser = _ApplicationUser_CreatedByUser ?? FindPropertyByName<ApplicationUser>("CreatedByUser");
            }
        }
        private IProperty _ApplicationUser_ClientsCreated = null;
        public IProperty ApplicationUser_ClientsCreated
        {
            get
            {
                return _ApplicationUser_ClientsCreated = _ApplicationUser_ClientsCreated ?? FindPropertyByName<ApplicationUser>("ClientsCreated");
            }
        }
        private IProperty _ApplicationUser_DocumentCategoriesCreated = null;
        public IProperty ApplicationUser_DocumentCategoriesCreated
        {
            get
            {
                return _ApplicationUser_DocumentCategoriesCreated = _ApplicationUser_DocumentCategoriesCreated ?? FindPropertyByName<ApplicationUser>("DocumentCategoriesCreated");
            }
        }
        private IProperty _ApplicationUser_SiteDocumentsCreated = null;
        public IProperty ApplicationUser_SiteDocumentsCreated
        {
            get
            {
                return _ApplicationUser_SiteDocumentsCreated = _ApplicationUser_SiteDocumentsCreated ?? FindPropertyByName<ApplicationUser>("SiteDocumentsCreated");
            }
        }
        private IProperty _ApplicationUser_FaultActionsTakenCreated = null;
        public IProperty ApplicationUser_FaultActionsTakenCreated
        {
            get
            {
                return _ApplicationUser_FaultActionsTakenCreated = _ApplicationUser_FaultActionsTakenCreated ?? FindPropertyByName<ApplicationUser>("FaultActionsTakenCreated");
            }
        }
        private IProperty _ApplicationUser_FaultCategoriesCreated = null;
        public IProperty ApplicationUser_FaultCategoriesCreated
        {
            get
            {
                return _ApplicationUser_FaultCategoriesCreated = _ApplicationUser_FaultCategoriesCreated ?? FindPropertyByName<ApplicationUser>("FaultCategoriesCreated");
            }
        }
        private IProperty _ApplicationUser_FaultDefaultRecommendationsCreated = null;
        public IProperty ApplicationUser_FaultDefaultRecommendationsCreated
        {
            get
            {
                return _ApplicationUser_FaultDefaultRecommendationsCreated = _ApplicationUser_FaultDefaultRecommendationsCreated ?? FindPropertyByName<ApplicationUser>("FaultDefaultRecommendationsCreated");
            }
        }
        private IProperty _ApplicationUser_FaultRecommendationsCreated = null;
        public IProperty ApplicationUser_FaultRecommendationsCreated
        {
            get
            {
                return _ApplicationUser_FaultRecommendationsCreated = _ApplicationUser_FaultRecommendationsCreated ?? FindPropertyByName<ApplicationUser>("FaultRecommendationsCreated");
            }
        }
        private IProperty _ApplicationUser_FaultTypesCreated = null;
        public IProperty ApplicationUser_FaultTypesCreated
        {
            get
            {
                return _ApplicationUser_FaultTypesCreated = _ApplicationUser_FaultTypesCreated ?? FindPropertyByName<ApplicationUser>("FaultTypesCreated");
            }
        }
        private IProperty _ApplicationUser_ProjectCreated = null;
        public IProperty ApplicationUser_ProjectCreated
        {
            get
            {
                return _ApplicationUser_ProjectCreated = _ApplicationUser_ProjectCreated ?? FindPropertyByName<ApplicationUser>("ProjectCreated");
            }
        }
        private IProperty _ApplicationUser_ReportReceiverEmailAddressesCreated = null;
        public IProperty ApplicationUser_ReportReceiverEmailAddressesCreated
        {
            get
            {
                return _ApplicationUser_ReportReceiverEmailAddressesCreated = _ApplicationUser_ReportReceiverEmailAddressesCreated ?? FindPropertyByName<ApplicationUser>("ReportReceiverEmailAddressesCreated");
            }
        }
        private IProperty _ApplicationUser_RiskAssessmentsCreated = null;
        public IProperty ApplicationUser_RiskAssessmentsCreated
        {
            get
            {
                return _ApplicationUser_RiskAssessmentsCreated = _ApplicationUser_RiskAssessmentsCreated ?? FindPropertyByName<ApplicationUser>("RiskAssessmentsCreated");
            }
        }
        private IProperty _ApplicationUser_RiskAssessmentSolutionsCreated = null;
        public IProperty ApplicationUser_RiskAssessmentSolutionsCreated
        {
            get
            {
                return _ApplicationUser_RiskAssessmentSolutionsCreated = _ApplicationUser_RiskAssessmentSolutionsCreated ?? FindPropertyByName<ApplicationUser>("RiskAssessmentSolutionsCreated");
            }
        }
        private IProperty _ApplicationUser_RiskAssessmentAnswersCreated = null;
        public IProperty ApplicationUser_RiskAssessmentAnswersCreated
        {
            get
            {
                return _ApplicationUser_RiskAssessmentAnswersCreated = _ApplicationUser_RiskAssessmentAnswersCreated ?? FindPropertyByName<ApplicationUser>("RiskAssessmentAnswersCreated");
            }
        }
        private IProperty _ApplicationUser_RiskAssessmentQuestionsCreated = null;
        public IProperty ApplicationUser_RiskAssessmentQuestionsCreated
        {
            get
            {
                return _ApplicationUser_RiskAssessmentQuestionsCreated = _ApplicationUser_RiskAssessmentQuestionsCreated ?? FindPropertyByName<ApplicationUser>("RiskAssessmentQuestionsCreated");
            }
        }
        private IProperty _ApplicationUser_PeopleCreated = null;
        public IProperty ApplicationUser_PeopleCreated
        {
            get
            {
                return _ApplicationUser_PeopleCreated = _ApplicationUser_PeopleCreated ?? FindPropertyByName<ApplicationUser>("PeopleCreated");
            }
        }
        private IProperty _ApplicationUser_PersonInspectionsCreated = null;
        public IProperty ApplicationUser_PersonInspectionsCreated
        {
            get
            {
                return _ApplicationUser_PersonInspectionsCreated = _ApplicationUser_PersonInspectionsCreated ?? FindPropertyByName<ApplicationUser>("PersonInspectionsCreated");
            }
        }
        private IProperty _ApplicationUser_PersonLoadingsCreated = null;
        public IProperty ApplicationUser_PersonLoadingsCreated
        {
            get
            {
                return _ApplicationUser_PersonLoadingsCreated = _ApplicationUser_PersonLoadingsCreated ?? FindPropertyByName<ApplicationUser>("PersonLoadingsCreated");
            }
        }
        private IProperty _ApplicationUser_PersonTypesCreated = null;
        public IProperty ApplicationUser_PersonTypesCreated
        {
            get
            {
                return _ApplicationUser_PersonTypesCreated = _ApplicationUser_PersonTypesCreated ?? FindPropertyByName<ApplicationUser>("PersonTypesCreated");
            }
        }
        private IProperty _ApplicationUser_FaultReportsCreated = null;
        public IProperty ApplicationUser_FaultReportsCreated
        {
            get
            {
                return _ApplicationUser_FaultReportsCreated = _ApplicationUser_FaultReportsCreated ?? FindPropertyByName<ApplicationUser>("FaultReportsCreated");
            }
        }
        private IProperty _ApplicationUser_SitesCreated = null;
        public IProperty ApplicationUser_SitesCreated
        {
            get
            {
                return _ApplicationUser_SitesCreated = _ApplicationUser_SitesCreated ?? FindPropertyByName<ApplicationUser>("SitesCreated");
            }
        }
        private IProperty _ApplicationUser_SiteAreasCreated = null;
        public IProperty ApplicationUser_SiteAreasCreated
        {
            get
            {
                return _ApplicationUser_SiteAreasCreated = _ApplicationUser_SiteAreasCreated ?? FindPropertyByName<ApplicationUser>("SiteAreasCreated");
            }
        }
        private IProperty _ApplicationUser_SiteInspectionsCreated = null;
        public IProperty ApplicationUser_SiteInspectionsCreated
        {
            get
            {
                return _ApplicationUser_SiteInspectionsCreated = _ApplicationUser_SiteInspectionsCreated ?? FindPropertyByName<ApplicationUser>("SiteInspectionsCreated");
            }
        }
        private IProperty _ApplicationUser_UserSettingsCreated = null;
        public IProperty ApplicationUser_UserSettingsCreated
        {
            get
            {
                return _ApplicationUser_UserSettingsCreated = _ApplicationUser_UserSettingsCreated ?? FindPropertyByName<ApplicationUser>("UserSettingsCreated");
            }
        }
        private IProperty _ApplicationUser_UserSettings = null;
        public IProperty ApplicationUser_UserSettings
        {
            get
            {
                return _ApplicationUser_UserSettings = _ApplicationUser_UserSettings ?? FindPropertyByName<ApplicationUser>("UserSettings");
            }
        }
        private IProperty _ApplicationUser_Sites = null;
        public IProperty ApplicationUser_Sites
        {
            get
            {
                return _ApplicationUser_Sites = _ApplicationUser_Sites ?? FindPropertyByName<ApplicationUser>("Sites");
            }
        }
        public Type TypeOf_ApplicationLog => typeof(ApplicationLog);
        private EntityConfiguration<ApplicationLog>_configurationFor_ApplicationLog = null;
        public EntityConfiguration<ApplicationLog>ConfigurationFor_ApplicationLog => _configurationFor_ApplicationLog = _configurationFor_ApplicationLog ?? Builder.EntityType<ApplicationLog>();
        private IProperty _ApplicationLog_Id = null;
        public IProperty ApplicationLog_Id
        {
            get
            {
                return _ApplicationLog_Id = _ApplicationLog_Id ?? FindPropertyByName<ApplicationLog>("Id");
            }
        }
        private IProperty _ApplicationLog_CreatedDate = null;
        public IProperty ApplicationLog_CreatedDate
        {
            get
            {
                return _ApplicationLog_CreatedDate = _ApplicationLog_CreatedDate ?? FindPropertyByName<ApplicationLog>("CreatedDate");
            }
        }
        private IProperty _ApplicationLog_Module = null;
        public IProperty ApplicationLog_Module
        {
            get
            {
                return _ApplicationLog_Module = _ApplicationLog_Module ?? FindPropertyByName<ApplicationLog>("Module");
            }
        }
        private IProperty _ApplicationLog_Message = null;
        public IProperty ApplicationLog_Message
        {
            get
            {
                return _ApplicationLog_Message = _ApplicationLog_Message ?? FindPropertyByName<ApplicationLog>("Message");
            }
        }
        private IProperty _ApplicationLog_Kind = null;
        public IProperty ApplicationLog_Kind
        {
            get
            {
                return _ApplicationLog_Kind = _ApplicationLog_Kind ?? FindPropertyByName<ApplicationLog>("Kind");
            }
        }
        public Type TypeOf_Client => typeof(Client);
        private EntityConfiguration<Client>_configurationFor_Client = null;
        public EntityConfiguration<Client>ConfigurationFor_Client => _configurationFor_Client = _configurationFor_Client ?? Builder.EntityType<Client>();
        private IProperty _Client_TypeId = null;
        public IProperty Client_TypeId
        {
            get
            {
                return _Client_TypeId = _Client_TypeId ?? FindPropertyByName<Client>("TypeId");
            }
        }
        private IProperty _Client_Id = null;
        public IProperty Client_Id
        {
            get
            {
                return _Client_Id = _Client_Id ?? FindPropertyByName<Client>("Id");
            }
        }
        private IProperty _Client_CreatedByUserId = null;
        public IProperty Client_CreatedByUserId
        {
            get
            {
                return _Client_CreatedByUserId = _Client_CreatedByUserId ?? FindPropertyByName<Client>("CreatedByUserId");
            }
        }
        private IProperty _Client_AverageSales = null;
        public IProperty Client_AverageSales
        {
            get
            {
                return _Client_AverageSales = _Client_AverageSales ?? FindPropertyByName<Client>("AverageSales");
            }
        }
        private IProperty _Client_AverageIncome = null;
        public IProperty Client_AverageIncome
        {
            get
            {
                return _Client_AverageIncome = _Client_AverageIncome ?? FindPropertyByName<Client>("AverageIncome");
            }
        }
        private IProperty _Client_Category = null;
        public IProperty Client_Category
        {
            get
            {
                return _Client_Category = _Client_Category ?? FindPropertyByName<Client>("Category");
            }
        }
        private IProperty _Client_Description = null;
        public IProperty Client_Description
        {
            get
            {
                return _Client_Description = _Client_Description ?? FindPropertyByName<Client>("Description");
            }
        }
        private IProperty _Client_Discount = null;
        public IProperty Client_Discount
        {
            get
            {
                return _Client_Discount = _Client_Discount ?? FindPropertyByName<Client>("Discount");
            }
        }
        private IProperty _Client_Name = null;
        public IProperty Client_Name
        {
            get
            {
                return _Client_Name = _Client_Name ?? FindPropertyByName<Client>("Name");
            }
        }
        private IProperty _Client_Guid = null;
        public IProperty Client_Guid
        {
            get
            {
                return _Client_Guid = _Client_Guid ?? FindPropertyByName<Client>("Guid");
            }
        }
        private IProperty _Client_CreatedDate = null;
        public IProperty Client_CreatedDate
        {
            get
            {
                return _Client_CreatedDate = _Client_CreatedDate ?? FindPropertyByName<Client>("CreatedDate");
            }
        }
        private IProperty _Client_RevisionKey = null;
        public IProperty Client_RevisionKey
        {
            get
            {
                return _Client_RevisionKey = _Client_RevisionKey ?? FindPropertyByName<Client>("RevisionKey");
            }
        }
        private IProperty _Client_PersistenceKey = null;
        public IProperty Client_PersistenceKey
        {
            get
            {
                return _Client_PersistenceKey = _Client_PersistenceKey ?? FindPropertyByName<Client>("PersistenceKey");
            }
        }
        private IProperty _Client_Users = null;
        public IProperty Client_Users
        {
            get
            {
                return _Client_Users = _Client_Users ?? FindPropertyByName<Client>("Users");
            }
        }
        private IProperty _Client_Type = null;
        public IProperty Client_Type
        {
            get
            {
                return _Client_Type = _Client_Type ?? FindPropertyByName<Client>("Type");
            }
        }
        private IProperty _Client_CreatedByUser = null;
        public IProperty Client_CreatedByUser
        {
            get
            {
                return _Client_CreatedByUser = _Client_CreatedByUser ?? FindPropertyByName<Client>("CreatedByUser");
            }
        }
        private IProperty _Client_People = null;
        public IProperty Client_People
        {
            get
            {
                return _Client_People = _Client_People ?? FindPropertyByName<Client>("People");
            }
        }
        private IProperty _Client_Sites = null;
        public IProperty Client_Sites
        {
            get
            {
                return _Client_Sites = _Client_Sites ?? FindPropertyByName<Client>("Sites");
            }
        }
        public Type TypeOf_ClientType => typeof(ClientType);
        private EntityConfiguration<ClientType>_configurationFor_ClientType = null;
        public EntityConfiguration<ClientType>ConfigurationFor_ClientType => _configurationFor_ClientType = _configurationFor_ClientType ?? Builder.EntityType<ClientType>();
        private IProperty _ClientType_Id = null;
        public IProperty ClientType_Id
        {
            get
            {
                return _ClientType_Id = _ClientType_Id ?? FindPropertyByName<ClientType>("Id");
            }
        }
        private IProperty _ClientType_Name = null;
        public IProperty ClientType_Name
        {
            get
            {
                return _ClientType_Name = _ClientType_Name ?? FindPropertyByName<ClientType>("Name");
            }
        }
        private IProperty _ClientType_Clients = null;
        public IProperty ClientType_Clients
        {
            get
            {
                return _ClientType_Clients = _ClientType_Clients ?? FindPropertyByName<ClientType>("Clients");
            }
        }
        public Type TypeOf_DocumentCategory => typeof(DocumentCategory);
        private EntityConfiguration<DocumentCategory>_configurationFor_DocumentCategory = null;
        public EntityConfiguration<DocumentCategory>ConfigurationFor_DocumentCategory => _configurationFor_DocumentCategory = _configurationFor_DocumentCategory ?? Builder.EntityType<DocumentCategory>();
        private IProperty _DocumentCategory_Id = null;
        public IProperty DocumentCategory_Id
        {
            get
            {
                return _DocumentCategory_Id = _DocumentCategory_Id ?? FindPropertyByName<DocumentCategory>("Id");
            }
        }
        private IProperty _DocumentCategory_CreatedByUserId = null;
        public IProperty DocumentCategory_CreatedByUserId
        {
            get
            {
                return _DocumentCategory_CreatedByUserId = _DocumentCategory_CreatedByUserId ?? FindPropertyByName<DocumentCategory>("CreatedByUserId");
            }
        }
        private IProperty _DocumentCategory_Name = null;
        public IProperty DocumentCategory_Name
        {
            get
            {
                return _DocumentCategory_Name = _DocumentCategory_Name ?? FindPropertyByName<DocumentCategory>("Name");
            }
        }
        private IProperty _DocumentCategory_Guid = null;
        public IProperty DocumentCategory_Guid
        {
            get
            {
                return _DocumentCategory_Guid = _DocumentCategory_Guid ?? FindPropertyByName<DocumentCategory>("Guid");
            }
        }
        private IProperty _DocumentCategory_CreatedDate = null;
        public IProperty DocumentCategory_CreatedDate
        {
            get
            {
                return _DocumentCategory_CreatedDate = _DocumentCategory_CreatedDate ?? FindPropertyByName<DocumentCategory>("CreatedDate");
            }
        }
        private IProperty _DocumentCategory_RevisionKey = null;
        public IProperty DocumentCategory_RevisionKey
        {
            get
            {
                return _DocumentCategory_RevisionKey = _DocumentCategory_RevisionKey ?? FindPropertyByName<DocumentCategory>("RevisionKey");
            }
        }
        private IProperty _DocumentCategory_PersistenceKey = null;
        public IProperty DocumentCategory_PersistenceKey
        {
            get
            {
                return _DocumentCategory_PersistenceKey = _DocumentCategory_PersistenceKey ?? FindPropertyByName<DocumentCategory>("PersistenceKey");
            }
        }
        private IProperty _DocumentCategory_CreatedByUser = null;
        public IProperty DocumentCategory_CreatedByUser
        {
            get
            {
                return _DocumentCategory_CreatedByUser = _DocumentCategory_CreatedByUser ?? FindPropertyByName<DocumentCategory>("CreatedByUser");
            }
        }
        private IProperty _DocumentCategory_Documents = null;
        public IProperty DocumentCategory_Documents
        {
            get
            {
                return _DocumentCategory_Documents = _DocumentCategory_Documents ?? FindPropertyByName<DocumentCategory>("Documents");
            }
        }
        public Type TypeOf_SiteDocument => typeof(SiteDocument);
        private EntityConfiguration<SiteDocument>_configurationFor_SiteDocument = null;
        public EntityConfiguration<SiteDocument>ConfigurationFor_SiteDocument => _configurationFor_SiteDocument = _configurationFor_SiteDocument ?? Builder.EntityType<SiteDocument>();
        private IProperty _SiteDocument_CategoryId = null;
        public IProperty SiteDocument_CategoryId
        {
            get
            {
                return _SiteDocument_CategoryId = _SiteDocument_CategoryId ?? FindPropertyByName<SiteDocument>("CategoryId");
            }
        }
        private IProperty _SiteDocument_SiteId = null;
        public IProperty SiteDocument_SiteId
        {
            get
            {
                return _SiteDocument_SiteId = _SiteDocument_SiteId ?? FindPropertyByName<SiteDocument>("SiteId");
            }
        }
        private IProperty _SiteDocument_CreatedByUserId = null;
        public IProperty SiteDocument_CreatedByUserId
        {
            get
            {
                return _SiteDocument_CreatedByUserId = _SiteDocument_CreatedByUserId ?? FindPropertyByName<SiteDocument>("CreatedByUserId");
            }
        }
        private IProperty _SiteDocument_Title = null;
        public IProperty SiteDocument_Title
        {
            get
            {
                return _SiteDocument_Title = _SiteDocument_Title ?? FindPropertyByName<SiteDocument>("Title");
            }
        }
        private IProperty _SiteDocument_Guid = null;
        public IProperty SiteDocument_Guid
        {
            get
            {
                return _SiteDocument_Guid = _SiteDocument_Guid ?? FindPropertyByName<SiteDocument>("Guid");
            }
        }
        private IProperty _SiteDocument_Id = null;
        public IProperty SiteDocument_Id
        {
            get
            {
                return _SiteDocument_Id = _SiteDocument_Id ?? FindPropertyByName<SiteDocument>("Id");
            }
        }
        private IProperty _SiteDocument_CreatedDate = null;
        public IProperty SiteDocument_CreatedDate
        {
            get
            {
                return _SiteDocument_CreatedDate = _SiteDocument_CreatedDate ?? FindPropertyByName<SiteDocument>("CreatedDate");
            }
        }
        private IProperty _SiteDocument_RevisionKey = null;
        public IProperty SiteDocument_RevisionKey
        {
            get
            {
                return _SiteDocument_RevisionKey = _SiteDocument_RevisionKey ?? FindPropertyByName<SiteDocument>("RevisionKey");
            }
        }
        private IProperty _SiteDocument_PersistenceKey = null;
        public IProperty SiteDocument_PersistenceKey
        {
            get
            {
                return _SiteDocument_PersistenceKey = _SiteDocument_PersistenceKey ?? FindPropertyByName<SiteDocument>("PersistenceKey");
            }
        }
        private IProperty _SiteDocument_Category = null;
        public IProperty SiteDocument_Category
        {
            get
            {
                return _SiteDocument_Category = _SiteDocument_Category ?? FindPropertyByName<SiteDocument>("Category");
            }
        }
        private IProperty _SiteDocument_Site = null;
        public IProperty SiteDocument_Site
        {
            get
            {
                return _SiteDocument_Site = _SiteDocument_Site ?? FindPropertyByName<SiteDocument>("Site");
            }
        }
        private IProperty _SiteDocument_CreatedByUser = null;
        public IProperty SiteDocument_CreatedByUser
        {
            get
            {
                return _SiteDocument_CreatedByUser = _SiteDocument_CreatedByUser ?? FindPropertyByName<SiteDocument>("CreatedByUser");
            }
        }
        public Type TypeOf_ReportActionsTaken => typeof(ReportActionsTaken);
        private EntityConfiguration<ReportActionsTaken>_configurationFor_ReportActionsTaken = null;
        public EntityConfiguration<ReportActionsTaken>ConfigurationFor_ReportActionsTaken => _configurationFor_ReportActionsTaken = _configurationFor_ReportActionsTaken ?? Builder.EntityType<ReportActionsTaken>();
        private IProperty _ReportActionsTaken_FaultReportId = null;
        public IProperty ReportActionsTaken_FaultReportId
        {
            get
            {
                return _ReportActionsTaken_FaultReportId = _ReportActionsTaken_FaultReportId ?? FindPropertyByName<ReportActionsTaken>("FaultReportId");
            }
        }
        private IProperty _ReportActionsTaken_CreatedByUserId = null;
        public IProperty ReportActionsTaken_CreatedByUserId
        {
            get
            {
                return _ReportActionsTaken_CreatedByUserId = _ReportActionsTaken_CreatedByUserId ?? FindPropertyByName<ReportActionsTaken>("CreatedByUserId");
            }
        }
        private IProperty _ReportActionsTaken_Notes = null;
        public IProperty ReportActionsTaken_Notes
        {
            get
            {
                return _ReportActionsTaken_Notes = _ReportActionsTaken_Notes ?? FindPropertyByName<ReportActionsTaken>("Notes");
            }
        }
        private IProperty _ReportActionsTaken_Guid = null;
        public IProperty ReportActionsTaken_Guid
        {
            get
            {
                return _ReportActionsTaken_Guid = _ReportActionsTaken_Guid ?? FindPropertyByName<ReportActionsTaken>("Guid");
            }
        }
        private IProperty _ReportActionsTaken_Id = null;
        public IProperty ReportActionsTaken_Id
        {
            get
            {
                return _ReportActionsTaken_Id = _ReportActionsTaken_Id ?? FindPropertyByName<ReportActionsTaken>("Id");
            }
        }
        private IProperty _ReportActionsTaken_CreatedDate = null;
        public IProperty ReportActionsTaken_CreatedDate
        {
            get
            {
                return _ReportActionsTaken_CreatedDate = _ReportActionsTaken_CreatedDate ?? FindPropertyByName<ReportActionsTaken>("CreatedDate");
            }
        }
        private IProperty _ReportActionsTaken_RevisionKey = null;
        public IProperty ReportActionsTaken_RevisionKey
        {
            get
            {
                return _ReportActionsTaken_RevisionKey = _ReportActionsTaken_RevisionKey ?? FindPropertyByName<ReportActionsTaken>("RevisionKey");
            }
        }
        private IProperty _ReportActionsTaken_PersistenceKey = null;
        public IProperty ReportActionsTaken_PersistenceKey
        {
            get
            {
                return _ReportActionsTaken_PersistenceKey = _ReportActionsTaken_PersistenceKey ?? FindPropertyByName<ReportActionsTaken>("PersistenceKey");
            }
        }
        private IProperty _ReportActionsTaken_PersonReport = null;
        public IProperty ReportActionsTaken_PersonReport
        {
            get
            {
                return _ReportActionsTaken_PersonReport = _ReportActionsTaken_PersonReport ?? FindPropertyByName<ReportActionsTaken>("PersonReport");
            }
        }
        private IProperty _ReportActionsTaken_CreatedByUser = null;
        public IProperty ReportActionsTaken_CreatedByUser
        {
            get
            {
                return _ReportActionsTaken_CreatedByUser = _ReportActionsTaken_CreatedByUser ?? FindPropertyByName<ReportActionsTaken>("CreatedByUser");
            }
        }
        public Type TypeOf_ReportCategory => typeof(ReportCategory);
        private EntityConfiguration<ReportCategory>_configurationFor_ReportCategory = null;
        public EntityConfiguration<ReportCategory>ConfigurationFor_ReportCategory => _configurationFor_ReportCategory = _configurationFor_ReportCategory ?? Builder.EntityType<ReportCategory>();
        private IProperty _ReportCategory_Id = null;
        public IProperty ReportCategory_Id
        {
            get
            {
                return _ReportCategory_Id = _ReportCategory_Id ?? FindPropertyByName<ReportCategory>("Id");
            }
        }
        private IProperty _ReportCategory_CreatedByUserId = null;
        public IProperty ReportCategory_CreatedByUserId
        {
            get
            {
                return _ReportCategory_CreatedByUserId = _ReportCategory_CreatedByUserId ?? FindPropertyByName<ReportCategory>("CreatedByUserId");
            }
        }
        private IProperty _ReportCategory_Name = null;
        public IProperty ReportCategory_Name
        {
            get
            {
                return _ReportCategory_Name = _ReportCategory_Name ?? FindPropertyByName<ReportCategory>("Name");
            }
        }
        private IProperty _ReportCategory_Guid = null;
        public IProperty ReportCategory_Guid
        {
            get
            {
                return _ReportCategory_Guid = _ReportCategory_Guid ?? FindPropertyByName<ReportCategory>("Guid");
            }
        }
        private IProperty _ReportCategory_CreatedDate = null;
        public IProperty ReportCategory_CreatedDate
        {
            get
            {
                return _ReportCategory_CreatedDate = _ReportCategory_CreatedDate ?? FindPropertyByName<ReportCategory>("CreatedDate");
            }
        }
        private IProperty _ReportCategory_RevisionKey = null;
        public IProperty ReportCategory_RevisionKey
        {
            get
            {
                return _ReportCategory_RevisionKey = _ReportCategory_RevisionKey ?? FindPropertyByName<ReportCategory>("RevisionKey");
            }
        }
        private IProperty _ReportCategory_PersistenceKey = null;
        public IProperty ReportCategory_PersistenceKey
        {
            get
            {
                return _ReportCategory_PersistenceKey = _ReportCategory_PersistenceKey ?? FindPropertyByName<ReportCategory>("PersistenceKey");
            }
        }
        private IProperty _ReportCategory_CreatedByUser = null;
        public IProperty ReportCategory_CreatedByUser
        {
            get
            {
                return _ReportCategory_CreatedByUser = _ReportCategory_CreatedByUser ?? FindPropertyByName<ReportCategory>("CreatedByUser");
            }
        }
        private IProperty _ReportCategory_ReportTypes = null;
        public IProperty ReportCategory_ReportTypes
        {
            get
            {
                return _ReportCategory_ReportTypes = _ReportCategory_ReportTypes ?? FindPropertyByName<ReportCategory>("ReportTypes");
            }
        }
        public Type TypeOf_ReportDefaultRecommendation => typeof(ReportDefaultRecommendation);
        private EntityConfiguration<ReportDefaultRecommendation>_configurationFor_ReportDefaultRecommendation = null;
        public EntityConfiguration<ReportDefaultRecommendation>ConfigurationFor_ReportDefaultRecommendation => _configurationFor_ReportDefaultRecommendation = _configurationFor_ReportDefaultRecommendation ?? Builder.EntityType<ReportDefaultRecommendation>();
        private IProperty _ReportDefaultRecommendation_Id = null;
        public IProperty ReportDefaultRecommendation_Id
        {
            get
            {
                return _ReportDefaultRecommendation_Id = _ReportDefaultRecommendation_Id ?? FindPropertyByName<ReportDefaultRecommendation>("Id");
            }
        }
        private IProperty _ReportDefaultRecommendation_CreatedByUserId = null;
        public IProperty ReportDefaultRecommendation_CreatedByUserId
        {
            get
            {
                return _ReportDefaultRecommendation_CreatedByUserId = _ReportDefaultRecommendation_CreatedByUserId ?? FindPropertyByName<ReportDefaultRecommendation>("CreatedByUserId");
            }
        }
        private IProperty _ReportDefaultRecommendation_Name = null;
        public IProperty ReportDefaultRecommendation_Name
        {
            get
            {
                return _ReportDefaultRecommendation_Name = _ReportDefaultRecommendation_Name ?? FindPropertyByName<ReportDefaultRecommendation>("Name");
            }
        }
        private IProperty _ReportDefaultRecommendation_Text = null;
        public IProperty ReportDefaultRecommendation_Text
        {
            get
            {
                return _ReportDefaultRecommendation_Text = _ReportDefaultRecommendation_Text ?? FindPropertyByName<ReportDefaultRecommendation>("Text");
            }
        }
        private IProperty _ReportDefaultRecommendation_Guid = null;
        public IProperty ReportDefaultRecommendation_Guid
        {
            get
            {
                return _ReportDefaultRecommendation_Guid = _ReportDefaultRecommendation_Guid ?? FindPropertyByName<ReportDefaultRecommendation>("Guid");
            }
        }
        private IProperty _ReportDefaultRecommendation_CreatedDate = null;
        public IProperty ReportDefaultRecommendation_CreatedDate
        {
            get
            {
                return _ReportDefaultRecommendation_CreatedDate = _ReportDefaultRecommendation_CreatedDate ?? FindPropertyByName<ReportDefaultRecommendation>("CreatedDate");
            }
        }
        private IProperty _ReportDefaultRecommendation_RevisionKey = null;
        public IProperty ReportDefaultRecommendation_RevisionKey
        {
            get
            {
                return _ReportDefaultRecommendation_RevisionKey = _ReportDefaultRecommendation_RevisionKey ?? FindPropertyByName<ReportDefaultRecommendation>("RevisionKey");
            }
        }
        private IProperty _ReportDefaultRecommendation_PersistenceKey = null;
        public IProperty ReportDefaultRecommendation_PersistenceKey
        {
            get
            {
                return _ReportDefaultRecommendation_PersistenceKey = _ReportDefaultRecommendation_PersistenceKey ?? FindPropertyByName<ReportDefaultRecommendation>("PersistenceKey");
            }
        }
        private IProperty _ReportDefaultRecommendation_CreatedByUser = null;
        public IProperty ReportDefaultRecommendation_CreatedByUser
        {
            get
            {
                return _ReportDefaultRecommendation_CreatedByUser = _ReportDefaultRecommendation_CreatedByUser ?? FindPropertyByName<ReportDefaultRecommendation>("CreatedByUser");
            }
        }
        private IProperty _ReportDefaultRecommendation_Recommendations = null;
        public IProperty ReportDefaultRecommendation_Recommendations
        {
            get
            {
                return _ReportDefaultRecommendation_Recommendations = _ReportDefaultRecommendation_Recommendations ?? FindPropertyByName<ReportDefaultRecommendation>("Recommendations");
            }
        }
        public Type TypeOf_ReportRecommendation => typeof(ReportRecommendation);
        private EntityConfiguration<ReportRecommendation>_configurationFor_ReportRecommendation = null;
        public EntityConfiguration<ReportRecommendation>ConfigurationFor_ReportRecommendation => _configurationFor_ReportRecommendation = _configurationFor_ReportRecommendation ?? Builder.EntityType<ReportRecommendation>();
        private IProperty _ReportRecommendation_ReportId = null;
        public IProperty ReportRecommendation_ReportId
        {
            get
            {
                return _ReportRecommendation_ReportId = _ReportRecommendation_ReportId ?? FindPropertyByName<ReportRecommendation>("ReportId");
            }
        }
        private IProperty _ReportRecommendation_RecommendationId = null;
        public IProperty ReportRecommendation_RecommendationId
        {
            get
            {
                return _ReportRecommendation_RecommendationId = _ReportRecommendation_RecommendationId ?? FindPropertyByName<ReportRecommendation>("RecommendationId");
            }
        }
        private IProperty _ReportRecommendation_CreatedByUserId = null;
        public IProperty ReportRecommendation_CreatedByUserId
        {
            get
            {
                return _ReportRecommendation_CreatedByUserId = _ReportRecommendation_CreatedByUserId ?? FindPropertyByName<ReportRecommendation>("CreatedByUserId");
            }
        }
        private IProperty _ReportRecommendation_Notes = null;
        public IProperty ReportRecommendation_Notes
        {
            get
            {
                return _ReportRecommendation_Notes = _ReportRecommendation_Notes ?? FindPropertyByName<ReportRecommendation>("Notes");
            }
        }
        private IProperty _ReportRecommendation_Guid = null;
        public IProperty ReportRecommendation_Guid
        {
            get
            {
                return _ReportRecommendation_Guid = _ReportRecommendation_Guid ?? FindPropertyByName<ReportRecommendation>("Guid");
            }
        }
        private IProperty _ReportRecommendation_Id = null;
        public IProperty ReportRecommendation_Id
        {
            get
            {
                return _ReportRecommendation_Id = _ReportRecommendation_Id ?? FindPropertyByName<ReportRecommendation>("Id");
            }
        }
        private IProperty _ReportRecommendation_CreatedDate = null;
        public IProperty ReportRecommendation_CreatedDate
        {
            get
            {
                return _ReportRecommendation_CreatedDate = _ReportRecommendation_CreatedDate ?? FindPropertyByName<ReportRecommendation>("CreatedDate");
            }
        }
        private IProperty _ReportRecommendation_RevisionKey = null;
        public IProperty ReportRecommendation_RevisionKey
        {
            get
            {
                return _ReportRecommendation_RevisionKey = _ReportRecommendation_RevisionKey ?? FindPropertyByName<ReportRecommendation>("RevisionKey");
            }
        }
        private IProperty _ReportRecommendation_PersistenceKey = null;
        public IProperty ReportRecommendation_PersistenceKey
        {
            get
            {
                return _ReportRecommendation_PersistenceKey = _ReportRecommendation_PersistenceKey ?? FindPropertyByName<ReportRecommendation>("PersistenceKey");
            }
        }
        private IProperty _ReportRecommendation_PersonReport = null;
        public IProperty ReportRecommendation_PersonReport
        {
            get
            {
                return _ReportRecommendation_PersonReport = _ReportRecommendation_PersonReport ?? FindPropertyByName<ReportRecommendation>("PersonReport");
            }
        }
        private IProperty _ReportRecommendation_Recommendation = null;
        public IProperty ReportRecommendation_Recommendation
        {
            get
            {
                return _ReportRecommendation_Recommendation = _ReportRecommendation_Recommendation ?? FindPropertyByName<ReportRecommendation>("Recommendation");
            }
        }
        private IProperty _ReportRecommendation_CreatedByUser = null;
        public IProperty ReportRecommendation_CreatedByUser
        {
            get
            {
                return _ReportRecommendation_CreatedByUser = _ReportRecommendation_CreatedByUser ?? FindPropertyByName<ReportRecommendation>("CreatedByUser");
            }
        }
        public Type TypeOf_ReportType => typeof(ReportType);
        private EntityConfiguration<ReportType>_configurationFor_ReportType = null;
        public EntityConfiguration<ReportType>ConfigurationFor_ReportType => _configurationFor_ReportType = _configurationFor_ReportType ?? Builder.EntityType<ReportType>();
        private IProperty _ReportType_Id = null;
        public IProperty ReportType_Id
        {
            get
            {
                return _ReportType_Id = _ReportType_Id ?? FindPropertyByName<ReportType>("Id");
            }
        }
        private IProperty _ReportType_CategoryId = null;
        public IProperty ReportType_CategoryId
        {
            get
            {
                return _ReportType_CategoryId = _ReportType_CategoryId ?? FindPropertyByName<ReportType>("CategoryId");
            }
        }
        private IProperty _ReportType_CreatedByUserId = null;
        public IProperty ReportType_CreatedByUserId
        {
            get
            {
                return _ReportType_CreatedByUserId = _ReportType_CreatedByUserId ?? FindPropertyByName<ReportType>("CreatedByUserId");
            }
        }
        private IProperty _ReportType_Name = null;
        public IProperty ReportType_Name
        {
            get
            {
                return _ReportType_Name = _ReportType_Name ?? FindPropertyByName<ReportType>("Name");
            }
        }
        private IProperty _ReportType_Guid = null;
        public IProperty ReportType_Guid
        {
            get
            {
                return _ReportType_Guid = _ReportType_Guid ?? FindPropertyByName<ReportType>("Guid");
            }
        }
        private IProperty _ReportType_CreatedDate = null;
        public IProperty ReportType_CreatedDate
        {
            get
            {
                return _ReportType_CreatedDate = _ReportType_CreatedDate ?? FindPropertyByName<ReportType>("CreatedDate");
            }
        }
        private IProperty _ReportType_RevisionKey = null;
        public IProperty ReportType_RevisionKey
        {
            get
            {
                return _ReportType_RevisionKey = _ReportType_RevisionKey ?? FindPropertyByName<ReportType>("RevisionKey");
            }
        }
        private IProperty _ReportType_PersistenceKey = null;
        public IProperty ReportType_PersistenceKey
        {
            get
            {
                return _ReportType_PersistenceKey = _ReportType_PersistenceKey ?? FindPropertyByName<ReportType>("PersistenceKey");
            }
        }
        private IProperty _ReportType_Category = null;
        public IProperty ReportType_Category
        {
            get
            {
                return _ReportType_Category = _ReportType_Category ?? FindPropertyByName<ReportType>("Category");
            }
        }
        private IProperty _ReportType_CreatedByUser = null;
        public IProperty ReportType_CreatedByUser
        {
            get
            {
                return _ReportType_CreatedByUser = _ReportType_CreatedByUser ?? FindPropertyByName<ReportType>("CreatedByUser");
            }
        }
        private IProperty _ReportType_FaultReports = null;
        public IProperty ReportType_FaultReports
        {
            get
            {
                return _ReportType_FaultReports = _ReportType_FaultReports ?? FindPropertyByName<ReportType>("FaultReports");
            }
        }
        public Type TypeOf_Project => typeof(Project);
        private EntityConfiguration<Project>_configurationFor_Project = null;
        public EntityConfiguration<Project>ConfigurationFor_Project => _configurationFor_Project = _configurationFor_Project ?? Builder.EntityType<Project>();
        private IProperty _Project_CreatedByUserId = null;
        public IProperty Project_CreatedByUserId
        {
            get
            {
                return _Project_CreatedByUserId = _Project_CreatedByUserId ?? FindPropertyByName<Project>("CreatedByUserId");
            }
        }
        private IProperty _Project_Title = null;
        public IProperty Project_Title
        {
            get
            {
                return _Project_Title = _Project_Title ?? FindPropertyByName<Project>("Title");
            }
        }
        private IProperty _Project_Description = null;
        public IProperty Project_Description
        {
            get
            {
                return _Project_Description = _Project_Description ?? FindPropertyByName<Project>("Description");
            }
        }
        private IProperty _Project_Guid = null;
        public IProperty Project_Guid
        {
            get
            {
                return _Project_Guid = _Project_Guid ?? FindPropertyByName<Project>("Guid");
            }
        }
        private IProperty _Project_Id = null;
        public IProperty Project_Id
        {
            get
            {
                return _Project_Id = _Project_Id ?? FindPropertyByName<Project>("Id");
            }
        }
        private IProperty _Project_CreatedDate = null;
        public IProperty Project_CreatedDate
        {
            get
            {
                return _Project_CreatedDate = _Project_CreatedDate ?? FindPropertyByName<Project>("CreatedDate");
            }
        }
        private IProperty _Project_RevisionKey = null;
        public IProperty Project_RevisionKey
        {
            get
            {
                return _Project_RevisionKey = _Project_RevisionKey ?? FindPropertyByName<Project>("RevisionKey");
            }
        }
        private IProperty _Project_PersistenceKey = null;
        public IProperty Project_PersistenceKey
        {
            get
            {
                return _Project_PersistenceKey = _Project_PersistenceKey ?? FindPropertyByName<Project>("PersistenceKey");
            }
        }
        private IProperty _Project_CreatedByUser = null;
        public IProperty Project_CreatedByUser
        {
            get
            {
                return _Project_CreatedByUser = _Project_CreatedByUser ?? FindPropertyByName<Project>("CreatedByUser");
            }
        }
        public Type TypeOf_ReportReceiverEmailAddress => typeof(ReportReceiverEmailAddress);
        private EntityConfiguration<ReportReceiverEmailAddress>_configurationFor_ReportReceiverEmailAddress = null;
        public EntityConfiguration<ReportReceiverEmailAddress>ConfigurationFor_ReportReceiverEmailAddress => _configurationFor_ReportReceiverEmailAddress = _configurationFor_ReportReceiverEmailAddress ?? Builder.EntityType<ReportReceiverEmailAddress>();
        private IProperty _ReportReceiverEmailAddress_SiteId = null;
        public IProperty ReportReceiverEmailAddress_SiteId
        {
            get
            {
                return _ReportReceiverEmailAddress_SiteId = _ReportReceiverEmailAddress_SiteId ?? FindPropertyByName<ReportReceiverEmailAddress>("SiteId");
            }
        }
        private IProperty _ReportReceiverEmailAddress_CreatedByUserId = null;
        public IProperty ReportReceiverEmailAddress_CreatedByUserId
        {
            get
            {
                return _ReportReceiverEmailAddress_CreatedByUserId = _ReportReceiverEmailAddress_CreatedByUserId ?? FindPropertyByName<ReportReceiverEmailAddress>("CreatedByUserId");
            }
        }
        private IProperty _ReportReceiverEmailAddress_EmailAddress = null;
        public IProperty ReportReceiverEmailAddress_EmailAddress
        {
            get
            {
                return _ReportReceiverEmailAddress_EmailAddress = _ReportReceiverEmailAddress_EmailAddress ?? FindPropertyByName<ReportReceiverEmailAddress>("EmailAddress");
            }
        }
        private IProperty _ReportReceiverEmailAddress_Guid = null;
        public IProperty ReportReceiverEmailAddress_Guid
        {
            get
            {
                return _ReportReceiverEmailAddress_Guid = _ReportReceiverEmailAddress_Guid ?? FindPropertyByName<ReportReceiverEmailAddress>("Guid");
            }
        }
        private IProperty _ReportReceiverEmailAddress_Id = null;
        public IProperty ReportReceiverEmailAddress_Id
        {
            get
            {
                return _ReportReceiverEmailAddress_Id = _ReportReceiverEmailAddress_Id ?? FindPropertyByName<ReportReceiverEmailAddress>("Id");
            }
        }
        private IProperty _ReportReceiverEmailAddress_CreatedDate = null;
        public IProperty ReportReceiverEmailAddress_CreatedDate
        {
            get
            {
                return _ReportReceiverEmailAddress_CreatedDate = _ReportReceiverEmailAddress_CreatedDate ?? FindPropertyByName<ReportReceiverEmailAddress>("CreatedDate");
            }
        }
        private IProperty _ReportReceiverEmailAddress_RevisionKey = null;
        public IProperty ReportReceiverEmailAddress_RevisionKey
        {
            get
            {
                return _ReportReceiverEmailAddress_RevisionKey = _ReportReceiverEmailAddress_RevisionKey ?? FindPropertyByName<ReportReceiverEmailAddress>("RevisionKey");
            }
        }
        private IProperty _ReportReceiverEmailAddress_PersistenceKey = null;
        public IProperty ReportReceiverEmailAddress_PersistenceKey
        {
            get
            {
                return _ReportReceiverEmailAddress_PersistenceKey = _ReportReceiverEmailAddress_PersistenceKey ?? FindPropertyByName<ReportReceiverEmailAddress>("PersistenceKey");
            }
        }
        private IProperty _ReportReceiverEmailAddress_Site = null;
        public IProperty ReportReceiverEmailAddress_Site
        {
            get
            {
                return _ReportReceiverEmailAddress_Site = _ReportReceiverEmailAddress_Site ?? FindPropertyByName<ReportReceiverEmailAddress>("Site");
            }
        }
        private IProperty _ReportReceiverEmailAddress_CreatedByUser = null;
        public IProperty ReportReceiverEmailAddress_CreatedByUser
        {
            get
            {
                return _ReportReceiverEmailAddress_CreatedByUser = _ReportReceiverEmailAddress_CreatedByUser ?? FindPropertyByName<ReportReceiverEmailAddress>("CreatedByUser");
            }
        }
        public Type TypeOf_RiskAssessment => typeof(RiskAssessment);
        private EntityConfiguration<RiskAssessment>_configurationFor_RiskAssessment = null;
        public EntityConfiguration<RiskAssessment>ConfigurationFor_RiskAssessment => _configurationFor_RiskAssessment = _configurationFor_RiskAssessment ?? Builder.EntityType<RiskAssessment>();
        private IProperty _RiskAssessment_SiteInspectionId = null;
        public IProperty RiskAssessment_SiteInspectionId
        {
            get
            {
                return _RiskAssessment_SiteInspectionId = _RiskAssessment_SiteInspectionId ?? FindPropertyByName<RiskAssessment>("SiteInspectionId");
            }
        }
        private IProperty _RiskAssessment_Id = null;
        public IProperty RiskAssessment_Id
        {
            get
            {
                return _RiskAssessment_Id = _RiskAssessment_Id ?? FindPropertyByName<RiskAssessment>("Id");
            }
        }
        private IProperty _RiskAssessment_CreatedByUserId = null;
        public IProperty RiskAssessment_CreatedByUserId
        {
            get
            {
                return _RiskAssessment_CreatedByUserId = _RiskAssessment_CreatedByUserId ?? FindPropertyByName<RiskAssessment>("CreatedByUserId");
            }
        }
        private IProperty _RiskAssessment_Guid = null;
        public IProperty RiskAssessment_Guid
        {
            get
            {
                return _RiskAssessment_Guid = _RiskAssessment_Guid ?? FindPropertyByName<RiskAssessment>("Guid");
            }
        }
        private IProperty _RiskAssessment_CreatedDate = null;
        public IProperty RiskAssessment_CreatedDate
        {
            get
            {
                return _RiskAssessment_CreatedDate = _RiskAssessment_CreatedDate ?? FindPropertyByName<RiskAssessment>("CreatedDate");
            }
        }
        private IProperty _RiskAssessment_RevisionKey = null;
        public IProperty RiskAssessment_RevisionKey
        {
            get
            {
                return _RiskAssessment_RevisionKey = _RiskAssessment_RevisionKey ?? FindPropertyByName<RiskAssessment>("RevisionKey");
            }
        }
        private IProperty _RiskAssessment_PersistenceKey = null;
        public IProperty RiskAssessment_PersistenceKey
        {
            get
            {
                return _RiskAssessment_PersistenceKey = _RiskAssessment_PersistenceKey ?? FindPropertyByName<RiskAssessment>("PersistenceKey");
            }
        }
        private IProperty _RiskAssessment_SiteInspection = null;
        public IProperty RiskAssessment_SiteInspection
        {
            get
            {
                return _RiskAssessment_SiteInspection = _RiskAssessment_SiteInspection ?? FindPropertyByName<RiskAssessment>("SiteInspection");
            }
        }
        private IProperty _RiskAssessment_CreatedByUser = null;
        public IProperty RiskAssessment_CreatedByUser
        {
            get
            {
                return _RiskAssessment_CreatedByUser = _RiskAssessment_CreatedByUser ?? FindPropertyByName<RiskAssessment>("CreatedByUser");
            }
        }
        private IProperty _RiskAssessment_RiskAssessmentSolution = null;
        public IProperty RiskAssessment_RiskAssessmentSolution
        {
            get
            {
                return _RiskAssessment_RiskAssessmentSolution = _RiskAssessment_RiskAssessmentSolution ?? FindPropertyByName<RiskAssessment>("RiskAssessmentSolution");
            }
        }
        public Type TypeOf_RiskAssessmentSolution => typeof(RiskAssessmentSolution);
        private EntityConfiguration<RiskAssessmentSolution>_configurationFor_RiskAssessmentSolution = null;
        public EntityConfiguration<RiskAssessmentSolution>ConfigurationFor_RiskAssessmentSolution => _configurationFor_RiskAssessmentSolution = _configurationFor_RiskAssessmentSolution ?? Builder.EntityType<RiskAssessmentSolution>();
        private IProperty _RiskAssessmentSolution_RiskAssessmentId = null;
        public IProperty RiskAssessmentSolution_RiskAssessmentId
        {
            get
            {
                return _RiskAssessmentSolution_RiskAssessmentId = _RiskAssessmentSolution_RiskAssessmentId ?? FindPropertyByName<RiskAssessmentSolution>("RiskAssessmentId");
            }
        }
        private IProperty _RiskAssessmentSolution_CreatedByUserId = null;
        public IProperty RiskAssessmentSolution_CreatedByUserId
        {
            get
            {
                return _RiskAssessmentSolution_CreatedByUserId = _RiskAssessmentSolution_CreatedByUserId ?? FindPropertyByName<RiskAssessmentSolution>("CreatedByUserId");
            }
        }
        private IProperty _RiskAssessmentSolution_Guid = null;
        public IProperty RiskAssessmentSolution_Guid
        {
            get
            {
                return _RiskAssessmentSolution_Guid = _RiskAssessmentSolution_Guid ?? FindPropertyByName<RiskAssessmentSolution>("Guid");
            }
        }
        private IProperty _RiskAssessmentSolution_Id = null;
        public IProperty RiskAssessmentSolution_Id
        {
            get
            {
                return _RiskAssessmentSolution_Id = _RiskAssessmentSolution_Id ?? FindPropertyByName<RiskAssessmentSolution>("Id");
            }
        }
        private IProperty _RiskAssessmentSolution_CreatedDate = null;
        public IProperty RiskAssessmentSolution_CreatedDate
        {
            get
            {
                return _RiskAssessmentSolution_CreatedDate = _RiskAssessmentSolution_CreatedDate ?? FindPropertyByName<RiskAssessmentSolution>("CreatedDate");
            }
        }
        private IProperty _RiskAssessmentSolution_RevisionKey = null;
        public IProperty RiskAssessmentSolution_RevisionKey
        {
            get
            {
                return _RiskAssessmentSolution_RevisionKey = _RiskAssessmentSolution_RevisionKey ?? FindPropertyByName<RiskAssessmentSolution>("RevisionKey");
            }
        }
        private IProperty _RiskAssessmentSolution_PersistenceKey = null;
        public IProperty RiskAssessmentSolution_PersistenceKey
        {
            get
            {
                return _RiskAssessmentSolution_PersistenceKey = _RiskAssessmentSolution_PersistenceKey ?? FindPropertyByName<RiskAssessmentSolution>("PersistenceKey");
            }
        }
        private IProperty _RiskAssessmentSolution_RiskAssessment = null;
        public IProperty RiskAssessmentSolution_RiskAssessment
        {
            get
            {
                return _RiskAssessmentSolution_RiskAssessment = _RiskAssessmentSolution_RiskAssessment ?? FindPropertyByName<RiskAssessmentSolution>("RiskAssessment");
            }
        }
        private IProperty _RiskAssessmentSolution_CreatedByUser = null;
        public IProperty RiskAssessmentSolution_CreatedByUser
        {
            get
            {
                return _RiskAssessmentSolution_CreatedByUser = _RiskAssessmentSolution_CreatedByUser ?? FindPropertyByName<RiskAssessmentSolution>("CreatedByUser");
            }
        }
        public Type TypeOf_RiskAssessmentAnswer => typeof(RiskAssessmentAnswer);
        private EntityConfiguration<RiskAssessmentAnswer>_configurationFor_RiskAssessmentAnswer = null;
        public EntityConfiguration<RiskAssessmentAnswer>ConfigurationFor_RiskAssessmentAnswer => _configurationFor_RiskAssessmentAnswer = _configurationFor_RiskAssessmentAnswer ?? Builder.EntityType<RiskAssessmentAnswer>();
        private IProperty _RiskAssessmentAnswer_QuestionId = null;
        public IProperty RiskAssessmentAnswer_QuestionId
        {
            get
            {
                return _RiskAssessmentAnswer_QuestionId = _RiskAssessmentAnswer_QuestionId ?? FindPropertyByName<RiskAssessmentAnswer>("QuestionId");
            }
        }
        private IProperty _RiskAssessmentAnswer_CreatedByUserId = null;
        public IProperty RiskAssessmentAnswer_CreatedByUserId
        {
            get
            {
                return _RiskAssessmentAnswer_CreatedByUserId = _RiskAssessmentAnswer_CreatedByUserId ?? FindPropertyByName<RiskAssessmentAnswer>("CreatedByUserId");
            }
        }
        private IProperty _RiskAssessmentAnswer_SpecificHazard = null;
        public IProperty RiskAssessmentAnswer_SpecificHazard
        {
            get
            {
                return _RiskAssessmentAnswer_SpecificHazard = _RiskAssessmentAnswer_SpecificHazard ?? FindPropertyByName<RiskAssessmentAnswer>("SpecificHazard");
            }
        }
        private IProperty _RiskAssessmentAnswer_PrecautionsToControlHazard = null;
        public IProperty RiskAssessmentAnswer_PrecautionsToControlHazard
        {
            get
            {
                return _RiskAssessmentAnswer_PrecautionsToControlHazard = _RiskAssessmentAnswer_PrecautionsToControlHazard ?? FindPropertyByName<RiskAssessmentAnswer>("PrecautionsToControlHazard");
            }
        }
        private IProperty _RiskAssessmentAnswer_Guid = null;
        public IProperty RiskAssessmentAnswer_Guid
        {
            get
            {
                return _RiskAssessmentAnswer_Guid = _RiskAssessmentAnswer_Guid ?? FindPropertyByName<RiskAssessmentAnswer>("Guid");
            }
        }
        private IProperty _RiskAssessmentAnswer_Id = null;
        public IProperty RiskAssessmentAnswer_Id
        {
            get
            {
                return _RiskAssessmentAnswer_Id = _RiskAssessmentAnswer_Id ?? FindPropertyByName<RiskAssessmentAnswer>("Id");
            }
        }
        private IProperty _RiskAssessmentAnswer_CreatedDate = null;
        public IProperty RiskAssessmentAnswer_CreatedDate
        {
            get
            {
                return _RiskAssessmentAnswer_CreatedDate = _RiskAssessmentAnswer_CreatedDate ?? FindPropertyByName<RiskAssessmentAnswer>("CreatedDate");
            }
        }
        private IProperty _RiskAssessmentAnswer_RevisionKey = null;
        public IProperty RiskAssessmentAnswer_RevisionKey
        {
            get
            {
                return _RiskAssessmentAnswer_RevisionKey = _RiskAssessmentAnswer_RevisionKey ?? FindPropertyByName<RiskAssessmentAnswer>("RevisionKey");
            }
        }
        private IProperty _RiskAssessmentAnswer_PersistenceKey = null;
        public IProperty RiskAssessmentAnswer_PersistenceKey
        {
            get
            {
                return _RiskAssessmentAnswer_PersistenceKey = _RiskAssessmentAnswer_PersistenceKey ?? FindPropertyByName<RiskAssessmentAnswer>("PersistenceKey");
            }
        }
        private IProperty _RiskAssessmentAnswer_Question = null;
        public IProperty RiskAssessmentAnswer_Question
        {
            get
            {
                return _RiskAssessmentAnswer_Question = _RiskAssessmentAnswer_Question ?? FindPropertyByName<RiskAssessmentAnswer>("Question");
            }
        }
        private IProperty _RiskAssessmentAnswer_CreatedByUser = null;
        public IProperty RiskAssessmentAnswer_CreatedByUser
        {
            get
            {
                return _RiskAssessmentAnswer_CreatedByUser = _RiskAssessmentAnswer_CreatedByUser ?? FindPropertyByName<RiskAssessmentAnswer>("CreatedByUser");
            }
        }
        public Type TypeOf_RiskAssessmentQuestion => typeof(RiskAssessmentQuestion);
        private EntityConfiguration<RiskAssessmentQuestion>_configurationFor_RiskAssessmentQuestion = null;
        public EntityConfiguration<RiskAssessmentQuestion>ConfigurationFor_RiskAssessmentQuestion => _configurationFor_RiskAssessmentQuestion = _configurationFor_RiskAssessmentQuestion ?? Builder.EntityType<RiskAssessmentQuestion>();
        private IProperty _RiskAssessmentQuestion_Id = null;
        public IProperty RiskAssessmentQuestion_Id
        {
            get
            {
                return _RiskAssessmentQuestion_Id = _RiskAssessmentQuestion_Id ?? FindPropertyByName<RiskAssessmentQuestion>("Id");
            }
        }
        private IProperty _RiskAssessmentQuestion_CreatedByUserId = null;
        public IProperty RiskAssessmentQuestion_CreatedByUserId
        {
            get
            {
                return _RiskAssessmentQuestion_CreatedByUserId = _RiskAssessmentQuestion_CreatedByUserId ?? FindPropertyByName<RiskAssessmentQuestion>("CreatedByUserId");
            }
        }
        private IProperty _RiskAssessmentQuestion_Name = null;
        public IProperty RiskAssessmentQuestion_Name
        {
            get
            {
                return _RiskAssessmentQuestion_Name = _RiskAssessmentQuestion_Name ?? FindPropertyByName<RiskAssessmentQuestion>("Name");
            }
        }
        private IProperty _RiskAssessmentQuestion_Guid = null;
        public IProperty RiskAssessmentQuestion_Guid
        {
            get
            {
                return _RiskAssessmentQuestion_Guid = _RiskAssessmentQuestion_Guid ?? FindPropertyByName<RiskAssessmentQuestion>("Guid");
            }
        }
        private IProperty _RiskAssessmentQuestion_CreatedDate = null;
        public IProperty RiskAssessmentQuestion_CreatedDate
        {
            get
            {
                return _RiskAssessmentQuestion_CreatedDate = _RiskAssessmentQuestion_CreatedDate ?? FindPropertyByName<RiskAssessmentQuestion>("CreatedDate");
            }
        }
        private IProperty _RiskAssessmentQuestion_RevisionKey = null;
        public IProperty RiskAssessmentQuestion_RevisionKey
        {
            get
            {
                return _RiskAssessmentQuestion_RevisionKey = _RiskAssessmentQuestion_RevisionKey ?? FindPropertyByName<RiskAssessmentQuestion>("RevisionKey");
            }
        }
        private IProperty _RiskAssessmentQuestion_PersistenceKey = null;
        public IProperty RiskAssessmentQuestion_PersistenceKey
        {
            get
            {
                return _RiskAssessmentQuestion_PersistenceKey = _RiskAssessmentQuestion_PersistenceKey ?? FindPropertyByName<RiskAssessmentQuestion>("PersistenceKey");
            }
        }
        private IProperty _RiskAssessmentQuestion_Answers = null;
        public IProperty RiskAssessmentQuestion_Answers
        {
            get
            {
                return _RiskAssessmentQuestion_Answers = _RiskAssessmentQuestion_Answers ?? FindPropertyByName<RiskAssessmentQuestion>("Answers");
            }
        }
        private IProperty _RiskAssessmentQuestion_CreatedByUser = null;
        public IProperty RiskAssessmentQuestion_CreatedByUser
        {
            get
            {
                return _RiskAssessmentQuestion_CreatedByUser = _RiskAssessmentQuestion_CreatedByUser ?? FindPropertyByName<RiskAssessmentQuestion>("CreatedByUser");
            }
        }
        public Type TypeOf_Person => typeof(Person);
        private EntityConfiguration<Person>_configurationFor_Person = null;
        public EntityConfiguration<Person>ConfigurationFor_Person => _configurationFor_Person = _configurationFor_Person ?? Builder.EntityType<Person>();
        private IProperty _Person_Location = null;
        public IProperty Person_Location
        {
            get
            {
                return _Person_Location = _Person_Location ?? FindPropertyByName<Person>("Location");
            }
        }
        private IProperty _Person_ClientId = null;
        public IProperty Person_ClientId
        {
            get
            {
                return _Person_ClientId = _Person_ClientId ?? FindPropertyByName<Person>("ClientId");
            }
        }
        private IProperty _Person_SiteId = null;
        public IProperty Person_SiteId
        {
            get
            {
                return _Person_SiteId = _Person_SiteId ?? FindPropertyByName<Person>("SiteId");
            }
        }
        private IProperty _Person_SiteAreaId = null;
        public IProperty Person_SiteAreaId
        {
            get
            {
                return _Person_SiteAreaId = _Person_SiteAreaId ?? FindPropertyByName<Person>("SiteAreaId");
            }
        }
        private IProperty _Person_TypeId = null;
        public IProperty Person_TypeId
        {
            get
            {
                return _Person_TypeId = _Person_TypeId ?? FindPropertyByName<Person>("TypeId");
            }
        }
        private IProperty _Person_LoadingId = null;
        public IProperty Person_LoadingId
        {
            get
            {
                return _Person_LoadingId = _Person_LoadingId ?? FindPropertyByName<Person>("LoadingId");
            }
        }
        private IProperty _Person_Id = null;
        public IProperty Person_Id
        {
            get
            {
                return _Person_Id = _Person_Id ?? FindPropertyByName<Person>("Id");
            }
        }
        private IProperty _Person_CreatedByUserId = null;
        public IProperty Person_CreatedByUserId
        {
            get
            {
                return _Person_CreatedByUserId = _Person_CreatedByUserId ?? FindPropertyByName<Person>("CreatedByUserId");
            }
        }
        private IProperty _Person_PhotoUrl = null;
        public IProperty Person_PhotoUrl
        {
            get
            {
                return _Person_PhotoUrl = _Person_PhotoUrl ?? FindPropertyByName<Person>("PhotoUrl");
            }
        }
        private IProperty _Person_PhotoRevisionKey = null;
        public IProperty Person_PhotoRevisionKey
        {
            get
            {
                return _Person_PhotoRevisionKey = _Person_PhotoRevisionKey ?? FindPropertyByName<Person>("PhotoRevisionKey");
            }
        }
        private IProperty _Person_Birthday = null;
        public IProperty Person_Birthday
        {
            get
            {
                return _Person_Birthday = _Person_Birthday ?? FindPropertyByName<Person>("Birthday");
            }
        }
        private IProperty _Person_Key = null;
        public IProperty Person_Key
        {
            get
            {
                return _Person_Key = _Person_Key ?? FindPropertyByName<Person>("Key");
            }
        }
        private IProperty _Person_InferredWhenKeyChanges = null;
        public IProperty Person_InferredWhenKeyChanges
        {
            get
            {
                return _Person_InferredWhenKeyChanges = _Person_InferredWhenKeyChanges ?? FindPropertyByName<Person>("InferredWhenKeyChanges");
            }
        }
        private IProperty _Person_IsComplete = null;
        public IProperty Person_IsComplete
        {
            get
            {
                return _Person_IsComplete = _Person_IsComplete ?? FindPropertyByName<Person>("IsComplete");
            }
        }
        private IProperty _Person_HasPaid = null;
        public IProperty Person_HasPaid
        {
            get
            {
                return _Person_HasPaid = _Person_HasPaid ?? FindPropertyByName<Person>("HasPaid");
            }
        }
        private IProperty _Person_Title = null;
        public IProperty Person_Title
        {
            get
            {
                return _Person_Title = _Person_Title ?? FindPropertyByName<Person>("Title");
            }
        }
        private IProperty _Person_Description = null;
        public IProperty Person_Description
        {
            get
            {
                return _Person_Description = _Person_Description ?? FindPropertyByName<Person>("Description");
            }
        }
        private IProperty _Person_Skills = null;
        public IProperty Person_Skills
        {
            get
            {
                return _Person_Skills = _Person_Skills ?? FindPropertyByName<Person>("Skills");
            }
        }
        private IProperty _Person_Category = null;
        public IProperty Person_Category
        {
            get
            {
                return _Person_Category = _Person_Category ?? FindPropertyByName<Person>("Category");
            }
        }
        private IProperty _Person_Guid = null;
        public IProperty Person_Guid
        {
            get
            {
                return _Person_Guid = _Person_Guid ?? FindPropertyByName<Person>("Guid");
            }
        }
        private IProperty _Person_CreatedDate = null;
        public IProperty Person_CreatedDate
        {
            get
            {
                return _Person_CreatedDate = _Person_CreatedDate ?? FindPropertyByName<Person>("CreatedDate");
            }
        }
        private IProperty _Person_RevisionKey = null;
        public IProperty Person_RevisionKey
        {
            get
            {
                return _Person_RevisionKey = _Person_RevisionKey ?? FindPropertyByName<Person>("RevisionKey");
            }
        }
        private IProperty _Person_PersistenceKey = null;
        public IProperty Person_PersistenceKey
        {
            get
            {
                return _Person_PersistenceKey = _Person_PersistenceKey ?? FindPropertyByName<Person>("PersistenceKey");
            }
        }
        private IProperty _Person_Client = null;
        public IProperty Person_Client
        {
            get
            {
                return _Person_Client = _Person_Client ?? FindPropertyByName<Person>("Client");
            }
        }
        private IProperty _Person_Site = null;
        public IProperty Person_Site
        {
            get
            {
                return _Person_Site = _Person_Site ?? FindPropertyByName<Person>("Site");
            }
        }
        private IProperty _Person_SiteArea = null;
        public IProperty Person_SiteArea
        {
            get
            {
                return _Person_SiteArea = _Person_SiteArea ?? FindPropertyByName<Person>("SiteArea");
            }
        }
        private IProperty _Person_Type = null;
        public IProperty Person_Type
        {
            get
            {
                return _Person_Type = _Person_Type ?? FindPropertyByName<Person>("Type");
            }
        }
        private IProperty _Person_Loading = null;
        public IProperty Person_Loading
        {
            get
            {
                return _Person_Loading = _Person_Loading ?? FindPropertyByName<Person>("Loading");
            }
        }
        private IProperty _Person_CreatedByUser = null;
        public IProperty Person_CreatedByUser
        {
            get
            {
                return _Person_CreatedByUser = _Person_CreatedByUser ?? FindPropertyByName<Person>("CreatedByUser");
            }
        }
        private IProperty _Person_Types = null;
        public IProperty Person_Types
        {
            get
            {
                return _Person_Types = _Person_Types ?? FindPropertyByName<Person>("Types");
            }
        }
        private IProperty _Person_Reports = null;
        public IProperty Person_Reports
        {
            get
            {
                return _Person_Reports = _Person_Reports ?? FindPropertyByName<Person>("Reports");
            }
        }
        public Type TypeOf_PersonInspection => typeof(PersonInspection);
        private EntityConfiguration<PersonInspection>_configurationFor_PersonInspection = null;
        public EntityConfiguration<PersonInspection>ConfigurationFor_PersonInspection => _configurationFor_PersonInspection = _configurationFor_PersonInspection ?? Builder.EntityType<PersonInspection>();
        private IProperty _PersonInspection_SiteInspectionId = null;
        public IProperty PersonInspection_SiteInspectionId
        {
            get
            {
                return _PersonInspection_SiteInspectionId = _PersonInspection_SiteInspectionId ?? FindPropertyByName<PersonInspection>("SiteInspectionId");
            }
        }
        private IProperty _PersonInspection_CreatedByUserId = null;
        public IProperty PersonInspection_CreatedByUserId
        {
            get
            {
                return _PersonInspection_CreatedByUserId = _PersonInspection_CreatedByUserId ?? FindPropertyByName<PersonInspection>("CreatedByUserId");
            }
        }
        private IProperty _PersonInspection_PersonId = null;
        public IProperty PersonInspection_PersonId
        {
            get
            {
                return _PersonInspection_PersonId = _PersonInspection_PersonId ?? FindPropertyByName<PersonInspection>("PersonId");
            }
        }
        private IProperty _PersonInspection_InspectionStatus = null;
        public IProperty PersonInspection_InspectionStatus
        {
            get
            {
                return _PersonInspection_InspectionStatus = _PersonInspection_InspectionStatus ?? FindPropertyByName<PersonInspection>("InspectionStatus");
            }
        }
        private IProperty _PersonInspection_StartTime = null;
        public IProperty PersonInspection_StartTime
        {
            get
            {
                return _PersonInspection_StartTime = _PersonInspection_StartTime ?? FindPropertyByName<PersonInspection>("StartTime");
            }
        }
        private IProperty _PersonInspection_EndTime = null;
        public IProperty PersonInspection_EndTime
        {
            get
            {
                return _PersonInspection_EndTime = _PersonInspection_EndTime ?? FindPropertyByName<PersonInspection>("EndTime");
            }
        }
        private IProperty _PersonInspection_ReasonForFailure = null;
        public IProperty PersonInspection_ReasonForFailure
        {
            get
            {
                return _PersonInspection_ReasonForFailure = _PersonInspection_ReasonForFailure ?? FindPropertyByName<PersonInspection>("ReasonForFailure");
            }
        }
        private IProperty _PersonInspection_IsDesignRequired = null;
        public IProperty PersonInspection_IsDesignRequired
        {
            get
            {
                return _PersonInspection_IsDesignRequired = _PersonInspection_IsDesignRequired ?? FindPropertyByName<PersonInspection>("IsDesignRequired");
            }
        }
        private IProperty _PersonInspection_DrawingNumber = null;
        public IProperty PersonInspection_DrawingNumber
        {
            get
            {
                return _PersonInspection_DrawingNumber = _PersonInspection_DrawingNumber ?? FindPropertyByName<PersonInspection>("DrawingNumber");
            }
        }
        private IProperty _PersonInspection_Guid = null;
        public IProperty PersonInspection_Guid
        {
            get
            {
                return _PersonInspection_Guid = _PersonInspection_Guid ?? FindPropertyByName<PersonInspection>("Guid");
            }
        }
        private IProperty _PersonInspection_Id = null;
        public IProperty PersonInspection_Id
        {
            get
            {
                return _PersonInspection_Id = _PersonInspection_Id ?? FindPropertyByName<PersonInspection>("Id");
            }
        }
        private IProperty _PersonInspection_CreatedDate = null;
        public IProperty PersonInspection_CreatedDate
        {
            get
            {
                return _PersonInspection_CreatedDate = _PersonInspection_CreatedDate ?? FindPropertyByName<PersonInspection>("CreatedDate");
            }
        }
        private IProperty _PersonInspection_RevisionKey = null;
        public IProperty PersonInspection_RevisionKey
        {
            get
            {
                return _PersonInspection_RevisionKey = _PersonInspection_RevisionKey ?? FindPropertyByName<PersonInspection>("RevisionKey");
            }
        }
        private IProperty _PersonInspection_PersistenceKey = null;
        public IProperty PersonInspection_PersistenceKey
        {
            get
            {
                return _PersonInspection_PersistenceKey = _PersonInspection_PersistenceKey ?? FindPropertyByName<PersonInspection>("PersistenceKey");
            }
        }
        private IProperty _PersonInspection_SiteInspection = null;
        public IProperty PersonInspection_SiteInspection
        {
            get
            {
                return _PersonInspection_SiteInspection = _PersonInspection_SiteInspection ?? FindPropertyByName<PersonInspection>("SiteInspection");
            }
        }
        private IProperty _PersonInspection_CreatedByUser = null;
        public IProperty PersonInspection_CreatedByUser
        {
            get
            {
                return _PersonInspection_CreatedByUser = _PersonInspection_CreatedByUser ?? FindPropertyByName<PersonInspection>("CreatedByUser");
            }
        }
        public Type TypeOf_PersonLoading => typeof(PersonLoading);
        private EntityConfiguration<PersonLoading>_configurationFor_PersonLoading = null;
        public EntityConfiguration<PersonLoading>ConfigurationFor_PersonLoading => _configurationFor_PersonLoading = _configurationFor_PersonLoading ?? Builder.EntityType<PersonLoading>();
        private IProperty _PersonLoading_Id = null;
        public IProperty PersonLoading_Id
        {
            get
            {
                return _PersonLoading_Id = _PersonLoading_Id ?? FindPropertyByName<PersonLoading>("Id");
            }
        }
        private IProperty _PersonLoading_CreatedByUserId = null;
        public IProperty PersonLoading_CreatedByUserId
        {
            get
            {
                return _PersonLoading_CreatedByUserId = _PersonLoading_CreatedByUserId ?? FindPropertyByName<PersonLoading>("CreatedByUserId");
            }
        }
        private IProperty _PersonLoading_Name = null;
        public IProperty PersonLoading_Name
        {
            get
            {
                return _PersonLoading_Name = _PersonLoading_Name ?? FindPropertyByName<PersonLoading>("Name");
            }
        }
        private IProperty _PersonLoading_Guid = null;
        public IProperty PersonLoading_Guid
        {
            get
            {
                return _PersonLoading_Guid = _PersonLoading_Guid ?? FindPropertyByName<PersonLoading>("Guid");
            }
        }
        private IProperty _PersonLoading_CreatedDate = null;
        public IProperty PersonLoading_CreatedDate
        {
            get
            {
                return _PersonLoading_CreatedDate = _PersonLoading_CreatedDate ?? FindPropertyByName<PersonLoading>("CreatedDate");
            }
        }
        private IProperty _PersonLoading_RevisionKey = null;
        public IProperty PersonLoading_RevisionKey
        {
            get
            {
                return _PersonLoading_RevisionKey = _PersonLoading_RevisionKey ?? FindPropertyByName<PersonLoading>("RevisionKey");
            }
        }
        private IProperty _PersonLoading_PersistenceKey = null;
        public IProperty PersonLoading_PersistenceKey
        {
            get
            {
                return _PersonLoading_PersistenceKey = _PersonLoading_PersistenceKey ?? FindPropertyByName<PersonLoading>("PersistenceKey");
            }
        }
        private IProperty _PersonLoading_People = null;
        public IProperty PersonLoading_People
        {
            get
            {
                return _PersonLoading_People = _PersonLoading_People ?? FindPropertyByName<PersonLoading>("People");
            }
        }
        private IProperty _PersonLoading_CreatedByUser = null;
        public IProperty PersonLoading_CreatedByUser
        {
            get
            {
                return _PersonLoading_CreatedByUser = _PersonLoading_CreatedByUser ?? FindPropertyByName<PersonLoading>("CreatedByUser");
            }
        }
        public Type TypeOf_PersonType => typeof(PersonType);
        private EntityConfiguration<PersonType>_configurationFor_PersonType = null;
        public EntityConfiguration<PersonType>ConfigurationFor_PersonType => _configurationFor_PersonType = _configurationFor_PersonType ?? Builder.EntityType<PersonType>();
        private IProperty _PersonType_Id = null;
        public IProperty PersonType_Id
        {
            get
            {
                return _PersonType_Id = _PersonType_Id ?? FindPropertyByName<PersonType>("Id");
            }
        }
        private IProperty _PersonType_CreatedByUserId = null;
        public IProperty PersonType_CreatedByUserId
        {
            get
            {
                return _PersonType_CreatedByUserId = _PersonType_CreatedByUserId ?? FindPropertyByName<PersonType>("CreatedByUserId");
            }
        }
        private IProperty _PersonType_Title = null;
        public IProperty PersonType_Title
        {
            get
            {
                return _PersonType_Title = _PersonType_Title ?? FindPropertyByName<PersonType>("Title");
            }
        }
        private IProperty _PersonType_Guid = null;
        public IProperty PersonType_Guid
        {
            get
            {
                return _PersonType_Guid = _PersonType_Guid ?? FindPropertyByName<PersonType>("Guid");
            }
        }
        private IProperty _PersonType_CreatedDate = null;
        public IProperty PersonType_CreatedDate
        {
            get
            {
                return _PersonType_CreatedDate = _PersonType_CreatedDate ?? FindPropertyByName<PersonType>("CreatedDate");
            }
        }
        private IProperty _PersonType_RevisionKey = null;
        public IProperty PersonType_RevisionKey
        {
            get
            {
                return _PersonType_RevisionKey = _PersonType_RevisionKey ?? FindPropertyByName<PersonType>("RevisionKey");
            }
        }
        private IProperty _PersonType_PersistenceKey = null;
        public IProperty PersonType_PersistenceKey
        {
            get
            {
                return _PersonType_PersistenceKey = _PersonType_PersistenceKey ?? FindPropertyByName<PersonType>("PersistenceKey");
            }
        }
        private IProperty _PersonType_People = null;
        public IProperty PersonType_People
        {
            get
            {
                return _PersonType_People = _PersonType_People ?? FindPropertyByName<PersonType>("People");
            }
        }
        private IProperty _PersonType_CreatedByUser = null;
        public IProperty PersonType_CreatedByUser
        {
            get
            {
                return _PersonType_CreatedByUser = _PersonType_CreatedByUser ?? FindPropertyByName<PersonType>("CreatedByUser");
            }
        }
        private IProperty _PersonType_PeopleMap = null;
        public IProperty PersonType_PeopleMap
        {
            get
            {
                return _PersonType_PeopleMap = _PersonType_PeopleMap ?? FindPropertyByName<PersonType>("PeopleMap");
            }
        }
        public Type TypeOf_PersonTypeMap => typeof(PersonTypeMap);
        private EntityConfiguration<PersonTypeMap>_configurationFor_PersonTypeMap = null;
        public EntityConfiguration<PersonTypeMap>ConfigurationFor_PersonTypeMap => _configurationFor_PersonTypeMap = _configurationFor_PersonTypeMap ?? Builder.EntityType<PersonTypeMap>();
        private IProperty _PersonTypeMap_PersonId = null;
        public IProperty PersonTypeMap_PersonId
        {
            get
            {
                return _PersonTypeMap_PersonId = _PersonTypeMap_PersonId ?? FindPropertyByName<PersonTypeMap>("PersonId");
            }
        }
        private IProperty _PersonTypeMap_TypeId = null;
        public IProperty PersonTypeMap_TypeId
        {
            get
            {
                return _PersonTypeMap_TypeId = _PersonTypeMap_TypeId ?? FindPropertyByName<PersonTypeMap>("TypeId");
            }
        }
        private IProperty _PersonTypeMap_Notes = null;
        public IProperty PersonTypeMap_Notes
        {
            get
            {
                return _PersonTypeMap_Notes = _PersonTypeMap_Notes ?? FindPropertyByName<PersonTypeMap>("Notes");
            }
        }
        private IProperty _PersonTypeMap_Description = null;
        public IProperty PersonTypeMap_Description
        {
            get
            {
                return _PersonTypeMap_Description = _PersonTypeMap_Description ?? FindPropertyByName<PersonTypeMap>("Description");
            }
        }
        private IProperty _PersonTypeMap_Guid = null;
        public IProperty PersonTypeMap_Guid
        {
            get
            {
                return _PersonTypeMap_Guid = _PersonTypeMap_Guid ?? FindPropertyByName<PersonTypeMap>("Guid");
            }
        }
        private IProperty _PersonTypeMap_CreatedDate = null;
        public IProperty PersonTypeMap_CreatedDate
        {
            get
            {
                return _PersonTypeMap_CreatedDate = _PersonTypeMap_CreatedDate ?? FindPropertyByName<PersonTypeMap>("CreatedDate");
            }
        }
        private IProperty _PersonTypeMap_Person = null;
        public IProperty PersonTypeMap_Person
        {
            get
            {
                return _PersonTypeMap_Person = _PersonTypeMap_Person ?? FindPropertyByName<PersonTypeMap>("Person");
            }
        }
        private IProperty _PersonTypeMap_Type = null;
        public IProperty PersonTypeMap_Type
        {
            get
            {
                return _PersonTypeMap_Type = _PersonTypeMap_Type ?? FindPropertyByName<PersonTypeMap>("Type");
            }
        }
        public Type TypeOf_PersonReport => typeof(PersonReport);
        private EntityConfiguration<PersonReport>_configurationFor_PersonReport = null;
        public EntityConfiguration<PersonReport>ConfigurationFor_PersonReport => _configurationFor_PersonReport = _configurationFor_PersonReport ?? Builder.EntityType<PersonReport>();
        private IProperty _PersonReport_PersonId = null;
        public IProperty PersonReport_PersonId
        {
            get
            {
                return _PersonReport_PersonId = _PersonReport_PersonId ?? FindPropertyByName<PersonReport>("PersonId");
            }
        }
        private IProperty _PersonReport_TypeId = null;
        public IProperty PersonReport_TypeId
        {
            get
            {
                return _PersonReport_TypeId = _PersonReport_TypeId ?? FindPropertyByName<PersonReport>("TypeId");
            }
        }
        private IProperty _PersonReport_Id = null;
        public IProperty PersonReport_Id
        {
            get
            {
                return _PersonReport_Id = _PersonReport_Id ?? FindPropertyByName<PersonReport>("Id");
            }
        }
        private IProperty _PersonReport_CreatedByUserId = null;
        public IProperty PersonReport_CreatedByUserId
        {
            get
            {
                return _PersonReport_CreatedByUserId = _PersonReport_CreatedByUserId ?? FindPropertyByName<PersonReport>("CreatedByUserId");
            }
        }
        private IProperty _PersonReport_Title = null;
        public IProperty PersonReport_Title
        {
            get
            {
                return _PersonReport_Title = _PersonReport_Title ?? FindPropertyByName<PersonReport>("Title");
            }
        }
        private IProperty _PersonReport_Status = null;
        public IProperty PersonReport_Status
        {
            get
            {
                return _PersonReport_Status = _PersonReport_Status ?? FindPropertyByName<PersonReport>("Status");
            }
        }
        private IProperty _PersonReport_Guid = null;
        public IProperty PersonReport_Guid
        {
            get
            {
                return _PersonReport_Guid = _PersonReport_Guid ?? FindPropertyByName<PersonReport>("Guid");
            }
        }
        private IProperty _PersonReport_CreatedDate = null;
        public IProperty PersonReport_CreatedDate
        {
            get
            {
                return _PersonReport_CreatedDate = _PersonReport_CreatedDate ?? FindPropertyByName<PersonReport>("CreatedDate");
            }
        }
        private IProperty _PersonReport_RevisionKey = null;
        public IProperty PersonReport_RevisionKey
        {
            get
            {
                return _PersonReport_RevisionKey = _PersonReport_RevisionKey ?? FindPropertyByName<PersonReport>("RevisionKey");
            }
        }
        private IProperty _PersonReport_PersistenceKey = null;
        public IProperty PersonReport_PersistenceKey
        {
            get
            {
                return _PersonReport_PersistenceKey = _PersonReport_PersistenceKey ?? FindPropertyByName<PersonReport>("PersistenceKey");
            }
        }
        private IProperty _PersonReport_ActionsTaken = null;
        public IProperty PersonReport_ActionsTaken
        {
            get
            {
                return _PersonReport_ActionsTaken = _PersonReport_ActionsTaken ?? FindPropertyByName<PersonReport>("ActionsTaken");
            }
        }
        private IProperty _PersonReport_Recommendations = null;
        public IProperty PersonReport_Recommendations
        {
            get
            {
                return _PersonReport_Recommendations = _PersonReport_Recommendations ?? FindPropertyByName<PersonReport>("Recommendations");
            }
        }
        private IProperty _PersonReport_Person = null;
        public IProperty PersonReport_Person
        {
            get
            {
                return _PersonReport_Person = _PersonReport_Person ?? FindPropertyByName<PersonReport>("Person");
            }
        }
        private IProperty _PersonReport_Type = null;
        public IProperty PersonReport_Type
        {
            get
            {
                return _PersonReport_Type = _PersonReport_Type ?? FindPropertyByName<PersonReport>("Type");
            }
        }
        private IProperty _PersonReport_CreatedByUser = null;
        public IProperty PersonReport_CreatedByUser
        {
            get
            {
                return _PersonReport_CreatedByUser = _PersonReport_CreatedByUser ?? FindPropertyByName<PersonReport>("CreatedByUser");
            }
        }
        public Type TypeOf_Site => typeof(Site);
        private EntityConfiguration<Site>_configurationFor_Site = null;
        public EntityConfiguration<Site>ConfigurationFor_Site => _configurationFor_Site = _configurationFor_Site ?? Builder.EntityType<Site>();
        private IProperty _Site_Id = null;
        public IProperty Site_Id
        {
            get
            {
                return _Site_Id = _Site_Id ?? FindPropertyByName<Site>("Id");
            }
        }
        private IProperty _Site_Location = null;
        public IProperty Site_Location
        {
            get
            {
                return _Site_Location = _Site_Location ?? FindPropertyByName<Site>("Location");
            }
        }
        private IProperty _Site_Area = null;
        public IProperty Site_Area
        {
            get
            {
                return _Site_Area = _Site_Area ?? FindPropertyByName<Site>("Area");
            }
        }
        private IProperty _Site_Line = null;
        public IProperty Site_Line
        {
            get
            {
                return _Site_Line = _Site_Line ?? FindPropertyByName<Site>("Line");
            }
        }
        private IProperty _Site_ParentId = null;
        public IProperty Site_ParentId
        {
            get
            {
                return _Site_ParentId = _Site_ParentId ?? FindPropertyByName<Site>("ParentId");
            }
        }
        private IProperty _Site_ClientId = null;
        public IProperty Site_ClientId
        {
            get
            {
                return _Site_ClientId = _Site_ClientId ?? FindPropertyByName<Site>("ClientId");
            }
        }
        private IProperty _Site_CreatedByUserId = null;
        public IProperty Site_CreatedByUserId
        {
            get
            {
                return _Site_CreatedByUserId = _Site_CreatedByUserId ?? FindPropertyByName<Site>("CreatedByUserId");
            }
        }
        private IProperty _Site_FullAddress = null;
        public IProperty Site_FullAddress
        {
            get
            {
                return _Site_FullAddress = _Site_FullAddress ?? FindPropertyByName<Site>("FullAddress");
            }
        }
        private IProperty _Site_Address = null;
        public IProperty Site_Address
        {
            get
            {
                return _Site_Address = _Site_Address ?? FindPropertyByName<Site>("Address");
            }
        }
        private IProperty _Site_PostCode = null;
        public IProperty Site_PostCode
        {
            get
            {
                return _Site_PostCode = _Site_PostCode ?? FindPropertyByName<Site>("PostCode");
            }
        }
        private IProperty _Site_Key = null;
        public IProperty Site_Key
        {
            get
            {
                return _Site_Key = _Site_Key ?? FindPropertyByName<Site>("Key");
            }
        }
        private IProperty _Site_Name = null;
        public IProperty Site_Name
        {
            get
            {
                return _Site_Name = _Site_Name ?? FindPropertyByName<Site>("Name");
            }
        }
        private IProperty _Site_LeftOf = null;
        public IProperty Site_LeftOf
        {
            get
            {
                return _Site_LeftOf = _Site_LeftOf ?? FindPropertyByName<Site>("LeftOf");
            }
        }
        private IProperty _Site_RightOf = null;
        public IProperty Site_RightOf
        {
            get
            {
                return _Site_RightOf = _Site_RightOf ?? FindPropertyByName<Site>("RightOf");
            }
        }
        private IProperty _Site_Level = null;
        public IProperty Site_Level
        {
            get
            {
                return _Site_Level = _Site_Level ?? FindPropertyByName<Site>("Level");
            }
        }
        private IProperty _Site_Left = null;
        public IProperty Site_Left
        {
            get
            {
                return _Site_Left = _Site_Left ?? FindPropertyByName<Site>("Left");
            }
        }
        private IProperty _Site_Right = null;
        public IProperty Site_Right
        {
            get
            {
                return _Site_Right = _Site_Right ?? FindPropertyByName<Site>("Right");
            }
        }
        private IProperty _Site_Guid = null;
        public IProperty Site_Guid
        {
            get
            {
                return _Site_Guid = _Site_Guid ?? FindPropertyByName<Site>("Guid");
            }
        }
        private IProperty _Site_CreatedDate = null;
        public IProperty Site_CreatedDate
        {
            get
            {
                return _Site_CreatedDate = _Site_CreatedDate ?? FindPropertyByName<Site>("CreatedDate");
            }
        }
        private IProperty _Site_RevisionKey = null;
        public IProperty Site_RevisionKey
        {
            get
            {
                return _Site_RevisionKey = _Site_RevisionKey ?? FindPropertyByName<Site>("RevisionKey");
            }
        }
        private IProperty _Site_PersistenceKey = null;
        public IProperty Site_PersistenceKey
        {
            get
            {
                return _Site_PersistenceKey = _Site_PersistenceKey ?? FindPropertyByName<Site>("PersistenceKey");
            }
        }
        private IProperty _Site_Documents = null;
        public IProperty Site_Documents
        {
            get
            {
                return _Site_Documents = _Site_Documents ?? FindPropertyByName<Site>("Documents");
            }
        }
        private IProperty _Site_AdditionalSendReportsTo = null;
        public IProperty Site_AdditionalSendReportsTo
        {
            get
            {
                return _Site_AdditionalSendReportsTo = _Site_AdditionalSendReportsTo ?? FindPropertyByName<Site>("AdditionalSendReportsTo");
            }
        }
        private IProperty _Site_People = null;
        public IProperty Site_People
        {
            get
            {
                return _Site_People = _Site_People ?? FindPropertyByName<Site>("People");
            }
        }
        private IProperty _Site_Parent = null;
        public IProperty Site_Parent
        {
            get
            {
                return _Site_Parent = _Site_Parent ?? FindPropertyByName<Site>("Parent");
            }
        }
        private IProperty _Site_Children = null;
        public IProperty Site_Children
        {
            get
            {
                return _Site_Children = _Site_Children ?? FindPropertyByName<Site>("Children");
            }
        }
        private IProperty _Site_Client = null;
        public IProperty Site_Client
        {
            get
            {
                return _Site_Client = _Site_Client ?? FindPropertyByName<Site>("Client");
            }
        }
        private IProperty _Site_CreatedByUser = null;
        public IProperty Site_CreatedByUser
        {
            get
            {
                return _Site_CreatedByUser = _Site_CreatedByUser ?? FindPropertyByName<Site>("CreatedByUser");
            }
        }
        private IProperty _Site_Areas = null;
        public IProperty Site_Areas
        {
            get
            {
                return _Site_Areas = _Site_Areas ?? FindPropertyByName<Site>("Areas");
            }
        }
        private IProperty _Site_SiteInspections = null;
        public IProperty Site_SiteInspections
        {
            get
            {
                return _Site_SiteInspections = _Site_SiteInspections ?? FindPropertyByName<Site>("SiteInspections");
            }
        }
        private IProperty _Site_Users = null;
        public IProperty Site_Users
        {
            get
            {
                return _Site_Users = _Site_Users ?? FindPropertyByName<Site>("Users");
            }
        }
        public Type TypeOf_SiteArea => typeof(SiteArea);
        private EntityConfiguration<SiteArea>_configurationFor_SiteArea = null;
        public EntityConfiguration<SiteArea>ConfigurationFor_SiteArea => _configurationFor_SiteArea = _configurationFor_SiteArea ?? Builder.EntityType<SiteArea>();
        private IProperty _SiteArea_Id = null;
        public IProperty SiteArea_Id
        {
            get
            {
                return _SiteArea_Id = _SiteArea_Id ?? FindPropertyByName<SiteArea>("Id");
            }
        }
        private IProperty _SiteArea_SiteId = null;
        public IProperty SiteArea_SiteId
        {
            get
            {
                return _SiteArea_SiteId = _SiteArea_SiteId ?? FindPropertyByName<SiteArea>("SiteId");
            }
        }
        private IProperty _SiteArea_CreatedByUserId = null;
        public IProperty SiteArea_CreatedByUserId
        {
            get
            {
                return _SiteArea_CreatedByUserId = _SiteArea_CreatedByUserId ?? FindPropertyByName<SiteArea>("CreatedByUserId");
            }
        }
        private IProperty _SiteArea_Guid = null;
        public IProperty SiteArea_Guid
        {
            get
            {
                return _SiteArea_Guid = _SiteArea_Guid ?? FindPropertyByName<SiteArea>("Guid");
            }
        }
        private IProperty _SiteArea_CreatedDate = null;
        public IProperty SiteArea_CreatedDate
        {
            get
            {
                return _SiteArea_CreatedDate = _SiteArea_CreatedDate ?? FindPropertyByName<SiteArea>("CreatedDate");
            }
        }
        private IProperty _SiteArea_RevisionKey = null;
        public IProperty SiteArea_RevisionKey
        {
            get
            {
                return _SiteArea_RevisionKey = _SiteArea_RevisionKey ?? FindPropertyByName<SiteArea>("RevisionKey");
            }
        }
        private IProperty _SiteArea_PersistenceKey = null;
        public IProperty SiteArea_PersistenceKey
        {
            get
            {
                return _SiteArea_PersistenceKey = _SiteArea_PersistenceKey ?? FindPropertyByName<SiteArea>("PersistenceKey");
            }
        }
        private IProperty _SiteArea_People = null;
        public IProperty SiteArea_People
        {
            get
            {
                return _SiteArea_People = _SiteArea_People ?? FindPropertyByName<SiteArea>("People");
            }
        }
        private IProperty _SiteArea_Site = null;
        public IProperty SiteArea_Site
        {
            get
            {
                return _SiteArea_Site = _SiteArea_Site ?? FindPropertyByName<SiteArea>("Site");
            }
        }
        private IProperty _SiteArea_CreatedByUser = null;
        public IProperty SiteArea_CreatedByUser
        {
            get
            {
                return _SiteArea_CreatedByUser = _SiteArea_CreatedByUser ?? FindPropertyByName<SiteArea>("CreatedByUser");
            }
        }
        public Type TypeOf_SiteInspection => typeof(SiteInspection);
        private EntityConfiguration<SiteInspection>_configurationFor_SiteInspection = null;
        public EntityConfiguration<SiteInspection>ConfigurationFor_SiteInspection => _configurationFor_SiteInspection = _configurationFor_SiteInspection ?? Builder.EntityType<SiteInspection>();
        private IProperty _SiteInspection_Id = null;
        public IProperty SiteInspection_Id
        {
            get
            {
                return _SiteInspection_Id = _SiteInspection_Id ?? FindPropertyByName<SiteInspection>("Id");
            }
        }
        private IProperty _SiteInspection_SiteId = null;
        public IProperty SiteInspection_SiteId
        {
            get
            {
                return _SiteInspection_SiteId = _SiteInspection_SiteId ?? FindPropertyByName<SiteInspection>("SiteId");
            }
        }
        private IProperty _SiteInspection_CreatedByUserId = null;
        public IProperty SiteInspection_CreatedByUserId
        {
            get
            {
                return _SiteInspection_CreatedByUserId = _SiteInspection_CreatedByUserId ?? FindPropertyByName<SiteInspection>("CreatedByUserId");
            }
        }
        private IProperty _SiteInspection_StartTime = null;
        public IProperty SiteInspection_StartTime
        {
            get
            {
                return _SiteInspection_StartTime = _SiteInspection_StartTime ?? FindPropertyByName<SiteInspection>("StartTime");
            }
        }
        private IProperty _SiteInspection_EndTime = null;
        public IProperty SiteInspection_EndTime
        {
            get
            {
                return _SiteInspection_EndTime = _SiteInspection_EndTime ?? FindPropertyByName<SiteInspection>("EndTime");
            }
        }
        private IProperty _SiteInspection_Guid = null;
        public IProperty SiteInspection_Guid
        {
            get
            {
                return _SiteInspection_Guid = _SiteInspection_Guid ?? FindPropertyByName<SiteInspection>("Guid");
            }
        }
        private IProperty _SiteInspection_CreatedDate = null;
        public IProperty SiteInspection_CreatedDate
        {
            get
            {
                return _SiteInspection_CreatedDate = _SiteInspection_CreatedDate ?? FindPropertyByName<SiteInspection>("CreatedDate");
            }
        }
        private IProperty _SiteInspection_RevisionKey = null;
        public IProperty SiteInspection_RevisionKey
        {
            get
            {
                return _SiteInspection_RevisionKey = _SiteInspection_RevisionKey ?? FindPropertyByName<SiteInspection>("RevisionKey");
            }
        }
        private IProperty _SiteInspection_PersistenceKey = null;
        public IProperty SiteInspection_PersistenceKey
        {
            get
            {
                return _SiteInspection_PersistenceKey = _SiteInspection_PersistenceKey ?? FindPropertyByName<SiteInspection>("PersistenceKey");
            }
        }
        private IProperty _SiteInspection_RiskAssessments = null;
        public IProperty SiteInspection_RiskAssessments
        {
            get
            {
                return _SiteInspection_RiskAssessments = _SiteInspection_RiskAssessments ?? FindPropertyByName<SiteInspection>("RiskAssessments");
            }
        }
        private IProperty _SiteInspection_PersonInspections = null;
        public IProperty SiteInspection_PersonInspections
        {
            get
            {
                return _SiteInspection_PersonInspections = _SiteInspection_PersonInspections ?? FindPropertyByName<SiteInspection>("PersonInspections");
            }
        }
        private IProperty _SiteInspection_Site = null;
        public IProperty SiteInspection_Site
        {
            get
            {
                return _SiteInspection_Site = _SiteInspection_Site ?? FindPropertyByName<SiteInspection>("Site");
            }
        }
        private IProperty _SiteInspection_CreatedByUser = null;
        public IProperty SiteInspection_CreatedByUser
        {
            get
            {
                return _SiteInspection_CreatedByUser = _SiteInspection_CreatedByUser ?? FindPropertyByName<SiteInspection>("CreatedByUser");
            }
        }
        public Type TypeOf_UserSetting => typeof(UserSetting);
        private EntityConfiguration<UserSetting>_configurationFor_UserSetting = null;
        public EntityConfiguration<UserSetting>ConfigurationFor_UserSetting => _configurationFor_UserSetting = _configurationFor_UserSetting ?? Builder.EntityType<UserSetting>();
        private IProperty _UserSetting_CreatedByUserId = null;
        public IProperty UserSetting_CreatedByUserId
        {
            get
            {
                return _UserSetting_CreatedByUserId = _UserSetting_CreatedByUserId ?? FindPropertyByName<UserSetting>("CreatedByUserId");
            }
        }
        private IProperty _UserSetting_Key1 = null;
        public IProperty UserSetting_Key1
        {
            get
            {
                return _UserSetting_Key1 = _UserSetting_Key1 ?? FindPropertyByName<UserSetting>("Key1");
            }
        }
        private IProperty _UserSetting_Id = null;
        public IProperty UserSetting_Id
        {
            get
            {
                return _UserSetting_Id = _UserSetting_Id ?? FindPropertyByName<UserSetting>("Id");
            }
        }
        private IProperty _UserSetting_UserId = null;
        public IProperty UserSetting_UserId
        {
            get
            {
                return _UserSetting_UserId = _UserSetting_UserId ?? FindPropertyByName<UserSetting>("UserId");
            }
        }
        private IProperty _UserSetting_Key2 = null;
        public IProperty UserSetting_Key2
        {
            get
            {
                return _UserSetting_Key2 = _UserSetting_Key2 ?? FindPropertyByName<UserSetting>("Key2");
            }
        }
        private IProperty _UserSetting_Key3 = null;
        public IProperty UserSetting_Key3
        {
            get
            {
                return _UserSetting_Key3 = _UserSetting_Key3 ?? FindPropertyByName<UserSetting>("Key3");
            }
        }
        private IProperty _UserSetting_Key4 = null;
        public IProperty UserSetting_Key4
        {
            get
            {
                return _UserSetting_Key4 = _UserSetting_Key4 ?? FindPropertyByName<UserSetting>("Key4");
            }
        }
        private IProperty _UserSetting_Value = null;
        public IProperty UserSetting_Value
        {
            get
            {
                return _UserSetting_Value = _UserSetting_Value ?? FindPropertyByName<UserSetting>("Value");
            }
        }
        private IProperty _UserSetting_CreatedDate = null;
        public IProperty UserSetting_CreatedDate
        {
            get
            {
                return _UserSetting_CreatedDate = _UserSetting_CreatedDate ?? FindPropertyByName<UserSetting>("CreatedDate");
            }
        }
        private IProperty _UserSetting_RevisionKey = null;
        public IProperty UserSetting_RevisionKey
        {
            get
            {
                return _UserSetting_RevisionKey = _UserSetting_RevisionKey ?? FindPropertyByName<UserSetting>("RevisionKey");
            }
        }
        private IProperty _UserSetting_PersistenceKey = null;
        public IProperty UserSetting_PersistenceKey
        {
            get
            {
                return _UserSetting_PersistenceKey = _UserSetting_PersistenceKey ?? FindPropertyByName<UserSetting>("PersistenceKey");
            }
        }
        private IProperty _UserSetting_CreatedByUser = null;
        public IProperty UserSetting_CreatedByUser
        {
            get
            {
                return _UserSetting_CreatedByUser = _UserSetting_CreatedByUser ?? FindPropertyByName<UserSetting>("CreatedByUser");
            }
        }
        private IProperty _UserSetting_User = null;
        public IProperty UserSetting_User
        {
            get
            {
                return _UserSetting_User = _UserSetting_User ?? FindPropertyByName<UserSetting>("User");
            }
        }
        public Type TypeOf_UserSite => typeof(UserSite);
        private EntityConfiguration<UserSite>_configurationFor_UserSite = null;
        public EntityConfiguration<UserSite>ConfigurationFor_UserSite => _configurationFor_UserSite = _configurationFor_UserSite ?? Builder.EntityType<UserSite>();
        private IProperty _UserSite_SiteId = null;
        public IProperty UserSite_SiteId
        {
            get
            {
                return _UserSite_SiteId = _UserSite_SiteId ?? FindPropertyByName<UserSite>("SiteId");
            }
        }
        private IProperty _UserSite_UserId = null;
        public IProperty UserSite_UserId
        {
            get
            {
                return _UserSite_UserId = _UserSite_UserId ?? FindPropertyByName<UserSite>("UserId");
            }
        }
        private IProperty _UserSite_User = null;
        public IProperty UserSite_User
        {
            get
            {
                return _UserSite_User = _UserSite_User ?? FindPropertyByName<UserSite>("User");
            }
        }
        private IProperty _UserSite_Site = null;
        public IProperty UserSite_Site
        {
            get
            {
                return _UserSite_Site = _UserSite_Site ?? FindPropertyByName<UserSite>("Site");
            }
        }
    }
}