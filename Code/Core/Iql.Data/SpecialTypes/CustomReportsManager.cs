using System;
using Iql.Data.Context;
using Iql.Entities.SpecialTypes;

namespace Iql.Data.SpecialTypes
{
    public class CustomReportsManager : SpecialTypeManager<CustomReportsDefinition, IqlCustomReport, Guid>
    {
        public CustomReportsManager(IDataContext dataContext) :
            base(dataContext, dataContext.EntityConfigurationContext.CustomReportsDefinition)
        {
        }
    }
}