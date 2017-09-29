using Microsoft.EntityFrameworkCore;

namespace EntityFramework.ReferenceApp.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=People.App.Data;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}