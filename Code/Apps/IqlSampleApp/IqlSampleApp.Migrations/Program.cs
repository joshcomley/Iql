using Brandless.AspNetCore.OData.Extensions.Configuration;
using Brandless.Data.EntityFramework.Migration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Tunnel.App.Data;

namespace IqlSampleApp.Migrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                    .Configure(_ => { })
                    .ConfigureServices(s => s
                        .AddSingleton<IAssemblyProvider, DefaultAssemblyProvider>()
                    )
                    .Build()
                ;
            var serviceProvider = host.Services;
            var model = ApplicationDbContext.Build(serviceProvider);
            new MigrationBuilder<ApplicationDbContext>().GenerateMigration("IqlSampleApp.Data");
        }
    }
}