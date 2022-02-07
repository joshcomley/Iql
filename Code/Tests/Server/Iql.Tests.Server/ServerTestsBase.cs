using System;
using System.Linq;
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
using IqlSampleApp.Data.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Server
{
    [TestClass]
    public class ServerTestsBase<T>
        where T : class
    {
        public static IWebHost Host { get; set; }
        public static bool BootedUp { get; set; }
        public static IEntityConfigurationBuilder Builder { get; set; }
        private Client _client;

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
                            // services.AddOData();
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
                        .UseUrls($"http://127.0.0.1:{HostPort.Next()}")
                        .Build()
                    ;
                Host.Start();
                Builder = Host.Services.GetService<IEntityConfigurationProvider>().Get<IIqlSampleAppService>();
                var context = Host.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }
        }

        [ClassCleanup]
        public void Close()
        {
            Host.StopAsync().Wait();
        }

        public Client Client
        {
            get
            {
                if (_client == null)
                {
                    var db = new ApplicationDbContext();
                    var clientType = db.ClientTypes.FirstOrDefault(_ => _.Name == "TestClientType");
                    if (clientType == null)
                    {
                        clientType = new ClientType { Name = "TestClientType", Id = 7829 };
                        db.ClientTypes.Add(clientType);
                        db.SaveChanges();
                    }
                    _client = db.Clients.FirstOrDefault(_ => _.Name == "Test client");
                    if (_client == null)
                    {
                        _client = new Client
                        {
                            Name = "Test client",
                            Guid = Guid.NewGuid(),
                            PersistenceKey = Guid.NewGuid(),
                            CreatedDate = DateTime.Now,
                            AverageIncome = 0,
                            AverageSales = 0,
                            Category = 1,
                            Discount = 0,
                            TypeId = clientType.Id
                        };
                        db.Clients.Add(_client);
                        var count = db.SaveChanges();
                    }
                }

                return _client;
            }
        }

        public FakeControllerContext<T> ControllerContext(Action<FakeControllerContext<T>> action = null)
        {
            var context = new FakeControllerContext<T>(Host.Services);
            action?.Invoke(context);
            return context;
        }

        public FakeControllerContext<TController> ControllerContext<TController>(Action<FakeControllerContext<TController>> action = null)
            where TController: class
        {
            var context = new FakeControllerContext<TController>(Host.Services);
            action?.Invoke(context);
            return context;
        }

        public async Task<FakeControllerContext<T>> ControllerContextAsync(Func<FakeControllerContext<T>, Task> action = null)
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
            var controllerContext = ControllerContext();
            var context = new IqlHttpServiceProviderContext(
                null,
                controllerContext.EntityConfiguration.Builder,
                null,
                controllerContext.ServerEvaluator,
                null);
            context.CurrentUserService = new ServerTestCurrentUserService(context);
            return new IqlHttpServiceProviderProvider<ApplicationUser>(context);
        }
    }
}