using System;
using Iql.Data.Context;
using Iql.Entities.SpecialTypes;

namespace Iql.Data.SpecialTypes
{
    public class UsersManager : SpecialTypeManager<UsersDefinition, IqlUser, Guid>
    {
        public UsersManager(IDataContext dataContext) :
            base(dataContext, dataContext.EntityConfigurationContext.UsersDefinition)
        {
            if (dataContext.EntityConfigurationContext.EntityType<IqlUser>().Properties.Count == 0)
            {
                var entityConfiguration = dataContext.EntityConfigurationContext.EntityType<IqlUser>();
                entityConfiguration
                    .DefineProperty(_ => _.Id, true, IqlType.Guid)
                    .DefineProperty(_ => _.Name, false, IqlType.String)
                    .HasKey(_ => _.Id)
                    ;
            }
        }
    }
}