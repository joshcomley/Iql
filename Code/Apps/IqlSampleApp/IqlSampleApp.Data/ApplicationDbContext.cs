using System;
using System.Linq;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Brandless.AspNetCore.OData.Extensions.Extensions;
using IqlSampleApp.Data.Contracts;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IqlSampleApp.Data
{
    /// <summary>
    ///     Unsecured implementation of ApplicationDbContext
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,
        IIqlSampleAppService
    {
        public static bool UseSqlite = false;

        private static bool _databaseChecked;
        private readonly IServiceProvider _serviceProvider;

        // Uncomment this when running migrations
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ApplicationDbContext(IServiceProvider serviceProvider, DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _serviceProvider = serviceProvider;
            // TODO: #639
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<ApplicationLog> ApplicationLogs { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<DocumentCategory> DocumentCategories { get; set; }
        public DbSet<ReportActionsTaken> ReportActionsTaken { get; set; }
        public DbSet<ReportCategory> ReportCategories { get; set; }
        public DbSet<ReportDefaultRecommendation> ReportDefaultRecommendations { get; set; }
        public DbSet<ReportRecommendation> ReportRecommendations { get; set; }
        public DbSet<ReportType> ReportTypes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ReportReceiverEmailAddress> ReportReceiverEmailAddresses { get; set; }
        public DbSet<RiskAssessment> RiskAssessments { get; set; }
        public DbSet<RiskAssessmentSolution> RiskAssessmentSolutions { get; set; }
        public DbSet<RiskAssessmentAnswer> RiskAssessmentAnswers { get; set; }
        public DbSet<RiskAssessmentQuestion> RiskAssessmentQuestions { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonInspection> PersonInspections { get; set; }
        public DbSet<PersonLoading> PersonLoadings { get; set; }
        public DbSet<PersonReport> PersonReports { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<PersonTypeMap> PersonTypesMap { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteArea> SiteAreas { get; set; }
        public DbSet<SiteDocument> SiteDocuments { get; set; }
        public DbSet<SiteInspection> SiteInspections { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<UserSite> UserSites { get; set; }

        IQueryable<ApplicationLog> IIqlSampleAppService.ApplicationLogs => ApplicationLogs;
        IQueryable<ApplicationUser> IIqlSampleAppService.Users => Users;
        IQueryable<Client> IIqlSampleAppService.Clients => Clients;
        IQueryable<ClientType> IIqlSampleAppService.ClientTypes => ClientTypes;
        IQueryable<DocumentCategory> IIqlSampleAppService.DocumentCategories => DocumentCategories;
        IQueryable<ReportActionsTaken> IIqlSampleAppService.ReportActionsTaken => ReportActionsTaken;
        IQueryable<ReportCategory> IIqlSampleAppService.ReportCategories => ReportCategories;
        IQueryable<ReportDefaultRecommendation> IIqlSampleAppService.ReportDefaultRecommendations => ReportDefaultRecommendations;
        IQueryable<ReportRecommendation> IIqlSampleAppService.ReportRecommendations => ReportRecommendations;
        IQueryable<ReportType> IIqlSampleAppService.ReportTypes => ReportTypes;
        IQueryable<Project> IIqlSampleAppService.Projects => Projects;
        IQueryable<ReportReceiverEmailAddress> IIqlSampleAppService.ReportReceiverEmailAddresses =>
            ReportReceiverEmailAddresses;
        IQueryable<RiskAssessment> IIqlSampleAppService.RiskAssessments => RiskAssessments;
        IQueryable<RiskAssessmentSolution> IIqlSampleAppService.RiskAssessmentSolutions => RiskAssessmentSolutions;
        IQueryable<RiskAssessmentAnswer> IIqlSampleAppService.RiskAssessmentAnswers => RiskAssessmentAnswers;
        IQueryable<RiskAssessmentQuestion> IIqlSampleAppService.RiskAssessmentQuestions => RiskAssessmentQuestions;
        IQueryable<Person> IIqlSampleAppService.People => People;
        IQueryable<PersonInspection> IIqlSampleAppService.PersonInspections => PersonInspections;
        IQueryable<PersonLoading> IIqlSampleAppService.PersonLoadings => PersonLoadings;
        IQueryable<PersonReport> IIqlSampleAppService.PersonReports => PersonReports;
        IQueryable<PersonType> IIqlSampleAppService.PersonTypes => PersonTypes;
        IQueryable<PersonTypeMap> IIqlSampleAppService.PersonTypesMap => PersonTypesMap;
        IQueryable<Site> IIqlSampleAppService.Sites => Sites;
        IQueryable<SiteArea> IIqlSampleAppService.SiteAreas => SiteAreas;
        IQueryable<SiteDocument> IIqlSampleAppService.SiteDocuments => SiteDocuments;
        IQueryable<SiteInspection> IIqlSampleAppService.SiteInspections => SiteInspections;
        IQueryable<UserSetting> IIqlSampleAppService.UserSettings => UserSettings;
        IQueryable<UserSite> IIqlSampleAppService.UserSites => UserSites;
        public const string ConnectionString = "Server=.;Database=IqlSampleApp;Integrated Security=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!UseSqlite)
            {
                optionsBuilder.UseSqlServer(ConnectionString
                    // When running migrations the connection string details don't actually matter
                    , _ => _.UseNetTopologySuite());
            }
            base.OnConfiguring(optionsBuilder);
        }

        public static ODataConfigurationResult Build(IServiceProvider serviceProvider)
        {
            if (ODataModel == null)
            {
                var model = ODataConfiguration
                    .GetEdmModel<IIqlSampleAppService, ApplicationDbContext>(
                        serviceProvider,
                        "IqlSampleApp");
                ODataModel = model;
                return model;
            }

            return ODataModel;
        }

        public static ODataConfigurationResult ODataModel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*
             * In order for OAuth to work, ensure you install:
             * AspNet.Security.OAuth.Validation
             */
            Build(_serviceProvider);

            builder.Entity<Site>().Ignore(_ => _.LeftOf).Ignore(_ => _.RightOf);

            builder.UseServiceProviderForFilters(_serviceProvider);

            builder.BuildNavigationPropertiesFromOData(ODataModel.ModelBuilder);
            //builder
            //    .Entity<Person>()
            //    .HasOne(u => u.CreatedByUser)
            //    .WithMany(c => c.Users)
            //    //.HasForeignKey(c => c.ClientId)
            //    //.HasPrincipalKey(c => c.Id)
            //    ;
            //builder
            //    .Entity<Client>()
            //    .HasMany(u => u.Users)
            //    .WithOne(c => c.Client);

            builder
                .Entity<Client>()
                .Property(c => c.TypeId)
                .HasDefaultValue(1)
                ;

            builder.Entity<Client>().HasAlternateKey(p => p.Guid);
            builder.Entity<Person>().HasAlternateKey(p => p.Guid);
            builder.Entity<Client>().Property(p => p.Guid).HasDefaultValueSql("newid()");
            builder.Entity<Person>().Property(p => p.Guid).HasDefaultValueSql("newid()");
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        // The following code creates the database and schema if they don't exist.
        // This is a temporary workaround since deploying database through EF migrations is
        // not yet supported in this release.
        // Please see this http://go.microsoft.com/fwlink/?LinkID=615859 for more information on how to do deploy the database
        // when publishing your application.
        public void EnsureDatabaseCreated()
        {
            if (!_databaseChecked)
            {
                _databaseChecked = true;
                Database.EnsureCreated();
            }
        }
    }
}
