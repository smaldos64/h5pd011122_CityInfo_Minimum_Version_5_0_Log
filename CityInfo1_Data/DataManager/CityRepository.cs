using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo1_Data.Context;
using CityInfo1_Data.Interfaces;
using CityInfo1_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfo1_Data.DataManager
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(DatabaseContext context) : base(context)
        {
            if (null == context)
            {
                throw new ArgumentNullException(nameof(context));
            }
            this.RepositoryContext.ChangeTracker.LazyLoadingEnabled = false;
        }

        public async Task<IEnumerable<City>> GetAllCities(bool IncludeRelations = false)
        {
            if (false == IncludeRelations)
            {
                var collection = await base.FindAll();
                return (collection);
            }
            else
            {
                var collection = await base.RepositoryContext.Cities.
                Include(co => co.Country).ToListAsync();

                var collection1 = collection.OrderByDescending(c => c.Country.CountryName);
                return (collection1);
            }
        }

        public async Task<City> GetCity(int CityId, bool IncludeRelations = false)
        {
            //if (false == IncludeRelations)
            //{
            //    var City_Object = base.FindOne(CityId);
            //    return await (City_Object);
            //}
            //else
            //{
            //    var City_Object = await base.RepositoryContext.Cities.Include(co => co.Country).
            //    FirstOrDefaultAsync();

            //    return (City_Object);
            //}
            var City_Object = base.FindOne(CityId);
            return await (City_Object);
        }

        public async Task<IEnumerable<City>> GetCitiesWithCountryID(int CountryID)
        {
            var collection = await base.FindByCondition(c => c.CountryID == CountryID);
            return (collection);
        }
    }
}
