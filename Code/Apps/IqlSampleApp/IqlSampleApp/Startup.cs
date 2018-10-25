using System;
using Brandless.AspNetCore.OData.Extensions;
using Brandless.AspNetCore.OData.Extensions.Binding;
using Iql.Conversion;
using Iql.DotNet;
using Iql.Server;
using Iql.Server.OData.Net;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tunnel.App.Data;
using Tunnel.App.Data.Models.Contracts;

namespace IqlSampleApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEdmModelAccessor>(new EdmModelAccessor());
            services.AddSingleton<IDesignTimeDbContextFactory<ApplicationDbContext>>(provider => new DesignTimeAppDbContextBuilder(provider));
            services.AddIql();
            services.AddOData();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var model = ApplicationDbContext.Build(app.ApplicationServices);
            app.UseMvc(builder =>
            {
                builder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                builder.MapODataServiceRoute("odata", "odata", model.Model);
            });

            app.UseIql<ITunnelService>(config =>
            {
                config.ConfigureFromOData<ITunnelService>(model.ModelBuilder);
            });
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                //await context.Database.EnsureCreatedAsync(cancellationToken);
                context.Database.Migrate();
            } //app.UseMvc();
        }
    }
}
