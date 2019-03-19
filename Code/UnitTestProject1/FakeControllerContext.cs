using System;
using Brandless.Data.EntityFramework.Crud;
using Iql.Entities;
using Iql.Server;
using Iql.Server.OData.Net;
using IqlSampleApp.Data;
using IqlSampleApp.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Iql.Tests.Server
{
    public class FakeControllerContext<T>
        where T : class
    {
        public IqlServerEvaluator ServerEvaluator { get; set; }
        public CrudManager CrudManager { get; set; }
        public CrudBase<ApplicationDbContext, ApplicationDbContext, T> CrudBase { get; set; }
        public EntityConfiguration<T> EntityConfiguration { get; set; }

        public FakeControllerContext(IServiceProvider services, bool forNewEntity)
        {
            EntityConfiguration = services.GetService<IEntityConfigurationProvider>().Get<IIqlSampleAppService>().EntityType<T>();
            CrudBase = new CrudBase<ApplicationDbContext, ApplicationDbContext, T>(services);
            CrudManager = new CrudManager(CrudBase.Secured.Context);
            ServerEvaluator = new IqlServerEvaluator(CrudManager, forNewEntity);
        }
    }
}