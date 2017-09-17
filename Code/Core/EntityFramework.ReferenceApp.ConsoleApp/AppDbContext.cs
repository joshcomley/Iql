using Microsoft.EntityFrameworkCore;

namespace EntityFramework.ReferenceApp.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyDb");
            base.OnConfiguring(optionsBuilder);
        }
    }
}