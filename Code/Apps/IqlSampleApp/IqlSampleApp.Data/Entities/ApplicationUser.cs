using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace IqlSampleApp.Data.Entities
{
    public class ApplicationUser : IdentityUser, Bases.IDbObject<string>
    {
        //public List<Person> PeopleCreatedBy { get; set; }
        //[Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        public override string Email { get; set; }

        public virtual UserPermissions Permissions { get; set; }
        public List<UserSite> Sites { get; set; }
        public List<PersonReport> FaultReportsCreated { get; set; }
        public List<ReportRecommendation> FaultRecommendationsCreated { get; set; }
        public List<ReportDefaultRecommendation> FaultDefaultRecommendationsCreated { get; set; }
        public List<Client> ClientsCreated { get; set; }
        public List<Person> PeopleCreated { get; set; }
        public List<PersonInspection> PersonInspectionsCreated { get; set; }
        public List<SiteInspection> SiteInspectionsCreated { get; set; }
        public List<Project> ProjectCreated { get; set; }
        public List<PersonType> PersonTypesCreated { get; set; }
        public List<DocumentCategory> DocumentCategoriesCreated { get; set; }
        public List<SiteDocument> SiteDocumentsCreated { get; set; }
        public List<ReportActionsTaken> FaultActionsTakenCreated { get; set; }
        public List<ReportType> FaultTypesCreated { get; set; }
        public List<ReportCategory> FaultCategoriesCreated { get; set; }
        public List<ReportReceiverEmailAddress> ReportReceiverEmailAddressesCreated { get; set; }
        public List<RiskAssessment> RiskAssessmentsCreated { get; set; }
        public List<RiskAssessmentAnswer> RiskAssessmentAnswersCreated { get; set; }
        public List<RiskAssessmentSolution> RiskAssessmentSolutionsCreated { get; set; }
        public List<RiskAssessmentQuestion> RiskAssessmentQuestionsCreated { get; set; }
        public List<PersonLoading> PersonLoadingsCreated { get; set; }
        public List<Site> SitesCreated { get; set; }
        public List<SiteArea> SiteAreasCreated { get; set; }
        public List<UserSetting> UserSettingsCreated { get; set; }
        public List<UserSetting> UserSettings { get; set; }

        //public override string NormalizedEmail { get; set; }
        [Required(ErrorMessage = "Please select a user type")]
        public UserType UserType { get; set; }

        public Client Client { get; set; }
        public int? ClientId { get; set; }

        [Required(ErrorMessage = "Please provide a name")]
        public string FullName { get; set; }

        public ApplicationUser CreatedByUser { get; set; }
        public string CreatedByUserId { get; set; }
        //public Guid Guid { get; }

        [NotMapped]
        public bool IsLockedOut
        {
            get { return LockoutEnabled && LockoutEnd > DateTime.Now; }
            set { }
        }

        [NotMapped]
        public bool IsEnabled
        {
            get { return !IsLockedOut; }
            set { }
        }
    }
}
