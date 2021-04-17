using CarView.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CarView.Infrastructure.Database.Configuration;

namespace CarView.Infrastructure.Database
{
    public class CarViewContext : DbContext
    {
        #region DbSets
        public DbSet<Car> Cars { get; set; }
        #endregion
        private readonly ConfigurationDatabase _configurationDatabase;
        public CarViewContext(ConfigurationDatabase configurationDatabase)
        {
            _configurationDatabase = configurationDatabase;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configurationDatabase.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
