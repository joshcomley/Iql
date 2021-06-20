﻿using Brandless.AspNetCore.OData.Extensions;
using Brandless.AspNetCore.OData.Extensions.Binding;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Brandless.Data;
using Brandless.Data.Contracts;
using Iql.Conversion;
using Iql.DotNet;
using Iql.Server;
using Iql.Server.OData.Net;
using IqlSampleApp.Data;
using IqlSampleApp.Data.Contracts;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Abstracts;
using Microsoft.AspNetCore.OData.NetTopology;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddControllers().AddOData(opt =>
            {
                Model = ApplicationDbContext.Build(services.BuildServiceProvider());
                opt.AddModel("odata", Model.Model, _ => _.AddNetTopology());
                opt.Count().Filter().Expand().Select().OrderBy().SetMaxTop(5);
            });
            services.AddODataNetTopology();

            services.AddSingleton<IRevisionKeyGenerator, StandardRevisionKeyGenerator>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            // OData = services.AddOData();
            services.AddMvcCore(_ => _.EnableEndpointRouting = false);
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

        public ODataConfigurationResult Model { get; set; }

        public IODataBuilder OData { get; set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(builder =>
            {
                // builder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                // builder.MapODataServiceRoute("odata", "odata", model.Model);
            });
            app.UseIql<IIqlSampleAppService>(config =>
            {
                config.ConfigureFromOData<IIqlSampleAppService>(Model.ModelBuilder);
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
