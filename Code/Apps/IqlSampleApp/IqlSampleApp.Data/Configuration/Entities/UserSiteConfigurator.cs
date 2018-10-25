using Brandless.AspNetCore.OData.Extensions.Configuration;
using Microsoft.AspNet.OData.Builder;
using Tunnel.App.Data.Entities;

namespace Tunnel.App.Web.OData.Configuration.Entities
{
    public class UserSiteConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<UserSite>()
                .HasKey(k => new {k.SiteId, k.UserId});
            builder
                .EntityType<UserSite>()
                .HasRequired(d => d.User, (site, user) => site.UserId == user.Id, user => user.Sites);
            builder
                .EntityType<UserSite>()
                .HasRequired(d => d.Site, (userSite, site) => userSite.SiteId == site.Id, site => site.Users);
        }
    }
}