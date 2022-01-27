using CityInfo1_Data.Context;
using CityInfo1_Data.DataManager;
using CityInfo1_Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo1_WebApi.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["connectionStrings:cityInfoDBConnectionString"];
            services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(connectionString, x => x.MigrationsAssembly("CityInfo1_WebApi")));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
