using System;
using System.Threading.Tasks;
using Brandless.AspNetCore.OData.Extensions;
using Brandless.AspNetCore.OData.Extensions.Binding;
using Iql.Conversion;
using Iql.DotNet;
using Iql.Entities;
using Iql.Entities.Services;
using Iql.Server;
using Iql.Server.OData.Net;
using IqlSampleApp.Data;
using IqlSampleApp.Data.Contracts;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Server
{
    [TestClass]
    public class ServerTestsBase
    {
        public static IWebHost Host { get; set; }
        public static bool BootedUp { get; set; }
        public static IEntityConfigurationBuilder Builder { get; set; }

        [TestInitialize]
        public void Bootup()
        {
            if (!BootedUp)
            {
                BootedUp = true;
                IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
                Host = WebHost.CreateDefaultBuilder()
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
                Host.Start();
                Builder = Host.Services.GetService<IEntityConfigurationProvider>().Get<IIqlSampleAppService>();
            }
        }

        public FakeControllerContext<T> ControllerContext<T>(Action<FakeControllerContext<T>> action = null)
            where T : class
        {
            var context = new FakeControllerContext<T>(Host.Services);
            action?.Invoke(context);
            return context;
        }

        public async Task<FakeControllerContext<T>> ControllerContextAsync<T>(Func<FakeControllerContext<T>, Task> action = null)
            where T : class
        {
            var context = new FakeControllerContext<T>(Host.Services);
            if (action != null)
            {
                await action(context);
            }
            return context;
        }

        protected IServiceProviderProvider ResolveServiceProviderProvider()
        {
            return new TestServiceProvider();
        }
    }
}