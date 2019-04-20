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
        }
    }
}