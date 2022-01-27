using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityInfo1_Data.Models;

namespace CityInfo1_Data.Context
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options,
                               IConfiguration configuration) : base(options)
        {
            this._configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if ENABLED_FOR_LAZY_LOADING_USAGE
            string connectionString;

            connectionString = this._configuration.GetConnectionString("cityInfoDBConnectionString");
            
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
#endif
        }
    }
}
