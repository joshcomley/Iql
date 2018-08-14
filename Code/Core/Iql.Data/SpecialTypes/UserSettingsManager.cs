using System;
using Iql.Data.Context;
using Iql.Entities.SpecialTypes;

namespace Iql.Data.SpecialTypes
{
    public class UserSettingsManager : SpecialTypeManager<UserSettingsDefinition, IqlUserSetting, Guid>
    {
        public UserSettingsManager(IDataContext dataContext) 
            : base(dataContext, dataContext.EntityConfigurationContext.UserSettingsDefinition)
        {
            if (dataContext.EntityConfigurationContext.EntityType<IqlUserSetting>().Properties.Count == 0)
            {
                var entityConfiguration = dataContext.EntityConfigurationContext.EntityType<IqlUserSetting>();
                entityConfiguration
                    .DefineProperty(_ => _.Id, true, IqlType.Guid)
                    .DefineProperty(_ => _.UserId, true, IqlType.String)
                    .DefineProperty(_ => _.Key1, false, IqlType.String)
                    .DefineProperty(_ => _.Key2, true, IqlType.String)
                    .DefineProperty(_ => _.Key3, true, IqlType.String)
                    .DefineProperty(_ => _.Key4, true, IqlType.String)
                    .DefineProperty(_ => _.Value, true, IqlType.String)
                    .HasKey(_ => _.Id)
                    ;
            }
        }
    }
}