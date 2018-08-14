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
            if (dataContext.EntityConfigurationContext.EntityType<IqlCustomReport>().Properties.Count == 0)
            {
                var entityConfiguration = dataContext.EntityConfigurationContext.EntityType<IqlCustomReport>();
                entityConfiguration
                    .DefineProperty(_ => _.Id, true, IqlType.Guid)
                    .DefineProperty(_ => _.UserId, true, IqlType.String)
                    .DefineProperty(_ => _.Name, false, IqlType.String)
                    .DefineProperty(_ => _.EntityType, true, IqlType.String)
                    .DefineProperty(_ => _.Iql, true, IqlType.String)
                    .DefineProperty(_ => _.Fields, true, IqlType.String)
                    .DefineProperty(_ => _.Sort, true, IqlType.String)
                    .DefineProperty(_ => _.SortDescending, true, IqlType.Boolean)
                    .DefineProperty(_ => _.Search, true, IqlType.String)
                    .HasKey(_ => _.Id)
                    ;
            }
        }
    }
}