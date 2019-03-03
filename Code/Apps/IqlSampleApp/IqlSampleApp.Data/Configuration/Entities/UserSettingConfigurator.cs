using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql;
using Iql.Entities;
using Iql.Entities.SpecialTypes;
using Iql.Server;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class UserSettingIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            var userSettings = builder.EntityType<UserSetting>();
            userSettings.Configure(config =>
            {
                config.ConfigureProperty(cr => cr.UserId,
                    c => { c.IsInferredWith(_ => new IqlCurrentUserIdExpression()); });
            });
            //userSettings.ManageKind = EntityManageKind.None;
            builder.UserSettingsDefinition = UserSettingsDefinition.Define(
                userSettings,
                _ => _.Id,
                _ => _.UserId,
                _ => _.Key1,
                _ => _.Key2,
                _ => _.Key3,
                _ => _.Key4,
                _ => _.Value
            );
        }
    }
    public class UserSettingODataConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            var userSettings = builder
                .EntityType<UserSetting>();
            userSettings.Property(p => p.Key1).IsRequired();
            userSettings
                .HasKey(_ => _.Id)
                .HasRequired(
                    v => v.User,
                    (report, user) => report.UserId == user.Id,
                    user => user.UserSettings
                )
                ;
        }
    }
}