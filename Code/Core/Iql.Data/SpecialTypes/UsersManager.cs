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
        }
    }
}