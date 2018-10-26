using System;
using Microsoft.EntityFrameworkCore.Design;

namespace IqlSampleApp.Data
{
    public class DesignTimeAppDbContextBuilder : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly IServiceProvider _serviceProvider;

        public DesignTimeAppDbContextBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            return new ApplicationDbContext(_serviceProvider);
        }
    }
}