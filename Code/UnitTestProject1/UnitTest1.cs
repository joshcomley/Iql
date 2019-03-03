using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Brandless.AspNetCore.OData.Extensions;
using Brandless.AspNetCore.OData.Extensions.Binding;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Brandless.Data.EntityFramework.Crud;
using Iql.Conversion;
using Iql.Data;
using Iql.Data.Evaluation;
using Iql.Data.Extensions;
using Iql.DotNet;
using Iql.Entities;
using Iql.Entities.InferredValues;
using Iql.Entities.Services;
using Iql.Server.OData.Net;
using IqlSampleApp.Data;
using IqlSampleApp.Data.Contracts;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Iql.Server;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.EntityFrameworkCore;
using IAssemblyProvider = Iql.Server.IAssemblyProvider;

namespace Iql.Tests.Server
{
    public class TestAssemblyProvider : IAssemblyProvider
    {
        public IEnumerable<Assembly> CandidateAssemblies => new[] {typeof(IIqlSampleAppService).Assembly};
    }
    public class TestServiceProvider : IServiceProviderProvider
    {
        public IqlServiceProvider ServiceProvider { get; } = new IqlServiceProvider();
    }
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
            var host = WebHost.CreateDefaultBuilder()
                    .Configure(app =>
                    {
                        var model = ApplicationDbContext.Build(app.ApplicationServices);
                        app.UseIql<IIqlSampleAppService>(config =>
                        {
                            config.ConfigureFromOData<IIqlSampleAppService>(model.ModelBuilder);
                        });
                    })
                    .ConfigureServices(services =>
                    {
                        services.AddSingleton<IEdmModelAccessor>(new EdmModelAccessor());
                        services.AddIql();
                        services.AddSingleton<IAssemblyProvider>(new TestAssemblyProvider());
                        services.AddOData();
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            // Configure the context to use Microsoft SQL Server.
                            options
                                .UseSqlServer(ApplicationDbContext.ConnectionString, _ =>
                                {
                                    _.UseNetTopologySuite();
                                    _.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                                });
                        });
                    })
                    .Build()
                ;
            host.Start();
            var builder = host.Services.GetService<IEntityConfigurationProvider>().Get<IIqlSampleAppService>();
            var entityConfiguration = builder.EntityType<UserSetting>();
            var crudBase = new CrudBase<ApplicationDbContext, ApplicationDbContext, UserSetting>(host.Services);
            var crudManager = new CrudManager(crudBase.Secured.Context);
            var serverEvaluator = new IqlServerEvaluator(crudManager, true);
            var dbObject = new UserSetting
            {
                Key1 = "Abc",
                Key2 = "Def",
                Value = "Fish"
            };
            Assert.AreEqual(default(DateTimeOffset), dbObject.CreatedDate);
            var clone = (UserSetting)dbObject.Clone(builder, typeof(UserSetting), RelationshipCloneMode.DoNotClone, (Dictionary<object, object>)null, (Dictionary<object, object>)null);
            var inferredValuesResult = await entityConfiguration.TrySetInferredValuesAsync(
                clone, 
                dbObject, 
                serverEvaluator, 
                ResolveServiceProviderProvider());
            Assert.AreNotEqual(default(DateTimeOffset), dbObject.CreatedDate);
        }

        private IServiceProviderProvider ResolveServiceProviderProvider()
        {
            return new TestServiceProvider();
        }
    }
}
