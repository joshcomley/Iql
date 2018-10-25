using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Brandless.Data.Entities;
using Microsoft.AspNet.OData.Builder;
using Tunnel.App.Data.Entities;

namespace Tunnel.App.Web.OData.Configuration.Entities
{
    public class UserConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            var user = builder.EntityType<ApplicationUser>();
            user.Ignore(p => p.NormalizedEmail);
            user.Ignore(p => p.NormalizedUserName);
            user.Ignore(p => p.ConcurrencyStamp);
            user.Ignore(p => p.SecurityStamp);
            user.Ignore(p => p.PasswordHash);
            user.Ignore(p => p.AccessFailedCount);
            ;
            user
                .HasOptional(
                    c => c.Client,
                    (applicationUser, client) => applicationUser.ClientId == client.Id,
                    c => c.Users);
            //user
            //    .HasKey(p => p.Id);
            ApplyCreatedByUser(builder, p => p.ClientsCreated);
            ApplyCreatedByUser(builder, p => p.PeopleCreated);
            ApplyCreatedByUser(builder, p => p.ProjectCreated);
            ApplyCreatedByUser(builder, p => p.PersonTypesCreated);
            ApplyCreatedByUser(builder, p => p.DocumentCategoriesCreated);
            ApplyCreatedByUser(builder, p => p.SiteDocumentsCreated);
            ApplyCreatedByUser(builder, p => p.FaultActionsTakenCreated);
            ApplyCreatedByUser(builder, p => p.FaultCategoriesCreated);
            ApplyCreatedByUser(builder, p => p.FaultTypesCreated);
            ApplyCreatedByUser(builder, p => p.FaultReportsCreated);
            ApplyCreatedByUser(builder, p => p.FaultRecommendationsCreated);
            ApplyCreatedByUser(builder, p => p.FaultDefaultRecommendationsCreated);
            ApplyCreatedByUser(builder, p => p.ReportReceiverEmailAddressesCreated);
            ApplyCreatedByUser(builder, p => p.RiskAssessmentsCreated);
            ApplyCreatedByUser(builder, p => p.RiskAssessmentAnswersCreated);
            ApplyCreatedByUser(builder, p => p.RiskAssessmentQuestionsCreated);
            ApplyCreatedByUser(builder, p => p.PersonInspectionsCreated);
            ApplyCreatedByUser(builder, p => p.PersonLoadingsCreated);
            ApplyCreatedByUser(builder, p => p.SitesCreated);
            ApplyCreatedByUser(builder, p => p.SiteInspectionsCreated);
        }

        private static void ApplyCreatedByUser<T>(ODataModelBuilder builder,
            Expression<Func<ApplicationUser, IEnumerable<T>>> partnerExpression)
            where T : class, ICreatedBy<ApplicationUser>
        {
            builder.EntityType<T>()
                .Ignore(p => p.CreatedByUserId)
                ;
            builder.EntityType<T>()
                .HasOptional(
                    c => c.CreatedByUser,
                    (entity, user) => entity.CreatedByUserId == user.Id,
                    partnerExpression)
                ;
        }
    }
}